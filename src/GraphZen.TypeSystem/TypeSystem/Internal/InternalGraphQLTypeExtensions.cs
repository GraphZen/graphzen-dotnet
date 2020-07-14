// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public static class InternalGraphQLTypeExtensions
    {
        public static string Print(this ISyntaxMember source) =>
            Check.NotNull(source, nameof(source)).ToSyntaxNode().ToSyntaxString();

        public static IReadOnlyList<TSyntaxNode> ToSyntaxNodes<TSyntaxNode>(
            this IEnumerable<ISyntaxMember> source)
            where TSyntaxNode : SyntaxNode
        {
            Check.NotNull(source, nameof(source));
            return source.Select(_ => (TSyntaxNode)_.ToSyntaxNode()).ToImmutableList();
        }


        public static IEnumerable<SyntaxNode> ToSyntaxNodes(
            this IEnumerable<ISyntaxMember> source)
        {
            Check.NotNull(source, nameof(source));
            return source.Select(_ => _.ToSyntaxNode());
        }
    }
}