#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy {
public partial interface IEnumValues {

      public EnumValue? FindValue(String name) 
            => Values.TryGetValue(Check.NotNull(name,nameof(name)), out var nameValue) ? nameValue : null;

        public bool HasValue(String name) 
            => Values.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public EnumValue GetValue(String name) 
            => FindValue(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{this} does not contain a value named '{name}'.");


        public bool TryGetValue(String name, [NotNullWhen(true)] out EnumValue? enumValue)
             => Values.TryGetValue(Check.NotNull(name, nameof(name)), out enumValue);
 


}
}
