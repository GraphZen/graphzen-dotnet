#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming

namespace GraphZen.LanguageModel
{
    public partial class VariableSyntax
    {

        /// <summary>Empty, read-only list of <see cref="VariableSyntax"/> nodes.</summary>
        public static IReadOnlyList<VariableSyntax> EmptyList { get; } = ImmutableList<VariableSyntax>.Empty;
        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> enters a <see cref="VariableSyntax"/> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterVariable(this);
        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> leaves a <see cref="VariableSyntax"/> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveVariable(this);
        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> enters a <see cref="VariableSyntax"/> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterVariable(this);
        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> leaves a <see cref="VariableSyntax"/> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeaveVariable(this);
        public override SyntaxKind Kind { get; } = SyntaxKind.Variable;

    }
}
