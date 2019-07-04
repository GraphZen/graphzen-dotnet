// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.Utilities;
using GraphZen.Utilities.Internal;
using Newtonsoft.Json.Linq;

namespace GraphZen.TypeSystem.Internal
{
    public static class AstFromValue
    {
        [CanBeNull]
        public static ValueSyntax Get(Maybe<object> maybeValue, IGraphQLType type)
        {
            if (!(maybeValue is Some<object> someValue))
            {
                return null;
            }

            var value = someValue.Value;

            if (type is NonNullType nn)
            {
                var astValue = Get(maybeValue, nn.OfType);
                return astValue is NullValueSyntax ? null : astValue;
            }

            if (value == null)
            {
                return SyntaxFactory.NullValue();
            }

            if (type is ListType list)
            {
                var itemType = list.OfType;
                if (value is ICollection collection)
                {
                    var valueNodes = new List<ValueSyntax>();
                    foreach (var item in collection)
                    {
                        var itemNode = Get(Maybe.Some(item), itemType);
                        if (itemNode != null)
                        {
                            valueNodes.Add(itemNode);
                        }
                    }

                    return SyntaxFactory.ListValue(valueNodes);
                }

                return Get(maybeValue, itemType);
            }

            if (type is InputObjectType inputObject)
            {
                var fieldsNodes = new List<ObjectFieldSyntax>();
                var valueDictionary = JObject.FromObject(value).ToDictionary();
                foreach (var field in inputObject.Fields.Values)
                {
                    if (valueDictionary.TryGetValue(field.Name, out var fv))
                    {
                        var fieldValue = Get(Maybe.Some(fv), field.InputType);
                        if (fieldValue != null)
                        {
                            fieldsNodes.Add(SyntaxFactory.ObjectField(SyntaxFactory.Name(field.Name), fieldValue));
                        }
                    }
                }

                return SyntaxFactory.ObjectValue(fieldsNodes);
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
                    return SyntaxFactory.BooleanValue(boolean);
                }

                if (InternalNumerics.IsNumber(someSerialized.Value))
                {
                    if (InternalNumerics.TryGetWholeDouble(someSerialized.Value, out var wholeResult))
                    {
                        if (InternalNumerics.TryConvertToInt32(wholeResult, out var intValue))
                        {
                            return SyntaxFactory.IntValue(intValue);
                        }
                    }

                    return SyntaxFactory.FloatValue(value.ToString().ToLower());
                }

                if (someSerialized.Value is string strVal)
                {
                    if (type is EnumType)
                    {
                        return SyntaxFactory.EnumValue(SyntaxFactory.Name(strVal));
                    }


                    if (type.Equals(SpecScalars.ID))
                    {
                        if (!strVal.TrimStart('-', '+').StartsWith("0") && double.TryParse(strVal, out var numeric))
                        {
                            if (InternalNumerics.TryGetWholeDouble(numeric, out var whole) &&
                                InternalNumerics.TryConvertToInt32(whole, out var intVal))
                            {
                                return SyntaxFactory.IntValue(intVal);
                            }
                        }
                    }

                    return SyntaxFactory.StringValue(strVal);
                }

                throw new Exception($"Cannot convert value to AST: {serialized.Inspect()}");
            }

            throw new Exception($"Unknown type: {type}");
        }
    }
}