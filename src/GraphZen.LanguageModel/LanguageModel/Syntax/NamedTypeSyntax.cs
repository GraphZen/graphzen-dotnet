// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     A named type within the type system.
    ///     http://facebook.github.io/graphql/June2018/#NamedType
    /// </summary>
    public partial class NamedTypeSyntax : NullableTypeSyntax, INamedSyntax
    {
        public NamedTypeSyntax(NameSyntax name, SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
        }

        public override IEnumerable<SyntaxNode> Children
        {
            get { yield return Name; }
        }

        /// <summary>
        ///     The name of the type.
        /// </summary>
        public NameSyntax Name { get; }

        public string GetDisplayValue() => ToSyntaxString();

        private bool Equals([NotNull] NamedTypeSyntax other) => Equals(Name, other.Name);

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

            return obj is NamedTypeSyntax && Equals((NamedTypeSyntax) obj);
        }

        public override int GetHashCode() => Name.GetHashCode();

        public override string ToString() => Name.ToString();
    }
}