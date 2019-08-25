// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.QueryEngine
{
    internal static class Values
    {
        internal static DynamicDictionary GetDirectiveValues(Directive directive, SyntaxNode node,
            IReadOnlyDictionary<string, object> variableValues)
        {
            if (node is IDirectivesSyntax directivesNode)
            {
                var directiveNode = directivesNode.Directives.SingleOrDefault(_ => _.Name.Value == directive.Name);
                if (directiveNode != null) return GetArgumentValues(directive, directiveNode, variableValues);
            }

            return null;
        }

        internal static Maybe<IReadOnlyDictionary<string, object>> GetVariableValues(
            Schema schema,
            IReadOnlyList<VariableDefinitionSyntax> variableDefinitions,
            IReadOnlyDictionary<string, object> inputs)
        {
            var coercedValues = new Dictionary<string, object>();
            var errors = new List<GraphQLError>();
            foreach (var varDefNode in variableDefinitions)
            {
                var variable = varDefNode.Variable;
                var varName = variable.Name.Value;
                var varType = schema.GetTypeFromAst(varDefNode.VariableType);
                if (!varType.IsInputType())
                {
                    errors.Add(new GraphQLError(
                        $"Variable \"{variable}\" expected value of type \"{varDefNode.VariableType}\" which cannot be used as an input type.",
                        new[] { varDefNode.VariableType }));
                }
                else
                {
                    var maybeValue = inputs.TryGetValue(varName, out var val)
                        ? Maybe.Some(val)
                        : Maybe.None<object>();
                    if (!maybeValue.HasValue && varDefNode.DefaultValue != null)
                    {
                        // If no value was provided to a variable with a default value,
                        // use the default value.
                        if (Helpers.ValueFromAst(varDefNode.DefaultValue, varType) is Some<object> some)
                            coercedValues[varName] = some.Value;
                    }
                    else if ((!maybeValue.HasValue || maybeValue is Some<object> v && v.Value == null) &&
                             varType is NonNullType)
                    {
                        // If no value or a nullish value was provided to a variable with a
                        // non-null type (required), produce an error.
                        var message = maybeValue.HasValue
                            ? $"Variable \"{variable}\" of non-null type \"{varType}\" must not be null."
                            : $"Variable \"{variable}\" of required type \"{varType}\" was not provided.";
                        errors.Add(new GraphQLError(
                            message, new[] { varDefNode }
                        ));
                    }
                    else if (maybeValue is Some<object> s)
                    {
                        var value = s.Value;
                        if (value == null)
                        {
                            // If the explicit value `null` was provided, an entry in the coerced
                            // values must exist as the value `null`.
                            coercedValues[varName] = null;
                        }
                        else
                        {
                            // Otherwise, a non-null value was provided, coerce it to the expected
                            // type or report an error if coercion fails.
                            var coerced = CoerceValue(value, varType, varDefNode, null);
                            if (coerced is None<object> erred)
                            {
                                var coercionErrors = erred.Errors.Select(_ =>
                                {
                                    // TODO: need to clean-up
                                    _.Message =
                                        $"Variable \"{variable}\" got invalid value `{value.Inspect()}`; {_.Message}";
                                    return _;
                                });
                                errors.AddRange(coercionErrors);
                            }

                            if (coerced is Some<object> some) coercedValues[varName] = some.Value;
                        }
                    }
                }
            }


            return errors.Any()
                ? Maybe.None<IReadOnlyDictionary<string, object>>(errors)
                : Maybe.Some<IReadOnlyDictionary<string, object>>(
                    new ReadOnlyDictionary<string, object>(coercedValues));
        }


        public static Maybe<object> CoerceValue(object value, IGraphQLType type, SyntaxNode blameNode,
            ResponsePath path)
        {
            GraphQLError CoercianError(string message, SyntaxNode blame, ResponsePath p, string subMessage = null,
                Exception originalError = null)
            {
                var pathStr = p?.ToString();
                var msg = new StringBuilder();
                msg.Append(message);
                if (!string.IsNullOrEmpty(pathStr)) msg.Append($" at {pathStr}");

                msg.Append(!string.IsNullOrEmpty(subMessage) ? $"; {subMessage}" : ".");
                return new GraphQLError(msg.ToString(), new[] { blame }, null, null, null, originalError);
            }

            if (type is NonNullType nonNull)
            {
                if (value == null)
                    return Maybe.None<object>(CoercianError(
                        $"Expected non-nullable type {type} not to be null",
                        blameNode, path));

                return CoerceValue(value, nonNull.OfType, blameNode, path);
            }

            if (value == null) return Maybe.Some<object>(null);


            switch (type)
            {
                case ListType list:
                    {
                        var errors = new List<GraphQLError>();
                        var itemType = list.OfType;

                        if (value is ICollection collection)
                        {
                            var coercedValue = new List<object>();
                            var index = 0;
                            foreach (var item in collection)
                            {
                                var coerced = CoerceValue(item, itemType, blameNode, path.AddPath(index++));
                                if (coerced is Some<object> someItem)
                                    coercedValue.Add(someItem.Value);
                                else if (coerced is None<object> none) errors.AddRange(none.Errors);
                            }

                            return errors.Any() ? Maybe.None<object>(errors) : Maybe.Some<object>(coercedValue);
                        }

                        // Lists accept a non-list value as a list of one
                        return CoerceValue(value, itemType, blameNode, null).Select<object>(_ => new[] { _ });
                    }
                case InputObjectType inputObject:
                    {
                        if (!(value is IDictionary<string, object> values))
                            return Maybe.None<object>(CoercianError($"Expected {type} to be an object", blameNode,
                                path));

                        var coercedValue = new Dictionary<string, object>();
                        var errors = new List<GraphQLError>();


                        // Ensure every defined field is valid
                        foreach (var field in inputObject.Fields.Values)
                        {
                            var fieldValue = values.TryGetValue(field.Name, out var fv) ? fv : null;
                            if (fieldValue == null)
                            {
                                if (field.DefaultValue is Some<object> someValue)
                                    coercedValue[field.Name] = someValue.Value;
                                else if (field.InputType is NonNullType)
                                    errors.Add(CoercianError(
                                        $"Field {path.AddPath(field.Name)} of required type {field.InputType} was not provided",
                                        blameNode, null));
                            }
                            else
                            {
                                var coercedField = CoerceValue(fieldValue, field.InputType, blameNode,
                                    path.AddPath(field.Name));
                                if (coercedField is None<object> erred)
                                    errors.AddRange(erred.Errors);
                                else if (!errors.Any() && coercedField is Some<object> some)
                                    coercedValue[field.Name] = some.Value;
                            }
                        }


                        // Ensure every provided field is defined
                        foreach (var fieldName in values.Keys.Where(_ =>
                        {
                            Debug.Assert(_ != null, nameof(_) + " != null");
                            return !inputObject.HasField(_);
                        }))
                        {
                            // TODO: Suggestions for subMessage in error
                            var suggestions = "Did you mean to select another field?";
                            errors.Add(CoercianError($"Field \"{fieldName}\" is not defined by type {type}",
                                blameNode,
                                path,
                                suggestions));
                        }

                        return errors.Any() ? Maybe.None<object>(errors) : Maybe.Some<object>(coercedValue);
                    }
                case ScalarType scalarType:
                    try
                    {
                        return scalarType.ParseValue(value);
                    }
                    catch (Exception e)
                    {
                        return Maybe.None<object>(CoercianError($"Expected type {type}", blameNode, path, e.Message,
                            e));
                    }
                case EnumType enumType:
                    if (value is string strValue)
                    {
                        var enumValue = enumType.FindValue(strValue);
                        if (enumValue != null) return Maybe.Some(enumValue.Value);
                    }

                    // TODO: Suggestions
                    var didYouMean = "did you mean some other enum value?";
                    return Maybe.None<object>(CoercianError($"Expected type {type}", blameNode, path, didYouMean));
                default:
                    throw new GraphQLException($"Provided type \"{type}\" must be an input type. ");
            }
        }


        public static DynamicDictionary GetArgumentValues<TNode>(IArgumentsContainer def,
            TNode node,
            IReadOnlyDictionary<string, object> variableValues) where TNode : SyntaxNode, IArgumentsContainerNode
        {
            var coercedValues = new DynamicDictionary();
            var argDefs = def.GetArguments();
            var argNodes = node.Arguments;

            var argNodeMap = argNodes.ToDictionary(arg => arg.Name.Value, arg => arg);

            foreach (var argDef in argDefs)
            {
                var name = argDef.Name;
                var argType = argDef.InputType;
                argNodeMap.TryGetValue(name, out var argumentNode);
                bool hasValue;
                bool isNull;

                if (argumentNode?.Value is VariableSyntax argVar)
                {
                    var variableName = argVar.Name.Value;
                    hasValue = variableValues != null && variableValues.ContainsKey(variableName);
                    isNull = hasValue && variableValues[variableName] == null;
                }
                else
                {
                    hasValue = argumentNode != null;
                    isNull = argumentNode?.Value is NullValueSyntax;
                }

                if (!hasValue && argDef.HasDefaultValue)
                {
                    coercedValues[name] = argDef.DefaultValue;
                }
                else if ((!hasValue || isNull) && argType is NonNullType)
                {
                    if (isNull)
                        throw new GraphQLException(
                            $"Argument \"{name}\" of non-null type \"{argType}\" must not be null.",
                            argumentNode.Value);

                    if (argumentNode?.Value is VariableSyntax var)
                        throw new GraphQLException(
                            $"Argument \"{name}\" of required type \"{argType}\" was provided the variable \"{var}\" which was not provided a runtime value.",
                            argumentNode.Value);

                    throw new GraphQLException(
                        $"Argument \"{name}\" of required type \"{argType}\" was not provided.",
                        node);
                }
                else if (hasValue)
                {
                    if (argumentNode.Value is NullValueSyntax)
                    {
                        coercedValues[name] = null;
                    }
                    else if (argumentNode.Value is VariableSyntax varArg)
                    {
                        var variableName = varArg.Name.Value;
                        coercedValues[name] = variableValues[variableName];
                    }
                    else
                    {
                        var valueNode = argumentNode.Value;
                        var maybeCoerced = Helpers.ValueFromAst(valueNode, argType, variableValues);
                        if (!(maybeCoerced is Some<object> someCoerced))
                            throw new GraphQLException(
                                $"Argument \"{name}\" has invalid value {valueNode.GetValue().Inspect()}.", node);

                        coercedValues[name] = someCoerced.Value;
                    }
                }
            }

            return coercedValues;
        }
    }
}