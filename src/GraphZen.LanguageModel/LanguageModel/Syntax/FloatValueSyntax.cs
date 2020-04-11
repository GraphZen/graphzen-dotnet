// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Float value
    ///     http://facebook.github.io/graphql/June2018/#FloatValue
    /// </summary>
    public partial class FloatValueSyntax : ValueSyntax
    {
        [GenFactory(typeof(SyntaxFactory))]
        public FloatValueSyntax(string value, SyntaxLocation? location = null) : base(location)
        {
            Value = Check.NotNull(value, nameof(value));
        }

        /// <summary>
        ///     The float value.
        /// </summary>

        public string Value { get; }


        public override IEnumerable<SyntaxNode> Children => Enumerable.Empty<SyntaxNode>();

        public string GetDisplayValue() => Value;

        private bool Equals(FloatValueSyntax other) => Value.Equals(other.Value);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is FloatValueSyntax && Equals((FloatValueSyntax)obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override object GetValue() => Value;
    }
}