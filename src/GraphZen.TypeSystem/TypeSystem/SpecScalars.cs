// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;


// ReSharper disable PossibleNullReferenceException

namespace GraphZen.TypeSystem
{
    public static class SpecScalars
    {
        // ReSharper disable once InconsistentNaming
        [NotNull]
        public static ScalarType ID { get; } = ScalarType.Create("ID", _ =>
            {
                _
                    .Description("The `ID` scalar type represents a unique identifier, often used to " +
                                 "refetch an object or as key for a cache. The ID type appears in a JSON " +
                                 "response as a String; however, it is not intended to be human-readable. " +
                                 "When expected as an input type, any string (such as `\"4\"`) or integer " +
                                 "(such as `4`) input value will be accepted as an ID.")
                    .Serializer(value =>
                    {
                        if (value is string str)
                        {
                            return Maybe.Maybe.Some<object>(str);
                        }

                        if (value is int intVal)
                        {
                            return Maybe.Maybe.Some<object>(intVal.ToString());
                        }

                        throw new Exception($"ID cannot represent value: {value.Inspect()}");
                    })
                    .ValueParser(value =>
                    {
                        if (value is string str)
                        {
                            return Maybe.Maybe.Some<object>(str);
                        }

                        if (InternalNumerics.TryGetWholeDouble(value, out var wholeVal))
                        {
                            return Maybe.Maybe.Some<object>(wholeVal.ToString(CultureInfo.InvariantCulture));
                        }

                        throw new Exception($"ID cannot represent value: {value.Inspect()}");
                    })
                    .LiteralParser(node =>
                    {
                        switch (node)
                        {
                            case StringValueSyntax stringValueNode:
                                return Maybe.Maybe.Some<object>(stringValueNode.Value);
                            case IntValueSyntax intValueNode:
                                return Maybe.Maybe.Some<object>(intValueNode.Value);
                            default:
                                return Maybe.Maybe.None<object>();
                        }
                    });
            }
        );


        [NotNull]
        public static ScalarType String { get; } = ScalarType.Create<string>(
            _ => _.Description("string value")
                .ValueParser(value =>
                {
                    if (value is string str)
                    {
                        return Maybe.Maybe.Some<object>(str);
                    }

                    throw new Exception($"String cannot represent a non string value: {value.Inspect()}");
                })
                .LiteralParser(node =>
                    node is StringValueSyntax svn ? Maybe.Maybe.Some<object>(svn.Value) : Maybe.Maybe.None<object>())
                .Serializer(value =>
                {
                    if (value is string str)
                    {
                        return Maybe.Maybe.Some<object>(str);
                    }

                    if (value is bool boolean)
                    {
                        return Maybe.Maybe.Some<object>(boolean ? "true" : "false");
                    }

                    if (InternalNumerics.IsNumber(value))
                    {
                        return Maybe.Maybe.Some<object>(value.ToString());
                    }

                    throw new Exception($"String cannot represent a non string value: {value}");
                })
        );

        [NotNull]
        public static ScalarType Int { get; } = ScalarType.Create<int>(_ =>
        {
            _
                .Description("integer value")
                .Name("Int")
                .ValueParser(value =>
                {
                    if (InternalNumerics.TryGetWholeDouble(value, out var wholeNumber))
                    {
                        if (InternalNumerics.TryConvertToInt32(wholeNumber, out var intValue))
                        {
                            return Maybe.Maybe.Some<object>(intValue);
                        }

                        throw new Exception($"Int cannot represent non 32-bit signed integer value: {value}");
                    }

                    throw new Exception($"Int cannot represent non-integer value: {value}");
                })
                .LiteralParser(
                    node => node is IntValueSyntax ivn ? Maybe.Maybe.Some<object>(ivn.Value) : Maybe.Maybe.None<object>())
                .Serializer(value =>

                {
                    if (value is bool boolean)
                    {
                        return Maybe.Maybe.Some<object>(boolean ? 1 : 0);
                    }


                    if (value is string str && str != "")
                    {
                        return Maybe.Maybe.Some<object>(Convert.ToInt32(value));
                    }

                    if (InternalNumerics.TryGetWholeDouble(value, out var wholeNumber))
                    {
                        if (InternalNumerics.TryConvertToInt32(wholeNumber, out var intValue))
                        {
                            return Maybe.Maybe.Some<object>(intValue);
                        }

                        throw new Exception($"Int cannot represent non 32-bit signed integer value: {value}");
                    }

                    throw new Exception($"Int cannot represent non-integer value: {value}");
                });
        });

        [NotNull]
        public static ScalarType Float { get; } = ScalarType.Create<float>(_ =>
        {
            _
                .Description("The `Float` scalar type represents signed double-precision fractional " +
                             "values as specified by " +
                             "[IEEE 754](http://en.wikipedia.org/wiki/IEEE_floating_point). ")
                .Name("Float")
                .Serializer(value =>
                {
                    try
                    {
                        return Maybe.Maybe.Some<object>(Convert.ToDouble(value));
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
                        return Maybe.Maybe.Some<object>(Convert.ToDouble(value));
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
                            return Maybe.Maybe.Some<object>(Convert.ToDouble(fvn.Value));
                        case IntValueSyntax ivn:
                            return Maybe.Maybe.Some<object>(Convert.ToDouble(ivn.Value));
                        default:
                            return Maybe.Maybe.None<object>();
                    }
                });
        });


        [NotNull]
        public static ScalarType Boolean { get; } = ScalarType.Create<bool>(_ =>
        {
            _.Description("boolean value")
                .ValueParser(val => Maybe.Maybe.Some<object>(Convert.ToBoolean(val)))
                .LiteralParser(
                    node => node is BooleanValueSyntax bvn ? Maybe.Maybe.Some<object>(bvn.Value) : Maybe.Maybe.None<object>())
                .Serializer(value => Maybe.Maybe.Some<object>(Convert.ToBoolean(value)));
        });

        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<ScalarType> All { get; } = new List<ScalarType>
        {
            ID,
            String,
            Int,
            Float,
            Boolean
        }.AsReadOnly();
    }
}