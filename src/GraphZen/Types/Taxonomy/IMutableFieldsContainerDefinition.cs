// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.Types.Builders;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    public interface IMutableFieldsContainerDefinition : IFieldsContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, FieldDefinition> Fields { get; }

        [NotNull]
        [ItemNotNull]
        new IEnumerable<FieldDefinition> GetFields();
    }
}