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
    ///     Enum type Definition
    ///     http://facebook.github.io/graphql/June2018/#EnumTypeDefinition
    /// </summary>
    public partial class EnumTypeDefinitionSyntax : TypeDefinitionSyntax, IDirectivesSyntax
    {
        public EnumTypeDefinitionSyntax(
            NameSyntax name,
            StringValueSyntax description = null,
            IReadOnlyList<DirectiveSyntax> directives = null,
            IReadOnlyList<EnumValueDefinitionSyntax> values = null,
            SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Description = description;
            Values = values ?? EnumValueDefinitionSyntax.EmptyList;
            Directives = directives ?? DirectiveSyntax.EmptyList;
        }

        /// <summary>
        ///     The name of the enum type.
        /// </summary>
        public override NameSyntax Name { get; }

        public override bool IsInputType { get; } = true;
        public override bool IsOutputType { get; } = true;


        /// <summary>
        ///     The values of the enum type.
        /// </summary>

        public IReadOnlyList<EnumValueDefinitionSyntax> Values { get; }


        public override IEnumerable<SyntaxNode> Children =>
            Name.ToEnumerable().Concat(Directives).Concat(Values);

        public override StringValueSyntax Description { get; }

        /// <summary>
        ///     Enum directives. (Optional)
        /// </summary>
        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        private bool Equals(EnumTypeDefinitionSyntax other) =>
            Name.Equals(other.Name) && Equals(Description, other.Description) &&
            Values.SequenceEqual(other.Values) &&
            Directives.SequenceEqual(other.Directives);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is EnumTypeDefinitionSyntax && Equals((EnumTypeDefinitionSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Values.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }
    }
}