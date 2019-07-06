// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen
{
    [AttributeUsage(AttributeTargets.Class
                    | AttributeTargets.Enum
                    | AttributeTargets.Field
                    | AttributeTargets.Method
                    | AttributeTargets.Property
                    | AttributeTargets.Parameter
                    | AttributeTargets.Interface)]
    public class GraphQLNameAttribute : Attribute
    {
        public GraphQLNameAttribute(string name)
        {
            Name = Check.NotNull(name, nameof(name));
        }

        [NotNull]
        public string Name { get; }
    }
}