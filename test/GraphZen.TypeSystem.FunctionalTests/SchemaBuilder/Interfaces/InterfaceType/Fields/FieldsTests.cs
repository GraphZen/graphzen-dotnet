// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Fields
{
    [NoReorder]
    public class FieldsTests
    {
        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => _.FromSchema(@"interface Foo { bar: String }"));
            schema.GetInterface("Foo").HasField("bar").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => _.FromSchema(@"extend interface Foo { bar: String }"));
            schema.GetInterface("Foo").HasField("bar").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").Field("foo", "String"); });
            schema.GetInterface("Foo").HasField("foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Interface("Foo");
                Action add = () => foo.Field(null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_added_with_invalid_name))]
        [Theory]
        [InlineData("{name}")]
        [InlineData("sdfa asf")]
        [InlineData("sdf*(#&aasf")]
        public void named_item_cannot_be_added_with_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var foo = _.Interface("Foo");
                Action add = () => foo.Field(name);
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot get or create GraphQL field builder for field \"{name}\" on interface Foo. The field name \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").Field("foo", "String", f => f.Name("bar")); });
            var foo = schema.GetInterface("Foo");
            foo.HasField("foo").Should().BeFalse();
            foo.HasField("bar").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo").Field("bar", "String", f =>
                {
                    Action rename = () => f.Name(null!);
                    rename.Should().ThrowArgumentNullException("name");
                });
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_with_an_invalid_name))]
        [Theory]
        [InlineData("{name}")]
        [InlineData("sdfa asf")]
        [InlineData("sdf*(#&aasf")]
        public void named_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo").Field("bar", "String", f =>
                {
                    Action rename = () => f.Name(name);
                    rename.Should().Throw<InvalidNameException>().WithMessage(
                        $"Cannot rename field bar on interface Foo: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
                });
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo")
                    .Field("bar", "String")
                    .Field("baz", "String", f =>
                    {
                        Action rename = () => f.Name("bar");
                        rename.Should().Throw<DuplicateNameException>().WithMessage(
                            "Cannot rename field baz to \"bar\": Interface Foo already contains a field named \"bar\".");
                    });
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").Field("field", "String").RemoveField("field"); });
            schema.GetInterface("Foo").HasField("field").Should().BeFalse();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Interface("Foo").Field("field", "String");
                Action remove = () => foo.RemoveField(null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }
    }
}