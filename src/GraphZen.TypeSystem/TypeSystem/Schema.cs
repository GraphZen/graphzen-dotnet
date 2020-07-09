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
using GraphZen.TypeSystem.Internal;
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


        public Schema(SchemaDefinition schemaDefinition) : base(schemaDefinition
            .DirectiveAnnotations, null!)
        {
            Check.NotNull(schemaDefinition, nameof(schemaDefinition));
            Description = schemaDefinition.Description;
            Definition = schemaDefinition;
            _directives = schemaDefinition.GetDirectives()
                .ToImmutableDictionary(_ => _.Name, _ => Directive.From(_, this));
            Directives = _directives.Values.ToImmutableList();
            _directivesByType = Directives.Where(_ => _.ClrType != null).ToImmutableDictionary(_ => _.ClrType!);


            Types =
                schemaDefinition.Types
                    .Select(CreateType)
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

            Introspection = new IntrospectionInfo(this);
        }

        internal IntrospectionInfo Introspection { get; }


        [GraphQLIgnore] public SchemaDefinition Definition { get; }

        [GraphQLIgnore] public IReadOnlyDictionary<string, NamedType> Types { get; }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Schema;


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
        public IEnumerable<Directive> GetDirectives(bool includeSpecDirectives = false) =>
            includeSpecDirectives ? Directives : Directives.Where(_ => !_.IsSpec);

        IObjectTypeDefinition? IQueryTypeDefinition.QueryType => QueryType;

        IObjectTypeDefinition? IMutationTypeDefinition.MutationType => MutationType;

        IObjectTypeDefinition? ISubscriptionTypeDefinition.SubscriptionType => SubscriptionType;

        IEnumerable<IDirectiveDefinition> IDirectivesDefinition.GetDirectives(bool includeSpecDirectives) =>
            GetDirectives(includeSpecDirectives);

        [GraphQLCanBeNull] public string? Description { get; }

        [GraphQLIgnore]
        public IEnumerable<NamedType> GetTypes(bool includeSpecTypes = false)
        {
            if (includeSpecTypes)
            {
                return Types.Values;
            }

            return Types.Values.Where(_ => !_.IsSpec);
        }


        public override SyntaxNode ToSyntaxNode() => _syntax.Value;


        [GraphQLIgnore]
        public DocumentSyntax ToDocumentSyntax() => _sdlSyntax.Value;


        [GraphQLIgnore]
        public static Schema Create() => Create(sb => { });


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
            var schemaBuilder = new SchemaBuilder();
            schemaBuilder.AddSpecMembers();
            schemaConfiguration(schemaBuilder);
            var schemaDef = schemaBuilder.GetInfrastructure<SchemaDefinition>();
            return schemaDef.ToSchema();
        }


        [GraphQLIgnore]
        public static Schema Create<TContext>(Action<SchemaBuilder<TContext>> schemaConfiguration)
            where TContext : GraphQLContext
        {
            Check.NotNull(schemaConfiguration, nameof(schemaConfiguration));
            var schemaBuilder = new SchemaBuilder<TContext>();
            schemaBuilder.AddSpecMembers();
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
                    return GetType(reference);
                case INamedTypeReference named:
                    return GetType(named.Name);
            }

            throw new InvalidOperationException($"Unable to get input type for definition {typeReference}.");
        }

        private IGraphQLType GetType(TypeReference reference)
        {
            IGraphQLType GetType(TypeSyntax node)
            {
                switch (node)
                {
                    case ListTypeSyntax list:
                        return ListType.Of(GetType(list.OfType));
                    case NonNullTypeSyntax nn:
                        return NonNullType.Of((INullableType)GetType(nn.OfType));
                    case NamedTypeSyntax _:
                        var nameMatch = FindType(reference.Identity.Name);
                        if (nameMatch != null)
                        {
                            return nameMatch;
                        }

                        if (reference.Identity.ClrType != null)
                        {
                            var typeMatches = Types.Values
                                .Where(_ => _.ClrType != null && _.ClrType.IsAssignableFrom(reference.Identity.ClrType))
                                .ToArray();

                            if (typeMatches.Length == 1)
                            {
                                return typeMatches[0];
                            }

                            if (typeMatches.Length > 1)
                            {
                                throw new Exception(
                                    $"More than one type in the schema matched type reference  \"{reference.Identity.Name}\" with CLR type {reference.Identity.ClrType}");
                            }

                            throw new Exception(
                                $"Unable to find output type for type reference named \"{reference.Identity.Name}\" with CLR type {reference.Identity.ClrType}");
                        }

                        throw new Exception(
                            $"Unable to find output type for type reference named \"{reference.Identity.Name}\"");
                }

                throw new Exception($"Unable to create type reference from type node: {node?.GetType()}");
            }

            return GetType(reference.TypeSyntax);
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


        public bool TryGetType(Type clrType, [NotNullWhen(true)] out INamedType type)
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


            foreach (var ifaceField in interfaceType.Fields)
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
        IEnumerable<INamedTypeDefinition> INamedTypesDefinition.GetTypes(bool includeSpecTypes) => GetTypes(includeSpecTypes);

        ISchemaDefinition IMemberDefinition.Schema => this;
        IEnumerable<IMemberDefinition> IMemberParentDefinition.Children() => Children();

        public IEnumerable<IMember> Children()
        {
            yield break;
        }


        internal class IntrospectionInfo
        {
            public IntrospectionInfo(Schema schema)
            {
                SchemaMetaFieldDef = new Field("__schema",
                    "Access the current type schema of this server.", null,
                    NonNullType.Of(schema.GetObject("__Schema")), null,
                    (source, args, context, info) => info.Schema, ImmutableArray<IDirectiveAnnotation>.Empty, null,
                    schema);

                TypeMetaFieldDef = new Field("__type",
                    "Request the type information of a single type.", null, schema.GetObject("__Type"),
                    field => new[]
                    {
                        new Argument("name", null, NonNullType.Of(schema.GetScalar<string>()), null, false, null, field,
                            null)
                    },
                    (source, args, context, info) => info.Schema.GetType(args.name), null, null, schema);

                TypeNameMetaFieldDef = new Field("__typename",
                    "The name of the current Object type at runtime.", null, NonNullType.Of(schema.GetScalar<string>()),
                    null,
                    (source, args, context, info) => info.ParentType.Name, null, null, schema);
            }

            public Field SchemaMetaFieldDef { get; }

            public Field TypeMetaFieldDef { get; }

            public Field TypeNameMetaFieldDef { get; }
        }
    }
}