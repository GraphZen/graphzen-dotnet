// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using ListValueSyntax = GraphZen.LanguageModel.ListValueSyntax;
using NullValueSyntax = GraphZen.LanguageModel.NullValueSyntax;
using ObjectValueSyntax = GraphZen.LanguageModel.ObjectValueSyntax;

namespace GraphZen.Utilities
{
    public static partial class Helpers
    {
        [NotNull]
        internal static Maybe<object> ValueFromAst(ValueSyntax valueSyntax, IGraphQLType type,
            IReadOnlyDictionary<string, object> variables = null)
        {
            bool IsMissingVariable(ValueSyntax value, IReadOnlyDictionary<string, object> vars) =>
                value is VariableSyntax variable && (vars == null || !vars.ContainsKey(variable.Name.Value));

            if (valueSyntax == null)
            {
                return Maybe.None<object>();
            }

            if (type is NonNullType nonNull)
            {
                if (valueSyntax is NullValueSyntax)
                {
                    return Maybe.None<object>();
                }

                return ValueFromAst(valueSyntax, nonNull.OfType, variables);
            }

            if (valueSyntax is NullValueSyntax)
            {
                return Maybe.Some<object>(null);
            }

            if (valueSyntax is VariableSyntax variableNode)
            {
                var variableName = variableNode.Name.Value;
                return variables != null && variables.TryGetValue(variableName, out var variableValue)
                    ? Maybe.Some(variableValue)
                    : Maybe.None<object>();
            }

            if (type is ListType listType)
            {
                var itemType = listType.OfType;
                if (valueSyntax is ListValueSyntax listNode)
                {
                    var coercedValues = new List<object>(listNode.Values.Count);
                    foreach (var itemNode in listNode.Values)
                    {
                        if (IsMissingVariable(itemNode, variables))
                        {
                            if (itemType is NonNullType)
                            {
                                return Maybe.None<object>();
                            }

                            coercedValues.Add(null);
                        }
                        else
                        {
                            var itemValue = ValueFromAst(itemNode, itemType, variables);
                            if (itemValue is Some<object> some)
                            {
                                coercedValues.Add(some.Value);
                            }
                            else
                            {
                                return itemValue;
                            }
                        }
                    }

                    return Maybe.Some<object>(coercedValues);
                }

                var coerecedValue = ValueFromAst(valueSyntax, itemType, variables);

                return coerecedValue.Select(_ => (object) new List<object> {_});
            }

            if (type is InputObjectType inputObject)
            {
                if (valueSyntax is ObjectValueSyntax objectValue)
                {
                    var coercedObject = new Dictionary<string, object>();
                    var fieldNodes = objectValue.Fields.ToDictionary(f =>
                    {
                        Debug.Assert(f != null, nameof(f) + " != null");
                        return f.Name.Value;
                    }, f => f);

                    foreach (var field in inputObject.Fields.Values)
                    {
                        Debug.Assert(field != null, nameof(field) + " != null");
                        if (!fieldNodes.TryGetValue(field.Name, out var fieldNode) ||
                            IsMissingVariable(fieldNode.Value, variables))
                        {
                            if (field.DefaultValue is Some<object> someDefaultValue)
                            {
                                coercedObject[field.Name] = someDefaultValue.Value;
                            }
                            else if (field.InputType is NonNullType)
                            {
                                return Maybe.None<object>();
                            }

                            continue;
                        }

                        var fieldValue = ValueFromAst(fieldNode.Value, field.InputType, variables);
                        if (fieldValue is Some<object> fv)
                        {
                            coercedObject[field.Name] = fv.Value;
                        }
                        else
                        {
                            return Maybe.None<object>();
                        }
                    }

                    return Maybe.Some<object>(coercedObject);
                }

                return Maybe.None<object>();
            }

            if (type is ILeafType leafType)
            {
                try
                {
                    return leafType.ParseLiteral(valueSyntax);
                }
                catch (Exception)
                {
                    return Maybe.None<object>();
                }
            }

            throw new Exception($"Unknown type: {type}");
        }
    }
}