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
    ///     Interface type extension
    ///     http://facebook.github.io/graphql/June2018/#InterfaceTypeExtension
    /// </summary>
    public partial class InterfaceTypeExtensionSyntax : TypeExtensionSyntax, IDirectivesSyntax
    {
        [GenFactory(typeof(SyntaxFactory))]
        public InterfaceTypeExtensionSyntax(NameSyntax name, IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<FieldDefinitionSyntax>? fields = null, SyntaxLocation? location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Directives = directives ?? DirectiveSyntax.EmptyList;
            Fields = fields ?? FieldDefinitionSyntax.EmptyList;
        }

        public override NameSyntax Name { get; }


        public IReadOnlyList<FieldDefinitionSyntax> Fields { get; }

        public override IEnumerable<SyntaxNode> Children() => Name.ToEnumerable().Concat(Directives).Concat(Fields);

        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        private bool Equals(InterfaceTypeExtensionSyntax other) =>
            Name.Equals(other.Name) && Directives.SequenceEqual(other.Directives) &&
            Fields.SequenceEqual(other.Fields);

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

            return obj is InterfaceTypeExtensionSyntax && Equals((InterfaceTypeExtensionSyntax)obj);
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