// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SchemaDefinition : AnnotatableMemberDefinition
    {
        [NotNull] private readonly List<DirectiveDefinition> _directives = new List<DirectiveDefinition>();


        [NotNull]
        private readonly Dictionary<string, ConfigurationSource> _ignoredTypes =
            new Dictionary<string, ConfigurationSource>();


        [NotNull] [ItemNotNull] private readonly List<TypeIdentity> _typeIdentities;

        [NotNull] [ItemNotNull] private readonly List<NamedTypeDefinition> _types = new List<NamedTypeDefinition>();

        // ReSharper disable once NotNullMemberIsNotInitialized


        public SchemaDefinition([NotNull] [ItemNotNull] IReadOnlyList<ScalarType> scalars) : base(ConfigurationSource
            .Convention)

        {
            Check.NotNull(scalars, nameof(scalars));
            Builder = new InternalSchemaBuilder(this);
            _typeIdentities = new List<TypeIdentity>();

            foreach (var scalarType in scalars)
            {
                var id = scalarType.ClrType != null
                    ? new TypeIdentity(scalarType.ClrType, this)
                    : new TypeIdentity(scalarType.Name, this);
                if (id.Name != scalarType.Name)
                {
                    id.Name = scalarType.Name;
                }
                // TODO: add logic to handle adding duplicate scalars

                AddTypeIdentity(id);
                var scalarDefinition = new ScalarTypeDefinition(scalarType, id, this, ConfigurationSource.Explicit);
                AddScalar(scalarDefinition);
            }
        }

        private string DebuggerDisplay { get; } = "schema";

        [NotNull]
        public InternalSchemaBuilder Builder { get; }


        [NotNull]
        public IReadOnlyList<NamedTypeDefinition> Types => _types;

        [NotNull]
        public IReadOnlyList<IDirectiveDefinition> Directives => _directives;

        [CanBeNull]
        public IObjectTypeDefinition QueryType { get; set; }

        [CanBeNull]
        public IObjectTypeDefinition MutationType { get; set; }

        [CanBeNull]
        public IObjectTypeDefinition SubscriptionType { get; set; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Schema;


        public ConfigurationSource? FindIgnoredTypeConfigurationSource([NotNull] string name) =>
            _ignoredTypes.FindValueOrDefault(name);

        public ConfigurationSource? FindIgnoredTypeConfigurationSource([NotNull] Type clrType) =>
            FindIgnoredTypeConfigurationSource(clrType.GetGraphQLName());


        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private bool TryDefineFirstUndefinedType([CanBeNull] TypeIdentity prev, out TypeIdentity identity)
        {
            var undefined = _typeIdentities.Where(_ =>
                _.Definition == null && _.ClrType != null && _.ClrType.NotIgnored()).ToArray();
            identity = undefined.FirstOrDefault();
            // ReSharper disable once PossibleUnintendedReferenceComparison
            if ((prev != null) & (prev == identity))
            {
                throw new InvalidOperationException("This type was already defined.");
            }

            if (identity != null)
            {
                if (Builder.Type(identity)?.Definition is IGraphQLTypeDefinition def &&
                    identity.Definition == null)
                {
                    identity.Definition = def;
                }

                return identity.Definition != null;
            }

            return false;
        }

        public bool TryGetTypeKind([NotNull] Type clrType, bool? isInputType, bool? isOutputType, out TypeKind kind,
            out ConfigurationSource configurationSource)
        {
            kind = default;
            var effectiveClrType = clrType.GetEffectiveClrType();
            if (effectiveClrType != clrType)
            {
                var result = TryGetTypeKind(effectiveClrType, isInputType, isOutputType, out kind,
                    out configurationSource);
                configurationSource = ConfigurationSource.DataAnnotation;
                return result;
            }
            // TODO: 

            //TODO: check for data annotation configurations

            configurationSource = ConfigurationSource.Convention;
            if (clrType.IsEnum)
            {
                kind = TypeKind.Enum;
                return true;
            }

            if (clrType.IsValueType)
            {
                kind = TypeKind.Scalar;
                return true;
            }

            if (clrType.IsInterface)
            {
                kind = TypeKind.Interface;
                return true;
            }

            if (isInputType == true)
            {
                if (isOutputType == true)
                {
                    kind = TypeKind.Scalar;
                    return true;
                }

                kind = TypeKind.InputObject;
                return true;
            }

            if (isOutputType == true)
            {
                kind = TypeKind.Object;
                return true;
            }

            return false;
        }


        private void FinalizeTypes()
        {
            TypeIdentity identity = null;
            while (TryDefineFirstUndefinedType(identity, out identity))
            {
            }
        }


        [CanBeNull]
        public TypeIdentity FindTypeIdentity([NotNull] TypeIdentity identity)
        {
            var validIdentities = _typeIdentities
                .Where(_ => _.Kind == null || identity.Kind == null || _.Kind == identity.Kind).ToList();
            var clrTypeAndName =
                // ReSharper disable once PossibleNullReferenceException
                validIdentities.SingleOrDefault(_ => _.ClrType == identity.ClrType && _.Name == identity.Name);
            if (clrTypeAndName != null)
            {
                return clrTypeAndName;
            }

            // ReSharper disable once PossibleNullReferenceException
            var nameOnly = validIdentities.SingleOrDefault(_ => _.Name == identity.Name);
            if (nameOnly != null)
            {
                if (identity.ClrType != null)
                {
                    if (nameOnly.ClrType == null)
                    {
                        nameOnly.ClrType = identity.ClrType;
                    }
                    else if (nameOnly.ClrType != identity.ClrType)
                    {
                        throw new InvalidOperationException(
                            $"Found mismatched type identity {nameOnly} for identity {identity}");
                    }
                }
            }


            return nameOnly;
        }


        [CanBeNull]
        public TypeIdentity FindOverlappingTypeIdentity(TypeIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));
            var ids = _typeIdentities.Where(_ => _.Overlaps(identity)).ToList();
            if (ids.Count > 1)
            {
                throw new InvalidOperationException();
            }

            return ids.SingleOrDefault();
        }

        public TypeReference GetOrAddTypeReference([NotNull] string type, IMemberDefinition referencingMember
        )
        {
            var typeNode = Builder.Parser.ParseType(type);
            var named = typeNode.GetNamedType();

            var identity = GetOverlappingOrAddTypeIdentity(
                new TypeIdentity(named.Name.Value, this)
                {
                    IsInputType = referencingMember is IInputDefinition,
                    IsOutputType = referencingMember is IOutputDefinition
                }
            );
            return identity != null ? new TypeReference(identity, typeNode) : null;
        }

        public TypeReference GetOrAddTypeReference([NotNull] MethodInfo method, IMemberDefinition referencingMember
        )
        {
            if (method.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType))
            {
                var identity = GetOverlappingOrAddTypeIdentity(
                    new TypeIdentity(innerClrType, this)
                    {
                        IsInputType = referencingMember is IInputDefinition,
                        IsOutputType = referencingMember is IOutputDefinition
                    }
                );
                return identity != null ? new TypeReference(identity, typeNode) : null;
            }

            return null;
        }

        public TypeReference GetOrAddTypeReference([NotNull] ParameterInfo parameter,
            IMemberDefinition referencingMember
        )
        {
            if (parameter.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType))
            {
                var identity = GetOverlappingOrAddTypeIdentity(
                    new TypeIdentity(innerClrType, this)
                    {
                        IsInputType = referencingMember is IInputDefinition,
                        IsOutputType = referencingMember is IOutputDefinition
                    }
                );
                return identity != null ? new TypeReference(identity, typeNode) : null;
            }

            return null;
        }

        public TypeReference NamedTypeReference(Type clrType, TypeKind kind)
        {
            if (clrType.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType))
            {
                if (clrType != innerClrType)
                {
                    return null;
                }


                var identity = GetOrAddTypeIdentity(new TypeIdentity(innerClrType, this, kind));
                return identity != null ? new TypeReference(identity, typeNode) : null;
            }

            return null;
        }


        public TypeReference GetOrAddTypeReference([NotNull] PropertyInfo property, IMemberDefinition referencingMember
        )
        {
            if (property.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType))
            {
                var identity = GetOverlappingOrAddTypeIdentity(
                    new TypeIdentity(innerClrType, this)
                    {
                        IsInputType = referencingMember is IInputDefinition,
                        IsOutputType = referencingMember is IOutputDefinition
                    }
                );

                return identity != null ? new TypeReference(identity, typeNode) : null;
            }

            return null;
        }

        public TypeReference GetOrAddTypeReference([NotNull] Type clrType, bool canBeNull, bool itemCanBeNull,
            IMemberDefinition referencingMember)
        {
            if (clrType.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType, canBeNull, itemCanBeNull))
            {
                var identity = GetOverlappingOrAddTypeIdentity(
                    new TypeIdentity(innerClrType, this)
                    {
                        IsInputType = referencingMember is IInputDefinition,
                        IsOutputType = referencingMember is IOutputDefinition
                    }
                );
                return identity != null ? new TypeReference(identity, typeNode) : null;
            }

            return null;
        }

        private TypeIdentity GetOverlappingOrAddTypeIdentity(TypeIdentity identity
        )
        {
            Check.NotNull(identity, nameof(identity));
            return FindOverlappingTypeIdentity(identity) ?? AddTypeIdentity(identity);
        }

        private TypeIdentity GetOrAddTypeIdentity(TypeIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));
            return FindTypeIdentity(identity) ?? AddTypeIdentity(identity);
        }

        private TypeIdentity AddTypeIdentity([NotNull] TypeIdentity identity)
        {
            var existing = FindTypeIdentity(identity);
            if (existing != null)
            {
                throw new InvalidOperationException();
            }

            _typeIdentities.Add(identity);
            return identity;
        }

        public ScalarTypeDefinition GetOrAddScalar(string name, ConfigurationSource configurationSource) =>
            GetOrAddType(Check.NotNull(name, nameof(name)),
                id => new ScalarTypeDefinition(id, this, configurationSource));

        public ScalarTypeDefinition GetOrAddScalar(Type clrType, ConfigurationSource configurationSource) =>
            GetOrAddType(Check.NotNull(clrType, nameof(clrType)),
                id => new ScalarTypeDefinition(id, this, configurationSource));


        public UnionTypeDefinition AddUnion([NotNull] Type clrType, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(new TypeIdentity(clrType, this));
            var unionType = new UnionTypeDefinition(id, this, configurationSource);
            return AddUnion(unionType);
        }

        public UnionTypeDefinition AddUnion([NotNull] string name, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(new TypeIdentity(name, this));
            var unionType = new UnionTypeDefinition(id, this, configurationSource);
            return AddUnion(unionType);
        }

        private UnionTypeDefinition AddUnion([NotNull] UnionTypeDefinition unionType)
        {
            _types.Add(unionType);
            return unionType;
        }

        public ScalarTypeDefinition AddScalar([NotNull] Type clrType, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(new TypeIdentity(clrType, this));
            var scalarType = new ScalarTypeDefinition(id, this, configurationSource);
            return AddScalar(scalarType);
        }

        public ScalarTypeDefinition AddScalar([NotNull] string name, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(new TypeIdentity(name, this));
            var scalarType = new ScalarTypeDefinition(id, this, configurationSource);
            return AddScalar(scalarType);
        }

        private ScalarTypeDefinition AddScalar([NotNull] ScalarTypeDefinition scalarType)
        {
            _types.Add(scalarType);
            return scalarType;
        }

        public InterfaceTypeDefinition AddInterface([NotNull] Type clrType, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(new TypeIdentity(clrType, this));
            var interfaceType = new InterfaceTypeDefinition(id, this, configurationSource);
            // ClrTypeConfigurator.InterfaceTypeConventionConfigurator.Configure(interfaceType);
            return AddInterface(interfaceType);
        }

        public InterfaceTypeDefinition AddInterface([NotNull] string name, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(new TypeIdentity(name, this));
            var interfaceType = new InterfaceTypeDefinition(id, this, configurationSource);
            return AddInterface(interfaceType);
        }

        private InterfaceTypeDefinition AddInterface([NotNull] InterfaceTypeDefinition interfaceType)
        {
            _types.Add(interfaceType);
            return interfaceType;
        }

        public EnumTypeDefinition AddEnum([NotNull] Type clrType, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(new TypeIdentity(clrType, this));
            var enumType = new EnumTypeDefinition(id, this, configurationSource);
            return AddEnum(enumType);
        }

        public EnumTypeDefinition AddEnum([NotNull] string name, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(new TypeIdentity(name, this));
            var enumType = new EnumTypeDefinition(id, this, configurationSource);
            return AddEnum(enumType);
        }

        private EnumTypeDefinition AddEnum([NotNull] EnumTypeDefinition enumType)
        {
            _types.Add(enumType);
            return enumType;
        }

        public InputObjectTypeDefinition AddInputObject([NotNull] Type clrType, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(new TypeIdentity(clrType, this));
            var inputObjectType = new InputObjectTypeDefinition(id, this, configurationSource);
            return AddInputObject(inputObjectType);
        }

        public InputObjectTypeDefinition AddInputObject([NotNull] string name, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(new TypeIdentity(name, this));
            var inputObjectType = new InputObjectTypeDefinition(id, this, configurationSource);
            return AddInputObject(inputObjectType);
        }

        private InputObjectTypeDefinition AddInputObject([NotNull] InputObjectTypeDefinition inputObjectType)
        {
            _types.Add(inputObjectType);
            return inputObjectType;
        }

        public ObjectTypeDefinition AddObject([NotNull] Type clrType, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(new TypeIdentity(clrType, this, TypeKind.Object));
            var objectType = new ObjectTypeDefinition(id, this, configurationSource);
            return AddObject(objectType);
        }

        public ObjectTypeDefinition AddObject([NotNull] string name, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(new TypeIdentity(name, this));
            var objectType = new ObjectTypeDefinition(id, this, configurationSource);
            return AddObject(objectType);
        }

        private ObjectTypeDefinition AddObject([NotNull] ObjectTypeDefinition objectType)
        {
            _types.Add(objectType);
            return objectType;
        }


        private T AddType<T>(Type clrType, Func<TypeIdentity, T> typeFactory)
            where T : NamedTypeDefinition
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(typeFactory, nameof(typeFactory));
            var identity = GetOrAddTypeIdentity(new TypeIdentity(clrType, this));
            return identity != null ? AddType(typeFactory(identity)) : null;
        }


        private T AddType<T>(string name, Func<TypeIdentity, T> typeFactory)
            where T : NamedTypeDefinition
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(typeFactory, nameof(typeFactory));
            var identity =
                GetOrAddTypeIdentity(new TypeIdentity(name, this));
            return identity != null ? AddType(typeFactory(identity)) : null;
        }


        private T AddType<T>(T type) where T : NamedTypeDefinition
        {
            Check.NotNull(type, nameof(type));

            if (type.ClrType != null)
            {
                var existingBasedOnType = _types.SingleOrDefault(_ => _.ClrType == type.ClrType);
                if (existingBasedOnType != null)
                {
                    throw new InvalidOperationException(
                        $"Cannot add type \"{type.Name}\" with CLR type \"{type.ClrType}\", a type named \"{existingBasedOnType.Name}\" already exists with that CLR type.");
                }
            }

            var existingBasedOnName = _types.SingleOrDefault(_ => _.Name == type.Name);
            if (existingBasedOnName != null)
            {
                throw new InvalidOperationException(
                    $"Cannot add type \"{type.Name}\", a {existingBasedOnName.GetType().Name} type with that name already exists.");
            }

            _types.Add(type);
            return type;
        }


        public void IgnoreType([NotNull] string name, ConfigurationSource configurationSource)
        {
            if (_ignoredTypes.TryGetValue(name, out var existingIgnoredConfigurationSource))
            {
                configurationSource = configurationSource.Max(existingIgnoredConfigurationSource);
            }

            _ignoredTypes[name] = configurationSource;
        }

        public void IgnoreType([NotNull] Type clrType, ConfigurationSource configurationSource)
        {
            IgnoreType(clrType.GetGraphQLName(), configurationSource);
        }


        public void UnignoreType([NotNull] string name)
        {
            _ignoredTypes.Remove(name);
        }


        public bool HasType<T>(string name) where T : NamedTypeDefinition
        {
            Check.NotNull(name, nameof(name));
            return _types.OfType<T>().Any(_ => _.Name == name);
        }

        public bool HasType<T>([NotNull] Type clrType) where T : NamedTypeDefinition
        {
            Check.NotNull(clrType, nameof(clrType));
            return _types.OfType<T>().Any(_ => _.ClrType == clrType);
        }

        public bool HasClrType([NotNull] Type clrType) => FindType<NamedTypeDefinition>(clrType) != null;
        public bool HasClrType<T>() => HasClrType(typeof(T));

        public bool HasClrTypeIdentity([NotNull] Type clrType) => _typeIdentities.Any(_ => _.ClrType == clrType);
        public bool HasClrTypeIdentity<T>() => HasClrTypeIdentity(typeof(T));

        public T FindType<T>(string name) where T : NamedTypeDefinition
        {
            Check.NotNull(name, nameof(name));
            return _types.OfType<T>().SingleOrDefault(_ => _.Name == name);
        }


        public T FindType<T>([NotNull] Type clrType) where T : NamedTypeDefinition
        {
            Check.NotNull(clrType, nameof(clrType));
            return _types.OfType<T>().SingleOrDefault(_ => _.ClrType == clrType);
        }

        [NotNull]
        public T GetType<T>([NotNull] string name) where T : NamedTypeDefinition
        {
            Check.NotNull(name, nameof(name));
            return _types.OfType<T>().SingleOrDefault(_ => _.Name == name) ??
                   throw new Exception($"No {typeof(T).Name} found named '{name}'.");
        }

        [NotNull]
        public T GetType<T>([NotNull] Type clrType) where T : NamedTypeDefinition
        {
            Check.NotNull(clrType, nameof(clrType));
            return _types.OfType<T>().SingleOrDefault(_ => _.ClrType == clrType) ??
                   throw new Exception($"No {typeof(T).Name} found with CLR type '{clrType}'.");
        }

        public bool TryGetType<T>(string name, out T type) where T : NamedTypeDefinition
        {
            Check.NotNull(name, nameof(name));
            type = _types.OfType<T>().SingleOrDefault(_ => _.Name == name);
            return type != null;
        }

        public bool TryGetType<T>([NotNull] Type clrType, out T type) where T : NamedTypeDefinition
        {
            Check.NotNull(clrType, nameof(clrType));
            type = _types.OfType<T>().SingleOrDefault(_ => _.ClrType == clrType);
            return type != null;
        }

        [CanBeNull]
        public T FindType<T>(TypeIdentity identity) where T : NamedTypeDefinition
        {
            Check.NotNull(identity, nameof(identity));

            var type = FindType(identity);

            if (type != null)
            {
                if (type is T requested)
                {
                    return requested;
                }

                throw new Exception(
                    $"Expected type \"{type.Name}\" to be of type \"{typeof(T).Name}\", but instead found a  type of \"{type.GetType().Name}\" ");
            }

            return null;
        }


        public NamedTypeDefinition FindType(TypeIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));
            return _types.SingleOrDefault(_ => _.Identity.Equals(identity));
        }

        public NamedTypeDefinition FindType([NotNull] string name) => _types.SingleOrDefault(_ => _.Name == name);

        public NamedTypeDefinition FindType([NotNull] Type clrType) =>
            _types.SingleOrDefault(_ => _.ClrType == clrType);

        [NotNull]
        public IEnumerable<NamedTypeDefinition> FindTypes([NotNull] Type clrType) =>
            _types.Where(_ => _.ClrType == clrType);

        public NamedTypeDefinition FindOutputType([NotNull] Type clrType) =>
            _types.SingleOrDefault(_ => _.IsOutputType() && _.ClrType == clrType);

        public NamedTypeDefinition FindType([NotNull] Type clrType, TypeKind kind) =>
            _types.Where(_ => _.Kind == kind)
                .SingleOrDefault(_ => _.ClrType == clrType);

        public NamedTypeDefinition FindInputType([NotNull] Type clrType) =>
            _types.SingleOrDefault(_ => _.IsInputType() && _.ClrType == clrType);

        private T GetOrAddType<T>(string name, Func<TypeIdentity, T> typeFactory
        ) where T : NamedTypeDefinition
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(typeFactory, nameof(typeFactory));
            var identity =
                GetOrAddTypeIdentity(new TypeIdentity(name, this));

            return FindType<T>(identity) ?? AddType(name, typeFactory);
        }

        private T GetOrAddType<T>(Type clrType, Func<TypeIdentity, T> typeFactory) where T : NamedTypeDefinition
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(typeFactory, nameof(typeFactory));
            var identity = GetOrAddTypeIdentity(new TypeIdentity(clrType, this));
            return FindType<T>(identity) ?? AddType(clrType, typeFactory);
        }

        [NotNull]
        public DirectiveDefinition GetOrAddDirective(string name, ConfigurationSource configurationSource)
        {
            Check.NotNull(name, nameof(name));
            // ReSharper disable once PossibleNullReferenceException
            var existing = _directives.SingleOrDefault(_ => _.Name == name);
            if (existing != null)
            {
                return existing;
            }

            var newDirective = new DirectiveDefinition(name, this, configurationSource);
            _directives.Add(newDirective);
            return newDirective;
        }

        [NotNull]
        public Schema ToSchema()
        {
            FinalizeTypes();
            return new Schema(this);
        }


        public void RemoveType([CanBeNull] NamedTypeDefinition type)
        {
            _types.Remove(type);
        }
    }
}