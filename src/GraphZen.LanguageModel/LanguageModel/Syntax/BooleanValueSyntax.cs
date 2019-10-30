// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Boolean value
    ///     http://facebook.github.io/graphql/June2018/#BooleanValue
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class BooleanValueSyntax : ValueSyntax
    {
        public BooleanValueSyntax(bool value, SyntaxLocation location = null) : base(location)
        {
            Value = value;
        }

        /// <summary>
        ///     The boolean value.
        /// </summary>
        public bool Value { get; }

        public override IEnumerable<SyntaxNode> Children => Enumerable.Empty<SyntaxNode>();

        public string GetDisplayValue() => Value.ToString();

        public override object GetValue() => Value;

        private bool Equals(BooleanValueSyntax other) => Value == other.Value;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is BooleanValueSyntax && Equals((BooleanValueSyntax)obj);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}