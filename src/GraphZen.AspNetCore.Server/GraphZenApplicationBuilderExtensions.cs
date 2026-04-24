// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Text.Json;
using GraphZen;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.QueryEngine;
using GraphZen.QueryEngine.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class GraphZenApplicationBuilderExtensions
{
    private const int DefaultMemoryThreshold = 1024 * 30;

    private static string? _sTempDirectory;

    public static string TempDirectory
    {
        get
        {
            if (_sTempDirectory == null)
            {
                // Look for folders in the following order.
                var temp =
                    Environment.GetEnvironmentVariable(
                        "ASPNETCORE_TEMP") ?? // ASPNETCORE_TEMP - User set temporary location.
                    Path.GetTempPath(); // Fall back.

                if (!Directory.Exists(temp)) throw new DirectoryNotFoundException(temp);

                _sTempDirectory = temp;
            }

            return _sTempDirectory;
        }
    }

    public static Func<string> TempDirectoryFactory => () => TempDirectory;

    public static IEndpointConventionBuilder MapGraphQL(this IEndpointRouteBuilder endpoints, string path = "/")
    {
        return endpoints.MapPost(path, async httpContext =>
        {
            var logger = httpContext.RequestServices.GetRequiredService<ILoggerFactory>()
                .CreateLogger(typeof(GraphZenApplicationBuilderExtensions));
            var request = httpContext.Request;
            var readStream = request.Body;
            if (!request.Body.CanSeek)
            {
                var memoryThreshold = DefaultMemoryThreshold;
                var contentLength = request.ContentLength.GetValueOrDefault();
                if (contentLength > 0 && contentLength < memoryThreshold)
                    memoryThreshold = (int)contentLength;

                readStream = new FileBufferingReadStream(request.Body, memoryThreshold, null,
                    TempDirectoryFactory);

                await readStream.DrainAsync(CancellationToken.None);
                readStream.Seek(0L, SeekOrigin.Begin);
            }

            ExecutionResult result;
            var graphQLContext = httpContext.RequestServices.GetRequiredService<GraphQLContext>();
            try
            {
                var req = await JsonSerializer.DeserializeAsync<GraphQLServerRequest>(readStream,
                    Json.SerializerOptions);
                var document = Parser.ParseDocument(req!.Query!);
                var queryValidator = httpContext.RequestServices.GetRequiredService<IQueryValidator>();
                var validationErrors = queryValidator.Validate(graphQLContext.Schema, document);

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
                            .FirstOrDefault(_ => _.Name?.Value == req.OperationName)
                        : operationDefinitions.First();

                    var rootClrType = operation?.OperationType == OperationType.Query
                        ? graphQLContext.Schema.QueryType.ClrType
                        : operation?.OperationType == OperationType.Mutation
                            ? graphQLContext.Schema.MutationType?.ClrType
                            : null;

                    var rootValue = rootClrType != null
                        ? httpContext.RequestServices.GetService(rootClrType)
                        : new { };

                    result = await new Executor().ExecuteAsync(graphQLContext.Schema, document,
                        rootValue,
                        graphQLContext, req.Variables, req.OperationName, new ExecutionOptions
                        {
                            ThrowOnError = false
                        });
                }
            }
            catch (GraphQLException gqlException)
            {
                logger.LogError(gqlException, "{Message}", gqlException.Message);
                result = new ExecutionResult(null, new[] { gqlException.GraphQLError });
            }
            catch (Exception e)
            {
                logger.LogError(e, "{Message}", e.Message);
                var coreOptions = graphQLContext.Options.GetExtension<CoreOptionsExtension>();
                var error = coreOptions.RevealInternalServerErrors
                    ? new GraphQLServerError(e.Message, innerException: e)
                    : new GraphQLServerError("An unknown error occured.");
                result = new ExecutionResult(null, new[] { error });
            }

            var resp = JsonSerializer.Serialize(result, Json.SerializerOptions);
            await httpContext.Response.WriteAsync(resp);
        });
    }
}
