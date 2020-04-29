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
    public partial class FragmentSpreadSyntax
    {

        /// <summary>Empty, read-only list of <see cref="FragmentSpreadSyntax"/> nodes.</summary>
        public static IReadOnlyList<FragmentSpreadSyntax> EmptyList { get; } = ImmutableList<FragmentSpreadSyntax>.Empty;
        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> enters a <see cref="FragmentSpreadSyntax"/> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterFragmentSpread(this);
        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> leaves a <see cref="FragmentSpreadSyntax"/> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveFragmentSpread(this);
        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> enters a <see cref="FragmentSpreadSyntax"/> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterFragmentSpread(this);
        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> leaves a <see cref="FragmentSpreadSyntax"/> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeaveFragmentSpread(this);
        public override SyntaxKind Kind { get; } = SyntaxKind.FragmentSpread;

    }
}
