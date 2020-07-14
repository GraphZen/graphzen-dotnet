// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [DisplayName("the schema")]
    public partial class MutableSchema : MutableAnnotatableMember, IMutableSchema
    {
        [GenDictionaryAccessors("name", nameof(DirectiveDefinition))]
        private readonly Dictionary<string, MutableDirectiveDefinition> _directives =
            new Dictionary<string, MutableDirectiveDefinition>();

        private readonly Dictionary<string, ConfigurationSource> _ignoredDirectives =
            new Dictionary<string, ConfigurationSource>();

        private readonly Dictionary<string, ConfigurationSource> _ignoredTypes =
            new Dictionary<string, ConfigurationSource>();

        [GenDictionaryAccessors("name", nameof(TypeIdentity))]
        private readonly Dictionary<string, TypeIdentity> _typeIdentities = new Dictionary<string, TypeIdentity>();

        private readonly Dictionary<TypeIdentity, MutableNamedTypeDefinition> _types =
            new Dictionary<TypeIdentity, MutableNamedTypeDefinition>();

        private ConfigurationSource? _mutationTypeConfigurationSource;
        private ConfigurationSource? _queryTypeConfigurationSource;
        private ConfigurationSource? _subscriptionTypeConfigurationSource;

        public MutableSchema() : base(ConfigurationSource.Convention)

        {
            InternalBuilder = new InternalSchemaBuilder(this);
        }

        private string DebuggerDisplay { [UsedImplicitly] get; } = "schema";
        internal new InternalSchemaBuilder InternalBuilder { get; }
        protected override MemberDefinitionBuilder GetInternalBuilder() => InternalBuilder;

        public IReadOnlyCollection<MutableNamedTypeDefinition> Types => _types.Values;
        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Schema;

        public override MutableSchema Schema => this;

        public IEnumerable<IMember> Children()
        {
            foreach (var directive in GetDirectives())
            {
                yield return directive;
            }

            foreach (var type in _types.Values)
            {
                yield return type;
            }
        }

        public MutableObjectType? QueryType { get; private set; }

        public bool SetQueryType(MutableObjectType type, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(GetQueryTypeConfigurationSource()))
            {
                _queryTypeConfigurationSource = configurationSource;
                if (QueryType != type)
                {
                    QueryType = type;


                    return true;
                }
            }

            return false;
        }

        public bool RemoveQueryType(ConfigurationSource configurationSource) => throw new NotImplementedException();

        public ConfigurationSource? GetQueryTypeConfigurationSource() => _queryTypeConfigurationSource;
        public MutableObjectType? MutationType { get; private set; }

        public bool SetMutationType(MutableObjectType? type, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(GetMutationTypeConfigurationSource()))
            {
                _mutationTypeConfigurationSource = configurationSource;
                if (MutationType != type)
                {
                    MutationType = type;
                    return true;
                }
            }

            return false;
        }

        public ConfigurationSource? GetMutationTypeConfigurationSource() => _mutationTypeConfigurationSource;
        public MutableObjectType? SubscriptionType { get; private set; }

        public bool SetSubscriptionType(MutableObjectType? type, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(GetSubscriptionTypeConfigurationSource()))
            {
                _subscriptionTypeConfigurationSource = configurationSource;
                if (SubscriptionType != type)
                {
                    SubscriptionType = type;
                    return true;
                }
            }

            return false;
        }

        public ConfigurationSource? GetSubscriptionTypeConfigurationSource() => _subscriptionTypeConfigurationSource;


        public bool RenameDirective(MutableDirectiveDefinition directive, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(directive.GetNameConfigurationSource()))
            {
                return false;
            }

            if (TryGetDirectiveDefinition(name, out var existing) && !existing.Equals(directive))
            {
                throw TypeSystemExceptions.DuplicateItemException.ForRename(directive, name);
            }

            _directives.Remove(directive.Name);
            _directives[name] = directive;
            return true;
        }

        public ConfigurationSource? FindIgnoredDirectiveConfigurationSource(string name) =>
            _ignoredDirectives.TryGetValue(name, out var cs) ? cs : (ConfigurationSource?)null;


        IObjectType? IQueryType.QueryType => QueryType;

        IObjectType? IMutationType.MutationType => MutationType;

        IObjectType? ISubscriptionType.SubscriptionType => SubscriptionType;

        public IEnumerable<MutableDirectiveDefinition> GetDirectives(bool includeSpecDirectives = false)
        {
            if (includeSpecDirectives)
            {
                throw new NotImplementedException(nameof(includeSpecDirectives) + "not supported yet");
            }

            return _directives.Values;
        }

        IEnumerable<IDirectiveDefinition> IDirectivesDefinition.GetDirectives(bool includeSpecDirectives) =>
            GetDirectives(includeSpecDirectives);

        public ConfigurationSource? FindIgnoredTypeConfigurationSource(string name) =>
            _ignoredTypes.FindValueOrDefault(name);

        public ConfigurationSource? FindIgnoredTypeConfigurationSource(Type clrType) =>
            FindIgnoredTypeConfigurationSource(clrType.GetGraphQLNameAnnotation());


        public bool TryGetTypeKind(Type clrType, bool? isInputType, bool? isOutputType, out TypeKind kind,
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


        internal TypeIdentity AddTypeIdentity(string name) => AddTypeIdentity(new TypeIdentity(name, this));
        internal TypeIdentity AddTypeIdentity(Type clrType) => AddTypeIdentity(new TypeIdentity(clrType, this));

        internal TypeIdentity AddTypeIdentity(TypeIdentity identity) => _typeIdentities[identity.Name] = identity;


        public MutableUnionType AddUnion(Type clrType, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(clrType);
            var unionType = new MutableUnionType(id, this, configurationSource);
            unionType.SetClrType(clrType, false, configurationSource);
            return AddUnion(unionType);
        }

        public MutableUnionType AddUnion(string name, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(name);
            var unionType = new MutableUnionType(id, this, configurationSource);
            return AddType(unionType);
        }

        private MutableUnionType AddUnion(MutableUnionType unionType) => AddType(unionType);

        private T AddType<T>(T type) where T : MutableNamedTypeDefinition
        {
            _types[type.Identity] = type;
            return type;
        }


        public MutableScalarType AddScalar(Type clrType, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(clrType);
            var scalarType = new MutableScalarType(id, this, configurationSource);
            scalarType.SetClrType(clrType, false, configurationSource);
            return AddScalar(scalarType);
        }

        public MutableScalarType AddScalar(string name, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(name);
            var scalarType = new MutableScalarType(id, this, configurationSource);
            return AddScalar(scalarType);
        }

        private MutableScalarType AddScalar(MutableScalarType scalarType) => AddType(scalarType);

        public MutableInterfaceType AddInterface(Type clrType, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(clrType);
            var interfaceType = new MutableInterfaceType(id, this, configurationSource);
            interfaceType.SetClrType(clrType, false, configurationSource);
            return AddInterface(interfaceType);
        }

        public MutableInterfaceType AddInterface(string name, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(name);
            var interfaceType = new MutableInterfaceType(id, this, configurationSource);
            return AddInterface(interfaceType);
        }

        private MutableInterfaceType AddInterface(MutableInterfaceType interfaceType) => AddType(interfaceType);

        public MutableEnumType AddEnum(Type clrType, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(clrType);
            var enumType = new MutableEnumType(id, this, configurationSource);
            enumType.SetClrType(clrType, false, configurationSource);
            return AddEnum(enumType);
        }

        public MutableEnumType AddEnum(string name, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(name);
            var enumType = new MutableEnumType(id, this, configurationSource);
            return AddEnum(enumType);
        }

        private MutableEnumType AddEnum(MutableEnumType enumType) => AddType(enumType);

        public MutableInputObjectType AddInputObject(Type clrType, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(clrType);
            var inputObjectType = new MutableInputObjectType(id, this, configurationSource);
            inputObjectType.SetClrType(clrType, false, configurationSource);
            return AddInputObject(inputObjectType);
        }

        public MutableInputObjectType AddInputObject(string name, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(name);
            var inputObjectType = new MutableInputObjectType(id, this, configurationSource);
            return AddInputObject(inputObjectType);
        }

        private MutableInputObjectType AddInputObject(MutableInputObjectType inputObjectType) =>
            AddType(inputObjectType);

        public MutableObjectType AddObject(Type clrType, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(clrType);
            var objectType = new MutableObjectType(id, this, configurationSource);
            objectType.SetClrType(clrType, false, configurationSource);
            return AddObject(objectType);
        }

        public MutableObjectType AddObject(string name, ConfigurationSource configurationSource)
        {
            var id = GetOrAddTypeIdentity(name);
            var objectType = new MutableObjectType(id, this, configurationSource);
            return AddObject(objectType);
        }

        private MutableObjectType AddObject(MutableObjectType objectType) => AddType(objectType);

        public void IgnoreDirective(Type clrType, ConfigurationSource configurationSource) =>
            IgnoreDirective(clrType.GetGraphQLNameAnnotation(), configurationSource);

        public void IgnoreDirective(string name, ConfigurationSource configurationSource)
        {
            var newCs = configurationSource.Max(FindIgnoredDirectiveConfigurationSource(name));
            _ignoredDirectives[name] = newCs;
            _directives.Remove(name);
        }

        public void UnignoreDirective(Type clrType, ConfigurationSource configurationSource) =>
            UnignoreDirective(clrType.GetGraphQLNameAnnotation(), configurationSource);

        public void UnignoreDirective(string name, ConfigurationSource configurationSource)
        {
            var existingIgnoredConfigurationSource = FindIgnoredDirectiveConfigurationSource(name);
            if (existingIgnoredConfigurationSource != null &&
                configurationSource.Overrides(existingIgnoredConfigurationSource))
            {
                _ignoredDirectives.Remove(name);
            }
        }

        public void IgnoreType(string name, ConfigurationSource configurationSource)
        {
            if (_ignoredTypes.TryGetValue(name, out var existingIgnoredConfigurationSource))
            {
                configurationSource = configurationSource.Max(existingIgnoredConfigurationSource);
            }

            _ignoredTypes[name] = configurationSource;
        }

        public void IgnoreType(Type clrType, ConfigurationSource configurationSource)
        {
            IgnoreType(clrType.GetGraphQLNameAnnotation(), configurationSource);
        }


        public void UnignoreType(string name)
        {
            _ignoredTypes.Remove(name);
        }

        public void UnignoreType(Type clrType)
        {
            UnignoreType(clrType.GetGraphQLNameAnnotation());
        }


        public bool HasType<T>(string name) where T : MutableNamedTypeDefinition => FindType<T>(name) != null;

        public bool HasType<T>(Type clrType) where T : MutableNamedTypeDefinition => FindType<T>(clrType) != null;


        public T? FindType<T>(string name) where T : MutableNamedTypeDefinition => FindTypeIdentity(name)?.Definition as T;

        public T? FindType<T>(Type clrType) where T : MutableNamedTypeDefinition
        {
            Check.NotNull(clrType, nameof(clrType));
            return _types.Values.OfType<T>().FirstOrDefault(_ => _.ClrType == clrType);
        }

        private T GetType<T>(string name) where T : MutableNamedTypeDefinition =>
            FindType(name) as T ?? throw new ItemNotFoundException($"No {typeof(T).Name} found named '{name}'.");

        public MutableNamedTypeDefinition GetType(string name) => GetType<MutableNamedTypeDefinition>(name);


        private T GetType<T>(Type clrType) where T : MutableNamedTypeDefinition =>
            FindType<T>(clrType) ??
            throw new ItemNotFoundException($"No {typeof(T).Name} found with CLR type '{clrType}'.");

        public bool TryGetType(string name, [NotNullWhen(true)] out MutableNamedTypeDefinition? type)
        {
            type = FindTypeIdentity(name)?.Definition;
            return type != null;
        }

        public bool TryGetType<T>(string name, [NotNullWhen(true)] out T? type) where T : MutableNamedTypeDefinition
        {
            type = FindType(name) as T;
            return type != null;
        }

        public bool TryGetType<T>(Type clrType, [NotNullWhen(true)] out T? type) where T : MutableNamedTypeDefinition
        {
            type = FindType<T>(clrType);
            return type != null;
        }

        public bool TryGetType(Type clrType, [NotNullWhen(true)] out MutableNamedTypeDefinition? type)
        {
            type = _types.Values.SingleOrDefault(_ => _.ClrType == clrType);
            return type != null;
        }


        public MutableNamedTypeDefinition? FindType(TypeIdentity identity) => TryGetType(identity, out var type) ? type : null;


        private bool TryGetType(TypeIdentity identity, [NotNullWhen(true)] out MutableNamedTypeDefinition? type) =>
            _types.TryGetValue(identity, out type);

        public MutableNamedTypeDefinition? FindType(string name) => FindTypeIdentity(name)?.Definition;

        public MutableNamedTypeDefinition? FindFirstType(Type clrType) =>
            _types.Values.FirstOrDefault(_ => _.ClrType == clrType);

        public TypeIdentity GetOrAddOutputTypeIdentity(Type clrType)
        {
            var id = FindPossibleOutputTypeIdentity(clrType);
            if (id != null)
            {
                return id;
            }

            if (clrType.TryGetGraphQLName(out var name))
            {
                return FindPossibleOutputTypeIdentity(name) ?? AddTypeIdentity(new TypeIdentity(clrType, this));
            }

            return AddTypeIdentity(new TypeIdentity(clrType, this));
        }

        public TypeIdentity? FindPossibleOutputTypeIdentity(Type clrType)
        {
            var knownOutputType =
                _typeIdentities.Values.SingleOrDefault(_ => _.ClrType == clrType && _.IsOutputType() == true);
            if (knownOutputType != null)
            {
                return knownOutputType;
            }

            return _typeIdentities.Values.SingleOrDefault(_ => _.ClrType == clrType && _.IsOutputType() != false);
        }


        public TypeIdentity? FindPossibleInputTypeIdentity(string name)
        {
            if (_typeIdentities.TryGetValue(name, out var id) && id.IsInputType() != false)
            {
                return id;
            }

            return null;
        }


        public TypeIdentity? FindPossibleInputTypeIdentity(Type clrType)
        {
            var knownInputType =
                _typeIdentities.Values.SingleOrDefault(_ => _.ClrType == clrType && _.IsInputType() == true);
            if (knownInputType != null)
            {
                return knownInputType;
            }

            return _typeIdentities.Values.SingleOrDefault(_ => _.ClrType == clrType && _.IsInputType() != false);
        }

        public TypeIdentity GetOrAddInputTypeIdentity(Type clrType)
        {
            var id = FindPossibleInputTypeIdentity(clrType);
            if (id != null)
            {
                return id;
            }

            if (clrType.TryGetGraphQLName(out var name))
            {
                return FindPossibleInputTypeIdentity(name) ?? AddTypeIdentity(new TypeIdentity(clrType, this));
            }

            return AddTypeIdentity(new TypeIdentity(clrType, this));
        }

        public TypeIdentity GetOrAddOutputTypeIdentity(string name) =>
            FindPossibleOutputTypeIdentity(name) ?? AddTypeIdentity(new TypeIdentity(name, this));

        public TypeIdentity GetOrAddInputTypeIdentity(string name) =>
            FindPossibleInputTypeIdentity(name) ?? AddTypeIdentity(new TypeIdentity(name, this));


        public TypeIdentity? FindPossibleOutputTypeIdentity(string name)
        {
            return _typeIdentities.Values.SingleOrDefault(_ => _.Name == name && _.IsOutputType() != false);
        }

        public TypeIdentity GetOrAddTypeIdentity(string name) => FindTypeIdentity(name) ?? AddTypeIdentity(name);
        public TypeIdentity GetOrAddTypeIdentity(Type clrType) => FindTypeIdentity(clrType) ?? AddTypeIdentity(clrType);

        public TypeIdentity? FindTypeIdentity(Type clrType) =>
            _typeIdentities.Values.SingleOrDefault(_ => _.ClrType == clrType) ??
            (clrType.TryGetGraphQLName(out var name) ? FindTypeIdentity(name) : null);


        public MutableNamedTypeDefinition? FindOutputType(Type clrType) =>
            _types.Values.SingleOrDefault(_ => _.IsOutputType() && _.ClrType == clrType);

        public MutableNamedTypeDefinition? FindType(Type clrType, TypeKind kind)
        {
            return _types.Values.Where(_ => _.Kind == kind)
                .SingleOrDefault(_ => _.ClrType == clrType);
        }

        public MutableNamedTypeDefinition? FindInputType(Type clrType)
        {
            return _types.Values.SingleOrDefault(_ => _.IsInputType() && _.ClrType == clrType);
        }


        public Schema ToSchema()
        {
            FinalizeTypes();
            return new Schema(this);
        }

        private void FinalizeTypes()
        {
            var typeRefs = GetTypeReferences().ToList();

            TypeReference? GetFirstUndefinedTypeReference() => typeRefs
                .FirstOrDefault(_ => _.Identity.Definition == null);

            var undefined = GetFirstUndefinedTypeReference();
            while (undefined != null)
            {
                var def = InternalBuilder.DefineType(undefined);
                if (def == null || !undefined.Update(def.Identity, undefined.TypeSyntax, def.GetConfigurationSource()))
                {
                    throw new Exception($"Unable to define type for type reference {undefined}");
                }

                undefined = GetFirstUndefinedTypeReference();
            }
        }


        public IEnumerable<TypeIdentity> GetTypeIdentities(bool includeSpecTypes = false)
        {
            if (includeSpecTypes)
            {
                return _typeIdentities.Values.AsEnumerable();
            }

            return _typeIdentities.Values.Where(_ => _.Definition == null || !_.Definition.IsSpec);
        }


        public void RemoveType(MutableNamedTypeDefinition type)
        {
            RemoveTypeIdentity(type.Identity);
            _types.Remove(type.Identity);

            foreach (var typeReference in GetTypeReferences().Where(_ => _.Identity.Equals(type.Identity))
                .Select(typeRef => typeRef.DeclaringMember))
            {
                switch (typeReference)
                {
                    case MutableArgument arg:
                        arg.DeclaringMember.RemoveArgument(arg);
                        break;
                    case MutableField field:
                        field.DeclaringType.RemoveField(field);
                        break;
                    case MutableInputField inputField:
                        inputField.DeclaringType.RemoveField(inputField);
                        break;
                }
            }
        }

        internal void RemoveTypeIdentity(TypeIdentity identity)
        {
            _typeIdentities.Remove(identity.Name);
        }

        public MutableDirectiveDefinition AddDirectiveDefinition(Type clrType, ConfigurationSource configurationSource)
        {
            var directive = new MutableDirectiveDefinition(null, clrType, this, configurationSource);
            return AddDirectiveDefinition(directive);
        }

        public MutableDirectiveDefinition AddDirectiveDefinition(string name, ConfigurationSource configurationSource)
        {
            var directive = new MutableDirectiveDefinition(name, null, this, configurationSource);
            return AddDirectiveDefinition(directive);
        }

        private MutableDirectiveDefinition AddDirectiveDefinition(MutableDirectiveDefinition directive)
        {
            _directives[directive.Name] = directive;
            return directive;
        }

        public bool RemoveDirectiveDefinition(MutableDirectiveDefinition annotationFoo)
        {
            _directives.Remove(annotationFoo.Name);
            return true;
        }

        [GraphQLIgnore]
        public MutableDirectiveDefinition? FindDirectiveDefinition<TDirective>() where TDirective : notnull
            => FindDirectiveDefinition(typeof(TDirective));

        public MutableDirectiveDefinition? FindDirectiveDefinition(Type clrType)
            => _directives.Values.SingleOrDefault(_ => _.ClrType == clrType);


        [GraphQLIgnore]
        public bool TryGetDirectiveDefinition(Type clrType, [NotNullWhen(true)] out MutableDirectiveDefinition? directive)
        {
            directive = FindDirectiveDefinition(clrType);
            return directive != null;
        }

        public IEnumerable<TypeReference> GetTypeReferences()
        {
            foreach (var directiveArg in _directives.Values.Where(_ => !_.IsSpec).SelectMany(_ => _.Arguments))
            {
                yield return directiveArg.ArgumentType;
            }

            foreach (var type in _types.Values.Where(_ => !_.IsSpec))
            {
                switch (type)
                {
                    case MutableFields hasOutputFields:
                        {
                            foreach (var field in hasOutputFields.GetFields())
                            {
                                yield return field.FieldType;

                                foreach (var arg in field.Arguments)
                                {
                                    yield return arg.ArgumentType;
                                }
                            }

                            break;
                        }
                    case MutableInputObjectType hasInputFields:
                        {
                            foreach (var field in hasInputFields.Fields)
                            {
                                yield return field.FieldType;
                            }

                            break;
                        }
                }
            }
        }

        public override string ToString() => "schema";
    }
}