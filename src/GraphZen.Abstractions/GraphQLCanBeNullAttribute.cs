// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


namespace GraphZen
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Parameter)]
    public class GraphQLCanBeNullAttribute : Attribute
    {
    }
}