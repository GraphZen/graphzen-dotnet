// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Language
{
    /// <summary>
    ///     Operation type definition
    ///     http://facebook.github.io/graphql/June2018/#OperationTypeDefinition
    /// </summary>
    public partial class OperationTypeDefinitionSyntax : SyntaxNode
    {
        public OperationTypeDefinitionSyntax(OperationType operationType, NamedTypeSyntax type,
            SyntaxLocation location = null) : base(location)
        {
            OperationType = operationType;
            Type = Check.NotNull(type, nameof(type));
        }

        /// <summary>
        ///     The operation type:  query, mutation, or subscription.
        /// </summary>
        public OperationType OperationType { get; }

        /// <summary>
        ///     The name of the operation.
        /// </summary>
        [NotNull]
        public NamedTypeSyntax Type { get; }

        public override IEnumerable<SyntaxNode> Children
        {
            get { yield return Type; }
        }

        private bool Equals([NotNull] OperationTypeDefinitionSyntax other) =>
            OperationType == other.OperationType && Type.Equals(other.Type);

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

            return obj is OperationTypeDefinitionSyntax && Equals((OperationTypeDefinitionSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) OperationType * 397) ^ Type.GetHashCode();
            }
        }
    }
}