// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.TypeSystem
{
    internal static class TestExt
    {
        public static void ShouldPass(this IEnumerable<GraphQLError> errors)
        {
            errors.Should().BeEmpty();
        }
    }
}