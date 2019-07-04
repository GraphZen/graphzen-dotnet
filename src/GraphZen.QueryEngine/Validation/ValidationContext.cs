// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;


namespace GraphZen.Validation
{
    public abstract class ValidationContext
    {
        [NotNull] [ItemNotNull] private readonly Lazy<GraphQLSyntaxWalker> _parentVisitor;


        protected ValidationContext([NotNull] DocumentSyntax ast, [NotNull] Lazy<GraphQLSyntaxWalker> parentVisitor)
        {
            AST = ast;
            _parentVisitor = parentVisitor;
        }

        [NotNull]
        public IReadOnlyCollection<SyntaxNode> Ancestors => _parentVisitor.Value.Ancestors;

        [NotNull]
        public DocumentSyntax AST { get; }

        [NotNull]
        [ItemNotNull]
        private List<GraphQLError> Errors { get; } = new List<GraphQLError>();

        public virtual void Enter(SyntaxNode node)
        {
        }

        public virtual void Leave(SyntaxNode node)
        {
        }

        public void ReportError(GraphQLError error)
        {
            Check.NotNull(error, nameof(error));
            Errors.Add(error);
        }

        [NotNull]
        [ItemNotNull]
        public IReadOnlyCollection<GraphQLError> GetErrors() => Errors.AsReadOnly();
    }
}