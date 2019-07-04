// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Field definition
    ///     http://facebook.github.io/graphql/June2018/#FieldDefinition
    /// </summary>
    public partial class FieldDefinitionSyntax : SyntaxNode, IDirectivesSyntax, IDescribedSyntax, INamedSyntax
    {
        public FieldDefinitionSyntax(
            NameSyntax name,
            TypeSyntax type, StringValueSyntax description = null,
            IReadOnlyList<LanguageModel.InputValueDefinitionSyntax> arguments = null,
            IReadOnlyList<DirectiveSyntax> directives = null, SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            FieldType = Check.NotNull(type, nameof(type));
            Description = description;
            Directives = directives ?? DirectiveSyntax.EmptyList;
            Arguments = arguments ?? LanguageModel.InputValueDefinitionSyntax.EmptyList;
        }

        /// <summary>
        ///     The type of the field.
        /// </summary>
        [NotNull]
        public TypeSyntax FieldType { get; }

        /// <summary>
        ///     Field arguments. (Optional)
        /// </summary>
        [NotNull]
        public IReadOnlyList<LanguageModel.InputValueDefinitionSyntax> Arguments { get; }

        public override IEnumerable<SyntaxNode> Children =>
            NodeExtensions.Concat(NodeExtensions.Concat((IEnumerable<SyntaxNode>) Name.ToEnumerable(), (IEnumerable<SyntaxNode>) Arguments)
                    .Concat(FieldType), (IEnumerable<SyntaxNode>) Directives);

        public StringValueSyntax Description { get; }

        /// <summary>
        ///     Field directives. (Optional)
        /// </summary>
        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        /// <summary>
        ///     The name of the field.
        /// </summary>
        public NameSyntax Name { get; }

        public string GetDisplayValue() => Name.Value;

        private bool Equals([NotNull] LanguageModel.FieldDefinitionSyntax other)
            => Name.Equals(other.Name) && Equals(Description, other.Description) &&
               FieldType.Equals(other.FieldType) && Arguments.SequenceEqual(other.Arguments) &&
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

            return obj is LanguageModel.FieldDefinitionSyntax && Equals((LanguageModel.FieldDefinitionSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ FieldType.GetHashCode();
                hashCode = (hashCode * 397) ^ Arguments.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString() => Name.ToString();
    }
}