// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     A variable.
    /// </summary>
    // ReSharper disable once UseNameofExpression
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class VariableSyntax : ValueSyntax
    {
        [GenFactory(nameof(SyntaxFactory))]
        public VariableSyntax(NameSyntax name, SyntaxLocation? location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
        }

        /// <summary>
        ///     The variable name.
        /// </summary>

        public NameSyntax Name { get; }

        public override IEnumerable<SyntaxNode> Children
        {
            get { yield return Name; }
        }


        private bool Equals(VariableSyntax other) => Name.Equals(other.Name);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is VariableSyntax && Equals((VariableSyntax)obj);
        }

        public override int GetHashCode() => Name.GetHashCode();

        public override object GetValue() => $"${Name.Value}";

        public override string ToString() => $"${Name}";
    }
}