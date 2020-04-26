// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     List type
    ///     http://facebook.github.io/graphql/June2018/#ListType
    /// </summary>
    public partial class ListTypeSyntax : NullableTypeSyntax
    {
        [GenFactory(typeof(SyntaxFactory))]
        public ListTypeSyntax(TypeSyntax type, SyntaxLocation? location = null) : base(location)
        {
            OfType = Check.NotNull(type, nameof(type));
        }

        /// <summary>
        ///     The type of object contained within the colleciton.
        /// </summary>

        public TypeSyntax OfType { get; }

        public override IEnumerable<SyntaxNode> Children
        {
            get { yield return OfType; }
        }

        private bool Equals(ListTypeSyntax other) => OfType.Equals(other.OfType);

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

            return obj is ListTypeSyntax && Equals((ListTypeSyntax)obj);
        }

        public override int GetHashCode() => OfType.GetHashCode();


        public override string ToString() => ToSyntaxString();
    }
}