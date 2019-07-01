// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Language
{
    /// <summary>
    ///     Null value
    ///     http://facebook.github.io/graphql/June2018/#NullValue
    /// </summary>
    public partial class NullValueSyntax : ValueSyntax
    {
        public NullValueSyntax(SyntaxLocation location = null) : base(location)
        {
        }

        public override IEnumerable<SyntaxNode> Children => Enumerable.Empty<SyntaxNode>();
        public string GetDisplayValue() => "null";

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

            return obj is NullValueSyntax;
        }

        public override int GetHashCode() => -1;

        [CanBeNull]
        public override object GetValue() => null;
    }
}