// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;




namespace GraphZen.LanguageModel
{
    public static class NamedSyntaxExtensions
    {
        public static IEnumerable<TNode> OrderByName<TNode>(
            this IEnumerable<TNode> source) where TNode : SyntaxNode, INamedSyntax
        {
            Check.NotNull(source, nameof(source));
            return source.OrderBy(_ => _.Name.Value);
        }

        public static TNode FindByName<TNode>(
            this IEnumerable<TNode> source, string name)
            where TNode : SyntaxNode, INamedSyntax
        {
            Check.NotNull(source, nameof(source));
            return source.SingleOrDefault(_ => _.Name.Value == name);
        }

        public static bool TryFindByName<TNode>(
            this IEnumerable<TNode> source, string name, out TNode result)
            where TNode : SyntaxNode, INamedSyntax
        {
            Check.NotNull(source, nameof(source));
            result = source.SingleOrDefault(_ => _.Name.Value == name);
            return result != null;
        }
    }
}