#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy {
public partial interface IMutableInputFieldsDefinition {

      public InputFieldDefinition? FindField(String Name) 
            => Fields.TryGetValue(Check.NotNull(Name,nameof(Name)), out var NameField) ? NameField : null;

        public bool HasField(String Name) 
            => Fields.ContainsKey(Check.NotNull(Name, nameof(Name)));

        
        public InputFieldDefinition GetField(String Name) 
            => FindField(Check.NotNull(Name, nameof(Name))) ?? throw new Exception($"{this} does not contain a field named '{Name}'.");


        public bool TryGetField(String Name, [NotNullWhen(true)] out InputFieldDefinition? inputFieldDefinition)
             => Fields.TryGetValue(Check.NotNull(Name, nameof(Name)), out inputFieldDefinition);
 


}
}
