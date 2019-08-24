// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableFieldsContainerDefinition : IFieldsContainerDefinition
    {
        
        IReadOnlyDictionary<string, FieldDefinition> Fields { get; }

        ConfigurationSource? FindIgnoredFieldConfigurationSource( string fieldName);
    }
}