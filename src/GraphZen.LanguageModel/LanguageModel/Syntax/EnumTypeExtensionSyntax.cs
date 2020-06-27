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
    ///     Enum type extension
    ///     http://facebook.github.io/graphql/June2018/#EnumTypeExtension
    /// </summary>
    public partial class EnumTypeExtensionSyntax : TypeExtensionSyntax, IDirectivesSyntax
    {
        [GenFactory(typeof(SyntaxFactory))]
        public EnumTypeExtensionSyntax(
            NameSyntax name, IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<EnumValueDefinitionSyntax>? values = null, SyntaxLocation? location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Directives = directives ?? DirectiveSyntax.EmptyList;
            Values = values ?? EnumValueDefinitionSyntax.EmptyList;
        }


        /// <summary>
        ///     Enum name
        /// </summary>
        public override NameSyntax Name { get; }

        /// <summary>
        ///     Enum extension values
        /// </summary>

        public IReadOnlyList<EnumValueDefinitionSyntax> Values { get; }

        public override IEnumerable<SyntaxNode> Children() => Name.ToEnumerable().Concat(Directives).Concat(Values);

        /// <summary>
        ///     Enum extension directives
        /// </summary>
        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        private bool Equals(EnumTypeExtensionSyntax other) =>
            Name.Equals(other.Name) &&
            Directives.SequenceEqual(other.Directives) &&
            Values.SequenceEqual(other.Values);

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

            return obj is EnumTypeExtensionSyntax && Equals((EnumTypeExtensionSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                hashCode = (hashCode * 397) ^ Values.GetHashCode();
                return hashCode;
            }
        }
    }
}