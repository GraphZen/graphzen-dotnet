#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem {
public partial class EnumTypeDefinition {

      public EnumValueDefinition? FindValue(String Name) 
            => Values.TryGetValue(Check.NotNull(Name,nameof(Name)), out var NameValue) ? NameValue : null;

        public bool HasValue(String Name) 
            => Values.ContainsKey(Check.NotNull(Name, nameof(Name)));

        
        public EnumValueDefinition GetValue(String Name) 
            => FindValue(Check.NotNull(Name, nameof(Name))) ?? throw new Exception($"{this} does not contain a value named '{Name}'.");


        public bool TryGetValue(String Name, [NotNullWhen(true)] out EnumValueDefinition? enumValueDefinition)
             => Values.TryGetValue(Check.NotNull(Name, nameof(Name)), out enumValueDefinition);
 


}
}
