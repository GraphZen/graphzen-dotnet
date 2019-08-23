// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Argument name/value pair
    ///     http://facebook.github.io/graphql/June2018/#Argument
    /// </summary>
    // ReSharper disable once UseNameofExpression
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class ArgumentSyntax : SyntaxNode, IDescribedSyntax
    {
        public ArgumentSyntax(NameSyntax name, StringValueSyntax description, ValueSyntax value,
            SyntaxLocation location = null) : base(location)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(value, nameof(value));
            Description = description;
            Name = name;
            Value = value;
        }

        /// <summary>
        ///     The argument name.
        /// </summary>
        [NotNull]
        public NameSyntax Name { get; }


        /// <summary>
        ///     The argument value.
        /// </summary>
        [NotNull]
        public ValueSyntax Value { get; }


        /// <summary>
        ///     Arguments child nodes
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<SyntaxNode> Children
        {
            get
            {
                yield return Name;
                yield return Value;
            }
        }


        public StringValueSyntax Description { get; }

        private bool Equals([NotNull] ArgumentSyntax other) => Name.Equals(other.Name) && Value.Equals(other.Value);

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

            return obj is ArgumentSyntax && Equals((ArgumentSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name.GetHashCode() * 397) ^ Value.GetHashCode();
            }
        }
    }
}