#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;

namespace GraphZen
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GraphQLNonNullAttribute : Attribute
    {
    }
}