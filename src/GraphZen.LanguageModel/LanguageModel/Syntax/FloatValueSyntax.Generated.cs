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
    public partial class FloatValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="FloatValueSyntax" /> nodes.</summary>
        public static IReadOnlyList<FloatValueSyntax> EmptyList { get; } = ImmutableList<FloatValueSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="FloatValueSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterFloatValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="FloatValueSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveFloatValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="FloatValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterFloatValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="FloatValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveFloatValue(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.FloatValue;
    }
}