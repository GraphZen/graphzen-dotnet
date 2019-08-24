#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableFieldDefinition : IFieldDefinition, IMutableAnnotatableDefinition,
        IMutableNamed,
        IMutableDescription,
        IMutableArgumentsContainerDefinition,
        IMutableDeprecation
    {
        [CanBeNull]
        new IGraphQLTypeReference FieldType { get; set; }

        [CanBeNull]
        new Resolver<object, object> Resolver { get; set; }

        [NotNull]
        new IMutableFieldsContainerDefinition DeclaringType { get; }
    }
}