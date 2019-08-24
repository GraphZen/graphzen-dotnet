// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Union type extension
    ///     http://facebook.github.io/graphql/June2018/#UnionTypeExtension
    /// </summary>
    public partial class UnionTypeExtensionSyntax : TypeExtensionSyntax, IDirectivesSyntax
    {
        public UnionTypeExtensionSyntax(NameSyntax name, IReadOnlyList<DirectiveSyntax> directives = null,
            IReadOnlyList<NamedTypeSyntax> types = null, SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Directives = directives ?? DirectiveSyntax.EmptyList;
            Types = types ?? NamedTypeSyntax.EmptyList;
        }

        public override NameSyntax Name { get; }

        
        public IReadOnlyList<NamedTypeSyntax> Types { get; }


        public override IEnumerable<SyntaxNode> Children => Name.ToEnumerable().Concat(Directives).Concat(Types);

        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        private bool Equals( UnionTypeExtensionSyntax other) => Name.Equals(other.Name) &&
                                                                         Directives.SequenceEqual(other.Directives) &&
                                                                         Types.SequenceEqual(other.Types);

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

            return obj is UnionTypeExtensionSyntax && Equals((UnionTypeExtensionSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                hashCode = (hashCode * 397) ^ Types.GetHashCode();
                return hashCode;
            }
        }
    }
}