// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Inline fragement
    ///     http://facebook.github.io/graphql/June2018/#InlineFragment
    /// </summary>
    public partial class InlineFragmentSyntax : SelectionSyntax, IFragmentTypeConditionSyntax
    {
        [GenFactory(typeof(SyntaxFactory))]
        public InlineFragmentSyntax(SelectionSetSyntax selectionSet, NamedTypeSyntax? typeCondition = null,
            IReadOnlyList<DirectiveSyntax>? directives = null, SyntaxLocation? location = null) : base(location)
        {
            SelectionSet = Check.NotNull(selectionSet, nameof(selectionSet));
            TypeCondition = typeCondition;
            Directives = directives ?? DirectiveSyntax.EmptyList;
        }

        /// <summary>
        ///     The fragment selection set.
        /// </summary>

        public SelectionSetSyntax SelectionSet { get; }


        public override IEnumerable<SyntaxNode> Children() =>
            TypeCondition.Concat(Directives).Concat(SelectionSet);


        public override IReadOnlyList<DirectiveSyntax> Directives { get; }

        /// <summary>
        ///     The type which this inline fragment applies to. (Optional)
        /// </summary>

        public NamedTypeSyntax? TypeCondition { get; }

        private bool Equals(InlineFragmentSyntax other) =>
            SelectionSet.Equals(other.SelectionSet) && Equals(TypeCondition, other.TypeCondition) &&
            Directives.SequenceEqual(other.Directives);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is InlineFragmentSyntax && Equals((InlineFragmentSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = SelectionSet.GetHashCode();
                hashCode = (hashCode * 397) ^ (TypeCondition != null ? TypeCondition.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }
    }
}