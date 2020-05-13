// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.InputObjects.InputObjectType.Fields
{
    [NoReorder]
    public class FieldsTests
    {
        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added_via_sdl))]
        [Fact()]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => _.FromSchema(@"input Foo {foo: String } "));
            schema.GetInputObject("Foo").HasField("foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => _.FromSchema(@"extend input foo {foo: String } "));
            schema.GetInputObject("Foo").HasField("foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").Field("foo", "String"); });
            schema.GetInputObject("Foo").HasField("foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var d = _.InputObject("Foo");
                new List<Action>
                {
                    () => d.Field(null!),
                    () => d.Field(null!, a => { }),
                    () => d.Field(null!, "String"),
                    () => d.Field(null!, "String", a => { }),
                    () => d.Field<string>((string) null!),
                    () => d.Field<string>((string) null!, a => { })
                }.ForEach(a => a.Should().ThrowArgumentNullException("name"));
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
                var d = _.InputObject("Foo");
                var foo = d.GetInfrastructure<IInputObjectTypeDefinition>();
                new List<Action>
                {
                    () => d.Field(name),
                    () => d.Field(name, a => { }),
                    () => d.Field(name, "String"),
                    () => d.Field(name, "String", a => { }),
                    () => d.Field<string>(name),
                    () => d.Field<string>(name, a => { })
                }.ForEach(a => a.Should().Throw<InvalidNameException>()
                    .WithMessage(
                        $"Cannot create argument named \"{name}\" for {foo}: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.")
                );
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").Field("foo", "String", a => { a.Name("bar"); }); });
            var foo = schema.GetInputObject("Foo");
            foo.HasField("foo").Should().BeFalse();
            foo.HasField("bar").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("foo", "String", a =>
                {
                    Action rename = () => a.Name(null!);
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
                _.InputObject("Foo").Field("foo", "String", a =>
                {
                    Action rename = () => a.Name(name);
                    rename.Should().Throw<InvalidNameException>().WithMessage(
                        $"Cannot rename argument foo on directive Foo: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
                });
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("foo")
                    .Field("foo", "String")
                    .Field("bar", "String", a =>
                    {
                        Action rename = () => a.Name("foo");
                        rename.Should().Throw<DuplicateNameException>().WithMessage(
                            "Cannot rename argument bar to \"foo\": InputObject foo already contains an argument named \"foo\".");
                    });
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact(Skip = "todo")]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo")
                    .Field("foo", "String").RemoveField("foo");
            });
            schema.GetInputObject("Foo").HasField("foo").Should().BeFalse();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var d = _.InputObject("Foo")
                    .Field("foo", "String");
                Action remove = () => d.RemoveField(null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }
    }
}