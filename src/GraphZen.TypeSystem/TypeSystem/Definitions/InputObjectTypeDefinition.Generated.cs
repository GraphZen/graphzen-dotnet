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
    public partial class InputObjectTypeDefinition
    {
        #region DictionaryAccessorGenerator

        [GraphQLIgnore]
        public InputFieldDefinition? FindField(string name)
            => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var field) ? field : null;

        [GraphQLIgnore]
        public bool HasField(string name)
            => Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public InputFieldDefinition GetField(string name)
            => FindField(Check.NotNull(name, nameof(name))) ??
               throw new ItemNotFoundException(
                   $"{this} does not contain a {nameof(InputFieldDefinition)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetField(string name, [NotNullWhen(true)] out InputFieldDefinition? inputFieldDefinition)
            => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out inputFieldDefinition);

        #endregion
    }
}
// Source Hash Code: 10185914240077353296