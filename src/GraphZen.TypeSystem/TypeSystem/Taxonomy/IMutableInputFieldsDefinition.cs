// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public partial interface IMutableInputFieldsDefinition : IInputFieldsDefinition
    {
        [GenDictionaryAccessors(nameof(InputFieldDefinition.Name), "Field")]
        IReadOnlyDictionary<string, InputFieldDefinition> Fields { get; }
        bool RemoveField(InputFieldDefinition field);
        bool AddField(InputFieldDefinition field);
        ConfigurationSource? FindIgnoredFieldConfigurationSource(string fieldName);
        new IEnumerable<InputFieldDefinition> GetFields();
    }
}