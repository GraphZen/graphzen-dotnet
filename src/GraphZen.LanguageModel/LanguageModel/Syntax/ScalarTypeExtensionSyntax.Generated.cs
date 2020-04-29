#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming

namespace GraphZen.LanguageModel {
public  partial class ScalarTypeExtensionSyntax {

	    /// <summary>Empty, read-only list of <see cref="ScalarTypeExtensionSyntax"/> nodes.</summary>
		public static IReadOnlyList<ScalarTypeExtensionSyntax> EmptyList {get;} = ImmutableList<ScalarTypeExtensionSyntax>.Empty; 
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> enters a <see cref="ScalarTypeExtensionSyntax"/> node.</summary>
		public override void VisitEnter( GraphQLSyntaxVisitor visitor) => visitor.EnterScalarTypeExtension(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> leaves a <see cref="ScalarTypeExtensionSyntax"/> node.</summary>
		public override void VisitLeave( GraphQLSyntaxVisitor visitor) => visitor.LeaveScalarTypeExtension(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> enters a <see cref="ScalarTypeExtensionSyntax"/> node.</summary>
		public override TResult VisitEnter<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterScalarTypeExtension(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> leaves a <see cref="ScalarTypeExtensionSyntax"/> node.</summary>
		public override TResult VisitLeave<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeaveScalarTypeExtension(this);
		public override SyntaxKind Kind {get;} = SyntaxKind.ScalarTypeExtension;	

}
}
