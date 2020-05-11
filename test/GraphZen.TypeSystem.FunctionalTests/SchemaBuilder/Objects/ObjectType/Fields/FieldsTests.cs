// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects.ObjectType.Fields
{
    [NoReorder]
    public class FieldsTests
    {
        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => _.FromSchema(@"type Foo { bar: String }"));
            schema.GetObject("Foo").HasField("bar").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => _.FromSchema(@"extend type Foo { bar: String }"));
            schema.GetObject("Foo").HasField("bar").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Object("Foo").Field("foo", "String"); });
            schema.GetObject("Foo").HasField("foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            var schema = Schema.Create(_ =>
            {
                var foo = _.Object("Foo");
                Action add = () => foo.Field(null!);
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_added_with_invalid_name))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_added_with_invalid_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_renamed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_with_null_value))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_with_an_invalid_name))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_renamed_with_an_invalid_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_removed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_removed_with_null_value))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}