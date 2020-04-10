#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy {
public partial interface IInputFields {

        public InputField? FindField(String name) 
            => Fields.TryGetValue(Check.NotNull(name,nameof(name)), out var f) ? f : null;

        public bool HasField(String name) 
            => Fields.ContainsKey(Check.NotNull(name, nameof(name)));
        
        public InputField GetField(String name) 
            => FindField(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{this} does not contain a {nameof(InputField)} with name '{name}'.");


        public bool TryGetField(String name, [NotNullWhen(true)] out InputField? inputField)
             => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out inputField);

}
}
