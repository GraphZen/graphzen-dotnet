// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.LanguageModel
{
    public partial class InterfaceTypeDefinitionSyntax
    {
        #region SyntaxNodeGenerator

        /// <summary>Empty, read-only list of <see cref="InterfaceTypeDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<InterfaceTypeDefinitionSyntax> EmptyList { get; } =
            ImmutableList<InterfaceTypeDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InterfaceTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterInterfaceTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InterfaceTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveInterfaceTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a
        ///     <see cref="InterfaceTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInterfaceTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a
        ///     <see cref="InterfaceTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInterfaceTypeDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.InterfaceTypeDefinition;

        #endregion
    }
}
// Source Hash Code: 7582578273286503015