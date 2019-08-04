// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IInputObjectType : IInputObjectTypeDefinition, INamedType, IInputFieldsContainer
    {
    }

    public interface IInputFieldsContainerDefinition
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<IInputFieldDefinition> GetFields();

    }

    public interface IInputFieldsContainer : IInputFieldsContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, InputField> Fields { get; }

        [NotNull]
        [ItemNotNull]
        new IEnumerable<InputField> GetFields();
    }
    public interface IMutableInputFieldsContainerDefinition : IInputFieldsContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, InputFieldDefinition> Fields { get; }

        ConfigurationSource? FindIgnoredFieldConfigurationSource([NotNull] string fieldName);

        [NotNull]
        [ItemNotNull]
        new IEnumerable<InputFieldDefinition> GetFields();
    }

}