#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy {
public partial interface IArguments {

      public Argument? FindArgument(String name) 
            => Arguments.TryGetValue(Check.NotNull(name,nameof(name)), out var nameArgument) ? nameArgument : null;

        public bool HasArgument(String name) 
            => Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public Argument GetArgument(String name) 
            => FindArgument(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{this} does not contain a argument named '{name}'.");


        public bool TryGetArgument(String name, [NotNullWhen(true)] out Argument? argument)
             => Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);
 


}
}
