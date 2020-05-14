// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NameSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Unions.UnionType.Name
{
    [NoReorder]
    public class NameTests
    {
        [Spec(nameof(TypeSystemSpecs.RequiredSpecs.required_item_cannot_be_set_with_null_value))]
        [Fact]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Union("Foo");
                Action rename = () => foo.Name(null!);
                rename.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(name_must_be_valid_name))]
        [Theory]
        [InlineData(" 323")]
        [InlineData(" 32#()*3")]
        [InlineData("")]
        public void named_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var foo = _.Union("Foo");
                Action rename = () => foo.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename union Foo. \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(name_cannot_be_duplicate))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Union("Foo");
                var bar = _.Union("Bar");
                Action rename = () => bar.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    "Cannot rename union Bar to \"Foo\", union Foo already exists. All GraphQL type names must be unique.");
            });
        }
    }
}