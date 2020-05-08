#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.LanguageModel {
public  partial class InlineFragmentSyntax {

	    /// <summary>Empty, read-only list of <see cref="InlineFragmentSyntax"/> nodes.</summary>
		public static IReadOnlyList<InlineFragmentSyntax> EmptyList {get;} = ImmutableList<InlineFragmentSyntax>.Empty; 
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> enters a <see cref="InlineFragmentSyntax"/> node.</summary>
		public override void VisitEnter( GraphQLSyntaxVisitor visitor) => visitor.EnterInlineFragment(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> leaves a <see cref="InlineFragmentSyntax"/> node.</summary>
		public override void VisitLeave( GraphQLSyntaxVisitor visitor) => visitor.LeaveInlineFragment(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> enters a <see cref="InlineFragmentSyntax"/> node.</summary>
		public override TResult VisitEnter<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterInlineFragment(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> leaves a <see cref="InlineFragmentSyntax"/> node.</summary>
		public override TResult VisitLeave<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeaveInlineFragment(this);
		public override SyntaxKind Kind {get;} = SyntaxKind.InlineFragment;	

}
}
// Source Hash Code: -1361408320