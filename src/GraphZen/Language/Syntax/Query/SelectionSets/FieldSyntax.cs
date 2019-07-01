// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using GraphZen.Language.Internal;
using JetBrains.Annotations;

namespace GraphZen.Language
{
    /// <summary>
    ///     Field selection
    ///     http://facebook.github.io/graphql/June2018/#sec-Language.Fields
    /// </summary>
    public partial class FieldSyntax : SelectionSyntax, IArgumentsContainerNode
    {
        public FieldSyntax(NameSyntax name, SelectionSetSyntax selectionSet) : this(name, null, null, null,
            selectionSet)
        {
        }


        public FieldSyntax(NameSyntax name,
            NameSyntax alias = null,
            IReadOnlyList<ArgumentSyntax> arguments = null,
            IReadOnlyList<DirectiveSyntax> directives = null,
            SelectionSetSyntax selectionSet = null,
            SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Alias = alias;
            SelectionSet = selectionSet;
            Arguments = arguments ?? ArgumentSyntax.EmptyList.ToList();
            Directives = directives ?? DirectiveSyntax.EmptyList;
        }

        /// <summary>
        ///     The name of the requested field.
        /// </summary>
        [NotNull]
        public NameSyntax Name { get; }

        [NotNull]
        public string FieldEntryKey => Alias?.Value ?? Name.Value;

        /// <summary>
        ///     Additional child selections. (Optional)
        /// </summary>
        [CanBeNull]
        public SelectionSetSyntax SelectionSet { get; }

        /// <summary>
        ///     A user-defined alias for the requested field. (Optional)
        /// </summary>
        public NameSyntax Alias { get; }

        /// <summary>
        ///     Field directives.
        /// </summary>
        public override IReadOnlyList<DirectiveSyntax> Directives { get; }


        public override IEnumerable<SyntaxNode> Children =>
            Alias.ToEnumerable().Concat(Name).Concat(Arguments)
                .Concat(Directives)
                .Concat(SelectionSet);


        /// <summary>
        ///     Field arguments.
        /// </summary>
        public IReadOnlyList<ArgumentSyntax> Arguments { get; }

        private bool Equals([NotNull] FieldSyntax other) =>
            Name.Equals(other.Name)
            && Equals(SelectionSet, other.SelectionSet)
            && Equals(Alias, other.Alias)
            && Directives.SequenceEqual(other.Directives)
            && Arguments.SequenceEqual(other.Arguments);

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

            return obj is FieldSyntax && Equals((FieldSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ SelectionSet?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Alias != null ? Alias.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                hashCode = (hashCode * 397) ^ Arguments.GetHashCode();
                return hashCode;
            }
        }
    }
}