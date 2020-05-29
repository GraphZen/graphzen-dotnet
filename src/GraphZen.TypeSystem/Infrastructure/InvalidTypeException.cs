// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public class InvalidTypeException : GraphQLException
    {
        public InvalidTypeException(string message) : base(message)
        {
        }

        public InvalidTypeException(string message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}