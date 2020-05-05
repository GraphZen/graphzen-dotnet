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
        public void object_can_be_added_to_schema()
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
                _.Object("Foo").Name("Poco");
            });
            schema.HasObject("Foo").Should().BeFalse();
            schema.HasObject("Poco").Should().BeTrue();
        }


        [Spec(nameof(named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void object_cannot_be_renamed_if_name_already_exists()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                Action rename = () => { _.Object("Poco").Name("Foo"); };
                rename.Should().Throw<DuplicateNameException>()
                    .WithMessage(TypeIdentity.GetDuplicateTypeNameErrorMessage("Poco", "Foo"));
            });
        }


        [Spec(nameof(named_item_cannot_be_added_with_invalid_name))]
        [Fact]
        public void object_cannot_be_added_with_invalid_name()
        {
            foreach (var (name, reason) in GraphQLNameTestHelpers.InvalidGraphQLNames)
            {
                Schema.Create(_ =>
                {
                    Action add = () => _.Object(name);
                    add.Should().ThrowInvalidNameArgument(name, reason);
                });
            }
        }


        [Spec(nameof(named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void object_cannot_be_added_with_null_value()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object((string) null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(named_item_cannot_be_removed_with_invalid_name))]
        [Fact]
        public void object_cannot_be_removed_with_invalid_name()
        {
            foreach (var (name, reason) in GraphQLNameTestHelpers.InvalidGraphQLNames)
            {
                Schema.Create(_ =>
                {
                    Action remove = () => _.RemoveObject(name);
                    remove.Should().ThrowInvalidNameArgument(name, reason);
                });
            }
        }


        [Spec(nameof(named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void object_cannot_be_removed_with_null_value()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveObject((string) null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(named_item_cannot_be_renamed_with_an_invalid_name))]
        [Fact]
        public void object_cannot_be_renamed_with_an_invalid_name()
        {
            foreach (var (name, reason) in GraphQLNameTestHelpers.InvalidGraphQLNames)
            {
                Schema.Create(_ =>
                {
                    _.Object("Foo");
                    Action rename = () => _.Object("Foo").Name(name);
                    var def = _.GetDefinition().GetObject("Foo");
                    rename.Should()
                        .Throw<InvalidNameException>()
                        .WithMessage(TypeSystemExceptionMessages.InvalidNameException.CannotRename(name, def), reason);
                });
            }
        }


        [Spec(nameof(named_item_cannot_be_renamed_with_null_value))]
        [Fact]
        public void object_cannot_be_renamed_with_null_value()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                var foo = _.GetDefinition().GetObject("Foo");
                Action rename = () => _.Object("Foo").Name(null!);
                rename.Should().Throw<InvalidNameException>()
                    .WithMessage(TypeSystemExceptionMessages.InvalidNameException.CannotRemove(foo));
            });
        }


        public class Poco
        {
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.adding_clr_type_to_item_does_not_change_name))]
        [Fact]
        public void adding_clr_type_to_item_does_not_change_name()
        {
            var schema = Schema.Create(_ => { _.Object("Foo").ClrType<Poco>(); });
            schema.GetObject("Foo").ClrType.Should().Be<Poco>();
        }


        [GraphQLName(AnnotatedName)]
        public class PocoNameAnnotated
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .adding_clr_type_with_name_annotation_to_item_does_not_change_name))]
        [Fact]
        public void adding_clr_type_with_name_annotation_to_item_does_not_change_name()
        {
            var schema = Schema.Create(_ => { _.Object("Foo").ClrType<PocoNameAnnotated>(); });
            schema.GetObject<PocoNameAnnotated>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_object_can_be_added()
        {
            var schema = Schema.Create(_ => { _.Object<PocoNameAnnotated>(); });
            schema.HasObject<PocoNameAnnotated>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_object_can_be_added_via_type_param()
        {
            var schema = Schema.Create(_ => { _.Object("Poco").ClrType<PocoNameAnnotated>(); });
            schema.GetObject<PocoNameAnnotated>().Name.Should().Be("Poco");
        }


        [Spec(nameof(clr_typed_item_can_be_removed))]
        [Fact]
        public void clr_typed_object_can_be_removed()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PocoNameAnnotated>();
                _.RemoveObject(typeof(PocoNameAnnotated));
            });
            schema.HasObject<PocoNameAnnotated>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_can_be_removed_via_type_param))]
        [Fact]
        public void clr_typed_object_can_be_removed_via_type_param()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PocoNameAnnotated>();
                _.RemoveObject<PocoNameAnnotated>();
            });
            schema.HasObject<PocoNameAnnotated>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_object_can_be_renamed()
        {
            var schema = Schema.Create(_ => { _.Object<PocoNameAnnotated>().Name("Baz"); });
            schema.GetObject<PocoNameAnnotated>().Name.Should().Be("Baz");
        }

        [GraphQLName(InvalidName)]
        public class PocoInvalidNameAnnotation
        {
            public const string InvalidName = "abc @#$%^";
        }

        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_object_cannot_be_added_with_invalid_name_attribute()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object<PocoInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    TypeSystemExceptionMessages.InvalidNameException.CannotCreateAnnotatedType(
                        typeof(PocoInvalidNameAnnotation), PocoInvalidNameAnnotation.InvalidName, TypeKind.Object));
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


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_object_cannot_be_renamed_if_name_already_exists()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                Action rename = () => _.Object<Poco>().Name("Foo");
                // TODO: test exception message
                rename.Should().Throw<DuplicateNameException>();
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
        [Fact]
        public void clr_typed_object_cannot_be_renamed_with_an_invalid_name()
        {
            foreach (var (name, reason) in GraphQLNameTestHelpers.InvalidGraphQLNames)
            {
                Schema.Create(_ =>
                {
                    _.Object<PocoNameAnnotated>();
                    Action rename = () => _.Object<PocoNameAnnotated>().Name(name);
                    var type = _.GetDefinition().GetObject<PocoNameAnnotated>();
                    rename.Should().Throw<InvalidNameException>()
                        .WithMessage(TypeSystemExceptionMessages.InvalidNameException.CannotRename(name, type), reason);
                });
            }
        }


        [Spec(nameof(clr_typed_item_can_be_renamed_with_null_value))]
        [Fact]
        public void clr_typed_object_can_be_renamed_with_null_value()
        {
            Schema.Create(_ =>
            {
                _.Object<PocoNameAnnotated>();
                Action rename = () => _.Object<PocoNameAnnotated>().Name(null);
                rename.Should().NotThrow();
            });
        }


        [Spec(nameof(clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_object_with_name_attribute_can_be_renamed()
        {
            var schema = Schema.Create(_ => { _.Object<PocoNameAnnotated>().Name("Foo"); });
            schema.GetObject<PocoNameAnnotated>().Name.Should().Be("Foo");
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"type Foo"); });
            schema.HasObject("Foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_via_sdl_extension()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"extend type Foo"); });
            schema.HasObject("Foo").Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_have_type_removed))]
        [Fact(Skip = "TODO")]
        public void typed_item_can_have_type_removed()
        {
            var schema = Schema.Create(_ => { _.Object<Poco>().ClrType(null); });
            schema.HasObject<Poco>().Should().BeFalse();
            schema.HasObject(nameof(Poco)).Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.untyped_item_can_have_clr_type_added))]
        [Fact]
        public void untyped_item_can_have_clr_type_added()
        {
            var schema = Schema.Create(_ => { _.Object("Foo").ClrType(typeof(Poco)); });
            schema.GetObject("Foo").ClrType.Should().Be<Poco>();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "TODO")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use()
        {
            Schema.Create(_ =>
            {
                _.Object<Poco>();
                _.Object("Foo");
                Action action = () => _.Object("Foo").ClrType<Poco>();
                action.Should().Throw<DuplicateClrTypeException>();
            });
        }


        [Spec(nameof(clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_object_can_have_clr_type_changed()
        {
            // Priority: High
            var schema = Schema.Create(_ => { _.Object<Poco>().ClrType<PocoNameAnnotated>(); });
            schema.HasObject<Poco>().Should().BeFalse();
            schema.HasObject<PocoNameAnnotated>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_object_with_name_annotation_type_removed_should_retain_annotated_name()
        {
            // Priority: High
            var schema = Schema.Create(_ => { _.Object<PocoNameAnnotated>().ClrType(null); });
            schema.GetObject(nameof(PocoNameAnnotated.AnnotatedName)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_object_with_type_removed_should_retain_clr_type_name()
        {
            // Priority: High
            var schema = Schema.Create(_ => { _.Object<Poco>().ClrType(null); });
            schema.GetObject(nameof(Poco)).ClrType.Should().BeNull();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .subsequently_clr_typed_item_can_have_custom_named_removed))]
        [Fact]
        public void subsequently_clr_typed_item_can_have_custom_named_removed()
        {
            // Priority: High
            var schema = Schema.Create(_ => { _.Object("Foo").ClrType<Poco>().ClrType(null); });
            schema.GetObject("Foo").ClrType.Should().BeNull();
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_annotation_conflicts))]
        [Fact(Skip = "TODO")]
        public void subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_annotation_conflicts()
        {
            Schema.Create(_ =>
            {
                _.Object(nameof(PocoNameAnnotated.AnnotatedName));
                Action removeCustomName = () => _.Object("Foo").ClrType<PocoNameAnnotated>().Name(null);
                // TODO: ensure meaningful exception message
                removeCustomName.Should().Throw<InvalidNameException>();
            });
        }


        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs
            .subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_conflicts))]
        [Fact(Skip = "TODO")]
        public void subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_conflicts()
        {
            Schema.Create(_ =>
            {
                _.Object(nameof(Poco));
                Action removeCustomName = () => _.Object("Foo").ClrType<Poco>().Name(null);
                // TODO: ensure meaningful exception message
                removeCustomName.Should().Throw<InvalidNameException>();
            });
        }
    }
}