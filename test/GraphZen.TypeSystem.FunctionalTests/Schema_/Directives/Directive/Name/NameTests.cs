// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NameSpecs;


namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.Name
{
    [NoReorder]
    public class NameTests
    {
        [Spec(nameof(can_be_renamed))]
        [Fact]
        public void can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").Name("Bar"); });
            schema.HasDirective("Foo").Should().BeFalse();
            schema.HasDirective("Bar").Should().BeTrue();
        }


        [Spec(nameof(name_cannot_be_null))]
        [Fact]
        public void required_item_cannot_be_set_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Directive("Foo");
                Action rename = () => foo.Name(null!);
                rename.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(name_must_be_valid_name))]
        [Theory]
        [InlineData("{name}")]
        [InlineData("LKSJ ((")]
        [InlineData("   ")]
        [InlineData(" )*(#&  ")]
        public void named_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var foo = _.Directive("Foo");
                Action rename = () => foo.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot rename directive Foo to \"{name}\". Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(name_cannot_be_duplicate))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Directive("Bar");
                var foo = _.Directive("Foo");
                Action rename = () => foo.Name("Bar");
                rename.Should().Throw<DuplicateItemException>().WithMessage(
                    "Cannot rename directive Foo to \"Bar\": a directive named \"Bar\" already exists.");
            });
        }
    }
}