// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentAssertions.Primitives;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public static class ObjectAssertionExtensions
    {
        [UsedImplicitly]
        public static AndConstraint<ObjectAssertions> BeEquivalentToJson(this ObjectAssertions objectAssertions,
            object expected)
        {
            return new AndConstraint<ObjectAssertions>(objectAssertions);
        }
    }
}