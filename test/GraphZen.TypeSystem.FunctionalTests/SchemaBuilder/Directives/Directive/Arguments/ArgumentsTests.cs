// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NamedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Directives.Directive.Arguments
{
    [NoReorder]
    public class ArgumentsTests
    {
        [Spec(nameof(named_item_can_be_added_via_sdl))]
        [Fact(Skip = "todo")]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => _.FromSchema(@"directive foo(foo: String) }"));
            schema.GetDirective("Foo").HasArgument("foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => _.FromSchema(@"extend directive foo(foo: String) }"));
            schema.GetDirective("Foo").HasArgument("foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").Argument("foo", "String"); });
            schema.GetDirective("Foo").HasArgument("foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var d = _.Directive("Foo");
                new List<Action>
                {
                    () => d.Argument(null!),
                    () => d.Argument(null!, a => { }),
                    () => d.Argument(null!, "String"),
                    () => d.Argument(null!, "String", a => { }),
                    () => d.Argument<string>(null!),
                    () => d.Argument<string>(null!, a => { })
                }.ForEach(a => a.Should().ThrowArgumentNullException("name"));
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
                var d = _.Directive("Foo");
                var foo = d.GetInfrastructure<IDirectiveDefinition>();
                new List<Action>
                {
                    () => d.Argument(name),
                    () => d.Argument(name, a => { }),
                    () => d.Argument(name, "String"),
                    () => d.Argument(name, "String", a => { }),
                    () => d.Argument<string>(name),
                    () => d.Argument<string>(name, a => { })
                }.ForEach(a => a.Should().Throw<InvalidNameException>()
                    .WithMessage(
                        $"Cannot create argument named \"{name}\" for {foo}: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.")
                );
            });
        }


        [Spec(nameof(named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").Argument("foo", "String", a => { a.Name("bar"); }); });
            var foo = schema.GetDirective("Foo");
            foo.HasArgument("foo").Should().BeFalse();
            foo.HasArgument("bar").Should().BeTrue();
        }


        


        [Spec(nameof(named_item_can_be_removed))]
        [Fact(Skip = "todo")]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("Foo")
                    .Argument("foo", "String").RemoveArgument("foo");
            });
            schema.GetDirective("Foo").HasArgument("foo").Should().BeFalse();
        }


        [Spec(nameof(named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var d = _.Directive("Foo")
                    .Argument("foo", "String");
                Action remove = () => d.RemoveArgument(null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }
    }
}