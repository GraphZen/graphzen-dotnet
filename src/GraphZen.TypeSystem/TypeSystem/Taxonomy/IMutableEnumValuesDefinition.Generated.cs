#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem.Taxonomy
{
    public partial interface IMutableEnumValuesDefinition
    {
        #region DictionaryAccessorGenerator

        [GraphQLIgnore]
        public EnumValueDefinition? FindValue(string name)
            => Values.TryGetValue(Check.NotNull(name, nameof(name)), out var value) ? value : null;

        [GraphQLIgnore]
        public bool HasValue(string name)
            => Values.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public EnumValueDefinition GetValue(string name)
            => FindValue(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{this} does not contain a {nameof(EnumValueDefinition)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetValue(string name, [NotNullWhen(true)] out EnumValueDefinition? enumValueDefinition)
            => Values.TryGetValue(Check.NotNull(name, nameof(name)), out enumValueDefinition);

        #endregion
    }
}
// Source Hash Code: 10019741925656070017