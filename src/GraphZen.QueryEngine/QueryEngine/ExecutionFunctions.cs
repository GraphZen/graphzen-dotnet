// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.QueryEngine.Internal;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.QueryEngine
{
    internal static class ExecutionFunctions
    {
        internal static async Task<ExecutionResult> ExecuteAsync(
            Schema schema,
            DocumentSyntax document,
            object rootValue,
            GraphQLContext context,
            IDictionary<string, object> variableValues, string operationName = null, ExecutionOptions options = null)
        {
            try
            {
                Check.NotNull(schema, nameof(schema));
                Check.NotNull(document, nameof(document));
                var exeContextMaybe = ExecutionContext.Build(schema, document, rootValue, context,
                    new ReadOnlyDictionary<string, object>(variableValues ?? new Dictionary<string, object>()),
                    operationName, options);

                if (exeContextMaybe is Some<ExecutionContext> some)
                {
                    var exeContext = some.Value;
                    Debug.Assert(exeContext != null, nameof(exeContext) + " != null");
                    var data = await ExecuteOperationAsync(exeContext, exeContext.Operation, rootValue);
                    var result = new ExecutionResult(data, exeContext.Errors.Reverse());
                    return result;
                }

                if (exeContextMaybe is None<ExecutionContext> none)
                {
                    var result = new ExecutionResult(null, none.Errors);
                    return result;
                }
            }
            catch (GraphQLLanguageModelException publicError)
            {
                return new ExecutionResult(null, new[] { publicError.GraphQLError });
            }
            catch (Exception e)
            {
                if (context?.Options != null)
                {
                    var error = context.Options.GetExtension<CoreOptionsExtension>().RevealInternalServerErrors
                        ? new GraphQLServerError(e.Message, innerException: e)
                        : new GraphQLServerError("An unknown error occured");
                    return new ExecutionResult(null, new[] { error });
                }

                throw;
            }

            return new ExecutionResult(null, new[] { new GraphQLServerError("An unknown error occured.") });
        }


        internal static async Task<IDictionary<string, object>> ExecuteOperationAsync(
            ExecutionContext exeContext,
            OperationDefinitionSyntax operation, object rootValue)
        {
            var type = exeContext.GetOperationRootType(operation);
            var fields = CollectFields(exeContext, type, operation.SelectionSet,
                new Dictionary<string, List<FieldSyntax>>(), new Dictionary<string, bool>());


            try
            {
                var result = operation.OperationType == OperationType.Mutation
                    ? await ExecuteFieldsSeriallyAsync(exeContext, type, rootValue, null, fields)
                    : await ExecuteFieldsAsync(exeContext, type, rootValue, null, fields);


                return result;
            }
            catch (Exception e)
            {
                exeContext.AddError(e);
                if (exeContext.Options.ThrowOnError)
                {
                    throw;
                }

                return null;
            }
        }


        private static async Task<IDictionary<string, object>> ExecuteFieldsAsync(
            ExecutionContext exeContext, ObjectType parentType, object sourceValue,
            ResponsePath path,
            Dictionary<string, List<FieldSyntax>> fields)
        {
            var asyncResults = new Dictionary<string, Task<Maybe<object>>>();

            foreach (var field in fields)
            {
                var responseName = field.Key;
                Debug.Assert(responseName != null, nameof(responseName) + " != null");
                var nodes = field.Value;
                Debug.Assert(nodes != null, nameof(nodes) + " != null");
                var fieldPath = path != null ? path.AddPath(responseName) : new ResponsePath(responseName);

                var taskResult = ResolveFieldAsync(exeContext, parentType, sourceValue, nodes, fieldPath);

                asyncResults[responseName] = taskResult;
            }

            var results = new Dictionary<string, object>();
            foreach (var asyncResult in asyncResults)
            {
                Debug.Assert(asyncResult.Value != null, "asyncResult.Value != null");

                var maybeResult = await asyncResult.Value;


                if (maybeResult is Some<object> someResult)
                {
                    Debug.Assert(asyncResult.Key != null, "asyncResult.Key != null");
                    results[asyncResult.Key] = someResult.Value;
                }
            }

            return results;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static async Task<IDictionary<string, object>> ExecuteFieldsSeriallyAsync(
            ExecutionContext exeContext, ObjectType parentType, object sourceValue,
            ResponsePath path,
            Dictionary<string, List<FieldSyntax>> fields)
        {
            var results = new Dictionary<string, object>();

            foreach (var field in fields)
            {
                var responseName = field.Key;
                Debug.Assert(responseName != null, nameof(responseName) + " != null");
                var nodes = field.Value;
                Debug.Assert(nodes != null, nameof(nodes) + " != null");
                var fieldPath = path != null ? path.AddPath(responseName) : new ResponsePath(responseName);

                var maybeResult = await ResolveFieldAsync(exeContext, parentType, sourceValue, nodes, fieldPath);

                if (maybeResult is Some<object> someResult)
                {
                    results[responseName] = someResult.Value;
                }
            }

            return results;
        }


        private static async Task<Maybe<object>> ResolveFieldAsync(
            ExecutionContext exeContext, ObjectType parentType,
            object sourceValue,
            List<FieldSyntax> fieldNodes, ResponsePath path)
        {
            var fieldNode = fieldNodes[0];
            var fieldName = fieldNode.Name.Value;
            var fieldDef = GetFieldDef(exeContext, exeContext.Schema, parentType, fieldName);

            if (fieldDef == null)
            {
                return Maybe.None<object>();
            }


            var info = exeContext.Build(fieldDef, fieldNodes, parentType, path);


            var maybeResult =
                await ResolveFieldValueOrErrorAsync(exeContext, fieldDef, fieldNodes, sourceValue, info);


            return await CompleteValueCatchingErrorAsync(exeContext, fieldDef.FieldType, fieldNodes, info, path,
                maybeResult);
        }

        private static async Task<Maybe<object>> CompleteValueCatchingErrorAsync(ExecutionContext exeContext,
            IGraphQLType returnType, List<FieldSyntax> fieldNodes,
            ResolveInfo info,
            ResponsePath path, Maybe<object> maybeResult)
        {
            try
            {
                if (maybeResult is Some<object> someResult)
                {
                    if (someResult.Value is Task awaitable)
                    {
                        await awaitable;
                        var result = awaitable.GetResult();
                        maybeResult = Maybe.Some(result);
                    }
                }

                var completed = await CompleteValueAsync(exeContext, returnType, fieldNodes, info, path, maybeResult);
                return completed;
            }
            catch (GraphQLLanguageModelException e)
            {
                if (exeContext.Options.ThrowOnError)
                {
                    throw;
                }

                exeContext.Errors.Add(e.GraphQLError.WithLocationInfo(fieldNodes, path));
                return Maybe.Some<object>(null);
            }
            catch (Exception e)
            {
                exeContext.AddError(e);
                if (exeContext.Options.ThrowOnError)
                {
                    throw;
                }

                return Maybe.Some<object>(null);
            }
        }


        private static async Task<Maybe<object>> CompleteValueAsync(
            ExecutionContext exeContext, IGraphQLType returnType,
            List<FieldSyntax> fieldNodes, ResolveInfo info,
            ResponsePath path,
            Maybe<object> maybeResult)
        {
            if (maybeResult is None<object> none)
            {
                none.ThrowFirstErrorOrDefault();
            }

            if (maybeResult is Some<object> some)
            {
                var result = some.Value;


                if (returnType is NonNullType nonNull)
                {
                    var completed = await CompleteValueAsync(exeContext, nonNull.OfType, fieldNodes, info, path,
                        maybeResult);
                    Debug.Assert(info.ParentType != null, "info.ParentType != null");
                    return completed ?? throw new GraphQLLanguageModelException(
                        $"Cannot return null for non - nullable field {info.ParentType.Name}.{info.FieldName}.");
                }

                if (result == null)
                {
                    return Maybe.Some<object>(null);
                }

                if (returnType is ListType listType)
                {
                    return await CompleteListValueAsync(exeContext, listType, fieldNodes, info, path, result);
                }

                if (returnType is ILeafType leafType)
                {
                    return CompleteLeafValue(leafType, result);
                }

                if (returnType is IAbstractType abstractType)
                {
                    return await CompleteAbstractValueAsync(exeContext, abstractType, fieldNodes, info, path, result);
                }

                if (returnType is ObjectType objectReturnType)
                {
                    return await CompleteObjectValueAsync(exeContext, objectReturnType, fieldNodes,
                        info, path,
                        result);
                }
            }

            throw new GraphQLLanguageModelException(
                $@"Cannot complete value of unexpected type ""{returnType?.GetType()}"".");
        }


        public static string DefaultResolveType(
            object value,
            GraphQLContext context,
            ResolveInfo info,
            IAbstractType abstractType)
        {
            var typeName = value?.GetType().GetGraphQLNameAnnotation(value);
            return typeName;

            /*
                        var possibleTypes = info.Schema.GetPossibleTypes(abstractType);
                        foreach (var type in possibleTypes)
                        {
                            if (type.IsTypeOf != null)
                            {
                                var result = type.IsTypeOf(value, context, info);
                                if (result)
                                {
                                    return type.Name;
                                }
                            }
                        }*/
        }


        private static async Task<Maybe<object>> CompleteAbstractValueAsync(
            ExecutionContext exeContext,
            IAbstractType returnType,
            List<FieldSyntax> fieldNodes,
            ResolveInfo info,
            ResponsePath path, object result)
        {
            result = await result.GetResultAsync();
            var runtimeTypeRef = returnType.ResolveType != null
                ? returnType.ResolveType(result, exeContext.ContextValue, info)
                : DefaultResolveType(result, exeContext.ContextValue, info, returnType);

            // not sure this is a valid assertion - may be a condition that is not handled currently
            // Debug.Assert(runtimeTypeRef != null, nameof(runtimeTypeRef) + " != null");
            var runtimeType = EnsureValidRuntimeType(runtimeTypeRef, exeContext, returnType, fieldNodes, info, result);
            return await CompleteObjectValueAsync(exeContext, runtimeType, fieldNodes, info, path, result);
        }


        private static ObjectType EnsureValidRuntimeType(
            string typeName, ExecutionContext exeContext,
            IAbstractType returnType, IReadOnlyList<FieldSyntax> fieldNodes,
            ResolveInfo info, object result)
        {
            IGraphQLType runtimeType =
                typeName != null ? exeContext.Schema.TryGetType(typeName, out var t) ? t : null : null;

            if (runtimeType == null || !(runtimeType is ObjectType runtimeObjectType))
            {
                var message = $"Abstract type {returnType.Name}" +
                              " must resolve to an Object type at " +
                              $"runtime for field {info.ParentType.Name}.{info.FieldName} with " +
                              $"value {result.Inspect()}, received \"{runtimeType.Inspect()}\". " +
                              $"Either the {returnType.Name} type should provide a \"resolveType\" " +
                              "function or each possible types should provide an " +
                              $"\"{nameof(IObjectType.IsTypeOf)}\" function.";
                throw new GraphQLLanguageModelException(message, fieldNodes);
            }

            if (!exeContext.Schema.IsPossibleType(returnType, runtimeObjectType))
            {
                throw new GraphQLLanguageModelException(
                    $"Runtime Object type \"{runtimeObjectType.Name}\" is not a possible type " +
                    $"for \"{returnType.Name}\".",
                    fieldNodes
                );
            }

            return runtimeObjectType;
        }

        private static async Task<Maybe<object>> CompleteObjectValueAsync(
            ExecutionContext exeContext,
            ObjectType returnType,
            List<FieldSyntax> fieldNodes, ResolveInfo info, ResponsePath path, object result)
        {
            if (returnType.IsTypeOf != null)
            {
                var isTypeOf = returnType.IsTypeOf(result, exeContext.ContextValue, info);
                if (!isTypeOf)
                {
                    var inspectable = Json.CreateJObject(result);
                    throw new GraphQLLanguageModelException(
                        $"Expected value of type \"{returnType}\" but got: {inspectable.Inspect()}.", fieldNodes);
                }
            }


            return Maybe.Some<object>(await CollectAndExecuteSubfields(exeContext, returnType, fieldNodes, info, path,
                result));
        }


        private static Task<IDictionary<string, object>> CollectAndExecuteSubfields(
            ExecutionContext exeContext, ObjectType returnType,
            // ReSharper disable once UnusedParameter.Local
            List<FieldSyntax> fieldNodes, ResolveInfo info, ResponsePath path, object result)
        {
            var subFieldNodes = new Dictionary<string, List<FieldSyntax>>();
            var visitedFargmentNames = new Dictionary<string, bool>();
            foreach (var fieldNode in fieldNodes)
            {
                if (fieldNode.SelectionSet != null)
                {
                    subFieldNodes = CollectFields(exeContext, returnType, fieldNode.SelectionSet, subFieldNodes,
                        visitedFargmentNames);
                }
            }

            return ExecuteFieldsAsync(exeContext, returnType, result, path, subFieldNodes);
        }

        private static Maybe<object> CompleteLeafValue(ILeafType returnType, object result)
        {
            var serializedResult = returnType.Serialize(result);
            if (serializedResult is None<object> none)
            {
                none.ThrowFirstErrorOrDefault($@"Expected a value of type `{returnType}` but received: {result}");
            }

            return serializedResult;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static async Task<Maybe<object>> CompleteListValueAsync(ExecutionContext exeContext,
            ListType returnType,
            List<FieldSyntax> fieldNodes,
            ResolveInfo info, ResponsePath path, object result)
        {
            if (result is string || !(result is IEnumerable collection))
            {
                throw new InvalidOperationException(
                    $"Expected IEnumerable, but did not find one for field {info.ParentType.Name}");
            }

            var itemType = returnType.OfType;
            // var containsTask = false;
            var completedResults = new List<object>();
            var index = 0;
            foreach (var item in collection)
            {
                var fieldPath = path.AddPath(index);
                var itemValue = Maybe.Some(item);
                var completedItem =
                    await CompleteValueCatchingErrorAsync(exeContext, itemType, fieldNodes, info, fieldPath, itemValue);
                if (completedItem is Some<object> some)
                {
                    completedResults.Add(some.Value);
                }
                else
                {
                    throw new InvalidOperationException(
                        $"There should always be a value produced by {nameof(CompleteValueCatchingErrorAsync)}");
                }

                index++;
            }

            return Maybe.Some<object>(completedResults);
        }

        private static async Task<Maybe<object>> ResolveFieldValueOrErrorAsync(ExecutionContext exeContext,
            Field field,
            List<FieldSyntax> fieldNodes,
            object sourceValue, ResolveInfo info)
        {
            try
            {
                var args = Values.GetArgumentValues(field, fieldNodes[0], exeContext.VariableValues);
                var context = exeContext.ContextValue;

                object result = default;
                if (field.Resolver != null)
                {
                    result = field.Resolver(sourceValue, args, context, info);
                }
                else
                {
                    var defaultResolverResult = exeContext.FieldResolver(sourceValue, args, context, info);
                    if (defaultResolverResult is Some<object> someResult)
                    {
                        result = someResult.Value;
                    }
                    else
                    {
                        Maybe.None<object>();
                    }
                }

                if (result is Task resultTask && result.GetType() != typeof(Task))
                {
                    await resultTask;
                    return Maybe.Some<object>(resultTask);
                }

                return Maybe.Some(result);
            }
            catch (GraphQLLanguageModelException gqlE)
            {
                return Maybe.None<object>(gqlE.GraphQLError.WithLocationInfo(fieldNodes, info.Path));
            }
            catch (Exception e)
            {
                if (exeContext.Options.ThrowOnError)
                {
                    throw;
                }

                return Maybe.None<object>(new GraphQLServerError(e.Message, fieldNodes, null,
                    null, info.Path.AsReadOnlyList(), e));
            }
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static Field GetFieldDef(ExecutionContext exeContext, Schema schema,
            ObjectType parentType,
            string fieldName)
        {
            if (fieldName == schema.Introspection.SchemaMetaFieldDef.Name && schema.QueryType.Equals(parentType))
            {
                return schema.Introspection.SchemaMetaFieldDef;
            }

            if (fieldName == schema.Introspection.TypeMetaFieldDef.Name && schema.QueryType.Equals(parentType))
            {
                return schema.Introspection.TypeMetaFieldDef;
            }

            if (fieldName == schema.Introspection.TypeNameMetaFieldDef.Name)
            {
                return schema.Introspection.TypeNameMetaFieldDef;
            }

            var field = parentType.FindField(fieldName);
            if (field == null && exeContext.Options.ThrowOnError)
            {
                throw new Exception($"Unable to find field \"{fieldName}\" on {parentType.Name}");
            }

            return field;
        }


        internal static Dictionary<string, List<FieldSyntax>> CollectFields(ExecutionContext exeContext,
            ObjectType runtimeType, SelectionSetSyntax selectionSet
            , Dictionary<string, List<FieldSyntax>> fields,
            Dictionary<string, bool> visitedFragmentNames)
        {
            foreach (var selection in selectionSet.Selections)
            {
                switch (selection)
                {
                    case FieldSyntax field:
                        if (!ShouldIncludeNode(exeContext, field))
                        {
                            continue;
                        }

                        if (!fields.ContainsKey(field.FieldEntryKey))
                        {
                            fields[field.FieldEntryKey] = new List<FieldSyntax>();
                        }

                        fields[field.FieldEntryKey]?.Add(field);
                        break;
                    case InlineFragmentSyntax inlineFragment:
                        if (!ShouldIncludeNode(exeContext, selection) ||
                            !DoesFragmentConditionMatch(exeContext, inlineFragment, runtimeType))
                        {
                            continue;
                        }

                        CollectFields(exeContext, runtimeType, inlineFragment.SelectionSet, fields,
                            visitedFragmentNames);
                        break;
                    case FragmentSpreadSyntax fragmentSpread:
                        var fragName = fragmentSpread.Name.Value;
                        if (visitedFragmentNames.ContainsKey(fragName) ||
                            !ShouldIncludeNode(exeContext, fragmentSpread))
                        {
                            continue;
                        }

                        visitedFragmentNames[fragName] = true;

                        if (!exeContext.Fragments.TryGetValue(fragName, out var fragment) ||
                            !DoesFragmentConditionMatch(exeContext, fragment, runtimeType))
                        {
                            continue;
                        }

                        CollectFields(exeContext, runtimeType, fragment.SelectionSet, fields, visitedFragmentNames);

                        break;
                }
            }

            return fields;
        }

        internal static bool DoesFragmentConditionMatch(ExecutionContext exeContext,
            IFragmentTypeConditionSyntax fragment, ObjectType type)
        {
            if (fragment.TypeCondition == null)
            {
                return true;
            }

            var conditionalType = exeContext.Schema.GetTypeFromAst(fragment.TypeCondition);
            if (type.Equals(conditionalType))
            {
                return true;
            }

            if (conditionalType is IAbstractType abstractType)
            {
                return exeContext.Schema.IsPossibleType(abstractType, type);
            }

            return false;
        }


        internal static bool ShouldIncludeNode(ExecutionContext exeContext, SyntaxNode node)
        {
            var skipDirective = exeContext.Schema.GetDirective("skip");
            var skip = Values.GetDirectiveValues(skipDirective, node, exeContext.VariableValues);
            if (skip != null && skip["if"] == true)
            {
                return false;
            }

            var includeDirective = exeContext.Schema.GetDirective("include");
            var include = Values.GetDirectiveValues(includeDirective, node, exeContext.VariableValues);
            if (include != null && include["if"] == false)
            {
                return false;
            }

            return true;
        }
    }
}