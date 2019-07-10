// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Directive
    ///     http://facebook.github.io/graphql/June2018/#Directive
    /// </summary>
    public partial class DirectiveSyntax : SyntaxNode, IArgumentsContainerNode, INamedSyntax
    {
        public DirectiveSyntax(NameSyntax name,
            IReadOnlyList<ArgumentSyntax> arguments = null,
            SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Arguments = arguments ?? ArgumentSyntax.EmptyList.ToList().AsReadOnly();
        }

        public override IEnumerable<SyntaxNode> Children =>
            Name.ToEnumerable().Concat(Arguments);


        /// <summary>
        ///     Directive arguments. (Optional)
        /// </summary>
        public IReadOnlyList<ArgumentSyntax> Arguments { get; }

        /// <summary>
        ///     The name of the directive.
        /// </summary>
        public NameSyntax Name { get; }

        private bool Equals([NotNull] DirectiveSyntax other) =>
            Name.Equals(other.Name) && Arguments.SequenceEqual(other.Arguments);

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

            return obj is DirectiveSyntax && Equals((DirectiveSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name.GetHashCode() * 397) ^ Arguments.GetHashCode();
            }
        }
    }
}