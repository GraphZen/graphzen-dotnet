// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Interface type definition
    ///     http://facebook.github.io/graphql/June2018/#InterfaceTypeDefinition
    /// </summary>
    public partial class InterfaceTypeDefinitionSyntax : TypeDefinitionSyntax, IDirectivesSyntax, IFieldsNode
    {
        public InterfaceTypeDefinitionSyntax(NameSyntax name,
            StringValueSyntax description = null,
            IReadOnlyList<DirectiveSyntax> directives = null,
            IReadOnlyList<FieldDefinitionSyntax> fields = null,
            SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Description = description;
            Directives = directives ?? DirectiveSyntax.EmptyList;
            Fields = fields ?? FieldDefinitionSyntax.EmptyList;
        }

        public override IEnumerable<SyntaxNode> Children => Name.ToEnumerable().Concat(Directives).Concat(Fields);

        public override StringValueSyntax Description { get; }

        public override bool IsInputType { get; } = false;
        public override bool IsOutputType { get; } = true;

        /// <summary>
        ///     Interface directives. (Optional)
        /// </summary>
        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        /// <summary>
        ///     The name of an interface.
        /// </summary>
        public override NameSyntax Name { get; }


        /// <summary>
        ///     The fields on an interface. (Optional)
        /// </summary>
        public IReadOnlyList<FieldDefinitionSyntax> Fields { get; }

        private bool Equals([NotNull] InterfaceTypeDefinitionSyntax other) =>
            Name.Equals(other.Name) && Equals(Description, other.Description) && Fields.SequenceEqual(other.Fields) &&
            Directives.SequenceEqual(other.Directives);

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

            return obj is InterfaceTypeDefinitionSyntax && Equals((InterfaceTypeDefinitionSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Fields.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }
    }
}