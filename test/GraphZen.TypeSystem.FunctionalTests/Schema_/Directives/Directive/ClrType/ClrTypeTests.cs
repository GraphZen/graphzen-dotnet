// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
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
            var schema = Schema.Create(_ => { _.Directive("Foo").ClrType(typeof(PlainClass)); });
            schema.GetDirective("Foo").ClrType.Should().Be<PlainClass>();
        }

        [Spec(nameof(clr_type_can_be_added_via_type_param))]
        [Fact]
        public void clr_type_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").ClrType<PlainClass>(); });
            schema.GetDirective("Foo").ClrType.Should().Be<PlainClass>();
        }


        [Spec(nameof(clr_type_can_be_changed))]
        [Fact]
        public void clr_type_can_be_changed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive<PlainClass>()
                    .Description("original type: " + typeof(PlainClass))
                    .ClrType(typeof(PlainClassAnnotatedName));
            });
            schema.HasDirective<PlainClass>().Should().BeFalse();
            schema.GetDirective<PlainClassAnnotatedName>().Description.Should()
                .Be("original type: " + typeof(PlainClass));
        }

        [Spec(nameof(clr_type_can_be_changed_via_type_param))]
        [Fact(Skip = "todo")]
        public void clr_type_can_be_changed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive<PlainClass>()
                    .Description("original type: " + typeof(PlainClass))
                    .ClrType<PlainClassAnnotatedName>();
            });
            schema.HasDirective<PlainClass>().Should().BeFalse();
            schema.GetDirective<PlainClassAnnotatedName>().Description.Should()
                .Be("original type: " + typeof(PlainClass));
        }


        [Spec(nameof(clr_type_can_be_added_with_custom_name))]
        [Fact(Skip = "todo")]
        public void clr_type_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").ClrType(typeof(PlainClass), "Bar"); });
            schema.HasDirective("Foo").Should().BeFalse();
            schema.GetDirective("Bar").ClrType.Should().Be<PlainClass>();
            schema.GetDirective<PlainClass>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_can_be_added_via_type_param_with_custom_name))]
        [Fact(Skip = "todo")]
        public void clr_type_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { _.Directive("Foo").ClrType<PlainClass>("Bar"); });
            schema.HasDirective("Foo").Should().BeFalse();
            schema.GetDirective("Bar").ClrType.Should().Be<PlainClass>();
            schema.GetDirective<PlainClass>().Name.Should().Be("Bar");
        }


        [Spec(nameof(clr_type_can_be_changed_with_custom_name))]
        [Fact(Skip = "todo")]
        public void clr_type_can_be_changed_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive<PlainClass>()
                    .Description("original type: " + typeof(PlainClass))
                    .ClrType(typeof(PlainClassAnnotatedName), "Foo");
            });
            schema.HasDirective<PlainClass>().Should().BeFalse();
            var foo = schema.GetDirective("Foo");
            foo.Description.Should()
                .Be("original type: " + typeof(PlainClass));
            foo.ClrType.Should().Be<PlainClassAnnotatedName>();
        }


        [Spec(nameof(clr_type_can_be_changed_via_type_param_with_custom_name))]
        [Fact(Skip = "todo")]
        public void clr_type_can_be_changed_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Directive<PlainClass>()
                    .Description("original type: " + typeof(PlainClass))
                    .ClrType<PlainClassAnnotatedName>("Foo");
            });
            schema.HasDirective<PlainClass>().Should().BeFalse();
            var foo = schema.GetDirective("Foo");
            foo.Description.Should()
                .Be("original type: " + typeof(PlainClass));
            foo.ClrType.Should().Be<PlainClassAnnotatedName>();
        }


        [Spec(nameof(clr_type_cannot_be_null))]
        [Fact]
        public void clr_type_cannot_be_null_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Directive("Foo");
                new List<Action>
                {
                    () => foo.ClrType(null!),
                    () => foo.ClrType(null!, "bar")
                }.ForEach(a => a.Should().ThrowArgumentNullException("clrType"));
            });
        }


        [Spec(nameof(clr_type_should_be_unique))]
        [Fact]
        public void clr_type_should_be_unique_()
        {
            Schema.Create(_ =>
            {
                _.Directive<PlainClass>();
                var foo = _.Directive("Foo");
                Action change = () => foo.ClrType<PlainClass>();
                change.Should().Throw<DuplicateClrTypeException>().WithMessage(
                    "Cannot set CLR type on directive Foo to CLR class 'PlainClass': directive PlainClass already exists with that CLR type.");
            });
        }


        [Spec(nameof(clr_type_name_should_be_unique))]
        [Fact(Skip = "todo")]
        public void clr_type_name_should_be_unique_()
        {
            Schema.Create(_ =>
            {
                _.Directive(nameof(PlainClass));
                var foo = _.Directive("Foo").ClrType<PlainClass>();
                Action change = () => foo.ClrType<PlainClass>();
                change.Should().Throw<DuplicateClrTypeException>().WithMessage(
                    "Cannot set CLR type on directive Foo to CLR class 'PlainClass': directive PlainClass already exists with that CLR type.");
            });
        }


        [Spec(nameof(clr_type_name_annotation_should_be_unique))]
        [Fact(Skip = "TODO")]
        public void clr_type_name_annotation_should_be_unique_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(clr_type_name_annotation_should_be_valid))]
        [Fact(Skip = "TODO")]
        public void clr_type_name_annotation_should_be_valid_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(custom_name_should_be_unique))]
        [Fact(Skip = "TODO")]
        public void custom_name_should_be_unique_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(custom_name_should_be_valid))]
        [Fact(Skip = "TODO")]
        public void custom_name_should_be_valid_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(custom_name_cannot_be_null))]
        [Fact(Skip = "TODO")]
        public void custom_name_cannot_be_null_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(changing_clr_type_changes_name))]
        [Fact(Skip = "TODO")]
        public void changing_clr_type_changes_name_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(changing_clr_type_with_name_annotation_changes_name))]
        [Fact(Skip = "TODO")]
        public void changing_clr_type_with_name_annotation_changes_name_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(clr_type_with_conflicting_name_can_be_added_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_can_be_added_using_custom_name_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(clr_type_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void clr_type_can_be_removed_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(clr_typed_item_when_type_removed_should_retain_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_when_type_removed_should_retain_name_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(custom_named_clr_typed_item_when_type_removed_should_retain_custom_name))]
        [Fact(Skip = "TODO")]
        public void custom_named_clr_typed_item_when_type_removed_should_retain_custom_name_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }
    }
}