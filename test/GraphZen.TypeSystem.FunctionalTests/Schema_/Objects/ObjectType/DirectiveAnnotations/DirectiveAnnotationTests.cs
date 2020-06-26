// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
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
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_added_with_null_name_()
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


        [Spec(nameof(DEPRECATED_directive_annotation_cannot_be_upserted_with_null_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_upserted_with_null_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotation_cannot_be_added_with_invalid_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_added_with_invalid_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(DEPRECATED_directive_annotation_cannot_be_upserted_with_invalid_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_upserted_with_invalid_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotations_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_can_be_removed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotations_can_be_removed_by_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_can_be_removed_by_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotations_cannot_be_removed_by_name_with_null_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_cannot_be_removed_by_name_with_null_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotations_are_removed_when_directive_is_removed))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_are_removed_when_directive_is_removed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotations_are_removed_when_directive_is_ignored))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_are_removed_when_directive_is_ignored_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}