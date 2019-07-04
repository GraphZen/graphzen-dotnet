﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.Language.Internal;
using GraphZen.QueryEngine;
using GraphZen.Validation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace GraphZen
{
    public static class GraphZenApplicationBuilderExtensions
    {
        [UsedImplicitly]
        public static void UseGraphQL(this IApplicationBuilder app)
        {
            app.Map("", a =>
            {
                a.Run(async httpContext =>
                {
                    Debug.Assert(httpContext != null, nameof(httpContext) + " != null");
                    Debug.Assert(httpContext.Request != null, "httpContext.Request != null");

                    if (httpContext.Request.Method == "POST")
                    {
                        // ReSharper disable once AssignNullToNotNullAttribute
                        using (var reader = new StreamReader(httpContext.Request.Body))
                        using (var jsonReader = new JsonTextReader(reader))
                        {
                            ExecutionResult result;
                            var context =
                                httpContext.RequestServices.GetRequiredService<GraphQLContext>();
                            Debug.Assert(context != null, nameof(context) + " != null");
                            Debug.Assert(httpContext.RequestServices != null, "httpContext.RequestServices != null");
                            try
                            {
                                var ser = Json.Serializer;
                                var req = ser.Deserialize<GraphQLRequest>(jsonReader);
                                // ReSharper disable once PossibleNullReferenceException
                                var document = Parser.ParseDocument(req.Query);
                                var queryValidator = httpContext.RequestServices.GetRequiredService<IQueryValidator>();
                                // ReSharper disable once PossibleNullReferenceException
                                var validationErrors = queryValidator.Validate(context.Schema, document);

                                if (validationErrors.Any())
                                {
                                    result = new ExecutionResult(null, validationErrors);
                                }
                                else
                                {
                                    var operationDefinitions =
                                        document.Definitions.OfType<OperationDefinitionSyntax>().ToList();

                                    var operation = req.OperationName != null
                                        ? operationDefinitions
                                            // ReSharper disable once PossibleNullReferenceException
                                            .FirstOrDefault(_ => _.Name?.Value == req.OperationName)
                                        : operationDefinitions.First();

                                    object rootValue;

                                    var rootClrType = operation?.OperationType == OperationType.Query
                                        ? context.Schema.QueryType.ClrType
                                        : operation?.OperationType == OperationType.Mutation
                                            ? context.Schema.MutationType?.ClrType
                                            : null;

                                    if (rootClrType != null)
                                    {
                                        rootValue = httpContext.RequestServices.GetService(rootClrType);
                                    }
                                    else
                                    {
                                        rootValue = new { };
                                    }

                                    result = await new Executor().ExecuteAsync(context.Schema, document,
                                        rootValue,
                                        context, req.Variables, req.OperationName, new ExecutionOptions
                                        {
                                            ThrowOnError = false
                                        });
                                }
                            }
                            catch (GraphQLException gqlException)
                            {
                                result = new ExecutionResult(null, new[] {gqlException.GraphQLError});
                            }
                            catch (Exception e)
                            {
                                var error = context.Options.RevealInternalServerErrors
                                    ? new GraphQLError(e.Message, innerException: e)
                                    : new GraphQLError("An unknown error occured.");
                                result = new ExecutionResult(null, new[] {error});
                            }

                            var resp = JsonConvert.SerializeObject(result, Json.SerializerSettings);
                            // ReSharper disable once PossibleNullReferenceException
                            await httpContext.Response.WriteAsync(resp);
                        }
                    }
                });
            });
        }
    }
}