// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel.Internal
{
    internal static class NodeExtensions
    {
        public static ISyntaxNodeLocation GetLocation(this IEnumerable<SyntaxNode>? nodes)
        {
            return nodes != null
                ? new LocationContainer(
                    SyntaxLocation.FromMany(nodes
                        .Where(n => n != null)
                        .Where(n => n.Location != null)
                        .Select(n => n.Location!).ToArray()))
                : new LocationContainer(null);
        }


        internal static IEnumerable<SyntaxNode> Concat(
            this IEnumerable<SyntaxNode> first,
            IEnumerable<SyntaxNode> second) =>
            first.Concat<SyntaxNode>(second);


        internal static IEnumerable<SyntaxNode> Concat(
            this IEnumerable<SyntaxNode> first,
            Func<IEnumerable<SyntaxNode>> second) =>
            first.Concat<SyntaxNode>(second());


        internal static IEnumerable<SyntaxNode> Concat(
            this IEnumerable<SyntaxNode> nodes,
            SyntaxNode? node) => node != null ? nodes.Concat(node.ToEnumerable()) : nodes;

        internal static IEnumerable<SyntaxNode> Concat(
            this SyntaxNode? node, IEnumerable<SyntaxNode> nodes) =>
            node != null ? node.ToEnumerable().Concat(nodes) : nodes;

        internal static IEnumerable<SyntaxNode> Concat(
            this SyntaxNode? node, SyntaxNode? other)
        {
            if (node != null)
            {
                yield return node;
            }

            if (other != null)
            {
                yield return other;
            }
        }


        internal static bool NodesEqual<T>(
            this IEnumerable<T> nodes,
            IEnumerable<T> otherNodes) where T : SyntaxNode
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
            public LocationContainer(SyntaxLocation? location)
            {
                Location = location;
            }

            public SyntaxLocation? Location { get; }
        }
    }
}