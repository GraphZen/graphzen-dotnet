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
public  partial class PunctuatorSyntax {

	    /// <summary>Empty, read-only list of <see cref="PunctuatorSyntax"/> nodes.</summary>
		public static IReadOnlyList<PunctuatorSyntax> EmptyList {get;} = ImmutableList<PunctuatorSyntax>.Empty; 
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> enters a <see cref="PunctuatorSyntax"/> node.</summary>
		public override void VisitEnter( GraphQLSyntaxVisitor visitor) => visitor.EnterPunctuator(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> leaves a <see cref="PunctuatorSyntax"/> node.</summary>
		public override void VisitLeave( GraphQLSyntaxVisitor visitor) => visitor.LeavePunctuator(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> enters a <see cref="PunctuatorSyntax"/> node.</summary>
		public override TResult VisitEnter<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterPunctuator(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> leaves a <see cref="PunctuatorSyntax"/> node.</summary>
		public override TResult VisitLeave<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeavePunctuator(this);
		public override SyntaxKind Kind {get;} = SyntaxKind.Punctuator;	

}
}
// Source Hash Code: 446269386