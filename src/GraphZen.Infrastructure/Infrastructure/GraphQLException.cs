// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public abstract class GraphQLException : Exception
    {
        protected GraphQLException(string message) : base(message)
        {
        }

        protected GraphQLException(string message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}