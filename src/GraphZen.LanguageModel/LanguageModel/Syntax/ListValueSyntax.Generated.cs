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
    public partial class ListValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ListValueSyntax" /> nodes.</summary>
        public static IReadOnlyList<ListValueSyntax> EmptyList { get; } = ImmutableList<ListValueSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ListValueSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterListValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ListValueSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveListValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ListValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterListValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ListValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveListValue(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.ListValue;
    }
}