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
    public partial class DirectiveDefinition
    {
        #region DictionaryAccessorGenerator

        [GraphQLIgnore]
        public ArgumentDefinition? FindArgument(string name)
            => Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var argument) ? argument : null;

        [GraphQLIgnore]
        public bool HasArgument(string name)
            => Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public ArgumentDefinition GetArgument(string name)
            => FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new ItemNotFoundException(
                   $"{this} does not contain a {nameof(ArgumentDefinition)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetArgument(string name, [NotNullWhen(true)] out ArgumentDefinition? argumentDefinition)
            => Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);

        #endregion
    }
}
// Source Hash Code: 17592297022856464992