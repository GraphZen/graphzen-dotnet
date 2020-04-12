// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public partial interface IEnumValues
    {
        [GraphQLIgnore]
        public EnumValue? FindValue(string name)
            => Values.TryGetValue(Check.NotNull(name, nameof(name)), out var value) ? value : null;

        [GraphQLIgnore]
        public bool HasValue(string name)
            => Values.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public EnumValue GetValue(string name)
            => FindValue(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{this} does not contain a {nameof(EnumValue)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetValue(string name, [NotNullWhen(true)] out EnumValue? enumValue)
            => Values.TryGetValue(Check.NotNull(name, nameof(name)), out enumValue);


        [GraphQLIgnore]
        public EnumValue? FindValue(object value)
            => ValuesByValue.TryGetValue(Check.NotNull(value, nameof(value)), out var _value) ? _value : null;

        [GraphQLIgnore]
        public bool HasValue(object value)
            => ValuesByValue.ContainsKey(Check.NotNull(value, nameof(value)));

        [GraphQLIgnore]
        public EnumValue GetValue(object value)
            => FindValue(Check.NotNull(value, nameof(value))) ??
               throw new Exception($"{this} does not contain a {nameof(EnumValue)} with value '{value}'.");

        [GraphQLIgnore]
        public bool TryGetValue(object value, [NotNullWhen(true)] out EnumValue? enumValue)
            => ValuesByValue.TryGetValue(Check.NotNull(value, nameof(value)), out enumValue);
    }
}