// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.QueryEngine;

public class Executor : IExecutor
{
    public Task<ExecutionResult> ExecuteAsync(Schema schema, DocumentSyntax document, object? rootValue,
        GraphQLContext? context,
        IDictionary<string, object>? variableValues = null, string? operationName = null,
        ExecutionOptions? options = null) =>
        ExecutionFunctions.ExecuteAsync(schema, document, rootValue, context, variableValues, operationName,
            options);
}