// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.


using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;


namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Unions.UnionType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
    {
        public abstract class PlainAbstractClass
        {
        }

        [GraphQLName(AnnotatedName)]
        public abstract class PlainAbstractClassAnnotatedName
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName("@)(*#")]
        public abstract class PlainAbstractClassInvalidNameAnnotation
        {
        }

        [Spec(nameof(clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>().ClrType(typeof(PlainAbstractClassAnnotatedName));
            });
            schema.HasUnion<PlainAbstractClass>().Should().BeFalse();
            schema.HasUnion<PlainAbstractClassAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_have_clr_type_changed_via_type_param))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_have_clr_type_changed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>().ClrType<PlainAbstractClassAnnotatedName>();
            });
            schema.HasUnion<PlainAbstractClass>().Should().BeFalse();
            schema.HasUnion<PlainAbstractClassAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var union = _.Union<PlainAbstractClass>();
                Action change = () => union.ClrType(null!);
                change.Should().ThrowArgumentNullException("clrType");
            });
        }

        [Spec(nameof(untyped_item_can_have_clr_type_added))]
        [Fact(Skip = "todo")]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClass>(); });
            schema.GetUnion("Foo").ClrType.Should().Be<PlainAbstractClass>();
        }


        [Spec(nameof(untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "todo")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            Schema.Create(_ =>
            {
                _.Union<PlainAbstractClass>();
                var foo = _.Union("Foo");
                Action add = () => foo.ClrType<PlainAbstractClass>();
                add.Should().Throw<DuplicateClrTypeException>().WithMessage("x");
            });
        }


        [Spec(nameof(adding_clr_type_to_item_changes_name))]
        [Fact(Skip = "todo")]
        public void adding_clr_type_to_item_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType(typeof(PlainAbstractClass)); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be(nameof(PlainAbstractClass));
        }


        [Spec(nameof(adding_clr_type_to_item_via_type_param_changes_name))]
        [Fact(Skip = "todo")]
        public void adding_clr_type_to_item_via_type_param_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClass>(); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClass>().Name.Should().Be(nameof(PlainAbstractClass));
        }


        [Spec(nameof(adding_clr_type_with_name_annotation_to_item_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType(typeof(PlainAbstractClassAnnotatedName)); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should()
                .Be(PlainAbstractClassAnnotatedName.AnnotatedName);
        }


        [Spec(nameof(adding_clr_type_with_name_annotation_to_item_via_type_pram_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_via_type_pram_changes_name_()
        {
            var schema = Schema.Create(_ => { _.Union("Foo").ClrType<PlainAbstractClassAnnotatedName>(); });
            schema.HasUnion("Foo").Should().BeFalse();
            schema.GetUnion<PlainAbstractClassAnnotatedName>().Name.Should()
                .Be(PlainAbstractClassAnnotatedName.AnnotatedName);
        }


        [Spec(nameof(clr_type_with_conflicting_name_can_be_added_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_can_be_added_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(nameof(PlainAbstractClass));
                _.Union("Foo").ClrType(typeof(PlainAbstractClass), "Bar");
            });
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
            schema.GetUnion("Bar").ClrType.Should().Be<PlainAbstractClass>();
        }


        [Spec(nameof(clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_can_be_added_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(PlainAbstractClassAnnotatedName.AnnotatedName);
                _.Union("Foo").ClrType(typeof(PlainAbstractClassAnnotatedName), "Bar");
            });
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedName).ClrType.Should().BeNull();
            schema.GetUnion("Bar").ClrType.Should().Be<PlainAbstractClassAnnotatedName>();
        }


        [Spec(nameof(clr_type_with_conflicting_name_can_be_added_via_type_param_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_can_be_added_via_type_param_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(nameof(PlainAbstractClass));
                _.Union("Foo").ClrType<PlainAbstractClass>("Bar");
            });
            schema.GetUnion(nameof(PlainAbstractClass)).ClrType.Should().BeNull();
            schema.GetUnion("Bar").ClrType.Should().Be<PlainAbstractClass>();
        }


        [Spec(nameof(clr_type_with_conflicting_name_annotation_can_be_added_via_type_param_using_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_type_with_conflicting_name_annotation_can_be_added_via_type_param_using_custom_name_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Union(PlainAbstractClassAnnotatedName.AnnotatedName);
                _.Union("Foo").ClrType<PlainAbstractClassAnnotatedName>("Bar");
            });
            schema.GetUnion(PlainAbstractClassAnnotatedName.AnnotatedName).ClrType.Should().BeNull();
            schema.GetUnion("Bar").ClrType.Should().Be<PlainAbstractClassAnnotatedName>();
        }
    }
}