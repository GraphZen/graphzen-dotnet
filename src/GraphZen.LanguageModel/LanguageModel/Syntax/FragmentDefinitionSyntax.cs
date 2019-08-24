#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Fragment definition
    ///     http://facebook.github.io/graphql/June2018/#FragmentDefinition
    /// </summary>
    public partial class FragmentDefinitionSyntax : ExecutableDefinitionSyntax, IFragmentTypeConditionSyntax,
        IDirectivesSyntax
    {
        public FragmentDefinitionSyntax(NameSyntax name, NamedTypeSyntax typeCondition,
            SelectionSetSyntax selectionSet, IReadOnlyList<DirectiveSyntax> directives = null,
            SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            TypeCondition = Check.NotNull(typeCondition, nameof(typeCondition));
            SelectionSet = Check.NotNull(selectionSet, nameof(selectionSet));
            Directives = directives ?? DirectiveSyntax.EmptyList;
        }

        /// <summary>
        ///     The name of the fragement.
        /// </summary>
        [NotNull]
        public NameSyntax Name { get; }

        /// <summary>
        ///     The fragement selection set.
        /// </summary>
        [NotNull]
        public SelectionSetSyntax SelectionSet { get; }


        public override IEnumerable<SyntaxNode> Children =>
            Name.ToEnumerable().Concat(TypeCondition).Concat(Directives).Concat(SelectionSet);


        /// <summary>
        ///     Fragement directives.
        /// </summary>
        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        /// <summary>
        ///     The fragment the name applies to.
        /// </summary>
        [NotNull]
        public NamedTypeSyntax TypeCondition { get; }

        private bool Equals([NotNull] FragmentDefinitionSyntax other) => Name.Equals(other.Name) &&
                                                                         TypeCondition.Equals(other.TypeCondition) &&
                                                                         SelectionSet.Equals(other.SelectionSet) &&
                                                                         Directives.SequenceEqual(other.Directives);

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

            return obj is FragmentDefinitionSyntax && Equals((FragmentDefinitionSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ TypeCondition.GetHashCode();
                hashCode = (hashCode * 397) ^ SelectionSet.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }
    }
}