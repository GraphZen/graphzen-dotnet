#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem.Taxonomy {
public  partial interface IMutableArgumentsDefinition {

        [GraphQLIgnore]
        public ArgumentDefinition? FindArgument(String name) 
            => Arguments.TryGetValue(Check.NotNull(name,nameof(name)), out var argument) ? argument : null;

        [GraphQLIgnore]
        public bool HasArgument(String name) 
            => Arguments.ContainsKey(Check.NotNull(name, nameof(name)));
        
        [GraphQLIgnore]
        public ArgumentDefinition GetArgument(String name) 
            => FindArgument(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{this} does not contain a {nameof(ArgumentDefinition)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetArgument(String name, [NotNullWhen(true)] out ArgumentDefinition? argumentDefinition)
             => Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);

}
}
// Source Hash Code: -607838276