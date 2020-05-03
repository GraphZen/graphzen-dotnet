// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Fields.Field.DirectiveAnnotations
{
    [NoReorder]
    public abstract class FieldDirectiveAnnotationsTests
    {
        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotation_can_be_added))]
        [Fact]
        public void directive_annotation_can_be_added()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotation_cannot_be_added_unless_directive_is_defined))]
        [Fact]
        public void directive_annotation_cannot_be_added_unless_directive_is_defined()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotation_cannot_be_added_unless_location_is_valid))]
        [Fact]
        public void directive_annotation_cannot_be_added_unless_location_is_valid()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotation_cannot_be_added_with_invalid_name))]
        [Fact]
        public void directive_annotation_cannot_be_added_with_invalid_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotation_cannot_be_added_with_null_name))]
        [Fact]
        public void directive_annotation_cannot_be_added_with_null_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotation_cannot_be_upserted_unless_directive_is_defined))]
        [Fact]
        public void directive_annotation_cannot_be_upserted_unless_directive_is_defined()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotation_cannot_be_upserted_unless_location_is_valid))]
        [Fact]
        public void directive_annotation_cannot_be_upserted_unless_location_is_valid()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotation_cannot_be_upserted_with_invalid_name))]
        [Fact]
        public void directive_annotation_cannot_be_upserted_with_invalid_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotation_cannot_be_upserted_with_null_name))]
        [Fact]
        public void directive_annotation_cannot_be_upserted_with_null_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotations_are_removed_when_directive_is_ignored))]
        [Fact]
        public void directive_annotations_are_removed_when_directive_is_ignored()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotations_are_removed_when_directive_is_removed))]
        [Fact]
        public void directive_annotations_are_removed_when_directive_is_removed()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotations_can_be_removed))]
        [Fact]
        public void directive_annotations_can_be_removed()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotations_can_be_removed_by_name))]
        [Fact]
        public void directive_annotations_can_be_removed_by_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(DirectiveAnnotationSpecs.directive_annotations_cannot_be_removed_by_name_with_null_name))]
        [Fact]
        public void directive_annotations_cannot_be_removed_by_name_with_null_name()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }
    }

// Move FieldDirectiveAnnotationsTests into a separate file to start writing tests
    [NoReorder]
    public class FieldDirectiveAnnotationsTestsScaffold
    {
    }
}