#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem {
public partial class EnumType {

      public EnumValue? FindValue(String Name) 
            => Values.TryGetValue(Check.NotNull(Name,nameof(Name)), out var NameValue) ? NameValue : null;

        public bool HasValue(String Name) 
            => Values.ContainsKey(Check.NotNull(Name, nameof(Name)));

        
        public EnumValue GetValue(String Name) 
            => FindValue(Check.NotNull(Name, nameof(Name))) ?? throw new Exception($"{this} does not contain a value named '{Name}'.");


        public bool TryGetValue(String Name, [NotNullWhen(true)] out EnumValue? enumValue)
             => Values.TryGetValue(Check.NotNull(Name, nameof(Name)), out enumValue);
 



      public EnumValue? FindValue(Object Value) 
            => ValuesByValue.TryGetValue(Check.NotNull(Value,nameof(Value)), out var ValueValue) ? ValueValue : null;

        public bool HasValue(Object Value) 
            => ValuesByValue.ContainsKey(Check.NotNull(Value, nameof(Value)));

        
        public EnumValue GetValue(Object Value) 
            => FindValue(Check.NotNull(Value, nameof(Value))) ?? throw new Exception($"{this} does not contain a value named '{Value}'.");


        public bool TryGetValue(Object Value, [NotNullWhen(true)] out EnumValue? enumValue)
             => ValuesByValue.TryGetValue(Check.NotNull(Value, nameof(Value)), out enumValue);
 


}
}
