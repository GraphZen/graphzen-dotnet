// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Scalar type definition
    ///     http://facebook.github.io/graphql/June2018/#ScalarTypeDefinition
    /// </summary>
    public partial class ScalarTypeDefinitionSyntax : TypeDefinitionSyntax
    {
        public ScalarTypeDefinitionSyntax(
            NameSyntax name,
            StringValueSyntax description = null,
            IReadOnlyList<DirectiveSyntax> directives = null,
            SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Description = description;
            Directives = directives ?? DirectiveSyntax.EmptyList;
        }

        /// <summary>
        ///     The name of the scalar type.
        /// </summary>
        public override NameSyntax Name { get; }

        public override bool IsInputType { get; } = true;
        public override bool IsOutputType { get; } = true;


        /// <summary>
        ///     Scalar type directives. (Optional)
        /// </summary>

        public IReadOnlyList<DirectiveSyntax> Directives { get; }


        public override IEnumerable<SyntaxNode> Children =>
            Name.ToEnumerable().Concat(Directives);

        public override StringValueSyntax Description { get; }

        private bool Equals(ScalarTypeDefinitionSyntax other)
        {
            return Name.Equals(other.Name) && Equals(Description, other.Description) &&
                   Directives.SequenceEqual(other.Directives);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is ScalarTypeDefinitionSyntax && Equals((ScalarTypeDefinitionSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }
    }
}