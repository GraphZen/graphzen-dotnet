// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel.Internal
{
    internal static class NodeExtensions
    {
        public static ISyntaxNodeLocation GetLocation(this IEnumerable<SyntaxNode> nodes)
        {
            return nodes != null
                ? new LocationContainer(
                    SyntaxLocation.FromMany(nodes
                        .Where(n => n != null)
                        .Where(n => n.Location != null)
                        .Select(n => n.Location).ToArray()))
                : new LocationContainer(null);
        }


        internal static IEnumerable<SyntaxNode> Concat(
            this IEnumerable<SyntaxNode> first,
            IEnumerable<SyntaxNode> second)
        {
            return first.Concat<SyntaxNode>(second);
        }


        internal static IEnumerable<SyntaxNode> Concat(
            this IEnumerable<SyntaxNode> first,
            Func<IEnumerable<SyntaxNode>> second)
        {
            return first.Concat<SyntaxNode>(second());
        }


        internal static IEnumerable<SyntaxNode> Concat(
            this IEnumerable<SyntaxNode> nodes,
            SyntaxNode node)
        {
            return nodes.Concat(node.ToEnumerable());
        }


        internal static bool NodesEqual<T>(
            this IEnumerable<T> nodes,
            IEnumerable<T> otherNodes) where T : SyntaxNode
        {
            var sequenceEquals = nodes.SequenceEqual(otherNodes);
            if (sequenceEquals) return true;

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