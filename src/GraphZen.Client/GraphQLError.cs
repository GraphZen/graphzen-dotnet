﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public class GraphQLError
    {
        public GraphQLError(string message)
        {
            Check.NotNull(message, nameof(message));
            Message = message;
        }

        public string Message { get; }
    }
}