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
    ///     Input object type Definition
    ///     http://facebook.github.io/graphql/June2018/#InputObjectTypeDefinition
    /// </summary>
    public partial class InputObjectTypeDefinitionSyntax : TypeDefinitionSyntax, IDirectivesSyntax
    {
        public InputObjectTypeDefinitionSyntax(
            NameSyntax name,
            StringValueSyntax description = null,
            IReadOnlyList<DirectiveSyntax> directives = null,
            IReadOnlyList<LanguageModel.InputValueDefinitionSyntax> fields = null,
            SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Description = description;
            Directives = directives ?? DirectiveSyntax.EmptyList;
            Fields = fields ?? LanguageModel.InputValueDefinitionSyntax.EmptyList;
        }

        public override NameSyntax Name { get; }
        public override bool IsInputType { get; } = true;
        public override bool IsOutputType { get; } = false;

        [NotNull]
        public IReadOnlyList<LanguageModel.InputValueDefinitionSyntax> Fields { get; }

        public override IEnumerable<SyntaxNode> Children =>
            NodeExtensions.Concat(NodeExtensions.Concat((IEnumerable<SyntaxNode>) Name.ToEnumerable(), (IEnumerable<SyntaxNode>) Directives), (IEnumerable<SyntaxNode>) Fields);

        public override StringValueSyntax Description { get; }

        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        private bool Equals([NotNull] LanguageModel.InputObjectTypeDefinitionSyntax other) =>
            Equals(Description, other.Description) && Name.Equals(other.Name) && Fields.SequenceEqual(other.Fields) &&
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

            return obj is LanguageModel.InputObjectTypeDefinitionSyntax && Equals((LanguageModel.InputObjectTypeDefinitionSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Description != null ? Description.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Fields.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }
    }
}