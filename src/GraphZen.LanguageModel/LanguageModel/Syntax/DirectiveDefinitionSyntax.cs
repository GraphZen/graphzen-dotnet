// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Directive Definition (type system)
    ///     http://facebook.github.io/graphql/June2018/#DirectiveDefinition
    /// </summary>
    public partial class DirectiveDefinitionSyntax : TypeSystemDefinitionSyntax, INamedSyntax, IDescribedSyntax
    {
        public DirectiveDefinitionSyntax(
            NameSyntax name,
            IReadOnlyList<NameSyntax> locations,
            StringValueSyntax description = null,
            IReadOnlyList<InputValueDefinitionSyntax> arguments = null,
            SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Description = description;
            Arguments = arguments ?? InputValueDefinitionSyntax.EmptyList;
            Locations = Check.NotNull(locations, nameof(locations));
        }

        /// <summary>
        ///     Directive arguments. (Optional)
        /// </summary>

        public IReadOnlyList<InputValueDefinitionSyntax> Arguments { get; }

        /// <summary>
        ///     Directive locations.
        /// </summary>

        public IReadOnlyList<NameSyntax> Locations { get; }

        public override IEnumerable<SyntaxNode> Children =>
            Name.ToEnumerable().Concat(Arguments)
                .Concat(Locations);

        public StringValueSyntax Description { get; }

        /// <summary>
        ///     The name of the directive.
        /// </summary>
        public NameSyntax Name { get; }

        private bool Equals(DirectiveDefinitionSyntax other)
        {
            return Name.Equals(other.Name) &&
                   Equals(Description, other.Description) &&
                   Arguments.SequenceEqual(other.Arguments) &&
                   Locations.SequenceEqual(other.Locations);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is DirectiveDefinitionSyntax && Equals((DirectiveDefinitionSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Arguments.GetHashCode();
                hashCode = (hashCode * 397) ^ Locations.GetHashCode();
                return hashCode;
            }
        }
    }
}