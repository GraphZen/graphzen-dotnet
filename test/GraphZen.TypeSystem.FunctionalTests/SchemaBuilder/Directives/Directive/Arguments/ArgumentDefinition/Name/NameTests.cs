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


namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Directives.Directive.Arguments.ArgumentDefinition.Name
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
                _.Directive("Foo").Argument("foo", "String", a =>
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
                _.Directive("Foo").Argument("foo", "String", a =>
                {
                    Action rename = () => a.Name(name);
                    rename.Should().Throw<InvalidNameException>().WithMessage(
                        $"Cannot rename argument foo on directive Foo: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
                });
            });
        }


        [Spec(nameof(name_cannot_be_duplicate))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Directive("foo")
                    .Argument("foo", "String")
                    .Argument("bar", "String", a =>
                    {
                        Action rename = () => a.Name("foo");
                        rename.Should().Throw<DuplicateNameException>().WithMessage(
                            "Cannot rename argument bar to \"foo\": Directive foo already contains an argument named \"foo\".");
                    });
            });
        }
    }
}