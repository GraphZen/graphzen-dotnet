#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming

namespace GraphZen.TypeSystem {
public  partial class InterfaceType {

        [GraphQLIgnore]
        public Field? FindField(String name) 
            => Fields.TryGetValue(Check.NotNull(name,nameof(name)), out var field) ? field : null;

        [GraphQLIgnore]
        public bool HasField(String name) 
            => Fields.ContainsKey(Check.NotNull(name, nameof(name)));
        
        [GraphQLIgnore]
        public Field GetField(String name) 
            => FindField(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{this} does not contain a {nameof(Field)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetField(String name, [NotNullWhen(true)] out Field? field)
             => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out field);

}
}
