// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System.ComponentModel;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    [GraphQLName("__TypeKind")]
    [Description("An enum describing what kind of type a given `__Type` is.")]
    public enum TypeKind
    {
        [Description("Indicates this type is a scalar.")]
        [GraphQLName("SCALAR")]
        Scalar,

        [Description("Indicates this type is an object fields` and `interfaces` are valid fields.")]
        [GraphQLName("OBJECT")]
        Object,

        [Description("Indicates this type is an interface. `fields` and `possibleTypes` are valid fields.")]
        [GraphQLName("INTERFACE")]
        Interface,

        [Description("Indicates this type is a union. `possibleTypes` is a valid field.")]
        [GraphQLName("UNION")]
        Union,

        [Description("Indicates this type is an enum. `enumValues` is a valid field.")]
        [GraphQLName("ENUM")]
        Enum,

        [Description("Indicates this type is an input object. `inputFields` is a valid field.")]
        [GraphQLName("INPUT_OBJECT")]
        InputObject,

        [Description("Indicates this type is a list. `ofType` is a valid field.")]
        [GraphQLName("LIST")]
        List,

        [Description("Indicates this type is a non-null. `ofType` is a valid field.")]
        [GraphQLName("NON_NULL")]
        NonNull
    }
}