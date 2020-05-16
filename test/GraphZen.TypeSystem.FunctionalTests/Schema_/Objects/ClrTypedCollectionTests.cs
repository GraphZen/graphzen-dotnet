// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects
{
    [NoReorder]
    public class ClrTypedCollectionTests
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


        [Spec(nameof(clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_object_can_be_added()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClassNameAnnotated>(); });
            schema.HasObject<PlainClassNameAnnotated>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_be_removed))]
        [Fact]
        public void clr_typed_object_can_be_removed()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PlainClassNameAnnotated>();
                _.RemoveObject(typeof(PlainClassNameAnnotated));
            });
            schema.HasObject<PlainClassNameAnnotated>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_can_be_removed_via_type_param))]
        [Fact]
        public void clr_typed_object_can_be_removed_via_type_param()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PlainClassNameAnnotated>();
                _.RemoveObject<PlainClassNameAnnotated>();
            });
            schema.HasObject<PlainClassNameAnnotated>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_object_cannot_be_added_with_invalid_name_attribute()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object<PlainClassInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    @"Cannot get or create GraphQL object type builder with CLR class 'PlainClassInvalidNameAnnotation'. The name ""abc @#$%^"" specified in the GraphQLNameAttribute on the PlainClassInvalidNameAnnotation CLR class is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_object_cannot_be_added_with_null_value()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object((Type) null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_object_cannot_be_removed_with_null_value()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveObject((Type) null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClass>(); });
            schema.HasObject<PlainClass>();
        }

        [Spec(nameof(clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_object_can_be_renamed()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClassNameAnnotated>().Name("Baz"); });
            schema.GetObject<PlainClassNameAnnotated>().Name.Should().Be("Baz");
        }

        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_object_cannot_be_renamed_if_name_already_exists()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                Action rename = () => _.Object<PlainClass>().Name("Foo");
                // TODO: test exception message
                rename.Should().Throw<DuplicateNameException>();
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
        [Theory]
        [InlineData("  xy")]
        [InlineData("")]
        public void clr_typed_object_cannot_be_renamed_with_an_invalid_name(string name)
        {
            Schema.Create(_ =>
            {
                _.Object<PlainClassNameAnnotated>();
                Action rename = () => _.Object<PlainClassNameAnnotated>().Name(name);
                rename.Should().Throw<InvalidNameException>()
                    .WithMessage(
                        $"Cannot rename object AnnotatedName: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_object_with_name_attribute_can_be_renamed()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClassNameAnnotated>().Name("Foo"); });
            schema.GetObject<PlainClassNameAnnotated>().Name.Should().Be("Foo");
        }
    }
}