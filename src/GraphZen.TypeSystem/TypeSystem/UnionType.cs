// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class UnionType : NamedTypeDefinition, IUnionType
    {
        private readonly Lazy<UnionTypeDefinitionSyntax> _syntax;
        private readonly Lazy<IReadOnlyDictionary<string, IObjectTypeReference>> _memberTypeMap;
        private readonly Lazy<IReadOnlyList<IObjectTypeReference>> _memberTypes;

        public UnionType(string name, string? description, Type? clrType,
            IEnumerable<IObjectTypeReference> memberTypes,
            TypeResolver<object, GraphQLContext>? resolveType,
            IReadOnlyList<IDirective> directives, Schema schema
        ) : base(name, description, clrType, directives, schema)
        {
            ResolveType = resolveType;

            _memberTypeMap = new Lazy<IReadOnlyDictionary<string, IObjectTypeReference>>(() =>
            {
                throw new NotImplementedException();
            });
            //_memberTypes = new Lazy<IReadOnlyList<ObjectType>>(() => MemberTypesMap.Values.ToImmutableList());
            //_syntax = new Lazy<UnionTypeDefinitionSyntax>(() => new UnionTypeDefinitionSyntax(SyntaxFactory.Name(Name), SyntaxHelpers.Description(Description), null, MemberTypes.Select(_ => (NamedTypeSyntax)_.ToTypeSyntax()).ToArray()));
        }

        public TypeResolver<object, GraphQLContext>? ResolveType { get; }



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


        public static UnionType From(IUnionType definition, Schema schema)
        {
            var lazyTypes = new Lazy<IReadOnlyDictionary<string, ObjectType>>(() =>
            {
                return definition.GetMemberTypes()
                    .ToDictionary(_ => _.Name, _ => schema.GetObject(_.Name));
            });
            return new UnionType(definition.Name, definition.Description, definition.ClrType, lazyTypes,
                definition.ResolveType, definition.DirectiveAnnotations, schema);
        }
    }
}