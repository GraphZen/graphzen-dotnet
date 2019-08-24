// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableInputFieldsContainerDefinition : IInputFieldsContainerDefinition
    {
        
        IReadOnlyDictionary<string, InputFieldDefinition> Fields { get; }

        ConfigurationSource? FindIgnoredFieldConfigurationSource( string fieldName);

        
        
        new IEnumerable<InputFieldDefinition> GetFields();
    }
}