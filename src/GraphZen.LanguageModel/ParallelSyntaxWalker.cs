// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    public abstract class ParallelSyntaxWalker : GraphQLSyntaxWalker
    {
        public ParallelSyntaxWalker(IReadOnlyCollection<GraphQLSyntaxVisitor<VisitAction>> visitors)
        {
            Visitors = Check.NotNull(visitors, nameof(visitors));
        }

        [NotNull]
        [ItemNotNull]
        private IReadOnlyCollection<GraphQLSyntaxVisitor<VisitAction>> Visitors { get; }

        [NotNull]
        private Dictionary<GraphQLSyntaxVisitor<VisitAction>, SyntaxNode> Skips { get; } =
            new Dictionary<GraphQLSyntaxVisitor<VisitAction>, SyntaxNode>();

        [NotNull]
        private HashSet<GraphQLSyntaxVisitor<VisitAction>> IgnoredVisitors { get; } =
            new HashSet<GraphQLSyntaxVisitor<VisitAction>>();

        private bool IsValidVisitor([NotNull] GraphQLSyntaxVisitor<VisitAction> visitor) =>
            !IgnoredVisitors.Contains(visitor) && !Skips.ContainsKey(visitor);

        public override void OnEnter(SyntaxNode node)
        {
            if (node != null)
            {
                foreach (var visitor in Visitors)
                {
                    if (IsValidVisitor(visitor))
                    {
                        HandleResult(node.VisitEnter(visitor), visitor, node);
                    }
                }
            }
        }

        public override void OnLeave(SyntaxNode node)
        {
            if (node != null)
            {
                foreach (var visitor in Visitors)
                {
                    if (IsValidVisitor(visitor))
                    {
                        HandleResult(node.VisitLeave(visitor), visitor, node);
                    }

                    if (Skips.TryGetValue(visitor, out var n) && n == node)
                    {
                        Skips.Remove(visitor);
                    }
                }
            }
        }

        private void HandleResult(VisitAction result, [NotNull] GraphQLSyntaxVisitor<VisitAction> visitor,
            SyntaxNode node)
        {
            switch (result)
            {
                case Skip _:
                    Skips[visitor] = node;
                    break;
                case Break _:
                    IgnoredVisitors.Add(visitor);
                    break;
            }
        }
    }
}