// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
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
            return source.Select(_ => (TSyntaxNode) _.ToSyntaxNode()).ToList().AsReadOnly();
        }


        public static IEnumerable<SyntaxNode> ToSyntaxNodes(
            this IEnumerable<ISyntaxConvertable> source)
        {
            Check.NotNull(source, nameof(source));
            return source.Select(_ => _.ToSyntaxNode());
        }


        public static bool IsInputType(this IGraphQLTypeReference type)
        {
            switch (type)
            {
                case IInputDefinition _:
                    return true;
                case IWrappingType wrapping:
                    return wrapping.OfType.IsInputType();
                default:
                    return false;
            }
        }


        public static bool IsOutputType(this IGraphQLTypeReference type)
        {
            Check.NotNull(type, nameof(type));
            switch (type)
            {
                case IOutputDefinition _:
                    return true;
                case IWrappingType wrapping:
                    return wrapping.OfType.IsOutputType();
                default:
                    return false;
            }
        }


        public static NamedType? GetNamedType(this IGraphQLType type)
        {
            switch (type)
            {
                case NamedType named:
                    return named;
                case IWrappingType wrapped:
                    return wrapped.OfType.GetNamedType();
                default:
                    return null;
            }
        }


        public static INullableType GetNullableType(this IGraphQLType type) =>
            type is NonNullType nonNull ? nonNull.OfType : (INullableType) type;
    }
}