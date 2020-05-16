// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NamedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType.Fields.Field.Arguments
{
    [NoReorder]
    public class NamedCollectionTests
    {





        [Spec(nameof(named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("foo", "String", f => { f.Argument("foo", "String"); });
            });
            schema.GetObject("Foo").GetField("foo").HasArgument("foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("foo", "String", f =>
                {
                    new List<Action>
                    {
                        () => f.Argument(null!),
                        () => f.Argument(null!, a => { }),
                        () => f.Argument(null!, "String"),
                        () => f.Argument(null!, "String", a => { }),
                        () => f.Argument<string>(null!),
                        () => f.Argument<string>(null!, a => { })
                    }.ForEach(a => a.Should().ThrowArgumentNullException("name"));
                });
            });
        }


        [Spec(nameof(named_item_cannot_be_added_with_invalid_name))]
        [Theory]
        [InlineData("{name}")]
        [InlineData("sdfa asf")]
        [InlineData("sdf*(#&aasf")]
        public void named_item_cannot_be_added_with_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("foo", "String", f =>
                {
                    var foo = f.GetInfrastructure<IFieldDefinition>();
                    new List<Action>
                    {
                        () => f.Argument(name),
                        () => f.Argument(name, a => { }),
                        () => f.Argument(name, "String"),
                        () => f.Argument(name, "String", a => { }),
                        () => f.Argument<string>(name),
                        () => f.Argument<string>(name, a => { })
                    }.ForEach(a => a.Should().Throw<InvalidNameException>()
                        .WithMessage(
                            $"Cannot create argument named \"{name}\" for {foo}: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.")
                    );
                });
            });
        }


        [Spec(nameof(named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("foo", "String",
                    f => { f.Argument("foo", "String", a => { a.Name("bar"); }); });
            });
            var foo = schema.GetObject("Foo").GetField("foo");
            foo.HasArgument("foo").Should().BeFalse();
            foo.HasArgument("bar").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_removed))]
        [Fact]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo")
                    .Field("foo", "String", f => { f.Argument("foo", "String").RemoveArgument("foo"); });
            });
            schema.GetObject("Foo").GetField("foo").HasArgument("foo").Should().BeFalse();
        }


        [Spec(nameof(named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo")
                    .Field("foo", "String", f =>
                    {
                        f.Argument("foo", "String");
                        Action remove = () => f.RemoveArgument(null!);
                        remove.Should().ThrowArgumentNullException("name");
                    });
            });
        }
    }
}