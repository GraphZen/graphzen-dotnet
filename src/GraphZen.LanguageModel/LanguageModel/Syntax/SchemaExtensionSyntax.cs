#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Schema extension
    ///     http://facebook.github.io/graphql/June2018/#SchemaExtension
    /// </summary>
    public partial class SchemaExtensionSyntax : TypeSystemExtensionSyntax, IDirectivesSyntax
    {
        public SchemaExtensionSyntax(
            IReadOnlyList<DirectiveSyntax> directives = null,
            IReadOnlyList<OperationTypeDefinitionSyntax> operationTypes = null,
            SyntaxLocation location = null) : base(location)
        {
            Directives = directives ?? DirectiveSyntax.EmptyList;
            OperationTypes = operationTypes ?? OperationTypeDefinitionSyntax.EmptyList;
        }

        /// <summary>
        ///     Schema operation types.
        /// </summary>
        [NotNull]
        public IReadOnlyList<OperationTypeDefinitionSyntax> OperationTypes { get; }

        public override IEnumerable<SyntaxNode> Children =>
            Directives.Concat(OperationTypes);

        /// <summary>
        ///     Schema directives.
        /// </summary>
        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        private bool Equals([NotNull] SchemaExtensionSyntax other) =>
            OperationTypes.SequenceEqual(other.OperationTypes) && Directives.SequenceEqual(other.Directives);

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

            return obj is SchemaExtensionSyntax && Equals((SchemaExtensionSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (OperationTypes.GetHashCode() * 397) ^ Directives.GetHashCode();
            }
        }
    }
}