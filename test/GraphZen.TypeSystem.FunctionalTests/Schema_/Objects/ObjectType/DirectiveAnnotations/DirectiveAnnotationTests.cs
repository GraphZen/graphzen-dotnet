// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.DirectiveAnnotationSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType.DirectiveAnnotations
{
    [NoReorder]
    public class DirectiveAnnotationTests
    {
        [Spec(nameof(directive_annotation_can_be_added))]
        [Fact]
        public void directive_annotation_can_be_added_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("Foo").Locations(DirectiveLocation.Object);
                _.Object("Foo").AddDirectiveAnnotation("Foo", "test");
            });
            schema.GetObject("Foo").FindDirectiveAnnotations("Foo").Single().Value.Should().Be("test");
        }


        [Spec(nameof(directive_annotation_cannot_be_added_unless_directive_is_defined))]
        [Fact]
        public void directive_annotation_cannot_be_added_unless_directive_is_defined_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Object("Foo");
                Action add = () => foo.AddDirectiveAnnotation("bar", "test");
                add.Should().Throw<InvalidOperationException>().WithMessage(
                    "Cannot annotate object Foo with directive bar: Directive bar has not been defined yet.");
            });
        }


        [Spec(nameof(directive_annotation_cannot_be_added_unless_location_is_valid))]
        [Fact]
        public void directive_annotation_cannot_be_added_unless_location_is_valid_()
        {
            Schema.Create(_ =>
            {
                _.Directive("bar").Locations(DirectiveLocation.ArgumentDefinition, DirectiveLocation.Schema,
                    DirectiveLocation.Query);
                var foo = _.Object("Foo");
                Action add = () => foo.AddDirectiveAnnotation("bar", "test");
                add.Should().Throw<InvalidOperationException>().WithMessage(
                    "Cannot annotate object Foo with directive bar: Directive bar cannot be annotated on objects because it is only valid on queries, the schema, or arguments.");
            });
        }


        [Spec(nameof(directive_annotation_cannot_be_added_with_null_name))]
        [Fact]
        public void directive_annotation_cannot_be_added_with_null_name_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Object("Foo");
                var adds = new List<Action>
                {
                    () => foo.AddDirectiveAnnotation(null!),
                    () => foo.AddDirectiveAnnotation(null!, "test")
                };
                adds.ForEach(add => { add.Should().ThrowArgumentNullException("name"); });
            });
        }


        [Spec(nameof(directive_annotation_cannot_be_added_with_invalid_name))]
        [Theory]
        [InlineData("{name}")]
        [InlineData("sdfa asf")]
        [InlineData("sdf*(#&aasf")]
        public void directive_annotation_cannot_be_added_with_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var foo = _.Object("Foo");
                var adds = new List<Action>
                {
                    () => foo.AddDirectiveAnnotation(name),
                    () => foo.AddDirectiveAnnotation(name, "test")
                };
                adds.ForEach(add =>
                {
                    add.Should().Throw<InvalidNameException>().WithMessage(
                        $"Cannot annotate object Foo with directive: \"{name}\" is not a valid directive name.");
                });
            });
        }


        [Spec(nameof(directive_annotations_can_be_removed))]
        [Fact]
        public void directive_annotations_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("foo").Locations(DirectiveLocation.Object);
                _.Directive("bar").Locations(DirectiveLocation.Object);
                _.Object("Baz")
                    .AddDirectiveAnnotation("foo")
                    .AddDirectiveAnnotation("bar")
                    .RemoveDirectiveAnnotations();
            });
            schema.GetObject("Baz").DirectiveAnnotations.Should().BeEmpty();
        }


        [Spec(nameof(directive_annotations_can_be_removed_by_name))]
        [Fact]
        public void directive_annotations_can_be_removed_by_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("foo").Locations(DirectiveLocation.Object);
                _.Directive("bar").Locations(DirectiveLocation.Object);
                _.Object("Baz")
                    .AddDirectiveAnnotation("foo")
                    .AddDirectiveAnnotation("bar")
                    .RemoveDirectiveAnnotations("bar");
            });
            var baz = schema.GetObject("Baz");
            baz.DirectiveAnnotations.Should().HaveCount(1);
            baz.HasAnyDirectiveAnnotation("foo").Should().BeTrue();
            baz.HasAnyDirectiveAnnotation("bar").Should().BeFalse();
        }


        [Spec(nameof(directive_annotations_cannot_be_removed_by_name_with_null_name))]
        [Fact]
        public void directive_annotations_cannot_be_removed_by_name_with_null_name_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Object("Foo");
                Action remove = () => foo.RemoveDirectiveAnnotations(null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(directive_annotations_are_removed_when_directive_is_removed))]
        [Fact]
        public void directive_annotations_are_removed_when_directive_is_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("foo").Locations(DirectiveLocation.Object);
                _.Object("Baz")
                    .AddDirectiveAnnotation("foo");
                _.RemoveDirective("foo");
            });
            var baz = schema.GetObject("Baz");
            baz.DirectiveAnnotations.Should().BeEmpty();
        }


        [Spec(nameof(directive_annotations_are_removed_when_directive_is_ignored))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_are_removed_when_directive_is_ignored_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}