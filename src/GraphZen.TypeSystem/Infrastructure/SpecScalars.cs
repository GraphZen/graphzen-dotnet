// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class SpecScalars
    {
        public static SchemaBuilder<TContext> AddSpecScalars<TContext>(this SchemaBuilder<TContext> schemaBuilder) where TContext : GraphQLContext
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
    }
}