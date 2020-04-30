// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
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
    internal class ExecutionContext
    {
        private ExecutionContext(Schema schema,
            object rootValue,
            IReadOnlyDictionary<string, FragmentDefinitionSyntax> fragments,
            GraphQLContext contextValue, OperationDefinitionSyntax operation,
            IReadOnlyDictionary<string, object> variableValues,
            ConcurrentBag<GraphQLServerError> errors, ExecutionOptions options)
        {
            Schema = Check.NotNull(schema, nameof(schema));
            //Directives = new SpecifiedDirectives(Schema);
            Fragments = Check.NotNull(fragments, nameof(fragments));
            ContextValue = Check.NotNull(contextValue, nameof(contextValue));
            Operation = Check.NotNull(operation, nameof(operation));
            VariableValues = Check.NotNull(variableValues, nameof(variableValues));
            Errors = Check.NotNull(errors, nameof(errors));
            RootValue = rootValue;
            Options = options ?? new ExecutionOptions();
        }

        //
        //public SpecifiedDirectives Directives { get; }


        public Schema Schema { get; }


        public ExecutionOptions Options { get; }


        public IReadOnlyDictionary<string, FragmentDefinitionSyntax> Fragments { get; }


        public GraphQLContext ContextValue { get; }


        public OperationDefinitionSyntax Operation { get; }

        public IReadOnlyDictionary<string, object> VariableValues { get; }


        public Resolver<object, object> FieldResolver { get; } = DefaultFieldResovler;


        public ConcurrentBag<GraphQLServerError> Errors { get; }

        public object RootValue { get; }


        private static Maybe<object> DefaultFieldResovler(object source, dynamic args,
            GraphQLContext context,
            ResolveInfo info)
        {
            if (source == null)
            {
                return Maybe.None<object>();
            }

            var fieldNameFirstCharUpper = info.FieldName.FirstCharToUpper();
            Debug.Assert(fieldNameFirstCharUpper != null, nameof(fieldNameFirstCharUpper) + " != null");
            var type = source.GetType();

            var prop = type.GetProperty(fieldNameFirstCharUpper) ?? type.GetProperty(info.FieldName);
            if (prop != null)
            {
                var meth = prop.PropertyType.GetMethod("Invoke");
                if (meth != null)
                {
                    var methodParameters = meth.GetParameters();
                    var parameters = new List<object>();
                    for (var i = 0; i < methodParameters.Length; i++)
                    {
                        var methodParam = methodParameters[i];
                        Debug.Assert(methodParam != null, nameof(methodParam) + " != null");
                        var parameterType = methodParam.ParameterType;
                        if (i == 0)
                        {
                            if (parameterType == typeof(object) || parameterType == typeof(DynamicDictionary))
                            {
                                parameters.Add(args);
                            }
                            else
                            {
                                throw new Exception(
                                    $"The arguments resolver parameter had an unexpected type of \"{parameterType}\". Expected either \"{typeof(object)}\" or \"{typeof(DynamicDictionary)}\".");
                            }
                        }
                        else if (i == 1)
                        {
                            if (parameterType.IsAssignableFrom(typeof(GraphQLContext)))
                            {
                                parameters.Add(context);
                            }
                            else
                            {
                                throw new Exception(
                                    $"The context resolver parameter had an unexpected type of \"{parameterType}\". Expected a type of \"{typeof(GraphQLContext)}\".");
                            }
                        }
                        else if (i == 2)
                        {
                            if (parameterType == typeof(ResolveInfo))
                            {
                                parameters.Add(info);
                            }
                            else
                            {
                                throw new Exception(
                                    $"The context resolver parameter had an unexpected type of \"{parameterType}\". Expected a type of \"{typeof(GraphQLContext)}\".");
                            }
                        }
                        else
                        {
                            throw new Exception("The resolver has more than the expected arguments.");
                        }
                    }

                    var result = meth.Invoke(prop.GetValue(source), parameters.ToArray());
                    return Maybe.Some(result);
                }

                return Maybe.Some(prop.GetValue(source));
            }

            var field = type.GetField(fieldNameFirstCharUpper) ?? type.GetField(info.FieldName);
            if (field != null)
            {
                return Maybe.Some(field.GetValue(source));
            }

            var method = type.GetMethod(fieldNameFirstCharUpper) ?? type.GetMethod(info.FieldName);

            if (method != null)
            {
                return InvokeMethodByArgName(method);
            }

            Maybe<object> InvokeMethodByArgName(MethodInfo mi)
            {
                Check.NotNull(mi, nameof(mi));
                var parameters = new List<object>();
                foreach (var parameter in mi.GetParameters())
                {
                    if (parameter.Name != null)
                    {
                        var argValue = args[parameter.Name];
                        parameters.Add(argValue);
                    }
                    else
                    {
                        break;
                    }
                }

                var methodResult = mi.Invoke(source, parameters.ToArray());

                return Maybe.Some(methodResult);
            }

            return Maybe.None<object>();
        }


        public ObjectType GetOperationRootType(OperationDefinitionSyntax operation)
        {
            Check.NotNull(operation, nameof(operation));

            switch (operation.OperationType)
            {
                case OperationType.Query:
                    // ReSharper disable once ConstantNullCoalescingCondition = in the future this should be caught by schema validations
                    return Schema.QueryType ??
                           throw new InvalidOperationException("Query type is not configured for this schema");
                case OperationType.Mutation:
                    return Schema.MutationType ?? throw new GraphQLLanguageModelException("Schema not configured for mutations.");
                case OperationType.Subscription:
                    return Schema.SubscriptionType ??
                           throw new GraphQLLanguageModelException("Schema not configured for subscriptions.");
                default:
                    throw new GraphQLLanguageModelException("Can only execute queries, mutations, and subscriptions", operation);
            }
        }

        internal static Maybe<ExecutionContext> Build(
            Schema schema,
            DocumentSyntax document,
            object rootValue, GraphQLContext context, IReadOnlyDictionary<string, object> rawVariableValues,
            string operationName = null,
            ExecutionOptions options = null)
        {
            Check.NotNull(schema, nameof(schema));
            Check.NotNull(document, nameof(document));
            context = context ?? new PreBuiltSchemaContext(schema);
            Check.NotNull(document, nameof(document));
            var errors = new ConcurrentBag<GraphQLServerError>();
            var fragments = new Dictionary<string, FragmentDefinitionSyntax>();
            OperationDefinitionSyntax operation = null;
            var hasMultipleAssumedOperations = false;
            foreach (var definition in document.Definitions)
            {
                switch (definition)
                {
                    case OperationDefinitionSyntax operationDefinition:
                        if (operation != null && operationName == null)
                        {
                            hasMultipleAssumedOperations = true;
                        }
                        else if (operationName == null || operationDefinition.Name?.Value == operationName)
                        {
                            operation = operationDefinition;
                        }

                        break;
                    case FragmentDefinitionSyntax fragmentDefinition:
                        fragments[fragmentDefinition.Name.Value] = fragmentDefinition;
                        break;
                }
            }

            if (operation == null)
            {
                if (operationName != null)
                {
                    errors.Add(new GraphQLServerError($"Unkown operation named '{operationName}'"));
                }
                else
                {
                    errors.Add(new GraphQLServerError("Must provide an operation"));
                }
            }
            else if (hasMultipleAssumedOperations)
            {
                errors.Add(new GraphQLServerError("Must provide operation name if query contains multiple operations"));
            }


            IReadOnlyDictionary<string, object> variableValues = null;
            if (operation != null)
            {
                var coercedVariableValues = Values.GetVariableValues(schema,
                    operation.VariableDefinitions,
                    rawVariableValues ?? new Dictionary<string, object>());
                if (coercedVariableValues is Some<IReadOnlyDictionary<string, object>> some)
                {
                    variableValues = some.Value;
                }
                else if (coercedVariableValues is None<IReadOnlyDictionary<string, object>> none)
                {
                    errors.AddRange(none.Errors);
                }
            }

            if (!errors.IsEmpty)
            {
                return Maybe.None<ExecutionContext>(errors);
            }

            var exeContext =
                new ExecutionContext(schema, rootValue, fragments, context, operation, variableValues, errors, options);
            return Maybe.Some(exeContext);
        }

        public void AddError(Exception e)
        {
            var error = e is GraphQLLanguageModelException graphQLException
                ? graphQLException.GraphQLError
                : new GraphQLServerError($"An unknown error occured. {e?.Message}", innerException: e);
            Errors.Add(error);
        }


        internal ResolveInfo Build(Field fieldDefinition,
            IReadOnlyList<FieldSyntax> fieldNodes, IFields parentType,
            ResponsePath path) =>
            new ResolveInfo(
                fieldNodes[0].Name.Value,
                fieldNodes,
                fieldDefinition.FieldType,
                parentType,
                path,
                Schema,
                Fragments,
                Operation,
                VariableValues,
                RootValue
            );

        private class PreBuiltSchemaContext : GraphQLContext
        {
            public PreBuiltSchemaContext(Schema schema) : base(schema)
            {
            }
        }
    }
}