// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NameSpecs;


namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Fields.Field.Name
{
    [NoReorder]
    public class NameTests
    {
        [Spec(nameof(name_cannot_be_null))]
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


        [Spec(nameof(name_must_be_valid_name))]
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


        [Spec(nameof(name_cannot_be_duplicate))]
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
    }
}