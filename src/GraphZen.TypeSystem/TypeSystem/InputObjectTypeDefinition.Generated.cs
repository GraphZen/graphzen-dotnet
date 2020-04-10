// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public partial class InputObjectTypeDefinition
    {
        public InputFieldDefinition? FindField(string Name)
            => Fields.TryGetValue(Check.NotNull(Name, nameof(Name)), out var NameField) ? NameField : null;

        public bool HasField(string Name)
            => Fields.ContainsKey(Check.NotNull(Name, nameof(Name)));


        public InputFieldDefinition GetField(string Name)
            => FindField(Check.NotNull(Name, nameof(Name))) ??
               throw new Exception($"{this} does not contain a field named '{Name}'.");


        public bool TryGetField(string Name, [NotNullWhen(true)] out InputFieldDefinition? inputFieldDefinition)
            => Fields.TryGetValue(Check.NotNull(Name, nameof(Name)), out inputFieldDefinition);
    }
}