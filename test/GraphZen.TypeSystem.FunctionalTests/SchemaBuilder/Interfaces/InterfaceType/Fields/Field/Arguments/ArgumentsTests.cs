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

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Fields.Field.Arguments
{
    [NoReorder]
    public class ArgumentsTests
    {
        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => _.FromSchema(@"interface Foo { foo(foo: String): String }"));
            schema.GetInterface("Foo").GetField("foo").HasArgument("foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => _.FromSchema(@"extend interface Foo { foo(foo: String): String }"));
            schema.GetInterface("Foo").GetField("foo").HasArgument("foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface("Foo").Field("foo", "String", f => { f.Argument("foo", "String"); });
            });
            schema.GetInterface("Foo").GetField("foo").HasArgument("foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo").Field("foo", "String", f =>
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


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_added_with_invalid_name))]
        [Theory]
        [InlineData("{name}")]
        [InlineData("sdfa asf")]
        [InlineData("sdf*(#&aasf")]
        public void named_item_cannot_be_added_with_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo").Field("foo", "String", f =>
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


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface("Foo").Field("foo", "String",
                    f => { f.Argument("foo", "String", a => { a.Name("bar"); }); });
            });
            var foo = schema.GetInterface("Foo").GetField("foo");
            foo.HasArgument("foo").Should().BeFalse();
            foo.HasArgument("bar").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_with_null_value))]
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


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_with_an_invalid_name))]
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


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
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


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface("Foo")
                    .Field("foo", "String", f => { f.Argument("foo", "String").RemoveArgument("foo"); });
            });
            schema.GetInterface("Foo").GetField("foo").HasArgument("foo").Should().BeFalse();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo")
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