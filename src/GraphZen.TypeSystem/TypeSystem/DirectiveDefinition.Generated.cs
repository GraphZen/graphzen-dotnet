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
        public Argument? FindArgument(string name)
            => ArgumentMap.TryGetValue(Check.NotNull(name, nameof(name)), out var argument) ? argument : null;

        [GraphQLIgnore]
        public bool HasArgument(string name)
            => ArgumentMap.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public Argument GetArgument(string name)
            => FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new ItemNotFoundException($"{this} does not contain a {nameof(Argument)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetArgument(string name, [NotNullWhen(true)] out Argument? argument)
            => ArgumentMap.TryGetValue(Check.NotNull(name, nameof(name)), out argument);

        #endregion
    }
}
// Source Hash Code: 3095676694873963515