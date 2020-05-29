// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NameSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Enums.EnumType.Values.EnumValue.Name
{
    [NoReorder]
    public class NameTests
    {
        [Spec(nameof(can_be_renamed))]
        [Fact]
        public void can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").Value("Bar", v => v.Name("Baz")); });
            var foo = schema.GetEnum("Foo");
            foo.HasValue("Bar").Should().BeFalse();
            foo.HasValue("Baz").Should().BeTrue();
        }


        [Spec(nameof(name_must_be_valid_name))]
        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("  ()(#$")]
        public void name_must_be_valid_name_(string name)
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo").Value("Bar", v =>

                {
                    Action rename = () => v.Name(name);
                    rename.Should().Throw<InvalidNameException>().WithMessage(
                        $"Cannot rename enum value Foo.Bar to \"{name}\". Names are limited to underscores and alpha-numeric ASCII characters.");
                });
            });
        }


        [Spec(nameof(name_cannot_be_null))]
        [Fact]
        public void name_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo").Value("Bar", v =>
                {
                    Action rename = () => v.Name(null!);
                    rename.Should().ThrowArgumentNullException("name");
                });
            });
        }


        [Spec(nameof(name_cannot_be_duplicate))]
        [Fact]
        public void name_cannot_be_duplicate_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo")
                    .Value("Bar")
                    .Value("Baz", v =>
                    {
                        Action rename = () => v.Name("Bar");
                        rename.Should().Throw<DuplicateItemException>().WithMessage(
                            "Cannot rename enum value Foo.Baz to \"Bar\": Enum Foo already contains a value named \"Bar\".");
                    });
            });
        }
    }
}