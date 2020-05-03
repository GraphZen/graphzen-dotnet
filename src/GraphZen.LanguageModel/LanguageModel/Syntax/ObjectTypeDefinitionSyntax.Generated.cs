// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming

namespace GraphZen.LanguageModel
{
    public partial class ObjectTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ObjectTypeDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<ObjectTypeDefinitionSyntax> EmptyList { get; } =
            ImmutableList<ObjectTypeDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ObjectTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterObjectTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ObjectTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveObjectTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ObjectTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterObjectTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ObjectTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveObjectTypeDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.ObjectTypeDefinition;
    }
}