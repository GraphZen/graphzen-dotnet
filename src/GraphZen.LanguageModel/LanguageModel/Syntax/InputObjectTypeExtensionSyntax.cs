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
    ///     Input object extension
    ///     http://facebook.github.io/graphql/June2018/#InputObjectTypeExtension
    /// </summary>
    public partial class InputObjectTypeExtensionSyntax : TypeExtensionSyntax
    {
        [GenFactory(nameof(SyntaxFactory))]
        public InputObjectTypeExtensionSyntax(NameSyntax name,
            IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<InputValueDefinitionSyntax>? fields = null, SyntaxLocation? location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Directives = directives ?? DirectiveSyntax.EmptyList;
            Fields = fields ?? InputValueDefinitionSyntax.EmptyList;
        }

        public override NameSyntax Name { get; }


        public IReadOnlyList<DirectiveSyntax> Directives { get; }


        public IReadOnlyList<InputValueDefinitionSyntax> Fields { get; }

        public override IEnumerable<SyntaxNode> Children => Name.ToEnumerable().Concat(Directives).Concat(Fields);

        private bool Equals(InputObjectTypeExtensionSyntax other) =>
            Name.Equals(other.Name) && Directives.SequenceEqual(other.Directives) &&
            Fields.SequenceEqual(other.Fields);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is InputObjectTypeExtensionSyntax && Equals((InputObjectTypeExtensionSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                hashCode = (hashCode * 397) ^ Fields.GetHashCode();
                return hashCode;
            }
        }
    }
}