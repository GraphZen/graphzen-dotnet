// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel.Validation
{
    public abstract class ValidationRuleVisitor : GraphQLSyntaxVisitor<VisitAction>
    {
        protected ValidationRuleVisitor(ValidationContext context)
        {
            Context = Check.NotNull(context, nameof(context));
        }

        
        protected ValidationContext Context { get; }

        public void ReportError(GraphQLError error) => Context.ReportError(error);

        public void ReportError(string message, params SyntaxNode[] nodes) =>
            ReportError(new GraphQLError(message, nodes));

        public void ReportError(string message, IReadOnlyList<SyntaxNode> nodes) =>
            ReportError(new GraphQLError(message, nodes));
    }
}