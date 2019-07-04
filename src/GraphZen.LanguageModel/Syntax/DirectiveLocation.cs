// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Valid locations where directives may be used (type system)
    ///     http://facebook.github.io/graphql/June2018/#DirectiveLocation
    /// </summary>
    [GraphQLName("__DirectiveLocation")]
    public enum DirectiveLocation
    {
        [Description("Location adjacent to a query operation.")] [GraphQLName("QUERY")]
        Query,

        [Description("Location adjacent to a mutation operation.")] [GraphQLName("MUTATION")]
        Mutation,

        [Description("Location adjacent to a subscription operation.")] [GraphQLName("SUBSCRIPTION")]
        Subscription,

        [Description("Location adjacent to a field.")] [GraphQLName("FIELD")]
        Field,

        [Description("Location adjacent to a fragment definition.")] [GraphQLName("FRAGMENT_DEFINITION")]
        FragmentDefinition,

        [Description("Location adjacent to a fragment spread.")] [GraphQLName("FRAGMENT_SPREAD")]
        FragmentSpread,

        [Description("Location adjacent to an inline fragment.")] [GraphQLName("INLINE_FRAGMENT")]
        InlineFragment,

        [Description("Location adjacent to a schema definition.")] [GraphQLName("SCHEMA")]
        Schema,

        [Description("Location adjacent to a scalar definition.")] [GraphQLName("SCALAR")]
        Scalar,

        [Description("Location adjacent to an object type definition.")] [GraphQLName("OBJECT")]
        Object,

        [Description("Location adjacent to a field Definition.")] [GraphQLName("FIELD_DEFINITION")]
        FieldDefinition,

        [Description("Location adjacent to an argument definition.")] [GraphQLName("ARGUMENT_DEFINITION")]
        ArgumentDefinition,

        [Description("Location adjacent to an interface definition.")] [GraphQLName("INTERFACE")]
        Interface,

        [Description("Location adjacent to a union Definition.")] [GraphQLName("UNION")]
        Union,

        [Description("Location adjacent to an enum definition.")] [GraphQLName("ENUM")]
        Enum,

        [Description("Location adjacent to an enum value definition.")] [GraphQLName("ENUM_VALUE")]
        EnumValue,

        [Description("Location adjacent to an input object type Definition.")] [GraphQLName("INPUT_OBJECT")]
        InputObject,

        [Description("Location adjacent to an input object field definition.")] [GraphQLName("INPUT_FIELD_DEFINITION")]
        InputFieldDefinition
    }
}