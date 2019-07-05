// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Object field
    ///     http://facebook.github.io/graphql/June2018/#ObjectField
    /// </summary>
    public partial class ObjectFieldSyntax : SyntaxNode
    {
        public ObjectFieldSyntax(NameSyntax name, ValueSyntax value, SyntaxLocation location = null) :
            base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Value = Check.NotNull(value, nameof(value));
        }


        /// <summary>
        ///     The name of the field.
        /// </summary>
        [NotNull]
        public NameSyntax Name { get; }


        /// <summary>
        ///     The value of the field.
        /// </summary>
        [NotNull]
        public ValueSyntax Value { get; }


        public override IEnumerable<SyntaxNode> Children
        {
            get
            {
                yield return Name;
                yield return Value;
            }
        }


        private bool Equals([NotNull] ObjectFieldSyntax other) => Name.Equals(other.Name) && Value.Equals(other.Value);

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

            return obj is ObjectFieldSyntax && Equals((ObjectFieldSyntax) obj);
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