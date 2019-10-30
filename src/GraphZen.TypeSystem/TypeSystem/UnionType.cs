// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class UnionType : NamedType, IUnionType
    {
        private readonly Lazy<UnionTypeDefinitionSyntax> _syntax;
        private readonly Lazy<IReadOnlyDictionary<string, ObjectType>> _memberTypeMap;
        private readonly Lazy<IReadOnlyList<ObjectType>> _memberTypes;

        public UnionType(string name, string? description, Type? clrType,
            Lazy<IReadOnlyDictionary<string, ObjectType>> lazyTypes,
            TypeResolver<object, GraphQLContext>? resolveType,
            IReadOnlyList<IDirectiveAnnotation> directives
        ) : base(
            Check.NotNull(name, nameof(name)), description, clrType, Check.NotNull(directives, nameof(directives)))
        {
            Check.NotNull(lazyTypes, nameof(lazyTypes));
            ResolveType = resolveType;

            _memberTypeMap = new Lazy<IReadOnlyDictionary<string, ObjectType>>(() =>
            {
                var types = lazyTypes.Value;
                if (types == null || types.Count == 0)
                    throw new Exception($"Must provide list of types for Union {name}");

                var includedTypeNames = new Dictionary<string, bool>();
                foreach (var objectType in types.Values)
                {
                    if (includedTypeNames.ContainsKey(objectType.Name))
                        throw new Exception($"Union {name} can include {objectType.Name} only once.");

                    includedTypeNames[objectType.Name] = true;
                }

                return types;
            });
            _memberTypes = new Lazy<IReadOnlyList<ObjectType>>(() => MemberTypesMap.Values.ToImmutableList());
            _syntax = new Lazy<UnionTypeDefinitionSyntax>(() =>
                new UnionTypeDefinitionSyntax(SyntaxFactory.Name(Name), SyntaxHelpers.Description(Description), null,
                    MemberTypes.Select(_ => (NamedTypeSyntax)_.ToTypeSyntax()).ToArray()));
        }

        public TypeResolver<object, GraphQLContext>? ResolveType { get; }

        public IEnumerable<ObjectType> GetMemberTypes() => MemberTypes;

        public IReadOnlyList<ObjectType> MemberTypes => _memberTypes.Value;
        public IReadOnlyDictionary<string, ObjectType> MemberTypesMap => _memberTypeMap.Value;

        public override TypeKind Kind { get; } = TypeKind.Union;

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Union;

        /*public static UnionType Create<TUnion>() => Create<TUnion, GraphQLContext>(null);

        public static UnionType Create<TUnion, TContext>(
            Action<UnionTypeBuilder<TUnion, TContext>> configurator) where TContext : GraphQLContext
        {
            var schemaDef = new SchemaDefinition();
            var definition = schemaDef.GetOrAddUnion(typeof(TUnion), ConfigurationSource.Explicit);
            if (configurator != null)
            {
                var builder = new UnionTypeBuilder<TUnion, TContext>(definition?.Builder);
                configurator(builder);
            }

            var schema = new Schema(schemaDef);
            return From(definition, schema);
        }*/
        IEnumerable<IObjectTypeDefinition> IMemberTypesDefinition.GetMemberTypes() => GetMemberTypes();


        public static UnionType From(IUnionTypeDefinition definition, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(schema, nameof(schema));
            var lazyTypes = new Lazy<IReadOnlyDictionary<string, ObjectType>>(() =>
            {
                return definition.GetMemberTypes()
                    .ToDictionary(_ => _.Name, _ => schema.GetObject(_.Name));
            });
            return new UnionType(definition.Name, definition.Description, definition.ClrType, lazyTypes,
                definition.ResolveType, definition.GetDirectiveAnnotations().ToList());
        }
    }
}