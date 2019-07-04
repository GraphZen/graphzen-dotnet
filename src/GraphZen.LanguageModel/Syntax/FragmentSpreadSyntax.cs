// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Fragment spread
    ///     http://facebook.github.io/graphql/June2018/#FragmentSpread
    /// </summary>
    public partial class FragmentSpreadSyntax : SelectionSyntax
    {
        public FragmentSpreadSyntax(NameSyntax name,
            IReadOnlyList<DirectiveSyntax> directives = null,
            SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            if (Name.Value.Equals("on", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ArgumentException("Fragment spreads cannot use the name 'on'.");
            }

            Directives = directives ?? DirectiveSyntax.EmptyList;
        }

        /// <summary>
        ///     The name of the fragment.
        /// </summary>
        [NotNull]
        public NameSyntax Name { get; }

        /// <summary>
        ///     Fragment spread directives. (Optional)
        /// </summary>
        public override IReadOnlyList<DirectiveSyntax> Directives { get; }


        public override IEnumerable<SyntaxNode> Children =>
            NodeExtensions.Concat((IEnumerable<SyntaxNode>) Name.ToEnumerable(), (IEnumerable<SyntaxNode>) Directives);


        private bool Equals([NotNull] LanguageModel.FragmentSpreadSyntax other) =>
            Name.Equals(other.Name) && Directives.SequenceEqual(other.Directives);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is LanguageModel.FragmentSpreadSyntax && Equals((LanguageModel.FragmentSpreadSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name.GetHashCode() * 397) ^ Directives.GetHashCode();
            }
        }
    }
}