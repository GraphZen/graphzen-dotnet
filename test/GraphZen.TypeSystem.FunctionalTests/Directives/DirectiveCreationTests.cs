﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSytemSpecs.ConfigurableItemSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Directives
{
    [Subject(nameof(Directive.Name))]
    public class DirectiveCreationTests : DirectiveSpecTest
    {
        [Fact(Skip = "obsolete")]
        public void it_can_create_directive_by_name()
        {
            Schema.Create(_ => { _.Object("Foo").DirectiveAnnotation("example"); });
        }

        [Fact]
        [Spec(nameof(Hello), Subject = "Howdy")]
        public void it_can_create_directive_from_sdl()
        {
            var schema = Schema.Create(@"directive @foo on FIELD");
            var foo = schema.FindDirective("foo");
            foo.Should().BeOfType<Directive>();
            foo.Name.Should().Be("foo");
        }

        [Fact(Skip = "TODO")]
        [Spec(nameof(Hello))]
        public void it_cannot_create_directive_from_sdl_without_directive_locations()
        {
            Action act = () => Schema.Create(@"directive @foo on FIELD");
            act.Should().Throw<InvalidOperationException>();
        }
    }
}