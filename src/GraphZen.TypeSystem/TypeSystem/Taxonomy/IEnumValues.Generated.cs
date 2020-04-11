#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy {
public  partial interface IEnumValues {

        public EnumValue? FindValue(String name) 
            => Values.TryGetValue(Check.NotNull(name,nameof(name)), out var value) ? value : null;

        public bool HasValue(String name) 
            => Values.ContainsKey(Check.NotNull(name, nameof(name)));
        
        public EnumValue GetValue(String name) 
            => FindValue(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{this} does not contain a {nameof(EnumValue)} with name '{name}'.");


        public bool TryGetValue(String name, [NotNullWhen(true)] out EnumValue? enumValue)
             => Values.TryGetValue(Check.NotNull(name, nameof(name)), out enumValue);


        public EnumValue? FindValue(Object value) 
            => ValuesByValue.TryGetValue(Check.NotNull(value,nameof(value)), out var _value) ? _value : null;

        public bool HasValue(Object value) 
            => ValuesByValue.ContainsKey(Check.NotNull(value, nameof(value)));
        
        public EnumValue GetValue(Object value) 
            => FindValue(Check.NotNull(value, nameof(value))) ?? throw new Exception($"{this} does not contain a {nameof(EnumValue)} with value '{value}'.");


        public bool TryGetValue(Object value, [NotNullWhen(true)] out EnumValue? enumValue)
             => ValuesByValue.TryGetValue(Check.NotNull(value, nameof(value)), out enumValue);

}
}
