#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IField : IFieldDefinition, IArgumentsContainer, ISyntaxConvertable
    {
        [NotNull]
        new IGraphQLType FieldType { get; }

        [CanBeNull]
        new IFieldsContainer DeclaringType { get; }
    }
}