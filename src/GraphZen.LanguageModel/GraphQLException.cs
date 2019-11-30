// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen
{
    public class GraphQLException : Exception
    {
        public GraphQLException(string message, params SyntaxNode[] nodes) : this(message, nodes, null)
        {
        }

        public GraphQLException(
            string message,
            IReadOnlyList<SyntaxNode>? nodes = null,
            Source? source = null,
            IReadOnlyList<int>? positions = null,
            ResponsePath? path = null,
            Exception? innerException = null
        ) : this(new GraphQLServerError(message, nodes, source, positions, path?.AsReadOnlyList(), innerException))
        {
        }


        internal GraphQLException(GraphQLServerError error) : base(Check.NotNull(error, nameof(error)).Message,
            error.InnerException)
        {
            GraphQLError = error;
        }


        public GraphQLServerError GraphQLError { get; }
    }
}