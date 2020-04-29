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
    public partial class InputValueDefinitionSyntax
    {

        /// <summary>Empty, read-only list of <see cref="InputValueDefinitionSyntax"/> nodes.</summary>
        public static IReadOnlyList<InputValueDefinitionSyntax> EmptyList { get; } = ImmutableList<InputValueDefinitionSyntax>.Empty;
        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> enters a <see cref="InputValueDefinitionSyntax"/> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterInputValueDefinition(this);
        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> leaves a <see cref="InputValueDefinitionSyntax"/> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveInputValueDefinition(this);
        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> enters a <see cref="InputValueDefinitionSyntax"/> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterInputValueDefinition(this);
        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> leaves a <see cref="InputValueDefinitionSyntax"/> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeaveInputValueDefinition(this);
        public override SyntaxKind Kind { get; } = SyntaxKind.InputValueDefinition;

    }
}
