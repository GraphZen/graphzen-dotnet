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


namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType.Fields.Field.Arguments.ArgumentDefinition.
    DirectiveAnnotations
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
                _.Directive("Foo").Locations(DirectiveLocation.ArgumentDefinition);
                _.Object("Foo").Field("Bar", "String",
                    f => { f.Argument("arg", "String", a => { a.AddDirectiveAnnotation("Foo", "test"); }); });
            });
            schema.GetObject("Foo").GetField("Bar").GetArgument("arg").FindDirectiveAnnotations("Foo").Single().Value
                .Should()
                .Be("test");
        }


        [Spec(nameof(directive_annotation_cannot_be_added_unless_directive_is_defined))]
        [Fact]
        public void directive_annotation_cannot_be_added_unless_directive_is_defined_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("Bar", "String", f =>
                {
                    f.Argument("arg", "String", a =>
                    {
                        Action add = () => a.AddDirectiveAnnotation("bar", "test");
                        add.Should().Throw<InvalidOperationException>().WithMessage(
                            "Cannot annotate object field argument Foo.Bar.arg with directive bar: Directive bar has not been defined yet.");
                    });
                });
            });
        }


        [Spec(nameof(directive_annotation_cannot_be_added_unless_location_is_valid))]
        [Fact]
        public void directive_annotation_cannot_be_added_unless_location_is_valid_()
        {
            Schema.Create(_ =>
            {
                _.Directive("bar").Locations(DirectiveLocation.Schema, DirectiveLocation.Query);
                _.Object("Foo").Field("Bar", "String", f =>
                {
                    f.Argument("arg", "String", a =>
                    {
                        Action add = () => a.AddDirectiveAnnotation("bar", "test");
                        add.Should().Throw<InvalidOperationException>().WithMessage(
                            "Cannot annotate object field argument Foo.Bar.arg with directive bar: Directive bar cannot be annotated on arguments because it is only valid on queries or the schema.");
                    });
                });
            });
        }


        [Spec(nameof(directive_annotation_cannot_be_added_with_null_name))]
        [Fact]
        public void directive_annotation_cannot_be_added_with_null_name_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("Bar", "String", f =>
                    {
                        f.Argument("arg", "String", a =>
                        {
                            var adds = new List<Action>
                            {
                                () => a.AddDirectiveAnnotation(null!),
                                () => a.AddDirectiveAnnotation(null!, "test")
                            };
                            adds.ForEach(add => { add.Should().ThrowArgumentNullException("name"); });
                        });
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
                _.Object("Foo").Field("Bar", "String", f =>
                {
                    f.Argument("arg", "String", a =>
                    {
                        var adds = new List<Action>
                        {
                            () => a.AddDirectiveAnnotation(name),
                            () => a.AddDirectiveAnnotation(name, "test")
                        };
                        adds.ForEach(add =>
                        {
                            add.Should().Throw<InvalidNameException>().WithMessage(
                                $"Cannot annotate object field argument Foo.Bar.arg with directive: \"{name}\" is not a valid directive name.");
                        });
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
                _.Directive("foo").Locations(DirectiveLocation.ArgumentDefinition);
                _.Directive("bar").Locations(DirectiveLocation.ArgumentDefinition);
                _.Object("Baz").Field("Bar", "String", f =>
                {
                    f.Argument("arg", "String", a =>
                    {
                        a.AddDirectiveAnnotation("foo")
                            .AddDirectiveAnnotation("bar")
                            .ClearDirectiveAnnotations();
                    });
                });
            });
            schema.GetObject("Baz").GetField("Bar").GetArgument("arg").DirectiveAnnotations.Should().BeEmpty();
        }


        [Spec(nameof(directive_annotations_can_be_removed_by_name))]
        [Fact]
        public void directive_annotations_can_be_removed_by_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("foo").Locations(DirectiveLocation.ArgumentDefinition);
                _.Directive("bar").Locations(DirectiveLocation.ArgumentDefinition);
                _.Object("Baz").Field("Bar", "String", f =>
                {
                    f.Argument("arg", "String", a =>
                    {
                        a.AddDirectiveAnnotation("foo")
                            .AddDirectiveAnnotation("bar")
                            .RemoveDirectiveAnnotations("bar");
                    });
                });
            });
            var arg = schema.GetObject("Baz").GetField("Bar").GetArgument("arg");
            arg.DirectiveAnnotations.Should().HaveCount(1);
            arg.HasAnyDirectiveAnnotation("foo").Should().BeTrue();
            arg.HasAnyDirectiveAnnotation("bar").Should().BeFalse();
        }


        [Spec(nameof(directive_annotations_cannot_be_removed_by_name_with_null_name))]
        [Fact]
        public void directive_annotations_cannot_be_removed_by_name_with_null_name_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("Bar", "String", f =>
                {
                    f.Argument("arg", "String", a =>
                    {
                        Action remove = () => a.RemoveDirectiveAnnotations(null!);
                        remove.Should().ThrowArgumentNullException("name");
                    });
                });
            });
        }


        [Spec(nameof(directive_annotations_are_removed_when_directive_is_removed))]
        [Fact]
        public void directive_annotations_are_removed_when_directive_is_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("foo").Locations(DirectiveLocation.ArgumentDefinition);
                _.Object("Foo").Field("Baz", "String",
                    f => { f.Argument("arg", "String", a => { a.AddDirectiveAnnotation("foo"); }); });
                _.RemoveDirective("foo");
            });
            var arg = schema.GetObject("Foo").GetField("Baz").GetArgument("arg");
            arg.DirectiveAnnotations.Should().BeEmpty();
        }


        [Spec(nameof(directive_annotations_are_renamed_when_directive_is_renamed))]
        [Fact]
        public void directive_annotations_are_renamed_when_directive_is_renamed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("foo").Locations(DirectiveLocation.ArgumentDefinition);
                _.Object("Baz").Field("Bar", "String",
                    f => { f.Argument("arg", "String", a => { a.AddDirectiveAnnotation("foo", "test"); }); });
                _.Directive("foo").Name("bar");
            });
            var arg = schema.GetObject("Baz").GetField("Bar").GetArgument("arg");
            arg.FindDirectiveAnnotations("bar").Single().Value.Should().Be("test");
        }
    }
}