// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable PartialTypeWithSinglePart
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Unions.UnionType.MemberTypes
{
    public partial class UnionTypeMemberTypesTests
    {
// Priority: Low
// Subject Name: MemberTypes
        [Spec(nameof(NamedTypeSetSpecs.set_item_can_be_added))]
        [Fact]
        public void set_item_can_be_added()
        {
            var schema = Schema.Create(_ => { });
        }


// Priority: Low
// Subject Name: MemberTypes
        [Spec(nameof(NamedTypeSetSpecs.set_item_can_be_removed))]
        [Fact]
        public void set_item_can_be_removed()
        {
            var schema = Schema.Create(_ => { });
        }


// Priority: Low
// Subject Name: MemberTypes
        [Spec(nameof(NamedTypeSetSpecs.set_item_must_be_valid_name))]
        [Fact]
        public void set_item_must_be_valid_name()
        {
            var schema = Schema.Create(_ => { });
        }
    }
}