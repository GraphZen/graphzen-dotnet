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

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces.InterfaceType.Fields.Field.Arguments.ArgumentDefinition.Name
{
    [NoReorder]
    public class NameTests
    {
        [Spec(nameof(TypeSystemSpecs.NameSpecs.can_be_renamed))]
        [Fact]
        public void can_be_renamed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface("Foo").Field("foo", "String", f =>
                {
                    f.Argument("foo", "String", a =>
                    {
                        a.Name("bar");
                    });
                });
            });
            var foo = schema.GetInterface("Foo").GetField("foo");
            foo.HasArgument("foo").Should().BeFalse();
            foo.HasArgument("bar").Should().BeTrue();
        }

        [Spec(nameof(name_cannot_be_null))]
        [Fact]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo").Field("bar", "String", f =>
                {
                    f.Argument("foo", "String", a =>
                    {
                        Action rename = () => a.Name(null!);
                        rename.Should().ThrowArgumentNullException("name");
                    });
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
                    f.Argument("foo", "String", a =>
                    {
                        Action rename = () => a.Name(name);
                        rename.Should().Throw<InvalidNameException>().WithMessage(
                            $"Cannot rename argument foo on field bar on interface Foo: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
                    });
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
                    .Field("foo", "String", f =>
                    {
                        f.Argument("foo", "String")
                            .Argument("bar", "String", a =>
                            {
                                Action rename = () => a.Name("foo");
                                rename.Should().Throw<DuplicateNameException>().WithMessage(
                                    "Cannot rename argument bar to \"foo\": Field foo on interface Foo already contains an argument named \"foo\".");
                            });
                    });
            });
        }
    }
}