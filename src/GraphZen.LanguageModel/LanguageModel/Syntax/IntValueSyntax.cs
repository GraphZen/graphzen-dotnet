// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Int value
    ///     http://facebook.github.io/graphql/June2018/#IntValue
    /// </summary>
    public partial class IntValueSyntax : ValueSyntax
    {
        public IntValueSyntax(int value, SyntaxLocation location = null) : base(location)
        {
            Value = value;
        }

        /// <summary>
        ///     The integer value.
        /// </summary>
        public int Value { get; }

        public override IEnumerable<SyntaxNode> Children => Enumerable.Empty<SyntaxNode>();

        public string GetDisplayValue() => Value.ToString();

        private bool Equals(IntValueSyntax other) => Value == other.Value;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is IntValueSyntax && Equals((IntValueSyntax) obj);
        }

        public override int GetHashCode() => Value.GetHashCode();


        public override object GetValue() => Value;
    }
}