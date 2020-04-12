// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public partial class DirectiveDefinition
    {
        [GraphQLIgnore]
        public ArgumentDefinition? FindArgument(string name)
            => Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var argument) ? argument : null;

        [GraphQLIgnore]
        public bool HasArgument(string name)
            => Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public ArgumentDefinition GetArgument(string name)
            => FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{this} does not contain a {nameof(ArgumentDefinition)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetArgument(string name, [NotNullWhen(true)] out ArgumentDefinition? argumentDefinition)
            => Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);
    }
}