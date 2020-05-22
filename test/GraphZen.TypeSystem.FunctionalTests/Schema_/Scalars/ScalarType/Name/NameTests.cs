// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NameSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Scalars.ScalarType.Name
{
    [NoReorder]
    public class NameTests
    {
        [Spec(nameof(can_be_renamed))]
        [Fact]
        public void can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").Name("Bar"); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.HasScalar("Bar").Should().BeTrue();
        }

        [Spec(nameof(name_cannot_be_null))]
        [Fact]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Scalar("Foo");
                Action rename = () => foo.Name(null!);
                rename.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(name_must_be_valid_name))]
        [Theory]
        [InlineData("")]
        public void named_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var foo = _.Scalar("Foo");
                Action rename = () => foo.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename scalar Foo: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(name_cannot_be_duplicate))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Scalar("Foo");
                var bar = _.Scalar("Bar");
                Action rename = () => bar.Name("Foo");
                rename.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot rename scalar Bar to \"Foo\": a type with that name (scalar Foo) already exists. All GraphQL type names must be unique.");
            });
        }
    }
}