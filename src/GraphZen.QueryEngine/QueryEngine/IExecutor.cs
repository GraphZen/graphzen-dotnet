// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.QueryEngine
{
    public interface IExecutor
    {
        Task<ExecutionResult> ExecuteAsync(Schema schema, DocumentSyntax document, object rootValue = null,
            GraphQLContext context = null,
            IDictionary<string, object> variableValues = null, string operationName = null,
            ExecutionOptions options = null);
    }
}