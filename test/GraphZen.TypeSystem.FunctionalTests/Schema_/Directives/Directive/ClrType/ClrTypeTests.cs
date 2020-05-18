// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.


using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;


namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.ClrType
{
    [NoReorder]
    public class ClrTypeTests
    {
        public class PlainClass
        {
        }

        [GraphQLName(AnnotatedNameValue)]
        public class PlainClassAnnotatedName
        {
            public const string AnnotatedNameValue = nameof(AnnotatedNameValue);
        }

        [GraphQLName("(*&#")]
        public class PlainClassInvalidNameAnnotation
        {
        }

        [Spec(nameof(clr_type_can_be_added))]
        [Fact]
        public void clr_type_can_be_added_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive("Foo").ClrType(typeof(PlainClass));
            });
            schema.GetDirective("Foo").ClrType.Should().Be<PlainClass>();
        }


        [Spec(nameof(clr_type_can_be_added_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_added_with_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_added_via_type_param))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_added_via_type_param_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_added_via_type_param_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_added_via_type_param_with_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_changed))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_changed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_changed_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_changed_with_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_changed_via_type_param))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_changed_via_type_param_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_changed_via_type_param_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_changed_via_type_param_with_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_cannot_be_null))]
        [Fact(Skip = "TODO")]
        public void clr_type_cannot_be_null_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_should_be_unique))]
        [Fact(Skip = "TODO")]
        public void clr_type_should_be_unique_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_name_should_be_unique))]
        [Fact(Skip = "TODO")]
        public void clr_type_name_should_be_unique_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_name_annotation_should_be_unique))]
        [Fact(Skip = "TODO")]
        public void clr_type_name_annotation_should_be_unique_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_name_annotation_should_be_valid))]
        [Fact(Skip = "TODO")]
        public void clr_type_name_annotation_should_be_valid_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(custom_name_should_be_unique))]
        [Fact(Skip = "TODO")]
        public void custom_name_should_be_unique_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(custom_name_should_be_valid))]
        [Fact(Skip = "TODO")]
        public void custom_name_should_be_valid_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(custom_name_cannot_be_null))]
        [Fact(Skip = "TODO")]
        public void custom_name_cannot_be_null_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(changing_clr_type_changes_name))]
        [Fact(Skip = "TODO")]
        public void changing_clr_type_changes_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(changing_clr_type_with_name_annotation_changes_name))]
        [Fact(Skip = "TODO")]
        public void changing_clr_type_with_name_annotation_changes_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_with_conflicting_name_can_be_added_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_can_be_added_using_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_removed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_typed_item_when_type_removed_should_retain_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_when_type_removed_should_retain_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(custom_named_clr_typed_item_when_type_removed_should_retain_custom_name))]
        [Fact(Skip = "TODO")]
        public void custom_named_clr_typed_item_when_type_removed_should_retain_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }

    }
}