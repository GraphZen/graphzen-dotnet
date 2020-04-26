// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     GraphQL schema definition
    ///     http://facebook.github.io/graphql/June2018/#SchemaDefinition
    /// </summary>
    public partial class SchemaDefinitionSyntax : TypeSystemDefinitionSyntax, IDirectivesSyntax
    {
        [GenFactory(typeof(SyntaxFactory))]
        public SchemaDefinitionSyntax(IReadOnlyList<OperationTypeDefinitionSyntax>? operationTypes = null,
            IReadOnlyList<DirectiveSyntax>? directives = null, SyntaxLocation? location = null) : base(location)
        {
            RootOperationTypes = operationTypes ?? OperationTypeDefinitionSyntax.EmptyList;
            Directives = directives ?? DirectiveSyntax.EmptyList;
        }

        /// <summary>
        ///     Schema operation types.
        /// </summary>


        public IReadOnlyList<OperationTypeDefinitionSyntax> RootOperationTypes { get; }

        public override IEnumerable<SyntaxNode> Children =>
            Directives.Concat(RootOperationTypes);

        /// <summary>
        ///     Schema directives.
        /// </summary>
        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        private bool Equals(SchemaDefinitionSyntax other) =>
            RootOperationTypes.SequenceEqual(other.RootOperationTypes) &&
            Directives.SequenceEqual(other.Directives);

        public SchemaDefinitionSyntax WithRootOperation(OperationTypeDefinitionSyntax definition)
        {
            return new SchemaDefinitionSyntax(RootOperationTypes.ToReadOnlyListWithMutations(_ =>
                {
                    Debug.Assert(_ != null, nameof(_) + " != null");
                    _.Add(definition);
                }),
                Directives);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is SchemaDefinitionSyntax && Equals((SchemaDefinitionSyntax)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (RootOperationTypes.GetHashCode() * 397) ^ Directives.GetHashCode();
            }
        }
    }
}