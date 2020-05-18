// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NameSpecs;


namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType.Name
{
    [NoReorder]
    public class NameTests
    {
        [Spec(nameof(name_cannot_be_duplicate))]
        [Fact]
        public void object_cannot_be_renamed_if_name_already_exists()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                var poco = _.Object("PlainClass");

                Action rename = () => { poco.Name("Foo"); };

                var pocoDef = _.GetDefinition().GetObject("PlainClass");
                var fooDef = _.GetDefinition().GetObject("Foo");

                rename.Should().Throw<DuplicateNameException>()
                    .WithMessage( "Cannot rename object PlainClass to \"Foo\", object Foo already exists. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(name_must_be_valid_name))]
        [Theory]
        [InlineData("  xy")]
        [InlineData("")]
        public void object_cannot_be_renamed_with_an_invalid_name(string name)
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                Action rename = () => _.Object("Foo").Name(name);
                rename.Should()
                    .Throw<InvalidNameException>()
                    .WithMessage(
                        $"Cannot rename object Foo: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(name_cannot_be_null))]
        [Fact]
        public void object_cannot_be_renamed_with_null_value()
        {
            Schema.Create(_ =>
            {
                var foo = _.Object("Foo");
                Action rename = () => foo.Name(null!);
                rename.Should().ThrowArgumentNullException("name");
            });
        }
    }
}