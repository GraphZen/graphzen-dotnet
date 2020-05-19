#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem {
public  partial class FieldsDefinition {
#region DictionaryAccessorGenerator



        [GraphQLIgnore]
        public FieldDefinition? FindField(String name) 
            => Fields.TryGetValue(Check.NotNull(name,nameof(name)), out var field) ? field : null;

        [GraphQLIgnore]
        public bool HasField(String name) 
            => Fields.ContainsKey(Check.NotNull(name, nameof(name)));
        
        [GraphQLIgnore]
        public FieldDefinition GetField(String name) 
            => FindField(Check.NotNull(name, nameof(name))) ?? throw new Exception($"{this} does not contain a {nameof(FieldDefinition)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetField(String name, [NotNullWhen(true)] out FieldDefinition? fieldDefinition)
             => Fields.TryGetValue(Check.NotNull(name, nameof(name)), out fieldDefinition);


#endregion
}
}
// Source Hash Code: 11982565525102511392