// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public partial interface IMutableInputFieldsDefinition
    {
        [GraphQLIgnore]
        public InputFieldDefinition? FindField(string name)
            => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var field) ? field : null;

        [GraphQLIgnore]
        public bool HasField(string name)
            => Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public InputFieldDefinition GetField(string name)
            => FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{this} does not contain a {nameof(InputFieldDefinition)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetField(string name, [NotNullWhen(true)] out InputFieldDefinition? inputFieldDefinition)
            => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out inputFieldDefinition);
    }
}