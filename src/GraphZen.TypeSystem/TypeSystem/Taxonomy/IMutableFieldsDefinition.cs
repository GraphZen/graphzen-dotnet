// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IMutableFieldsDefinition : IFieldsDefinition
    {
        new IReadOnlyCollection<FieldDefinition> Fields { get; }

        [GenDictionaryAccessors(nameof(Field.Name), nameof(Field))]
        IReadOnlyDictionary<string, FieldDefinition> FieldMap { get; }
        FieldDefinition? GetOrAddField(string name, Type clrType, ConfigurationSource configurationSource);
        FieldDefinition? GetOrAddField(string name, string type, ConfigurationSource configurationSource);
        bool RemoveField(FieldDefinition field);
        bool AddField(FieldDefinition field);
        ConfigurationSource? FindIgnoredFieldConfigurationSource(string fieldName);
    }
}