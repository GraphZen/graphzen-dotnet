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

namespace GraphZen.TypeSystem.Internal
{
    public static class GraphQLTypeExtensions
    {
        public static string Print(this ISyntaxConvertable source) =>
            Check.NotNull(source, nameof(source)).ToSyntaxNode().ToSyntaxString();


        public static IReadOnlyList<TSyntaxNode> ToSyntaxNodes<TSyntaxNode>(
            this IEnumerable<ISyntaxConvertable> source)
            where TSyntaxNode : SyntaxNode
        {
            Check.NotNull(source, nameof(source));
            return source.Select(_ => (TSyntaxNode)_.ToSyntaxNode()).ToImmutableList();
        }


        public static IEnumerable<SyntaxNode> ToSyntaxNodes(
            this IEnumerable<ISyntaxConvertable> source)
        {
            Check.NotNull(source, nameof(source));
            return source.Select(_ => _.ToSyntaxNode());
        }


        public static bool IsInputType(this IGraphQLTypeReference type) =>
            type switch
            {
                IInputDefinition _ => true,
                IWrappingType wrapping => wrapping.OfType.IsInputType(),
                _ => false
            };


        public static bool IsOutputType(this IGraphQLTypeReference type) => type switch
        {
            IOutputDefinition _ => true,
            IWrappingType wrapping => wrapping.OfType.IsOutputType(),
            _ => false
        };


        public static NamedType GetNamedType(this IGraphQLType type) =>
            type switch
            {
                NamedType named => named,
                IWrappingType wrapped => wrapped.OfType.GetNamedType(),
                _ => throw new Exception()
            };

        public static NamedType? MaybeGetNamedType(this IGraphQLType type)
        {
            switch (type)
            {
                case NamedType named:
                    return named;
                case IWrappingType wrapped:
                    return wrapped.OfType.MaybeGetNamedType();
                default:
                    return null;
            }
        }


        public static INullableType GetNullableType(this IGraphQLType type) =>
            type is NonNullType nonNull ? nonNull.OfType : (INullableType)type;
    }
}