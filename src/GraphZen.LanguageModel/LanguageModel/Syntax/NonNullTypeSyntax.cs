// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Non-null type
    ///     http://facebook.github.io/graphql/June2018/#NonNullType
    /// </summary>
    public partial class NonNullTypeSyntax : TypeSyntax
    {
        public NonNullTypeSyntax(NullableTypeSyntax type, SyntaxLocation location = null) : base(location)
        {
            OfType = Check.NotNull(type, nameof(type));
        }

        /// <summary>
        ///     Inner type.
        /// </summary>
        [NotNull]
        public NullableTypeSyntax OfType { get; }

        public override IEnumerable<SyntaxNode> Children
        {
            get { yield return OfType; }
        }

        private bool Equals([NotNull] NonNullTypeSyntax other) => OfType.Equals(other.OfType);

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

            return obj is NonNullTypeSyntax && Equals((NonNullTypeSyntax) obj);
        }

        public override int GetHashCode() => OfType.GetHashCode();

        public override string ToString() => $"{OfType}!";
    }
}