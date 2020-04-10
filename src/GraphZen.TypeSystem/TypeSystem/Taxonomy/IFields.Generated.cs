#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy {
public partial interface IFields {

      public Field? FindField(String name) 
            => Fields.TryGetValue(Check.NotNull(name,nameof(name)), out var nameField) ? nameField : null;

        public bool HasField(String name) 
            => Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public Field GetField(String name) 
            => FindField(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{this} does not contain a field named '{name}'.");


        public bool TryGetField(String name, [NotNullWhen(true)] out Field? field)
             => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out field);
 


}
}
