// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Float value
    ///     http://facebook.github.io/graphql/June2018/#FloatValue
    /// </summary>
    public partial class FloatValueSyntax : ValueSyntax
    {
        public FloatValueSyntax(string value, SyntaxLocation location = null) : base(location)
        {
            Value = Check.NotNull(value, nameof(value));
        }

        /// <summary>
        ///     The float value.
        /// </summary>
        [NotNull]
        public string Value { get; }


        public override IEnumerable<SyntaxNode> Children => Enumerable.Empty<SyntaxNode>();
        public string GetDisplayValue() => Value;

        private bool Equals([NotNull] FloatValueSyntax other) => Value.Equals(other.Value);

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

            return obj is FloatValueSyntax && Equals((FloatValueSyntax) obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override object GetValue() => Value;
    }
}