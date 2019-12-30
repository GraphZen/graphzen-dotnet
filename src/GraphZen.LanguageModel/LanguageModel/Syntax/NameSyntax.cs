// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;




namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     A case sensitive name consisting of upper or lowercase letters, digits, and underscores.
    /// </summary>
    [DebuggerDisplay("Name={Value}")]
    public partial class NameSyntax : SyntaxNode
    {
        public NameSyntax(string value, SyntaxLocation? location = null) : base(location)
        {
            Value = Check.NotNull(value, nameof(value));
            if (!value.IsValidGraphQLName())
                throw new ArgumentException(
                    $"Error creating name '{value}': Names are limited to underscores and alpha-numeric ASCII characters.");
        }

        /// <summary>
        ///     Case sensitive name value.
        /// </summary>

        public string Value { get; }

        public override IEnumerable<SyntaxNode> Children => Enumerable.Empty<SyntaxNode>();

        public string GetDisplayValue() => Value;


        // public static implicit operator NameSyntax( string name) => new NameSyntax(name);

        private bool Equals(NameSyntax other) => string.Equals(Value, other.Value);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is NameSyntax syntax && Equals(syntax);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}