// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
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
                throw new ArgumentException(
                    $"Enum values cannot be 'true', 'false', or 'null'. Supplied value was: '{value.Value}'",
                    nameof(value));
        }


        private static string[] ProhibtedValues { get; } = { "true", "false", "null" };

        /// <summary>
        ///     The enum value.
        /// </summary>

        public string Value { get; }


        public override IEnumerable<SyntaxNode> Children => Enumerable.Empty<SyntaxNode>();


        internal static bool IsValidValue(string value)
        {
            return ProhibtedValues.All(v => !v.Equals(value));
        }


        private bool Equals(EnumValueSyntax other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is EnumValueSyntax && Equals((EnumValueSyntax)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override object GetValue()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}