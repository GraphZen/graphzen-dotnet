// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using JetBrains.Annotations;

namespace GraphZen.Language.Internal
{
    internal static class NodeExtensions
    {
        [NotNull]
        public static ISyntaxNodeLocation GetLocation(this IEnumerable<SyntaxNode> nodes) => nodes != null
            ? new LocationContainer(
                SyntaxLocation.FromMany(nodes
                    .Where(n => n != null)
                    .Where(n => n.Location != null)
                    .Select(n => n.Location).ToArray()))
            : new LocationContainer(null);


        [NotNull]
        [ItemNotNull]
        internal static IEnumerable<SyntaxNode> Concat(
            [NotNull] [ItemNotNull] this IEnumerable<SyntaxNode> first,
            [NotNull] [ItemNotNull] IEnumerable<SyntaxNode> second) =>
            first.Concat<SyntaxNode>(second);

        [NotNull]
        [ItemNotNull]
        internal static IEnumerable<SyntaxNode> Concat(
            [NotNull] [ItemNotNull] this IEnumerable<SyntaxNode> first,
            [NotNull] Func<IEnumerable<SyntaxNode>> second) =>
            // ReSharper disable once AssignNullToNotNullAttribute
            first.Concat<SyntaxNode>(second());

        [NotNull]
        [ItemNotNull]
        internal static IEnumerable<SyntaxNode> Concat(
            [NotNull] [ItemNotNull] this IEnumerable<SyntaxNode> nodes,
            SyntaxNode node) =>
            nodes.Concat(node.ToEnumerable());


        internal static bool NodesEqual<T>(
            [NotNull] this IEnumerable<T> nodes,
            [NotNull] IEnumerable<T> otherNodes) where T : SyntaxNode
        {
            var sequenceEquals = nodes.SequenceEqual(otherNodes);
            if (sequenceEquals)
            {
                return true;
            }

            return false;
        }

        private struct LocationContainer : ISyntaxNodeLocation
        {
            public LocationContainer(SyntaxLocation location)
            {
                Location = location;
            }

            public SyntaxLocation Location { get; }
        }
    }
}