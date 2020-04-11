// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public partial interface IFields
    {
        public Field? FindField(string name)
            => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var field) ? field : null;

        public bool HasField(string name)
            => Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        public Field GetField(string name)
            => FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{this} does not contain a {nameof(Field)} with name '{name}'.");


        public bool TryGetField(string name, [NotNullWhen(true)] out Field? field)
            => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out field);
    }
}