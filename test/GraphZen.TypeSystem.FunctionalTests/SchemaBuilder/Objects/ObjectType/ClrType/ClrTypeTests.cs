// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects.ObjectType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
    {
        public class PlainClass
        {
        }

        [GraphQLName(AnnotatedName)]
        public class PlainClassNameAnnotated
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName(InvalidName)]
        public class PlainClassInvalidNameAnnotation
        {
            public const string InvalidName = "abc @#$%^";
        }

        [Spec(nameof(untyped_item_can_have_clr_type_added))]
        [Fact]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.Object("Foo").ClrType(typeof(PlainClass)); });
            schema.GetObject("Foo").ClrType.Should().Be<PlainClass>();
        }


        [Spec(nameof(untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "needs impl")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            Schema.Create(_ =>
            {
                _.Object<PlainClass>();
                _.Object("Foo");
                Action action = () => _.Object("Foo").ClrType<PlainClass>();
                action.Should().Throw<DuplicateClrTypeException>();
            });
        }


        [Spec(nameof(clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_object_can_have_clr_type_changed()
        {
            // Priority: High
            var schema = Schema.Create(_ => { _.Object<PlainClass>().ClrType<PlainClassNameAnnotated>(); });
            schema.HasObject<PlainClass>().Should().BeFalse();
            schema.HasObject<PlainClassNameAnnotated>().Should().BeTrue();
        }

        [Spec(nameof(clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Object<PlainClass>();
                Action change = () => _.Object<PlainClass>().ClrType(null!);
                change.Should().ThrowArgumentNullException("clrType");
            });
        }

        


        [Spec(nameof(clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_object_with_type_removed_should_retain_clr_type_name()
        {
            // Priority: High
            var schema = Schema.Create(_ => { _.Object<PlainClass>().RemoveClrType(); });
            schema.GetObject(nameof(PlainClass)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_can_have_clr_type_removed))]
        [Fact(Skip = "needs design")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PlainClass>().RemoveClrType();
                _.Object<PlainClassNameAnnotated>().RemoveClrType();
            });
            schema.HasObject(nameof(PlainClass)).Should().BeTrue();
            schema.HasObject(PlainClassNameAnnotated.AnnotatedName).Should().BeTrue();
        }
    }
}