// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NameSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.InputObjects.InputObjectType.Fields.InputField.Name
{
    [NoReorder]
    public class NameTests
    {
        [Spec(nameof(can_be_renamed))]
        [Fact]
        public void can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").Field("Bar", "String", f => { f.Name("Baz"); }); });
            var foo = schema.GetInputObject("Foo");
            foo.HasField("Bar").Should().BeFalse();
            foo.HasField("Baz").Should().BeTrue();
        }

        [Spec(nameof(name_cannot_be_null))]
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


        [Spec(nameof(name_must_be_valid_name))]
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
                        $"Cannot rename input object field Foo.foo to \"{name}\". Names are limited to underscores and alpha-numeric ASCII characters.");
                });
            });
        }


        [Spec(nameof(name_cannot_be_duplicate))]
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
                        rename.Should().Throw<DuplicateItemException>().WithMessage(
                            "Cannot rename input object field foo.bar to \"foo\". Input object foo already contains a field named \"foo\".");
                    });
            });
        }
    }
}