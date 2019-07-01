// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Language
{
    /// <summary>
    ///     Enum value
    ///     http://facebook.github.io/graphql/June2018/#EnumValue
    /// </summary>
    public partial class EnumValueSyntax : ValueSyntax
    {
        public EnumValueSyntax(NameSyntax value) : base(Check.NotNull(value, nameof(value)).Location)
        {
            Value = value.Value;

            if (!IsValidValue(value.Value))
            {
                throw new ArgumentException(
                    $"Enum values cannot be 'true', 'false', or 'null'. Supplied value was: '{value.Value}'",
                    nameof(value));
            }
        }


        [NotNull]
        [ItemNotNull]
        private static string[] ProhibtedValues { get; } = {"true", "false", "null"};

        /// <summary>
        ///     The enum value.
        /// </summary>
        [NotNull]
        public string Value { get; }


        public override IEnumerable<SyntaxNode> Children => Enumerable.Empty<SyntaxNode>();


        internal static bool IsValidValue(string value) =>
            ProhibtedValues.All(v => !v.Equals(value));


        private bool Equals([NotNull] EnumValueSyntax other) => string.Equals(Value, other.Value);

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

            return obj is EnumValueSyntax && Equals((EnumValueSyntax) obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override object GetValue() => Value;

        public override string ToString() => Value;
    }
}