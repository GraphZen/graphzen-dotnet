#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem {
public partial class EnumTypeDefinition {

        public EnumValueDefinition? FindValue(String name) 
            => Values.TryGetValue(Check.NotNull(name,nameof(name)), out var value) ? value : null;

        public bool HasValue(String name) 
            => Values.ContainsKey(Check.NotNull(name, nameof(name)));
        
        public EnumValueDefinition GetValue(String name) 
            => FindValue(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{this} does not contain a {nameof(EnumValueDefinition)} with name '{name}'.");


        public bool TryGetValue(String name, [NotNullWhen(true)] out EnumValueDefinition? enumValueDefinition)
             => Values.TryGetValue(Check.NotNull(name, nameof(name)), out enumValueDefinition);

}
}
