// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.TypeSystem.Taxonomy;

public interface IMutableInputFieldsDefinition : IInputFieldsDefinition
{
    IReadOnlyDictionary<string, InputFieldDefinition> Fields { get; }

    ConfigurationSource? FindIgnoredFieldConfigurationSource(string fieldName);


    new IEnumerable<InputFieldDefinition> GetFields();
}
