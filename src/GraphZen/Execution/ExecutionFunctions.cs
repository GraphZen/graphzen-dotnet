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
using GraphZen.Infrastructure.Extensions;
using GraphZen.Internal;
using GraphZen.Language;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.Utilities;
using GraphZen.Utilities.Internal;
using JetBrains.Annotations;

namespace GraphZen.Execution
{
    internal static class ExecutionFunctions
    {
        [NotNull]
        internal static async Task<ExecutionResult> ExecuteAsync(
            Schema schema,
            DocumentSyntax document,
            object rootValue,
            [CanBeNull] GraphQLContext context,
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
            catch (GraphQLException publicError)
            {
                return new ExecutionResult(null, new[] {publicError.GraphQLError});
            }
            catch (Exception e)
            {
                if (context?.Options != null)
                {
                    var error = context.Options.RevealInternalServerErrors
                        ? new GraphQLError(e.Message, innerException: e)
                        : new GraphQLError("An unknown error occured");
                    return new ExecutionResult(null, new[] {error});
                }

                throw;
            }

            return new ExecutionResult(null, new[] {new GraphQLError("An unknown error occured.")});
        }


        [NotNull]
        internal static async Task<IDictionary<string, object>> ExecuteOperationAsync(
            [NotNull] ExecutionContext exeContext,
            [NotNull] OperationDefinitionSyntax operation, object rootValue)
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

        [NotNull]
        private static async Task<IDictionary<string, object>> ExecuteFieldsAsync(
            [NotNull] ExecutionContext exeContext, [NotNull] ObjectType parentType, object sourceValue,
            [CanBeNull] ResponsePath path,
            [NotNull] Dictionary<string, List<FieldSyntax>> fields)
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
        [NotNull]
        private static async Task<IDictionary<string, object>> ExecuteFieldsSeriallyAsync(
            [NotNull] ExecutionContext exeContext, [NotNull] ObjectType parentType, object sourceValue,
            [CanBeNull] ResponsePath path,
            [NotNull] Dictionary<string, List<FieldSyntax>> fields)
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

        [NotNull]
        private static async Task<Maybe<object>> ResolveFieldAsync(
            [NotNull] ExecutionContext exeContext, [NotNull] ObjectType parentType,
            object sourceValue,
            [NotNull] [ItemNotNull] List<FieldSyntax> fieldNodes, [NotNull] ResponsePath path)
        {
            var fieldNode = fieldNodes[0];
            var fieldName = fieldNode.Name.Value;
            var fieldDef = GetFieldDef(exeContext, exeContext.Schema, parentType, fieldName);

            if (fieldDef == null)
            {
                return Maybe.None<object>();
            }


            var info = ResolveInfo.Build(exeContext, fieldDef, fieldNodes, parentType, path);


            var maybeResult =
                await ResolveFieldValueOrErrorAsync(exeContext, fieldDef, fieldNodes, sourceValue, info);


            return await CompleteValueCatchingErrorAsync(exeContext, fieldDef.FieldType, fieldNodes, info, path,
                maybeResult);
        }

        private static async Task<Maybe<object>> CompleteValueCatchingErrorAsync([NotNull] ExecutionContext exeContext,
            IGraphQLType returnType, [NotNull] [ItemNotNull] List<FieldSyntax> fieldNodes,
            [NotNull] ResolveInfo info,
            [NotNull] ResponsePath path, [NotNull] Maybe<object> maybeResult)
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
            catch (GraphQLException e)
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
            [NotNull] ExecutionContext exeContext, IGraphQLType returnType,
            [NotNull] [ItemNotNull] List<FieldSyntax> fieldNodes, [NotNull] ResolveInfo info,
            [NotNull] ResponsePath path,
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
                    return completed ?? throw new GraphQLException(
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

            throw new GraphQLException($@"Cannot complete value of unexpected type ""{returnType?.GetType()}"".");
        }


        public static string DefaultResolveType(
            object value,
            [NotNull] GraphQLContext context,
            [NotNull] ResolveInfo info,
            [NotNull] IAbstractType abstractType)
        {
            var typeName = value?.GetType().GetGraphQLName(value);
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


        [NotNull]
        private static async Task<Maybe<object>> CompleteAbstractValueAsync(
            [NotNull] ExecutionContext exeContext,
            [NotNull] IAbstractType returnType,
            [NotNull] List<FieldSyntax> fieldNodes,
            [NotNull] ResolveInfo info,
            [NotNull] ResponsePath path, object result)
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

        [NotNull]
        private static ObjectType EnsureValidRuntimeType(
            string typeName, [NotNull] ExecutionContext exeContext,
            [NotNull] IAbstractType returnType, [NotNull] IReadOnlyList<FieldSyntax> fieldNodes,
            [NotNull] ResolveInfo info, object result)
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
                throw new GraphQLException(message, fieldNodes);
            }

