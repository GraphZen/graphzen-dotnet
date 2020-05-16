#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem.Taxonomy
{
    public partial interface IArguments
    {
        #region DictionaryAccessorGenerator

        [GraphQLIgnore]
        public Argument? FindArgument(string name)
            => Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var argument) ? argument : null;

        [GraphQLIgnore]
        public bool HasArgument(string name)
            => Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public Argument GetArgument(string name)
            => FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{this} does not contain a {nameof(Argument)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetArgument(string name, [NotNullWhen(true)] out Argument? argument)
            => Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);

        #endregion
    }
}
// Source Hash Code: 12705553750028283125