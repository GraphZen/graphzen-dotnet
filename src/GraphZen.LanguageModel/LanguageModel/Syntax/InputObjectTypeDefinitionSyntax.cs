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
    ///     Input object type Definition
    ///     http://facebook.github.io/graphql/June2018/#InputObjectTypeDefinition
    /// </summary>
    public partial class InputObjectTypeDefinitionSyntax : TypeDefinitionSyntax, IDirectivesSyntax
    {
        [GenFactory(nameof(SyntaxFactory))]
        public InputObjectTypeDefinitionSyntax(
            NameSyntax name,
            StringValueSyntax? description = null,
            IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<InputValueDefinitionSyntax>? fields = null,
            SyntaxLocation? location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Description = description;
            Directives = directives ?? DirectiveSyntax.EmptyList;
            Fields = fields ?? InputValueDefinitionSyntax.EmptyList;
        }

        public override NameSyntax Name { get; }
        public override bool IsInputType { get; } = true;
        public override bool IsOutputType { get; } = false;


        public IReadOnlyList<InputValueDefinitionSyntax> Fields { get; }

        public override IEnumerable<SyntaxNode> Children =>
            Name.ToEnumerable().Concat(Directives).Concat(Fields);

        public override StringValueSyntax? Description { get; }

        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        private bool Equals(InputObjectTypeDefinitionSyntax other) =>
            Equals(Description, other.Description) && Name.Equals(other.Name) &&
            Fields.SequenceEqual(other.Fields) &&
            Directives.SequenceEqual(other.Directives);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is InputObjectTypeDefinitionSyntax && Equals((InputObjectTypeDefinitionSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Description != null ? Description.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Fields.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }
    }
}