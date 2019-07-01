// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.Language.Internal;
using GraphZen.Types;
using JetBrains.Annotations;

namespace GraphZen.Execution
{
    public abstract class ExecutorHarness
    {
        protected readonly IExecutor Executor = new Executor();


        public Task<ExecutionResult> ExecuteAsync(Schema schema, DocumentSyntax document, object rootValue = null,
            dynamic variables = null, string operationName = null)
        {
            var vars = variables != null ? TestHelpers.ToDictionary(variables) : null;
            return Executor.ExecuteAsync(schema, document, rootValue, null, vars,
                operationName);
        }

        public Task<ExecutionResult> ExecuteAsync(Schema schema, string doc, object rootValue = null,
            dynamic variables = null, string operationName = null, bool throwOnError = false)
        {
            var vars = variables != null ? TestHelpers.ToDictionary(variables) : null;
            var ast = doc != null ? Parser.ParseDocument(doc) : null;
            return Executor.ExecuteAsync(schema, ast, rootValue, null, vars,
                operationName, new ExecutionOptions
                {
                    ThrowOnError = throwOnError
                });
        }
    }
}