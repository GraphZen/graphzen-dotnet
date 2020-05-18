// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    internal static class SchemaBuilderSpecExtensions
    {
        public static SchemaBuilder<TContext> AddSpecMembers<TContext>(this SchemaBuilder<TContext> schemaBuilder)
            where TContext : GraphQLContext => schemaBuilder.AddSpecScalars().AddSpecDirectives().AddIntrospectionTypes();

        private static SchemaBuilder<TContext> AddSpecScalars<TContext>(this SchemaBuilder<TContext> schemaBuilder)
            where TContext : GraphQLContext
        {
            schemaBuilder.Scalar("ID")
                .Description(SpecScalarSyntaxNodes.ID.Description?.Value!)
                .Serializer(value =>
                {
                    if (value is string str)
                    {
                        return Maybe.Some<object>(str);
                    }

                    if (value is int intVal)
                    {
                        return Maybe.Some<object>(intVal.ToString());
                    }

                    throw new Exception($"ID cannot represent value: {value.Inspect()}");
                })
                .ValueParser(value =>
                {
                    if (value is string str)
                    {
                        return Maybe.Some<object>(str);
                    }

                    if (InternalNumerics.TryGetWholeDouble(value, out var wholeVal))
                    {
                        return Maybe.Some<object>(wholeVal.ToString(CultureInfo.InvariantCulture));
                    }

                    throw new Exception($"ID cannot represent value: {value.Inspect()}");
                })
                .LiteralParser(node =>
                {
                    switch (node)
                    {
                        case StringValueSyntax stringValueNode:
                            return Maybe.Some<object>(stringValueNode.Value);
                        case IntValueSyntax intValueNode:
                            return Maybe.Some<object>(intValueNode.Value);
                        default:
                            return Maybe.None<object>();
                    }
                });

            schemaBuilder.Scalar<string>()
                .Name("String")
                .Description(SpecScalarSyntaxNodes.String.Description?.Value!)
                .ValueParser(value =>
                {
                    if (value is string str)
                    {
                        return Maybe.Some<object>(str);
                    }

                    throw new Exception($"String cannot represent a non string value: {value.Inspect()}");
                })
                .LiteralParser(node =>
                    node is StringValueSyntax svn ? Maybe.Some<object>(svn.Value) : Maybe.None<object>())
                .Serializer(value =>
                {
                    if (value is string str)
                    {
                        return Maybe.Some<object>(str);
                    }

                    if (value is bool boolean)
                    {
                        return Maybe.Some<object>(boolean ? "true" : "false");
                    }

                    if (InternalNumerics.IsNumber(value))
                    {
                        return Maybe.Some<object>(value.ToString()!);
                    }

                    throw new Exception($"String cannot represent a non string value: {value}");
                });

            schemaBuilder.Scalar<int>()
                .Description(SpecScalarSyntaxNodes.Int.Description?.Value!)
                .Name("Int")
                .ValueParser(value =>
                {
                    if (InternalNumerics.TryGetWholeDouble(value, out var wholeNumber))
                    {
                        if (InternalNumerics.TryConvertToInt32(wholeNumber, out var intValue))
                        {
                            return Maybe.Some<object>(intValue);
                        }

                        throw new Exception($"Int cannot represent non 32-bit signed integer value: {value}");
                    }

                    throw new Exception($"Int cannot represent non-integer value: {value}");
                })
                .LiteralParser(
                    node => node is IntValueSyntax ivn ? Maybe.Some<object>(ivn.Value) : Maybe.None<object>())
                .Serializer(value =>

                {
                    if (value is bool boolean)
                    {
                        return Maybe.Some<object>(boolean ? 1 : 0);
                    }


                    if (value is string str && str != "")
                    {
                        return Maybe.Some<object>(Convert.ToInt32(value));
                    }

                    if (InternalNumerics.TryGetWholeDouble(value, out var wholeNumber))
                    {
                        if (InternalNumerics.TryConvertToInt32(wholeNumber, out var intValue))
                        {
                            return Maybe.Some<object>(intValue);
                        }

                        throw new Exception($"Int cannot represent non 32-bit signed integer value: {value}");
                    }

                    throw new Exception($"Int cannot represent non-integer value: {value}");
                });


            schemaBuilder.Scalar("Float")
                .Description(SpecScalarSyntaxNodes.Float.Description?.Value!)
                .ClrType<float>()
                .Serializer(value =>
                {
                    try
                    {
                        return Maybe.Some<object>(Convert.ToDouble(value));
                    }
                    catch (InvalidCastException e)
                    {
                        throw new Exception($"Float cannot represent non-numeric value: {value.Inspect()}", e);
                    }
                })
                .ValueParser(value =>
                {
                    try
                    {
                        return Maybe.Some<object>(Convert.ToDouble(value));
                    }
                    catch (InvalidCastException e)
                    {
                        throw new Exception($"Float cannot represent non-numeric value: {value.Inspect()}", e);
                    }
                })
                .LiteralParser(node =>
                {
                    switch (node)
                    {
                        case FloatValueSyntax fvn:
                            return Maybe.Some<object>(Convert.ToDouble(fvn.Value));
                        case IntValueSyntax ivn:
                            return Maybe.Some<object>(Convert.ToDouble(ivn.Value));
                        default:
                            return Maybe.None<object>();
                    }
                });

            schemaBuilder.Scalar("Boolean").Description(SpecScalarSyntaxNodes.Boolean.Description?.Value!)
                .ValueParser(val => Maybe.Some<object>(Convert.ToBoolean(val)))
                .LiteralParser(
                    node => node is BooleanValueSyntax bvn ? Maybe.Some<object>(bvn.Value) : Maybe.None<object>())
                .Serializer(value => Maybe.Some<object>(Convert.ToBoolean(value)));

            return schemaBuilder;
        }

        private static SchemaBuilder<TContext> AddSpecDirectives<TContext>(this SchemaBuilder<TContext> schemaBuilder)
            where TContext : GraphQLContext
        {
            schemaBuilder.Directive("deprecated")
                .Description("Marks an element of a GraphQL schema as no longer supported.")
                .Locations(DirectiveLocation.EnumValue, DirectiveLocation.FieldDefinition)
                .Argument("reason", "String", reason =>
                {
                    reason.Description("Explains why this element was deprecated, usually also including a " +
                                       "suggestion for how to access supported similar data. Formatted " +
                                       "in [Markdown](https://daringfireball.net/projects/markdown/).")
                        .DefaultValue("No longer supported");
                });

            schemaBuilder.Directive("skip")
                .Description(
                    "'Directs the executor to skip this field or fragment only when the `if` argument is true.")
                .Locations(DirectiveLocation.Field, DirectiveLocation.FragmentSpread, DirectiveLocation.InlineFragment)
                .Argument<bool>("if", _ => _.Description("Skipped when true."));

            schemaBuilder.Directive("include")
                .Description(
                    "Directs the executor to include this field or fragment only when the `if` argument is true.")
                .Locations(DirectiveLocation.Field, DirectiveLocation.FragmentSpread, DirectiveLocation.InlineFragment)
                .Argument<bool>("if", _ => _.Description("Included when true."));

            return schemaBuilder;
        }

        private static SchemaBuilder<TContext> AddIntrospectionTypes<TContext>(this SchemaBuilder<TContext> sb)
            where TContext : GraphQLContext
        {
            sb.Object<IGraphQLType>()
                .Description("The fundamental unit of any GraphQL Schema is the type. There are " +
                             "many kinds of types in GraphQL as represented by the `__TypeKind` enum." +
                             "\n\nDepending on the kind of a type, certain fields describe " +
                             "information about that type. Scalar types provide no information " +
                             "beyond a name and description, while Enum types provide their values. " +
                             "Object and Interface types provide the fields they describe. Abstract " +
                             "types, Union and Interface, provide the Object types possible " +
                             "at runtime. List and NonNull types compose other types.', ")
                .Field(_ => _.Kind)
                .Field("name", "String", _ => { _.Resolve(type => type is NamedType named ? named.Name : null); }
                )
                .Field("description", "String", _ => _
                    .Resolve(type => type is NamedType named ? named.Description : null))
                .Field("fields", "[__Field!]", _ =>
                {
                    _.Argument("includeDeprecated", "Boolean", a => { a.DefaultValue(false); })
                        .Resolve((type, args) =>
                        {
                            if (type is IFields fieldsType)
                            {
                                var includeDeprecated = args.includeDeprecated == true;
                                return fieldsType.Fields.Values.Where(field =>
                                    includeDeprecated || !field.IsDeprecated);
                            }

                            return null;
                        });
                })
                .Field("interfaces", "[__Type!]", _ =>
                {
                    _.Resolve(type =>
                        type is ObjectType obj ? obj.Interfaces.Cast<IGraphQLTypeReference>().ToList() : null);
                })
                .Field("possibleTypes", "[__Type!]", _ => _.Resolve(
                    (source, args, context, info) => source is IAbstractType abstractType
                        ? info.Schema.GetPossibleTypes(abstractType).Cast<IGraphQLTypeReference>().ToList()
                        : null))
                .Field("enumValues", "[__EnumValue!]", _ => _
                    .Argument("includeDeprecated", "Boolean", a => a.DefaultValue(false))
                    .Resolve((type, args) =>
                    {
                        if (type is EnumType enumType)
                        {
                            return enumType.GetValues()
                                .Where(f => args.includeDeprecated || !f.IsDeprecated).ToList();
                        }

                        return null;
                    }))
                .Field("inputFields", "[__InputValue!]", _ => _
                    .Resolve(type =>
                        type is InputObjectType inputObject
                            ? inputObject.Fields.Cast<InputValue>().ToList()
                            : null))
                .Field("ofType", "__Type", _ => _
                    .Resolve(source => source is IWrappingType wrapping ? wrapping.OfType : null));


            sb.Object<Schema>()
                .Field<List<IGraphQLType>>("types",
                    _ =>
                    {
                        _.Resolve(s => s.Types.Values.Cast<IGraphQLType>().ToList())
                            .Description("A list of all types supported by this server.");
                    });

            sb.Object<Directive>()
                .Field<IEnumerable<InputValue>>("args", f => f.Resolve(d => d.GetArguments()));

            sb.Enum<DirectiveLocation>();

            sb.Object<Field>()
                .Field<IEnumerable<InputValue>>("args", f => f.Resolve(d => d.GetArguments()));

            sb.Object<InputValue>()
                .Field("defaultValue", "String", _ => _
                    .Resolve((input, args, context, info) =>
                    {
                        if (input.HasDefaultValue)
                        {
                            var ast = AstFromValue.Get(Maybe.Some(input.DefaultValue!), input.InputType);
                            return ast?.ToSyntaxString();
                        }

                        return null;
                    }));

            sb.Object<EnumValue>();

            sb.Enum<TypeKind>();
            return sb;
        }
    }
}