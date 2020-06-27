// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_
{
    [NoReorder]
    public class MemberTests
    {
        [Spec(nameof(TypeSystemSpecs.MemberSpecs.should_be_included_in_schema_descendants))]
        [Fact]
        public void should_be_included_in_schema_descendants_()
        {
            var schema = Schema.Create();
            schema.DescendantsAndSelf().Should().ContainSingle(_ => _ == schema);
        }


        [Spec(nameof(TypeSystemSpecs.MemberSpecs.should_be_included_in_schema_definition_descendants))]
        [Fact]
        public void should_be_included_in_schema_definition_descendants_()
        {
            Schema.Create(_ =>
            {
                var schema = _.GetDefinition();
                schema.DescendantsAndSelf().Should().ContainSingle(d => d == schema);
            });
        }
    }
}