            if (!exeContext.Schema.IsPossibleType(returnType, runtimeObjectType))
            {
                throw new GraphQLException(
                    $"Runtime Object type \"{runtimeObjectType.Name}\" is not a possible type " +
                    $"for \"{returnType.Name}\".",
                    fieldNodes
                );
            }

            return runtimeObjectType;
        }

        private static async Task<Maybe<object>> CompleteObjectValueAsync(
            [NotNull] ExecutionContext exeContext,
            [NotNull] ObjectType returnType,
            [NotNull] [ItemNotNull] List<FieldSyntax> fieldNodes, ResolveInfo info, ResponsePath path, object result)
        {
            if (returnType.IsTypeOf != null)
            {
                var isTypeOf = returnType.IsTypeOf(result, exeContext.ContextValue, info);
                if (!isTypeOf)
                {
                    var inspectable = Json.CreateJObject(result);
                    throw new GraphQLException(
                        $"Expected value of type \"{returnType}\" but got: {inspectable.Inspect()}.", fieldNodes);
                }
            }


            return Maybe.Some<object>(await CollectAndExecuteSubfields(exeContext, returnType, fieldNodes, info, path,
                result));
        }

        [NotNull]
        private static Task<IDictionary<string, object>> CollectAndExecuteSubfields(
            [NotNull] ExecutionContext exeContext, [NotNull] ObjectType returnType,
            // ReSharper disable once UnusedParameter.Local
            [NotNull] [ItemNotNull] List<FieldSyntax> fieldNodes, ResolveInfo info, ResponsePath path, object result)
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

        private static Maybe<object> CompleteLeafValue([NotNull] ILeafType returnType, object result)
        {
            var serializedResult = returnType.Serialize(result);
            if (serializedResult is None<object> none)
            {
                none.ThrowFirstErrorOrDefault($@"Expected a value of type `{returnType}` but received: {result}");
            }

            return serializedResult;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static async Task<Maybe<object>> CompleteListValueAsync([NotNull] ExecutionContext exeContext,
            [NotNull] ListType returnType,
            [NotNull] List<FieldSyntax> fieldNodes,
            [NotNull] ResolveInfo info, ResponsePath path, object result)
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

        private static async Task<Maybe<object>> ResolveFieldValueOrErrorAsync([NotNull] ExecutionContext exeContext,
            [NotNull] Field field,
            [NotNull] [ItemNotNull] List<FieldSyntax> fieldNodes,
            object sourceValue, [NotNull] ResolveInfo info)
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
            catch (GraphQLException gqlE)
            {
                return Maybe.None<object>(gqlE.GraphQLError.WithLocationInfo(fieldNodes, info.Path));
            }
            catch (Exception e)
            {
                if (exeContext.Options.ThrowOnError)
                {
                    throw;
                }

                return Maybe.None<object>(new GraphQLError(e.Message, fieldNodes, null,
                    null, info.Path.AsReadOnlyList(), e));
            }
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static Field GetFieldDef([NotNull] ExecutionContext exeContext, [NotNull] Schema schema,
            [NotNull] ObjectType parentType,
            [NotNull] string fieldName)
        {
            if (fieldName == Introspection.SchemaMetaFieldDef.Name && schema.QueryType.Equals(parentType))
            {
                return Introspection.SchemaMetaFieldDef;
            }

            if (fieldName == Introspection.TypeMetaFieldDef.Name && schema.QueryType.Equals(parentType))
            {
                return Introspection.TypeMetaFieldDef;
            }

            if (fieldName == Introspection.TypeNameMetaFieldDef.Name)
            {
                return Introspection.TypeNameMetaFieldDef;
            }

            var field = parentType.FindField(fieldName);
            if (field == null && exeContext.Options.ThrowOnError)
            {
                throw new Exception($"Unable to find field \"{fieldName}\" on {parentType.Name}");
            }

            return field;
        }


        [NotNull]
        internal static Dictionary<string, List<FieldSyntax>> CollectFields([NotNull] ExecutionContext exeContext,
            [NotNull] ObjectType runtimeType, [NotNull] SelectionSetSyntax selectionSet
            , [NotNull] Dictionary<string, List<FieldSyntax>> fields,
            [NotNull] Dictionary<string, bool> visitedFragmentNames)
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

        internal static bool DoesFragmentConditionMatch([NotNull] ExecutionContext exeContext,
            [NotNull] IFragmentTypeConditionSyntax fragment, [NotNull] ObjectType type)
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


        internal static bool ShouldIncludeNode([NotNull] ExecutionContext exeContext, SyntaxNode node)
        {
            var skip = Values.GetDirectiveValues(SpecDirectives.Skip, node, exeContext.VariableValues);
            if (skip != null && skip["if"] == true)
            {
                return false;
            }

            var include = Values.GetDirectiveValues(SpecDirectives.Include, node, exeContext.VariableValues);
            if (include != null && include["if"] == false)
            {
                return false;
            }

            return true;
        }
    }
}