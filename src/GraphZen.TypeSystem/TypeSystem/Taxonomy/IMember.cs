// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IMember
    {
        internal string? GetName() => this is IName named ? named.Name : null;

        ISchema Schema { get; }


        internal IMember? GetParentMember() => this switch
        {
            IDirectiveDefinition directive => directive.Schema,
            INamedTypeDefinition type => type.Schema,
            IArgument argumentDefinition => argumentDefinition.DeclaringMember,
            IEnumValue enumValueDefinition => enumValueDefinition.DeclaringEnum,
            IField fieldDefinition when
            fieldDefinition.DeclaringType is IInterfaceType @interface => @interface,
            IField fieldDefinition when fieldDefinition.DeclaringType is IObjectType @object =>
            @object,
            IInputField inputFieldDefinition => inputFieldDefinition.DeclaringType,
            _ => null
        };
    }
}