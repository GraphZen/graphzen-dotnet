// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLName("__Schema")]
    [Description("A GraphQL Schema defines the capabilities of a GraphQL server. It exposes all " +
                 "available types and directives on the server, as well as the entry points for " +
                 "query, mutation, and subscription operations.")]
    public partial class Schema : AnnotatableMember, ISchema
    {
        [GenDictionaryAccessors("name", nameof(Directive))]
        private readonly ImmutableDictionary<string, Directive> _directives;

        [GenDictionaryAccessors("clrType", nameof(Directive))]
        private readonly ImmutableDictionary<Type, Directive> _directivesByType;


        private readonly Lazy<IReadOnlyList<EnumType>> _enums;

        private readonly Dictionary<string, List<ObjectType>> _implementations =
            new Dictionary<string, List<ObjectType>>();


        private readonly Lazy<IReadOnlyList<InputObjectType>> _inputObjects;


        private readonly Lazy<IReadOnlyList<InterfaceType>> _interfaces;


        private readonly Lazy<IReadOnlyList<ObjectType>> _objects;


        private readonly Dictionary<string, Dictionary<string, bool>> _possibleTypeMap =
            new Dictionary<string, Dictionary<string, bool>>();


        private readonly Lazy<IReadOnlyList<ScalarType>> _scalars;

        private readonly Lazy<DocumentSyntax> _sdlSyntax;
        private readonly Lazy<SchemaDefinitionSyntax> _syntax;


        private readonly Lazy<IReadOnlyList<UnionType>> _unions;


        public Schema(SchemaDefinition schemaDefinition, IEnumerable<NamedType>? types = null) : base(schemaDefinition
            .DirectiveAnnotations)
        {
            Check.NotNull(schemaDefinition, nameof(schemaDefinition));
            Description = schemaDefinition.Description;
            Definition = schemaDefinition;
            types = types ?? Enumerable.Empty<NamedType>();

            var initialTypes = new List<NamedType>();
            // initialTypes.AddRange(SpecScalars.All);
            // ReSharper disable once PossibleNullReferenceException
            //if (schemaDefinition.Types.All(_ => _.Name != "String"))
            //{
            //    initialTypes.AddRange(SpecScalars.All);
            //}
            if (schemaDefinition.Types.All(_ => _.Name != "__Schema"))
            {
                var introTypes = Introspection.IntrospectionTypes;
                initialTypes.AddRange(introTypes);
            }


            initialTypes.AddRange(types);


            var definedDirectives = schemaDefinition.GetDirectives()
                .ToImmutableDictionary(_ => _.Name, _ => Directive.From(_, ResolveType));

            _directives = SpecDirectives.All.RemoveAll(d => definedDirectives.ContainsKey(d.Name))
                .AddRange(definedDirectives.Values).ToImmutableDictionary(_ => _.Name);
            Directives = _directives.Values.ToImmutableList();
            _directivesByType = Directives.Where(_ => _.ClrType != null).ToImmutableDictionary(_ => _.ClrType!);

            var definedTypes = schemaDefinition.Types
                .Select(CreateType);
            initialTypes.AddRange(definedTypes);

            // ReSharper disable once PossibleNullReferenceException
            var duplicate = initialTypes.GroupBy(_ => _.Name).Where(_ => _.Count() > 1).Select(_ => _.First())
                .FirstOrDefault();

            if (duplicate != null)
            {
                throw new InvalidOperationException($"Type `{duplicate.Name}` defined twice, cannot create schema.");
            }

            // ReSharper disable once PossibleNullReferenceException
            Types = initialTypes
                .OrderBy(_ => _.Name)
                .ToDictionary(t => t.Name, t => t);


            QueryType = FindType<ObjectType>(Definition.QueryType?.Name ?? "Query")!;
            MutationType = Definition.MutationType != null ? FindType<ObjectType>(Definition.MutationType.Name) : null;
            SubscriptionType = Definition.SubscriptionType != null
                ? FindType<ObjectType>(Definition.SubscriptionType.Name)
                : null;


            // Keep track of all implementations by interface name.

            foreach (var type in Types.Values)
            {
                if (type is ObjectType objectType)
                {
                    foreach (var iface in objectType.Interfaces)
                    {
                        if (_implementations.TryGetValue(iface.Name, out var impls))
                        {
                            impls.Add(objectType);
                        }
                        else
                        {
                            _implementations[iface.Name] = new List<ObjectType>
                            {
                                objectType
                            };
                        }
                    }
                }
            }

            foreach (var type in Types.Values)
            {
                if (type is ObjectType objectType)
                {
                    foreach (var iface in objectType.Interfaces)
                    {
                        AssertObjectImplementsInterfaces(objectType, iface);
                    }
                }
            }

            _syntax = new Lazy<SchemaDefinitionSyntax>(() =>
            {
                var rootOperationTypes = new List<OperationTypeDefinitionSyntax>();
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (QueryType != null)
                {
                    rootOperationTypes.Add(new OperationTypeDefinitionSyntax(OperationType.Query,
                        SyntaxFactory.NamedType(SyntaxFactory.Name(QueryType.Name))));
                }

                if (MutationType != null)
                {
                    rootOperationTypes.Add(new OperationTypeDefinitionSyntax(OperationType.Mutation,
                        SyntaxFactory.NamedType(SyntaxFactory.Name(MutationType.Name))));
                }

                if (SubscriptionType != null)
                {
                    rootOperationTypes.Add(new OperationTypeDefinitionSyntax(OperationType.Subscription,
                        SyntaxFactory.NamedType(SyntaxFactory.Name(SubscriptionType.Name))));
                }


                return new SchemaDefinitionSyntax(rootOperationTypes);
            });

            _sdlSyntax = new Lazy<DocumentSyntax>(() =>
            {
                var definitions = new List<DefinitionSyntax>();
                var schemaDef = _syntax.Value;
                if (!schemaDef.IsSchemaOfCommonNames())
                {
                    definitions.Add(schemaDef);
                }

                definitions.AddRange(Directives.ToSyntaxNodes<DirectiveDefinitionSyntax>());
                definitions.AddRange(Types.Values.ToSyntaxNodes<TypeDefinitionSyntax>());
                return new DocumentSyntax(definitions);
            });

            _objects = new Lazy<IReadOnlyList<ObjectType>>(() => GetTypes<ObjectType>().ToImmutableList());
            _interfaces = new Lazy<IReadOnlyList<InterfaceType>>(() => GetTypes<InterfaceType>().ToImmutableList());
            _unions = new Lazy<IReadOnlyList<UnionType>>(() => GetTypes<UnionType>().ToImmutableList());
            _enums = new Lazy<IReadOnlyList<EnumType>>(() => GetTypes<EnumType>().ToImmutableList());
            _inputObjects =
                new Lazy<IReadOnlyList<InputObjectType>>(() => GetTypes<InputObjectType>().ToImmutableList());
            _scalars = new Lazy<IReadOnlyList<ScalarType>>(() => GetTypes<ScalarType>().ToImmutableList());
        }


        [GraphQLIgnore] public SchemaDefinition Definition { get; }

        [GraphQLIgnore] public IReadOnlyDictionary<string, NamedType> Types { get; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Schema;

        [GraphQLIgnore] public IReadOnlyList<ObjectType> Objects => _objects.Value;


        [GraphQLName("directives")]
        [Description("A list of all types supported by this server.")]
        public IReadOnlyList<Directive> Directives { get; }


        [Description("A list of all types supported by this server.")]

        public ObjectType QueryType { get; }


        [Description("If this server support subscription, the type that subscription operations will be rooted at.")]
        [GraphQLCanBeNull]
        public ObjectType? MutationType { get; }

        [Description("If this server support subscription, the type that subscription operations will be rooted at.")]
        [GraphQLCanBeNull]
        public ObjectType? SubscriptionType { get; }

        [GraphQLIgnore]
        IEnumerable<IInputObjectTypeDefinition> IInputObjectTypesDefinition.GetInputObjects() =>
            GetInputObjects();


        [GraphQLIgnore] public IReadOnlyList<InputObjectType> InputObjects => _inputObjects.Value;

        [GraphQLIgnore]
        public IEnumerable<InputObjectType> GetInputObjects() => InputObjects;

        [GraphQLIgnore]
        IEnumerable<IEnumTypeDefinition> IEnumTypesDefinition.GetEnums() => GetEnums();


        [GraphQLIgnore] public IReadOnlyList<EnumType> Enums => _enums.Value;

        [GraphQLIgnore]
        IEnumerable<IScalarTypeDefinition> IScalarTypesDefinition.GetScalars() => GetScalars();

        [GraphQLIgnore] public IReadOnlyList<ScalarType> Scalars => _scalars.Value;

        [GraphQLIgnore]
        IEnumerable<IUnionTypeDefinition> IUnionTypesDefinition.GetUnions() => GetUnions();


        [GraphQLIgnore] public IReadOnlyList<UnionType> Unions => _unions.Value;

        [GraphQLIgnore]
        IEnumerable<IInterfaceTypeDefinition> IInterfaceTypesDefinition.GetInterfaces() => GetInterfaces();

        [GraphQLIgnore] public IReadOnlyList<InterfaceType> Interfaces => _interfaces.Value;

        [GraphQLIgnore]
        IEnumerable<IObjectTypeDefinition> IObjectTypesDefinition.GetObjects(bool includeSpecTypes) =>
            GetObjects(includeSpecTypes);


        [GraphQLIgnore]
        IEnumerable<IDirectiveDefinition> IDirectivesDefinition.GetDirectives() => GetDirectives();

        [GraphQLIgnore]
        public IEnumerable<EnumType> GetEnums() => Enums;

        [GraphQLIgnore]
        public IEnumerable<ScalarType> GetScalars() => Scalars;

        [GraphQLIgnore]
        public IEnumerable<UnionType> GetUnions() => Unions;

        [GraphQLIgnore]
        public IEnumerable<InterfaceType> GetInterfaces() => Interfaces;


        [GraphQLIgnore]
        public IEnumerable<ObjectType> GetObjects(bool includeSpecTypes = false)
        {
            if (includeSpecTypes)
            {
                return _objects.Value;
            }

            return _objects.Value.Where(_ => _.IsIntrospection == false);
        }

        [GraphQLIgnore]
        public IEnumerable<Directive> GetDirectives() => Directives;

        IObjectTypeDefinition? IQueryTypeDefinition.QueryType => QueryType;

        IObjectTypeDefinition? IMutationTypeDefinition.MutationType => MutationType;

        IObjectTypeDefinition? ISubscriptionTypeDefinition.SubscriptionType => SubscriptionType;

        [GraphQLIgnore]
        public IEnumerable<NamedType> GetTypes() => Types.Values;

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;


        [GraphQLIgnore]
        public DocumentSyntax ToDocumentSyntax() => _sdlSyntax.Value;


        [GraphQLIgnore]
        public static Schema Create(DocumentSyntax schemaDocument)
        {
            Check.NotNull(schemaDocument, nameof(schemaDocument));
            return Create(sb => sb.FromSchema(schemaDocument));
        }

        [GraphQLIgnore]
        public static Schema Create(string schemaDocument)
        {
            Check.NotNull(schemaDocument, nameof(schemaDocument));
            return Create(sb => sb.FromSchema(schemaDocument));
        }


        public static Schema Create(Action<SchemaBuilder> schemaConfiguration)
        {
            Check.NotNull(schemaConfiguration, nameof(schemaConfiguration));
            var schemaBuilder = new SchemaBuilder(new SchemaDefinition(SpecScalars.All));
            schemaConfiguration(schemaBuilder);
            var schemaDef = schemaBuilder.GetInfrastructure<SchemaDefinition>();
            return schemaDef.ToSchema();
        }


        [GraphQLIgnore]
        public static Schema Create<TContext>(Action<SchemaBuilder<TContext>> schemaConfiguration)
            where TContext : GraphQLContext
        {
            Check.NotNull(schemaConfiguration, nameof(schemaConfiguration));
            var schemaBuilder = new SchemaBuilder<TContext>(new SchemaDefinition(SpecScalars.All));
            schemaConfiguration(schemaBuilder);
            var schemaDef = schemaBuilder.GetInfrastructure<SchemaDefinition>();
            return schemaDef.ToSchema();
        }

        [GraphQLIgnore]
        public IGraphQLType? GetTypeFromAst(TypeSyntax typeSyntax)
        {
            switch (typeSyntax)
            {
                case ListTypeSyntax listNode:
                    {
                        var innerType = GetTypeFromAst(listNode.OfType);
                        return innerType != null ? ListType.Of(innerType) : null;
                    }
                case NonNullTypeSyntax nnNode:
                    {
                        var innerType = GetTypeFromAst(nnNode.OfType);
                        switch (innerType)
                        {
                            case null:
                                return null;
                            case INullableType nullable:
                                return NonNullType.Of(nullable);
                        }
                    }
                    break;
                case NamedTypeSyntax namedTypeNode:
                    return Types.TryGetValue(namedTypeNode.Name.Value, out var result) ? result : null;
            }

            throw new Exception($"Unexpected type kind: {typeSyntax?.GetType()}");
        }

        private NamedType CreateType(INamedTypeDefinition definition) => NamedType.From(definition, this);


        public IEnumerable<T> GetTypes<T>() where T : INamedType => Types.Values.OfType<T>();


        [GraphQLIgnore]
        public IGraphQLType ResolveType(IGraphQLTypeReference typeReference)
        {
            switch (typeReference)
            {
                case NamedType named:
                    return named;
                case NonNullType nn:
                    return nn;
                case ListType lt:
                    return lt;
                case TypeReference reference:
                    return reference.ToType(this);
                case INamedTypeReference named:
                    return GetType(named.Name);
            }

            throw new InvalidOperationException($"Unable to get input type for definition {typeReference}.");
        }


        [GraphQLIgnore]
        public INamedType GetType(string name) => GetType<NamedType>(name);

        public bool HasType<T>(string name) where T : NamedType
        {
            Check.NotNull(name, nameof(name));
            return FindType<T>(name) != null;
        }

        public bool TryGetType(string name, [NotNullWhen(true)] out NamedType? type)
        {
            Check.NotNull(name, nameof(name));
            return Types.TryGetValue(name, out type);
        }

        [GraphQLIgnore]
        public INamedType? FindType(string name)
        {
            Check.NotNull(name, nameof(name));
            return TryGetType(name, out var type) ? type : null;
        }

        public T? FindType<T>(Type clrType) where T : class, INamedType
        {
            Check.NotNull(clrType, nameof(clrType));
            return TryGetType<T>(clrType, out var type) ? type : null;
        }


        public T? FindType<T>(string name) where T : class, INamedType
        {
            Check.NotNull(name, nameof(name));
            return TryGetType<T>(name, out var type) ? type : null;
        }

        public bool TryGetType<T>(string name, [NotNullWhen(true)] out T? type) where T : class, IGraphQLType
        {
            Check.NotNull(name, nameof(name));
            if (TryGetType(name, out var t))
            {
                if (t is T requestedType)
                {
                    type = requestedType;
                    return true;
                }
            }

            type = default;
            return false;
        }

        public bool HasType<T>(Type clrType) where T : class, INamedType
        {
            Check.NotNull(clrType, nameof(clrType));
            return TryGetType<T>(clrType, out _);
        }

        public bool TryGetType<T>(Type clrType, [NotNullWhen(true)] out T? type) where T : class, INamedType
        {
            Check.NotNull(clrType, nameof(clrType));
            if (TryGetType(clrType, out var t))
            {
                if (t is T requestedType)
                {
                    type = requestedType;
                    return true;
                }
            }

            type = default;
            return false;
        }


        public bool TryGetType(Type clrType, out INamedType type)
        {
            Check.NotNull(clrType, nameof(clrType));
            type = GetTypes<NamedType>().SingleOrDefault(_ => _.ClrType == clrType);
            return type != null;
        }


        public T GetType<T>(Type clrType) where T : INamedType
        {
            Check.NotNull(clrType, nameof(clrType));
            if (TryGetType(clrType, out var type))
            {
                if (type is T requestedType)
                {
                    return requestedType;
                }

                throw new InvalidOperationException(
                    $"Type with CLR type \"{clrType}\" already exists, but its type \"{type.GetType().Name}\" is different than the requested type of \"{typeof(T).Name}\".");
            }

            throw new InvalidOperationException($"Type with CLR type \"{clrType.Name}\" is not defined.");
        }


        public T GetType<T>(string name) where T : INamedType
        {
            Check.NotNull(name, nameof(name));
            if (TryGetType(name, out var type))
            {
                if (type is T requestedType)
                {
                    return requestedType;
                }

                throw new InvalidOperationException(
                    $"Type named \"{name}\" already exists, but its type \"{type.GetType().Name}\" is different than the requested type of \"{typeof(T).Name}\".");
            }

            throw new InvalidOperationException($"Type \"{name}\" is not defined.");
        }


        [GraphQLIgnore]
        public IEnumerable<ObjectType> GetPossibleTypes(IAbstractType abstractType)
        {
            if (abstractType is UnionType unionType)
            {
                return unionType.MemberTypes;
            }

            return abstractType is InterfaceType interfaceType
                ? _implementations[interfaceType.Name] ?? throw new InvalidOperationException()
                : throw new GraphQLLanguageModelException("Expected interface type");
        }

        [GraphQLIgnore]
        public bool IsPossibleType(IAbstractType abstractType, ObjectType possibleType)
        {
            Check.NotNull(abstractType, nameof(abstractType));
            Check.NotNull(possibleType, nameof(possibleType));

            if (!_possibleTypeMap.ContainsKey(abstractType.Name))
            {
                var possibleTypes = GetPossibleTypes(abstractType);
                _possibleTypeMap[abstractType.Name] = possibleTypes.ToDictionary(_ => _.Name, _ => true);
            }

            return _possibleTypeMap.TryGetValue(abstractType.Name, out var pTypes) &&
                   pTypes.ContainsKey(possibleType.Name);
        }

        private void AssertObjectImplementsInterfaces(ObjectType objectType, InterfaceType interfaceType)
        {
            Check.NotNull(objectType, nameof(objectType));
            Check.NotNull(interfaceType, nameof(interfaceType));


            foreach (var ifaceField in interfaceType.Fields.Values)
            {
                var fieldName = ifaceField.Name;
                var objectField = objectType.FindField(ifaceField.Name);
                if (objectField == null)
                {
                    throw new GraphQLLanguageModelException(
                        $"\"{interfaceType.Name}\" expects field \"{ifaceField.Name}\" but \"{objectType.Name}\" " +
                        "does not provide it.");
                }

                // Assert interface field type is satisfied by object field type, by being
                // a valid subtype. (covariant)
                if (!objectField.FieldType.IsSubtypeOf(ifaceField.FieldType, this))
                {
                    throw new GraphQLLanguageModelException(
                        $"{ifaceField} expects type \"{ifaceField.FieldType}\" " +
                        "but" +
                        $"{objectField} provides type \"{objectField.FieldType}\".");
                }

                // Assert each interface field arg is implemented.
                foreach (var ifaceArg in ifaceField.GetArguments())
                {
                    var argName = ifaceArg.Name;
                    var objectArg = objectField.FindArgument(argName);
                    // Assert interface field arg exists on object field.
                    if (objectArg == null)
                    {
                        throw new GraphQLLanguageModelException(
                            $"{ifaceField} expects argument \"{argName}\" " +
                            "but" +
                            $"{objectField} does not provide it.");
                    }

                    // Assert interface field arg type matches object field arg type.
                    if (!ifaceArg.InputType.Equals(objectArg.InputType))
                    {
                        throw new GraphQLLanguageModelException(
                            $"{ifaceField}(${argName}) expects type \"{ifaceArg.InputType}\" " +
                            "but" +
                            $"{objectField}(${argName}) provides type \"{objectArg.InputType}\".");
                    }
                }

                foreach (var objectArg in objectField.GetArguments())
                {
                    var argName = objectArg.Name;
                    var ifaceArg = ifaceField.FindArgument(argName);
                    if (ifaceArg == null)
                    {
                        if (!(objectArg.InputType is NonNullType))
                        {
                            throw new GraphQLLanguageModelException(
                                $"{objectField}({argName}) is of required type " +
                                $"\"{objectArg.InputType}\" but is not also provided by the " +
                                $"interface {interfaceType.Name}.{fieldName}.");
                        }
                    }
                }
            }
        }

        [GraphQLIgnore]
        public Directive? FindDirective<TDirective>() where TDirective : notnull
            => FindDirective(typeof(TDirective));

        [GraphQLIgnore]
        public bool HasDirective<TDirective>() where TDirective : notnull => HasDirective(typeof(TDirective));

        [GraphQLIgnore]
        public Directive GetDirective<TDirective>() where TDirective : notnull => GetDirective(typeof(TDirective));

        [GraphQLIgnore]
        public bool TryGetDirective<TDirective>([NotNullWhen(true)] out Directive? directive) =>
            TryGetDirective(typeof(TDirective), out directive);

        public override string ToString() => "Schema";
        [GraphQLCanBeNull] public string? Description { get; }
    }
}