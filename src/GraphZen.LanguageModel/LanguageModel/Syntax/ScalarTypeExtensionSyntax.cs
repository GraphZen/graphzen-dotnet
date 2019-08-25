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
    ///     Scalar type extension
    ///     http://facebook.github.io/graphql/June2018/#ScalarTypeExtension
    /// </summary>
    public partial class ScalarTypeExtensionSyntax : TypeExtensionSyntax, IDirectivesSyntax
    {
        public ScalarTypeExtensionSyntax(NameSyntax name, IReadOnlyList<DirectiveSyntax> directives,
            SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Directives = directives ?? DirectiveSyntax.EmptyList;
        }

        public override NameSyntax Name { get; }

        public override IEnumerable<SyntaxNode> Children => Name.ToEnumerable().Concat(Directives);

        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        private bool Equals(ScalarTypeExtensionSyntax other)
        {
            return Name.Equals(other.Name) && Directives.SequenceEqual(other.Directives);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is ScalarTypeExtensionSyntax && Equals((ScalarTypeExtensionSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name.GetHashCode() * 397) ^ Directives.GetHashCode();
            }
        }
    }
}