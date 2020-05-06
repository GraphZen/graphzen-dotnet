// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem
{
    public partial class EnumTypeDefinition
    {
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
    }
}