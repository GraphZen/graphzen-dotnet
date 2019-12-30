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
    ///     Enum value definition
    ///     http://facebook.github.io/graphql/June2018/#EnumValueDefinition
    /// </summary>
    public partial class EnumValueDefinitionSyntax : SyntaxNode, IDirectivesSyntax, IDescribedSyntax
    {
        public EnumValueDefinitionSyntax(EnumValueSyntax value,
            StringValueSyntax? description = null,
            IReadOnlyList<DirectiveSyntax>? directives = null,
            SyntaxLocation? location = null) : base(location)
        {
            Value = Check.NotNull(value, nameof(value));
            Description = description;
            Directives = directives ?? DirectiveSyntax.EmptyList;
        }

        /// <summary>
        ///     The enum type value.
        /// </summary>

        public EnumValueSyntax Value { get; }


        public override IEnumerable<SyntaxNode> Children =>
            Value.ToEnumerable().Concat(Directives);

        public StringValueSyntax? Description { get; }

        /// <summary>
        ///     Enum value directives.
        /// </summary>
        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        private bool Equals(EnumValueDefinitionSyntax other) =>
            Value.Equals(other.Value) && Equals(Description, other.Description) &&
            Directives.SequenceEqual(other.Directives);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is EnumValueDefinitionSyntax && Equals((EnumValueDefinitionSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Value.GetHashCode();
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString() => Value.ToString();
    }
}