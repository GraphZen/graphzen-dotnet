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
    public partial class MutableInputObjectType
    {
        #region DictionaryAccessorGenerator

        [GraphQLIgnore]
        public MutableInputField? FindField(string name)
            => FieldMap.TryGetValue(Check.NotNull(name, nameof(name)), out var field) ? field : null;

        [GraphQLIgnore]
        public bool HasField(string name)
            => FieldMap.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public MutableInputField GetField(string name)
            => FindField(Check.NotNull(name, nameof(name))) ??
               throw new ItemNotFoundException(
                   $"{this} does not contain a {nameof(MutableInputField)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetField(string name, [NotNullWhen(true)] out MutableInputField? inputFieldDefinition)
            => FieldMap.TryGetValue(Check.NotNull(name, nameof(name)), out inputFieldDefinition);

        #endregion
    }
}
// Source Hash Code: 5030709039319470940