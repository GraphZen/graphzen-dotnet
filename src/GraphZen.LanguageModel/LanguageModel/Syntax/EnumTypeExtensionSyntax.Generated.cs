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
public  partial class EnumTypeExtensionSyntax {
#region SyntaxNodeGenerator

	    /// <summary>Empty, read-only list of <see cref="EnumTypeExtensionSyntax"/> nodes.</summary>
		public static IReadOnlyList<EnumTypeExtensionSyntax> EmptyList {get;} = ImmutableList<EnumTypeExtensionSyntax>.Empty; 
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> enters a <see cref="EnumTypeExtensionSyntax"/> node.</summary>
		public override void VisitEnter( GraphQLSyntaxVisitor visitor) => visitor.EnterEnumTypeExtension(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> leaves a <see cref="EnumTypeExtensionSyntax"/> node.</summary>
		public override void VisitLeave( GraphQLSyntaxVisitor visitor) => visitor.LeaveEnumTypeExtension(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> enters a <see cref="EnumTypeExtensionSyntax"/> node.</summary>
		public override TResult VisitEnter<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterEnumTypeExtension(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> leaves a <see cref="EnumTypeExtensionSyntax"/> node.</summary>
		public override TResult VisitLeave<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeaveEnumTypeExtension(this);
		public override SyntaxKind Kind {get;} = SyntaxKind.EnumTypeExtension;	

#endregion
}
}
// Source Hash Code: 7182955556738891353