
using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Global

#nullable restore

namespace GraphZen.TypeSystem {


 public static partial class FieldDefinitionArgumentsAccessorExtensions {

        
        public static ArgumentDefinition? FindArgument( this FieldDefinition source, string name) 
            => source.Arguments.TryGetValue(Check.NotNull(name,nameof(name)), out var nameArgument) ? nameArgument : null;

        public static bool HasArgument( this FieldDefinition source,  string name) 
            => source.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static ArgumentDefinition GetArgument( this FieldDefinition source,  string name) 
            => source.FindArgument(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{source} does not contain a argument named '{name}'.");

        public static bool TryGetArgument( this FieldDefinition source,  string name, [NotNullWhen(true)] out ArgumentDefinition? argumentDefinition)
             => source.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);
}
 




 public static partial class DirectiveDefinitionArgumentsAccessorExtensions {

        
        public static ArgumentDefinition? FindArgument( this DirectiveDefinition source, string name) 
            => source.Arguments.TryGetValue(Check.NotNull(name,nameof(name)), out var nameArgument) ? nameArgument : null;

        public static bool HasArgument( this DirectiveDefinition source,  string name) 
            => source.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static ArgumentDefinition GetArgument( this DirectiveDefinition source,  string name) 
            => source.FindArgument(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{source} does not contain a argument named '{name}'.");

        public static bool TryGetArgument( this DirectiveDefinition source,  string name, [NotNullWhen(true)] out ArgumentDefinition? argumentDefinition)
             => source.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);
}
 




 public static partial class FieldArgumentsAccessorExtensions {

        
        public static Argument? FindArgument( this Field source, string name) 
            => source.Arguments.TryGetValue(Check.NotNull(name,nameof(name)), out var nameArgument) ? nameArgument : null;

        public static bool HasArgument( this Field source,  string name) 
            => source.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static Argument GetArgument( this Field source,  string name) 
            => source.FindArgument(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{source} does not contain a argument named '{name}'.");

        public static bool TryGetArgument( this Field source,  string name, [NotNullWhen(true)] out Argument? argument)
             => source.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);
}
 




 public static partial class IArgumentsArgumentsAccessorExtensions {

        
        public static Argument? FindArgument( this IArguments source, string name) 
            => source.Arguments.TryGetValue(Check.NotNull(name,nameof(name)), out var nameArgument) ? nameArgument : null;

        public static bool HasArgument( this IArguments source,  string name) 
            => source.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static Argument GetArgument( this IArguments source,  string name) 
            => source.FindArgument(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{source} does not contain a argument named '{name}'.");

        public static bool TryGetArgument( this IArguments source,  string name, [NotNullWhen(true)] out Argument? argument)
             => source.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);
}
 


}