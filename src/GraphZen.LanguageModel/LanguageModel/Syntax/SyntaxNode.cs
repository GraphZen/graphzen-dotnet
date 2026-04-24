// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;

namespace GraphZen.LanguageModel;

/// <summary>
///     GraphQL AST node
/// </summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public abstract class SyntaxNode : ISyntaxNodeLocation
{
    private readonly Lazy<string> _printed;

    protected SyntaxNode(SyntaxLocation? location)
    {
        Location = location;
        _printed = new Lazy<string>(() => new Printer().Print(this));
    }


    public abstract IEnumerable<SyntaxNode> Children { get; }

    public abstract SyntaxKind Kind { [DebuggerStepThrough] get; }


    internal string DebuggerDisplay => ToSyntaxString();

    public SyntaxLocation? Location { get; }


    public IEnumerable<SyntaxNode> DescendantNodes()
    {
        return Children.SelectMany(c => c.DescendantNodes());
    }


    public abstract void VisitLeave(GraphQLSyntaxVisitor visitor);
    public abstract void VisitEnter(GraphQLSyntaxVisitor visitor);

    public abstract TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor);
    public abstract TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor);


    public string ToSyntaxString() => _printed.Value;
}