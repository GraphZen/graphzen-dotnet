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
    ///     List value
    ///     http://facebook.github.io/graphql/June2018/#ListValue
    /// </summary>
    public partial class ListValueSyntax : ValueSyntax
    {
        [GenFactory(nameof(SyntaxFactory)]
        public ListValueSyntax(IReadOnlyList<ValueSyntax> values, SyntaxLocation? location = null) : base(location)
        {
            Values = values ?? EmptyValuesCollection;
        }

        /// <summary>
        ///     The values contained within the list. (Optional)
        /// </summary>


        public IReadOnlyList<ValueSyntax> Values { get; }

        public override IEnumerable<SyntaxNode> Children => Values;

        private bool Equals(ListValueSyntax other) => Values.SequenceEqual(other.Values);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is ListValueSyntax && Equals((ListValueSyntax) obj);
        }

        public override int GetHashCode() => Values.GetHashCode();

        public override object GetValue() => Values;
    }
}