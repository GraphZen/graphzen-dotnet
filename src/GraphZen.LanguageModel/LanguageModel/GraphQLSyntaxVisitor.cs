// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    public abstract partial class GraphQLSyntaxVisitor
    {
        public virtual void Visit(SyntaxNode node)
        {
        }

        public virtual void OnEnter(SyntaxNode node)
        {
        }

        public virtual void OnLeave(SyntaxNode node)
        {
        }
    }

    public abstract partial class GraphQLSyntaxVisitor<TResult>
    {
        public virtual TResult Visit(SyntaxNode node) => default;

        public virtual TResult OnEnter(SyntaxNode node) => default;

        public virtual TResult OnLeave(SyntaxNode node) => default;
    }
}