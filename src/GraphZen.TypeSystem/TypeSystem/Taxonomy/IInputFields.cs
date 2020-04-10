// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public partial interface IInputFields : IInputFieldsDefinition
    {
        [GenDictionaryAccessors(nameof(InputField.Name), "Field")]
        IReadOnlyDictionary<string, InputField> Fields { get; }


        new IEnumerable<InputField> GetFields();
    }
}