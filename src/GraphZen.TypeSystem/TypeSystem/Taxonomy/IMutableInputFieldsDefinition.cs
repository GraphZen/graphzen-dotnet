// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableInputFieldsDefinition : IInputFieldsDefinition
    {
        [GenAccessorExtensions("Field")]
        IReadOnlyDictionary<string, InputFieldDefinition> Fields { get; }

        ConfigurationSource? FindIgnoredFieldConfigurationSource(string fieldName);


        new IEnumerable<InputFieldDefinition> GetFields();
    }
}