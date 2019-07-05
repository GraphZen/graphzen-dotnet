// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Object type extension
    ///     http://facebook.github.io/graphql/June2018/#ObjectTypeExtension
    /// </summary>
    public partial class ObjectTypeExtensionSyntax : TypeExtensionSyntax, IDirectivesSyntax
    {
        public ObjectTypeExtensionSyntax(NameSyntax name,
            IReadOnlyList<NamedTypeSyntax> interfaces = null, IReadOnlyList<DirectiveSyntax> directives = null,
            IReadOnlyList<FieldDefinitionSyntax> fields = null, SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Interfaces = interfaces ?? NamedTypeSyntax.EmptyList;
            Directives = directives ?? DirectiveSyntax.EmptyList;
            Fields = fields ?? FieldDefinitionSyntax.EmptyList;
        }

        public override NameSyntax Name { get; }

        [NotNull]
        public IReadOnlyList<FieldDefinitionSyntax> Fields { get; }

        [NotNull]
        public IReadOnlyList<NamedTypeSyntax> Interfaces { get; }

        public override IEnumerable<SyntaxNode> Children =>
            Name.ToEnumerable().Concat(Interfaces).Concat(Directives).Concat(Fields);

        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        private bool Equals([NotNull] ObjectTypeExtensionSyntax other) =>
            Name.Equals(other.Name) && Fields.SequenceEqual(other.Fields) &&
            Directives.SequenceEqual(other.Directives) &&
            Interfaces.SequenceEqual(other.Interfaces);

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

            return obj is ObjectTypeExtensionSyntax && Equals((ObjectTypeExtensionSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Fields.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                hashCode = (hashCode * 397) ^ Interfaces.GetHashCode();
                return hashCode;
            }
        }
    }
}