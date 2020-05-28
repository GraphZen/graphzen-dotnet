// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMemberDefinition
    {
        internal IMemberDefinition? GetParentMember()
        {
            return this switch
            {
                IArgumentDefinition argumentDefinition => argumentDefinition.DeclaringMember,
                IEnumValueDefinition enumValueDefinition => enumValueDefinition.DeclaringType,
                IFieldDefinition fieldDefinition when
                fieldDefinition.DeclaringType is IInterfaceTypeDefinition @interface => @interface,
                IFieldDefinition fieldDefinition when fieldDefinition.DeclaringType is IObjectTypeDefinition @object =>
                @object,
                IInputFieldDefinition inputFieldDefinition => inputFieldDefinition.DeclaringType,
                _ => null
            };
        }
    }
}