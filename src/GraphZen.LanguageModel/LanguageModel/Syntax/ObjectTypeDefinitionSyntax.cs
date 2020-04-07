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
    ///     Object type definition
    ///     http://facebook.github.io/graphql/June2018/#ObjectTypeDefinition
    /// </summary>
    public partial class ObjectTypeDefinitionSyntax : TypeDefinitionSyntax, IDirectivesSyntax, IFieldsNode
    {
        [GenFactory(nameof(SyntaxFactory))]
        public ObjectTypeDefinitionSyntax(NameSyntax name,
            StringValueSyntax? description = null,
            IReadOnlyList<NamedTypeSyntax>? interfaces = null,
            IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<FieldDefinitionSyntax>? fields = null,
            SyntaxLocation? location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Fields = fields ?? FieldDefinitionSyntax.EmptyList;
            Description = description;
            Interfaces = interfaces ?? NamedTypeSyntax.EmptyList;
            Directives = directives ?? DirectiveSyntax.EmptyList;
        }


        /// <summary>
        ///     Interfaces implemented by the object type. (Optional)
        /// </summary>

        public IReadOnlyList<NamedTypeSyntax> Interfaces { get; }


        public override IEnumerable<SyntaxNode> Children =>
            Name.ToEnumerable().Concat(Interfaces).Concat(Directives).Concat(Fields);

        public override StringValueSyntax? Description { get; }

        public override bool IsInputType { get; } = false;
        public override bool IsOutputType { get; } = true;

        /// <summary>
        ///     Object type directives. (Optional)
        /// </summary>
        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        /// <summary>
        ///     The name of the object.
        /// </summary>
        public override NameSyntax Name { get; }


        public IReadOnlyList<FieldDefinitionSyntax> Fields { get; }


        private bool Equals(ObjectTypeDefinitionSyntax other) =>
            Name.Equals(other.Name) &&
            Fields.SequenceEqual(other.Fields) &&
            Equals(Description, other.Description) &&
            Interfaces.SequenceEqual(other.Interfaces) &&
            Directives.SequenceEqual(other.Directives);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is ObjectTypeDefinitionSyntax && Equals((ObjectTypeDefinitionSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Fields.GetHashCode();
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Interfaces.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString() => Name.ToString();
    }
}