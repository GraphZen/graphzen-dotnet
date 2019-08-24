// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    public abstract class GraphQLSyntaxWalker<TResult> : GraphQLSyntaxVisitor<TResult>
    {
         private readonly Stack<SyntaxNode> _ancestors = new Stack<SyntaxNode>();

        
        public IReadOnlyCollection<SyntaxNode> Ancestors => _ancestors;

        public override TResult Visit(SyntaxNode node)
        {
            if (node != null)
            {
                node.VisitEnter(this);
                _ancestors.Push(node);
                foreach (var child in node.Children)
                {
                    Visit(child);
                }

                _ancestors.Pop();
                return node.VisitLeave(this);
            }

            return default;
        }
    }

    public abstract class GraphQLSyntaxWalker : GraphQLSyntaxVisitor
    {
         private readonly Stack<SyntaxNode> _ancestors = new Stack<SyntaxNode>();

        
        public IReadOnlyCollection<SyntaxNode> Ancestors => _ancestors;

        public override void Visit(SyntaxNode node)
        {
            if (node != null)
            {
                node.VisitEnter(this);
                _ancestors.Push(node);
                foreach (var child in node.Children)
                {
                    Visit(child);
                }

                _ancestors.Pop();
                node.VisitLeave(this);
            }
        }
    }
}