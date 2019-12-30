// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;




namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Union type definition
    ///     http://facebook.github.io/graphql/June2018/#UnionTypeDefinition
    /// </summary>
    public partial class UnionTypeDefinitionSyntax : TypeDefinitionSyntax, IDirectivesSyntax
    {
        public UnionTypeDefinitionSyntax(NameSyntax name,
            StringValueSyntax? description = null,
            IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<NamedTypeSyntax>? types = null,
            SyntaxLocation? location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Description = description;
            MemberTypes = types ?? NamedTypeSyntax.EmptyList;
            Directives = directives ?? DirectiveSyntax.EmptyList;
        }

        /// <summary>
        ///     The name of the union type.
        /// </summary>
        public override NameSyntax Name { get; }

        public override bool IsInputType { get; } = false;
        public override bool IsOutputType { get; } = true;


        public IReadOnlyList<NamedTypeSyntax> MemberTypes { get; }


        public override IEnumerable<SyntaxNode> Children =>
            Name.ToEnumerable().Concat(Directives).Concat(MemberTypes);

        public override StringValueSyntax? Description { get; }

        public IReadOnlyList<DirectiveSyntax> Directives { get; }


        private bool Equals(UnionTypeDefinitionSyntax other) =>
            Name.Equals(other.Name) && Equals(Description, other.Description) &&
            MemberTypes.SequenceEqual(other.MemberTypes) &&
            Directives.SequenceEqual(other.Directives);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is UnionTypeDefinitionSyntax && Equals((UnionTypeDefinitionSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ MemberTypes.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }
    }
}