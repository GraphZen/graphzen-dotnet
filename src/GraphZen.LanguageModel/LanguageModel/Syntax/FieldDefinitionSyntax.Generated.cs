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
public  partial class FieldDefinitionSyntax {

	    /// <summary>Empty, read-only list of <see cref="FieldDefinitionSyntax"/> nodes.</summary>
		public static IReadOnlyList<FieldDefinitionSyntax> EmptyList {get;} = ImmutableList<FieldDefinitionSyntax>.Empty; 
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> enters a <see cref="FieldDefinitionSyntax"/> node.</summary>
		public override void VisitEnter( GraphQLSyntaxVisitor visitor) => visitor.EnterFieldDefinition(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> leaves a <see cref="FieldDefinitionSyntax"/> node.</summary>
		public override void VisitLeave( GraphQLSyntaxVisitor visitor) => visitor.LeaveFieldDefinition(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> enters a <see cref="FieldDefinitionSyntax"/> node.</summary>
		public override TResult VisitEnter<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterFieldDefinition(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> leaves a <see cref="FieldDefinitionSyntax"/> node.</summary>
		public override TResult VisitLeave<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeaveFieldDefinition(this);
		public override SyntaxKind Kind {get;} = SyntaxKind.FieldDefinition;	

}
}
// Source Hash Code: 747423667