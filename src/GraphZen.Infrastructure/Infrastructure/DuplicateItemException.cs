// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public class DuplicateItemException : GraphQLException
    {
        public DuplicateItemException(string message) : base(message)
        {
        }
    }

    public class ItemNotFoundException : GraphQLException
    {
        public ItemNotFoundException([JetBrains.Annotations.NotNull] string message) : base(message)
        {
        }

        public ItemNotFoundException([JetBrains.Annotations.NotNull] string message, [CanBeNull] Exception? innerException) : base(message, innerException)
        {
        }
    }
}