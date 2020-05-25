// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem
{
    public partial class EnumType
    {
        #region DictionaryAccessorGenerator

        [GraphQLIgnore]
        public EnumValue? FindValue(string name)
            => Values.TryGetValue(Check.NotNull(name, nameof(name)), out var value) ? value : null;

        [GraphQLIgnore]
        public bool HasValue(string name)
            => Values.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public EnumValue GetValue(string name)
            => FindValue(Check.NotNull(name, nameof(name))) ??
               throw new ItemNotFoundException($"{this} does not contain a {nameof(EnumValue)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetValue(string name, [NotNullWhen(true)] out EnumValue? enumValue)
            => Values.TryGetValue(Check.NotNull(name, nameof(name)), out enumValue);

        #endregion

        #region DictionaryAccessorGenerator

        [GraphQLIgnore]
        public EnumValue? FindValue(object value)
            => ValuesByValue.TryGetValue(Check.NotNull(value, nameof(value)), out var schemaBuildervalue)
                ? schemaBuildervalue
                : null;

        [GraphQLIgnore]
        public bool HasValue(object value)
            => ValuesByValue.ContainsKey(Check.NotNull(value, nameof(value)));

        [GraphQLIgnore]
        public EnumValue GetValue(object value)
            => FindValue(Check.NotNull(value, nameof(value))) ??
               throw new ItemNotFoundException($"{this} does not contain a {nameof(EnumValue)} with value '{value}'.");

        [GraphQLIgnore]
        public bool TryGetValue(object value, [NotNullWhen(true)] out EnumValue? enumValue)
            => ValuesByValue.TryGetValue(Check.NotNull(value, nameof(value)), out enumValue);

        #endregion
    }
}
// Source Hash Code: 11283436566665434552