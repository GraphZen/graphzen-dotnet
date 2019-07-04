// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.TypeSystem;
using GraphZen.Utilities.Internal;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using static GraphZen.Language.SyntaxFactory;
using static GraphZen.Infrastructure.InternalNumerics;

namespace GraphZen.Utilities
{
    public static partial class Helpers
    {
        [CanBeNull]
        public static ValueSyntax AstFromValue(Maybe<object> maybeValue, IGraphQLType type)
        {
            if (!(maybeValue is Some<object> someValue))
            {
                return null;
            }

            var value = someValue.Value;

            if (type is NonNullType nn)
            {
                var astValue = AstFromValue(maybeValue, nn.OfType);
                return astValue is NullValueSyntax ? null : astValue;
            }

            if (value == null)
            {
                return NullValue();
            }

            if (type is ListType list)
            {
                var itemType = list.OfType;
                if (value is ICollection collection)
                {
                    var valueNodes = new List<ValueSyntax>();
                    foreach (var item in collection)
                    {
                        var itemNode = AstFromValue(Maybe.Some(item), itemType);
                        if (itemNode != null)
                        {
                            valueNodes.Add(itemNode);
                        }
                    }

                    return ListValue(valueNodes);
                }

                return AstFromValue(maybeValue, itemType);
            }

            if (type is InputObjectType inputObject)
            {
                var fieldsNodes = new List<ObjectFieldSyntax>();
                var valueDictionary = JObject.FromObject(value).ToDictionary();
                foreach (var field in inputObject.Fields.Values)
                {
                    if (valueDictionary.TryGetValue(field.Name, out var fv))
                    {
                        var fieldValue = AstFromValue(Maybe.Some(fv), field.InputType);
                        if (fieldValue != null)
                        {
                            fieldsNodes.Add(ObjectField(Name(field.Name), fieldValue));
                        }
                    }
                }

                return ObjectValue(fieldsNodes);
            }

            if (type is ILeafType leafType)
            {
                var serialized = leafType.Serialize(value);


                if (!(serialized is Some<object> someSerialized))
                {
                    return null;
                }

                if (someSerialized.Value is bool boolean)
                {
                    return BooleanValue(boolean);
                }

                if (IsNumber(someSerialized.Value))
                {
                    if (TryGetWholeDouble(someSerialized.Value, out var wholeResult))
                    {
                        if (TryConvertToInt32(wholeResult, out var intValue))
                        {
                            return IntValue(intValue);
                        }
                    }

                    return FloatValue(value.ToString().ToLower());
                }

                if (someSerialized.Value is string strVal)
                {
                    if (type is EnumType)
                    {
                        return EnumValue(Name(strVal));
                    }


                    if (type.Equals(SpecScalars.ID))
                    {
                        if (!strVal.TrimStart('-', '+').StartsWith("0") && double.TryParse(strVal, out var numeric))
                        {
                            if (TryGetWholeDouble(numeric, out var whole) &&
                                TryConvertToInt32(whole, out var intVal))
                            {
                                return IntValue(intVal);
                            }
                        }
                    }

                    return StringValue(strVal);
                }

                throw new Exception($"Cannot convert value to AST: {serialized.Inspect()}");
            }

            throw new Exception($"Unknown type: {type}");
        }
    }
}