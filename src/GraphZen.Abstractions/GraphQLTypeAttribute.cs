// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public class GraphQLTypeAttribute : Attribute
    {
        public GraphQLTypeAttribute(Type clrType)
        {
            ClrType = Check.NotNull(clrType, nameof(clrType));
        }

        public Type ClrType { get; }
    }
}