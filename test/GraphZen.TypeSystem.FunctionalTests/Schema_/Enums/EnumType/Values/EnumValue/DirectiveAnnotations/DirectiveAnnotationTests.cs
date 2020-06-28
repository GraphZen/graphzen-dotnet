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


namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Enums.EnumType.Values.EnumValue.DirectiveAnnotations
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
                _.Directive("Foo").Locations(DirectiveLocation.EnumValue);
                _.Enum("Foo").Value("Bar", v => { v.AddDirectiveAnnotation("Foo", "test"); });
            });
            schema.GetEnum("Foo").GetValue("Bar").FindDirectiveAnnotations("Foo").Single().Value.Should().Be("test");
        }


        [Spec(nameof(directive_annotation_cannot_be_added_unless_directive_is_defined))]
        [Fact]
        public void directive_annotation_cannot_be_added_unless_directive_is_defined_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo").Value("Bar", v =>
                {
                    Action add = () => v.AddDirectiveAnnotation("bar", "test");
                    add.Should().Throw<InvalidOperationException>().WithMessage(
                        "Cannot annotate enum value Foo.Bar with directive bar: Directive bar has not been defined yet.");
                });
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
                _.Enum("Foo").Value("Bar", v =>
                {
                    Action add = () => v.AddDirectiveAnnotation("bar", "test");
                    add.Should().Throw<InvalidOperationException>().WithMessage(
                        "Cannot annotate enum value Foo.Bar with directive bar: Directive bar cannot be annotated on enum values because it is only valid on queries, the schema, or arguments.");
                });
            });
        }


        [Spec(nameof(directive_annotation_cannot_be_added_with_null_name))]
        [Fact]
        public void directive_annotation_cannot_be_added_with_null_name_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo").Value("Bar", v =>
                    {
                        var adds = new List<Action>
                        {
                            () => v.AddDirectiveAnnotation(null!),
                            () => v.AddDirectiveAnnotation(null!, "test")
                        };
                        adds.ForEach(add => { add.Should().ThrowArgumentNullException("name"); });
                    }
                );
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
                _.Enum("Foo").Value("Bar", v =>
                {
                    var adds = new List<Action>
                    {
                        () => v.AddDirectiveAnnotation(name),
                        () => v.AddDirectiveAnnotation(name, "test")
                    };
                    adds.ForEach(add =>
                    {
                        add.Should().Throw<InvalidNameException>().WithMessage(
                            $"Cannot annotate enum value Foo.Bar with directive: \"{name}\" is not a valid directive name.");
                    });
                });
            });
        }


        [Spec(nameof(directive_annotations_can_be_removed))]
        [Fact]
        public void directive_annotations_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("foo").Locations(DirectiveLocation.EnumValue);
                _.Directive("bar").Locations(DirectiveLocation.EnumValue);
                _.Enum("Baz").Value("Bar", v =>
                {
                    v.AddDirectiveAnnotation("foo")
                        .AddDirectiveAnnotation("bar")
                        .ClearDirectiveAnnotations();
                });
            });
            schema.GetEnum("Baz").GetValue("Bar").DirectiveAnnotations.Should().BeEmpty();
        }


        [Spec(nameof(directive_annotations_can_be_removed_by_name))]
        [Fact]
        public void directive_annotations_can_be_removed_by_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("foo").Locations(DirectiveLocation.EnumValue);
                _.Directive("bar").Locations(DirectiveLocation.EnumValue);
                _.Enum("Baz").Value("Bar", v =>
                {
                    v.AddDirectiveAnnotation("foo")
                        .AddDirectiveAnnotation("bar")
                        .RemoveDirectiveAnnotations("bar");
                });
            });
            var bar = schema.GetEnum("Baz").GetValue("Bar");
            bar.DirectiveAnnotations.Should().HaveCount(1);
            bar.HasAnyDirectiveAnnotation("foo").Should().BeTrue();
            bar.HasAnyDirectiveAnnotation("bar").Should().BeFalse();
        }


        [Spec(nameof(directive_annotations_cannot_be_removed_by_name_with_null_name))]
        [Fact]
        public void directive_annotations_cannot_be_removed_by_name_with_null_name_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo").Value("Bar", v =>
                {
                    Action remove = () => v.RemoveDirectiveAnnotations(null!);
                    remove.Should().ThrowArgumentNullException("name");
                });
            });
        }


        [Spec(nameof(directive_annotations_are_removed_when_directive_is_removed))]
        [Fact]
        public void directive_annotations_are_removed_when_directive_is_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("foo").Locations(DirectiveLocation.EnumValue);
                _.Enum("Foo").Value("Baz", v => { v.AddDirectiveAnnotation("foo"); });
                _.RemoveDirective("foo");
            });
            var baz = schema.GetEnum("Foo").GetValue("Baz");
            baz.DirectiveAnnotations.Should().BeEmpty();
        }


        [Spec(nameof(directive_annotations_are_renamed_when_directive_is_renamed))]
        [Fact]
        public void directive_annotations_are_renamed_when_directive_is_renamed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("foo").Locations(DirectiveLocation.EnumValue);
                _.Enum("Baz").Value("Bar", v => { v.AddDirectiveAnnotation("foo", "test"); });
                _.Directive("foo").Name("bar");
            });
            var bar = schema.GetEnum("Baz").GetValue("Bar");
            bar.FindDirectiveAnnotations("bar").Single().Value.Should().Be("test");
        }
    }
}