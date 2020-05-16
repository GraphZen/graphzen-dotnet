// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NamedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Scalars
{
    [NoReorder]
    public class NamedCollectionTests
    {
        [Spec(nameof(named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo"); });
            schema.HasScalar("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Scalar((string)null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(named_item_cannot_be_added_with_invalid_name))]
        [Theory]
        [InlineData("xx 09880")]
        public void named_item_cannot_be_added_with_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Scalar(name);
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot get or create GraphQL type builder for scalar named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Scalar("Foo").Name("Bar"); });
            schema.HasScalar("Foo").Should().BeFalse();
            schema.HasScalar("Bar").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_removed))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Scalar("Foo");
                _.RemoveScalar("Foo");
            });
            schema.HasScalar("Foo").Should().BeFalse();
        }


        [Spec(nameof(named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveScalar((string)null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }
    }
}