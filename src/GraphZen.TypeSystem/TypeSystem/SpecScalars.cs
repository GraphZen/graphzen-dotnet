// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public static class SpecScalars
    {
        // ReSharper disable once InconsistentNaming

        public static ScalarType ID { get; } = ScalarType.Create("ID", _ =>
            {
                _
                    .Description(SpecScalarSyntaxNodes.ID.Description?.Value)
                    .Serializer(value =>
                    {
                        if (value is string str) return Maybe.Some<object>(str);

                        if (value is int intVal) return Maybe.Some<object>(intVal.ToString());

                        throw new Exception($"ID cannot represent value: {value.Inspect()}");
                    })
                    .ValueParser(value =>
                    {
                        if (value is string str) return Maybe.Some<object>(str);

                        if (InternalNumerics.TryGetWholeDouble(value, out var wholeVal))
                            return Maybe.Some<object>(wholeVal.ToString(CultureInfo.InvariantCulture));

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
            }
        );


        public static ScalarType String { get; } = ScalarType.Create<string>(
            _ => _.Description(SpecScalarSyntaxNodes.String.Description?.Value)
                .ValueParser(value =>
                {
                    if (value is string str) return Maybe.Some<object>(str);

                    throw new Exception($"String cannot represent a non string value: {value.Inspect()}");
                })
                .LiteralParser(node =>
                    node is StringValueSyntax svn ? Maybe.Some<object>(svn.Value) : Maybe.None<object>())
                .Serializer(value =>
                {
                    if (value is string str) return Maybe.Some<object>(str);

                    if (value is bool boolean) return Maybe.Some<object>(boolean ? "true" : "false");

                    if (InternalNumerics.IsNumber(value)) return Maybe.Some<object>(value.ToString()!);

                    throw new Exception($"String cannot represent a non string value: {value}");
                })
        );


        public static ScalarType Int { get; } = ScalarType.Create<int>(_ =>
        {
            _
                .Description(SpecScalarSyntaxNodes.Int.Description?.Value)
                .Name("Int")
                .ValueParser(value =>
                {
                    if (InternalNumerics.TryGetWholeDouble(value, out var wholeNumber))
                    {
                        if (InternalNumerics.TryConvertToInt32(wholeNumber, out var intValue))
                            return Maybe.Some<object>(intValue);

                        throw new Exception($"Int cannot represent non 32-bit signed integer value: {value}");
                    }

                    throw new Exception($"Int cannot represent non-integer value: {value}");
                })
                .LiteralParser(
                    node => node is IntValueSyntax ivn ? Maybe.Some<object>(ivn.Value) : Maybe.None<object>())
                .Serializer(value =>

                {
                    if (value is bool boolean) return Maybe.Some<object>(boolean ? 1 : 0);


                    if (value is string str && str != "") return Maybe.Some<object>(Convert.ToInt32(value));

                    if (InternalNumerics.TryGetWholeDouble(value, out var wholeNumber))
                    {
                        if (InternalNumerics.TryConvertToInt32(wholeNumber, out var intValue))
                            return Maybe.Some<object>(intValue);

                        throw new Exception($"Int cannot represent non 32-bit signed integer value: {value}");
                    }

                    throw new Exception($"Int cannot represent non-integer value: {value}");
                });
        });


        public static ScalarType Float { get; } = ScalarType.Create<float>(_ =>
        {
            _
                .Description(SpecScalarSyntaxNodes.Float.Description?.Value)
                .Name("Float")
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
        });


        public static ScalarType Boolean { get; } = ScalarType.Create<bool>(_ =>
        {
            _.Description(SpecScalarSyntaxNodes.Boolean.Description?.Value)
                .ValueParser(val => Maybe.Some<object>(Convert.ToBoolean(val)))
                .LiteralParser(
                    node => node is BooleanValueSyntax bvn ? Maybe.Some<object>(bvn.Value) : Maybe.None<object>())
                .Serializer(value => Maybe.Some<object>(Convert.ToBoolean(value)));
        });


        public static IReadOnlyList<ScalarType> All { get; } = new List<ScalarType>
        {
            ID,
            String,
            Int,
            Float,
            Boolean
        }.ToImmutableList();
    }
}