// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem.Taxonomy
{
    public partial interface IInputFields
    {
        #region DictionaryAccessorGenerator

        [GraphQLIgnore]
        public InputField? FindField(string name)
            => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var field) ? field : null;

        [GraphQLIgnore]
        public bool HasField(string name)
            => Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public InputField GetField(string name)
            => FindField(Check.NotNull(name, nameof(name))) ??
               throw new ItemNotFoundException($"{this} does not contain a {nameof(InputField)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetField(string name, [NotNullWhen(true)] out InputField? inputField)
            => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out inputField);

        #endregion
    }
}
// Source Hash Code: 14373871271445884014