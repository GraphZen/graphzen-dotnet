// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType.DirectiveAnnotations
{
    [NoReorder]
    public class DirectiveAnnotationTests
    {
        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs.directive_annotation_can_be_added))]
        [Fact(Skip = "wip")]
        public void directive_annotation_can_be_added_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("Foo");
                _.Object("Foo").AddDirectiveAnnotation("Foo", "test");
            });
            var directive = schema.GetObject("Foo").FindDirectiveAnnotations("Foo").Single();
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs
            .directive_annotation_cannot_be_added_unless_directive_is_defined))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_added_unless_directive_is_defined_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs
            .directive_annotation_cannot_be_upserted_unless_directive_is_defined))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_upserted_unless_directive_is_defined_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs
            .directive_annotation_cannot_be_added_unless_location_is_valid))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_added_unless_location_is_valid_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs
            .directive_annotation_cannot_be_upserted_unless_location_is_valid))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_upserted_unless_location_is_valid_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs.directive_annotation_cannot_be_added_with_null_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_added_with_null_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs.directive_annotation_cannot_be_upserted_with_null_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_upserted_with_null_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs.directive_annotation_cannot_be_added_with_invalid_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_added_with_invalid_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs
            .directive_annotation_cannot_be_upserted_with_invalid_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_upserted_with_invalid_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs.directive_annotations_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_can_be_removed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs.directive_annotations_can_be_removed_by_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_can_be_removed_by_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs
            .directive_annotations_cannot_be_removed_by_name_with_null_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_cannot_be_removed_by_name_with_null_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs
            .directive_annotations_are_removed_when_directive_is_removed))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_are_removed_when_directive_is_removed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.DirectiveAnnotationSpecs
            .directive_annotations_are_removed_when_directive_is_ignored))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_are_removed_when_directive_is_ignored_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}