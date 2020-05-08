#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem.Taxonomy {
public  partial interface IInputFields {

        [GraphQLIgnore]
        public InputField? FindField(String name) 
            => Fields.TryGetValue(Check.NotNull(name,nameof(name)), out var field) ? field : null;

        [GraphQLIgnore]
        public bool HasField(String name) 
            => Fields.ContainsKey(Check.NotNull(name, nameof(name)));
        
        [GraphQLIgnore]
        public InputField GetField(String name) 
            => FindField(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{this} does not contain a {nameof(InputField)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetField(String name, [NotNullWhen(true)] out InputField? inputField)
             => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out inputField);

}
}
// Source Hash Code: 354785662