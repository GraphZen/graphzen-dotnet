// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.LanguageModel.Validation;

public abstract class ValidationRuleVisitor : GraphQLSyntaxVisitor<VisitAction>
{
    protected ValidationRuleVisitor(ValidationContext context) => Context = Check.NotNull(context, nameof(context));


    protected ValidationContext Context { get; }

    public void ReportError(GraphQLServerError error)
    {
        Context.ReportError(error);
    }

    public void ReportError(string message, params SyntaxNode[] nodes)
    {
        ReportError(new GraphQLServerError(message, nodes));
    }

    public void ReportError(string message, IReadOnlyList<SyntaxNode> nodes)
    {
        ReportError(new GraphQLServerError(message, nodes));
    }
}
