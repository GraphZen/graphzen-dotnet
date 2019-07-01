// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Language
{
    /// <summary>
    ///     Selection set
    ///     http://facebook.github.io/graphql/June2018/#SelectionSet
    /// </summary>
    public partial class SelectionSetSyntax : SyntaxNode
    {
        public SelectionSetSyntax(IReadOnlyList<SelectionSyntax> selections,
            SyntaxLocation location = null) : base(location)
        {
            Selections = Check.NotNull(selections, nameof(selections));
        }

        /// <summary>
        ///     The set of data requested by an operation.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public IReadOnlyList<SelectionSyntax> Selections { get; }

        public override IEnumerable<SyntaxNode> Children => Selections;

        public int Count => Selections.Count;

        public IEnumerator<SelectionSyntax> GetEnumerator() => Selections.GetEnumerator();
        private bool Equals([NotNull] SelectionSetSyntax other) => Selections.SequenceEqual(other.Selections);

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

            return obj is SelectionSetSyntax && Equals((SelectionSetSyntax) obj);
        }

        public override int GetHashCode() => Selections.GetHashCode();
    }
}