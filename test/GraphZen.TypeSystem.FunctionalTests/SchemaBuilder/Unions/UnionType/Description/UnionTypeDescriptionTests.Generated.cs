// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable PartialTypeWithSinglePart
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Unions.UnionType.Description
{
    public partial class UnionTypeDescriptionTests
    {
// Priority: Low
// Subject Name: Description
        [Spec(nameof(UpdateableSpecs.it_can_be_updated))]
        [Fact]
        public void it_can_be_updated()
        {
            var schema = Schema.Create(_ => { });
        }


// Priority: Low
// Subject Name: Description
        [Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
        [Fact]
        public void optional_item_can_be_removed()
        {
            var schema = Schema.Create(_ => { });
        }


// Priority: Low
// Subject Name: Description
        [Spec(nameof(OptionalSpecs.parent_can_be_created_without))]
        [Fact]
        public void parent_can_be_created_without()
        {
            var schema = Schema.Create(_ => { });
        }
    }
}