// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Operation definition
    ///     http://facebook.github.io/graphql/June2018/#OperationDefinition
    /// </summary>
    public partial class OperationDefinitionSyntax : ExecutableDefinitionSyntax, IDirectivesSyntax
    {
        public OperationDefinitionSyntax(
            OperationType type,
            SelectionSetSyntax selectionSet,
            NameSyntax name = null,
            IReadOnlyList<VariableDefinitionSyntax> variableDefinitions = null,
            IReadOnlyList<DirectiveSyntax> directives = null,
            SyntaxLocation location = null) : base(location)
        {
            OperationType = type;
            SelectionSet = Check.NotNull(selectionSet, nameof(selectionSet));
            Name = name;
            VariableDefinitions = variableDefinitions ?? VariableDefinitionSyntax.EmptyList;
            Directives = directives ?? DirectiveSyntax.EmptyList;
        }

        /// <summary>
        ///     Data requested by the fetch operation.
        /// </summary>
        [NotNull]
        public SelectionSetSyntax SelectionSet { get; }

        /// <summary>
        ///     The type of the operation - either QueryType, MutationType, or Subscription.
        /// </summary>
        public OperationType OperationType { get; }

        /// <summary>
        ///     The name of the operation. (Optional)
        /// </summary>
        [CanBeNull]
        public NameSyntax Name { get; }

        /// <summary>
        ///     Operation variable definitions. (Optional)
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public IReadOnlyList<VariableDefinitionSyntax> VariableDefinitions { get; }

        public override IEnumerable<SyntaxNode> Children =>
            Name.ToEnumerable().Concat(VariableDefinitions).Concat(Directives).Concat(SelectionSet);


        /// <summary>
        ///     Directives for the operation. (Optional)
        /// </summary>
        public IReadOnlyList<DirectiveSyntax> Directives { get; }

        private bool Equals([NotNull] OperationDefinitionSyntax other) =>
            SelectionSet.Equals(other.SelectionSet) && OperationType == other.OperationType && Equals(Name, other.Name)
            && VariableDefinitions.SequenceEqual(other.VariableDefinitions)
            && Directives.SequenceEqual(other.Directives);

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

            return obj is OperationDefinitionSyntax && Equals((OperationDefinitionSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = SelectionSet.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) OperationType;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ VariableDefinitions.GetHashCode();
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }
    }
}