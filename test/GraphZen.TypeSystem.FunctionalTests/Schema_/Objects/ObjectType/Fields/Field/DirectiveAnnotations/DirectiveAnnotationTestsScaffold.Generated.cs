// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.DirectiveAnnotationSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType.Fields.Field.DirectiveAnnotations
{
    [NoReorder]
    public abstract class DirectiveAnnotationTests
    {
        [Spec(nameof(directive_annotation_can_be_added))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_can_be_addedschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotation_cannot_be_added_unless_directive_is_defined))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_added_unless_directive_is_definedschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotation_cannot_be_upserted_unless_directive_is_defined))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_upserted_unless_directive_is_definedschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotation_cannot_be_added_unless_location_is_valid))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_added_unless_location_is_validschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotation_cannot_be_upserted_unless_location_is_valid))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_upserted_unless_location_is_validschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotation_cannot_be_added_with_null_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_added_with_null_nameschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotation_cannot_be_upserted_with_null_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_upserted_with_null_nameschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotation_cannot_be_added_with_invalid_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_added_with_invalid_nameschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotation_cannot_be_upserted_with_invalid_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotation_cannot_be_upserted_with_invalid_nameschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotations_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_can_be_removedschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotations_can_be_removed_by_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_can_be_removed_by_nameschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotations_cannot_be_removed_by_name_with_null_name))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_cannot_be_removed_by_name_with_null_nameschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotations_are_removed_when_directive_is_removed))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_are_removed_when_directive_is_removedschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(directive_annotations_are_removed_when_directive_is_ignored))]
        [Fact(Skip = "TODO")]
        public void directive_annotations_are_removed_when_directive_is_ignoredschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move DirectiveAnnotationTests into a separate file to start writing tests
    [NoReorder]
    public class DirectiveAnnotationTestsScaffold
    {
    }
}
// Source Hash Code: 5969292903117790265