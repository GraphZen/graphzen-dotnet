#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel {
public  partial class DirectiveDefinitionSyntax {

	    /// <summary>Empty, read-only list of <see cref="DirectiveDefinitionSyntax"/> nodes.</summary>
		public static IReadOnlyList<DirectiveDefinitionSyntax> EmptyList {get;} = ImmutableList<DirectiveDefinitionSyntax>.Empty; 
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> enters a <see cref="DirectiveDefinitionSyntax"/> node.</summary>
		public override void VisitEnter( GraphQLSyntaxVisitor visitor) => visitor.EnterDirectiveDefinition(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> leaves a <see cref="DirectiveDefinitionSyntax"/> node.</summary>
		public override void VisitLeave( GraphQLSyntaxVisitor visitor) => visitor.LeaveDirectiveDefinition(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> enters a <see cref="DirectiveDefinitionSyntax"/> node.</summary>
		public override TResult VisitEnter<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterDirectiveDefinition(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> leaves a <see cref="DirectiveDefinitionSyntax"/> node.</summary>
		public override TResult VisitLeave<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeaveDirectiveDefinition(this);
		public override SyntaxKind Kind {get;} = SyntaxKind.DirectiveDefinition;	

}
}
