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
    ///     String value
    ///     http://facebook.github.io/graphql/June2018/#StringValue
    /// </summary>
    public partial class StringValueSyntax : ValueSyntax
    {
        [GenFactory(typeof(SyntaxFactory))]
        public StringValueSyntax(string value, bool isBlockString = false,
            SyntaxLocation? location = null) :
            base(location)
        {
            Check.NotNull(value, nameof(value));
            IsBlockString = isBlockString;
            Value = IsBlockString ? LanguageHelpers.BlockStringValue(value) : value;
        }


        public bool IsBlockString { get; }

        /// <summary>
        ///     The string value.
        /// </summary>

        public string Value { get; }

        public override IEnumerable<SyntaxNode> Children() => Enumerable.Empty<SyntaxNode>();

        private bool Equals(StringValueSyntax other) =>
            IsBlockString == other.IsBlockString && string.Equals(Value, other.Value);

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

            return obj is StringValueSyntax && Equals((StringValueSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = IsBlockString.GetHashCode();
                hashCode = (hashCode * 397) ^ Value.GetHashCode();
                return hashCode;
            }
        }

        public override object GetValue() => Value;

        public override string ToString() => $"\"{Value}\"";
    }
}