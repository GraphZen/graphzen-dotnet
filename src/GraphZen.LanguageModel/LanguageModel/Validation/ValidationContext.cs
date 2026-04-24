// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.LanguageModel.Validation;

public abstract class ValidationContext
{
    private readonly Lazy<GraphQLSyntaxWalker?> _parentVisitor;


    protected ValidationContext(DocumentSyntax ast, Lazy<GraphQLSyntaxWalker?> parentVisitor)
    {
        AST = ast;
        _parentVisitor = parentVisitor;
    }


    public IReadOnlyCollection<SyntaxNode> Ancestors => _parentVisitor.Value!.Ancestors;


    public DocumentSyntax AST { get; }


    private List<GraphQLServerError> Errors { get; } = new();

    public virtual void Enter(SyntaxNode node)
    {
    }

    public virtual void Leave(SyntaxNode node)
    {
    }

    public void ReportError(GraphQLServerError error)
    {
        Check.NotNull(error, nameof(error));
        Errors.Add(error);
    }


    public IReadOnlyCollection<GraphQLServerError> GetErrors() => Errors.AsReadOnly();
}