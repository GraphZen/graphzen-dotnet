// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypedCollectionSpecs;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NamedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects
{
    [NoReorder]
    public class ObjectsTests
    {
        [Spec(nameof(named_item_can_be_added))]
        [Fact]
        public void _object_can_be_added_to_schema()
        {
            var schema = Schema.Create(_ => { _.Object("Foo"); });
            schema.HasObject("Foo").Should().BeTrue();
        }

        [Spec(nameof(named_item_can_be_removed))]
        [Fact]
        public void object_can_be_removed_from_schema()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo");
                _.RemoveObject("Foo");
            });
            schema.HasObject("Foo").Should().BeFalse();
        }


        [Spec(nameof(named_item_can_be_renamed))]
        [Fact]
        public void object_can_be_renamed()
        {
            // Priority: High
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo");
                _.Object("Foo").Name("PlainClass");
            });
            schema.HasObject("Foo").Should().BeFalse();
            schema.HasObject("PlainClass").Should().BeTrue();
        }


        [Spec(nameof(named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void object_cannot_be_renamed_if_name_already_exists()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                var poco = _.Object("PlainClass");

                Action rename = () => { poco.Name("Foo"); };

                var pocoDef = _.GetDefinition().GetObject("PlainClass");
                var fooDef = _.GetDefinition().GetObject("Foo");

                rename.Should().Throw<DuplicateNameException>()
                    .WithMessage(
                        TypeSystemExceptionMessages.DuplicateNameException.DuplicateType(pocoDef.Identity, "Foo",
                            fooDef.Identity));
            });
        }


        [Spec(nameof(named_item_cannot_be_added_with_invalid_name))]
        [Theory]
        [InlineData(")(&(*#")]
        public void object_cannot_be_added_with_invalid_name(string name)
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object(name);
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot get or create GraphQL type builder for object named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void object_cannot_be_added_with_null_value()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object((string)null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        


        [Spec(nameof(named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void object_cannot_be_removed_with_null_value()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveObject((string)null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(named_item_cannot_be_renamed_with_an_invalid_name))]
        [Theory]
        [InlineData("  xy")]
        [InlineData("")]
        public void object_cannot_be_renamed_with_an_invalid_name(string name)
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                Action rename = () => _.Object("Foo").Name(name);
                rename.Should()
                    .Throw<InvalidNameException>()
                    .WithMessage(
                        $"Cannot rename object Foo. \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(named_item_cannot_be_renamed_with_null_value))]
        [Fact]
        public void object_cannot_be_renamed_with_null_value()
        {
            Schema.Create(_ =>
            {
                var foo = _.Object("Foo");
                Action rename = () => foo.Name(null!);
                rename.Should().ThrowArgumentNullException("name");
            });
        }


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


        [Spec(nameof(clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_object_can_be_renamed()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClassNameAnnotated>().Name("Baz"); });
            schema.GetObject<PlainClassNameAnnotated>().Name.Should().Be("Baz");
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
                Action add = () => _.Object((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_object_cannot_be_removed_with_null_value()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveObject((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
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
                        $"Cannot rename object AnnotatedName. \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_object_with_name_attribute_can_be_renamed()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClassNameAnnotated>().Name("Foo"); });
            schema.GetObject<PlainClassNameAnnotated>().Name.Should().Be("Foo");
        }


        [Spec(nameof(named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"type Foo"); });
            schema.HasObject("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"extend type Foo"); });
            schema.HasObject("Foo").Should().BeTrue();
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


        [Spec(nameof(clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_object_with_name_annotation_type_removed_should_retain_annotated_name()
        {
            // Priority: High
            var schema = Schema.Create(_ => { _.Object<PlainClassNameAnnotated>().RemoveClrType(); });
            schema.GetObject(nameof(PlainClassNameAnnotated.AnnotatedName)).ClrType.Should().BeNull();
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

        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_can_be_added_via_type_param))]
        [Fact()]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClass>(); });
            schema.HasObject<PlainClass>();
        }
    }
}