#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen
{
    public class GraphQLTypeAttribute : Attribute
    {
        public GraphQLTypeAttribute(string name)
        {
            Name = Check.NotNull(name, nameof(name));
        }

        public GraphQLTypeAttribute(Type clrType)
        {
            ClrType = Check.NotNull(clrType, nameof(clrType));
        }

        [CanBeNull]
        public string Name { get; }

        [CanBeNull]
        public Type ClrType { get; }
    }
}