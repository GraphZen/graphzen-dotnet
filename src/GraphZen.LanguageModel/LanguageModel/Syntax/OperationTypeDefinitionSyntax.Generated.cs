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
public  partial class OperationTypeDefinitionSyntax {

	    /// <summary>Empty, read-only list of <see cref="OperationTypeDefinitionSyntax"/> nodes.</summary>
		public static IReadOnlyList<OperationTypeDefinitionSyntax> EmptyList {get;} = ImmutableList<OperationTypeDefinitionSyntax>.Empty; 
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> enters a <see cref="OperationTypeDefinitionSyntax"/> node.</summary>
		public override void VisitEnter( GraphQLSyntaxVisitor visitor) => visitor.EnterOperationTypeDefinition(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> leaves a <see cref="OperationTypeDefinitionSyntax"/> node.</summary>
		public override void VisitLeave( GraphQLSyntaxVisitor visitor) => visitor.LeaveOperationTypeDefinition(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> enters a <see cref="OperationTypeDefinitionSyntax"/> node.</summary>
		public override TResult VisitEnter<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterOperationTypeDefinition(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> leaves a <see cref="OperationTypeDefinitionSyntax"/> node.</summary>
		public override TResult VisitLeave<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeaveOperationTypeDefinition(this);
		public override SyntaxKind Kind {get;} = SyntaxKind.OperationTypeDefinition;	

}
}
// Source Hash Code: 1750500953