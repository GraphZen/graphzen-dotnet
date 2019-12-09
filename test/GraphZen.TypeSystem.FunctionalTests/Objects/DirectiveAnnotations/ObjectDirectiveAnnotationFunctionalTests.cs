// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Objects.DirectiveAnnotations
{
    public class ObjectDirectiveAnnotationFunctionalTests
    {
        [Fact]
        public void it_should_throw_adding_directive_with_invalid_directive_location()
        {
            Schema.Create(_ =>
            {
                Action act = () => _.Object("Foo").DirectiveAnnotation("unknown");
                act.Should().Throw<InvalidOperationException>()
                    .WithMessage(
                        "Unknown directive: cannot add 'unknown' directive to the object 'Foo'. Ensure the 'unknown' directive is defined in the schema before it is used.");
            });
        }

        [Fact]
        public void it_should_throw_on_adding_with_invalid_location()
        {
            Schema.Create(_ =>
            {
                var directiveLocations = Enum.GetValues(typeof(DirectiveLocation)).Cast<DirectiveLocation>()
                    .Where(_ => _ != DirectiveLocation.Object).ToArray();
                _.Directive("notObject").Locations(directiveLocations);

                Action act = () => _.Object("Foo").DirectiveAnnotation("notObject");
                act.Should().Throw<InvalidOperationException>()
                    .WithMessage(
                        $"Invalid directive location: the 'notObject' directive cannot be annotated on the object 'Foo'. The 'notObject' directive is only valid on a {directiveLocations.Select(_ => _.GetDisplayValue()).OrList()}.");
            });
        }

        [Fact]
        public void it_should_throw_on_adding_with_invalid_location_without_defined_locations()
        {
            Schema.Create(_ =>
            {
                _.Directive("notObject");

                Action act = () => _.Object("Foo").DirectiveAnnotation("notObject");
                act.Should().Throw<InvalidOperationException>()
                    .WithMessage(
                        "Invalid directive location: the 'notObject' directive cannot be annotated on the object 'Foo'.");
            });
        }
    }
}