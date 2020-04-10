#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy {
public partial interface IMutableArgumentsDefinition {

      public ArgumentDefinition? FindArgument(String Name) 
            => Arguments.TryGetValue(Check.NotNull(Name,nameof(Name)), out var NameArgument) ? NameArgument : null;

        public bool HasArgument(String Name) 
            => Arguments.ContainsKey(Check.NotNull(Name, nameof(Name)));

        
        public ArgumentDefinition GetArgument(String Name) 
            => FindArgument(Check.NotNull(Name, nameof(Name))) ?? throw new Exception($"{this} does not contain a argument named '{Name}'.");


        public bool TryGetArgument(String Name, [NotNullWhen(true)] out ArgumentDefinition? argumentDefinition)
             => Arguments.TryGetValue(Check.NotNull(Name, nameof(Name)), out argumentDefinition);
 


}
}
