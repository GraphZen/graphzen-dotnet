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
    public partial class ObjectTypeExtensionSyntax
    {
        #region SyntaxNodeGenerator

        /// <summary>Empty, read-only list of <see cref="ObjectTypeExtensionSyntax" /> nodes.</summary>
        public static IReadOnlyList<ObjectTypeExtensionSyntax> EmptyList { get; } =
            ImmutableList<ObjectTypeExtensionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ObjectTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterObjectTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ObjectTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveObjectTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ObjectTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterObjectTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ObjectTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveObjectTypeExtension(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.ObjectTypeExtension;

        #endregion
    }
}
// Source Hash Code: 13756148523351973527