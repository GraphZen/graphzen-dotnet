// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
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
                var poco = _.Object("Poco");

                Action rename = () => { poco.Name("Foo"); };

                var pocoDef = _.GetDefinition().GetObject("Poco");
                var fooDef = _.GetDefinition().GetObject("Foo");

                rename.Should().Throw<DuplicateNameException>()
                    .WithMessage(
                        TypeSystemExceptionMessages.DuplicateNameException.DuplicateType(pocoDef.Identity, "Foo",
                            fooDef.Identity));
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
                    add.Should().ThrowArgumentExceptionForName(name, reason);
                });
            }
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


        [Spec(nameof(named_item_cannot_be_removed_with_invalid_name))]
        [Fact]
        public void object_cannot_be_removed_with_invalid_name()
        {
            foreach (var (name, reason) in GraphQLNameTestHelpers.InvalidGraphQLNames)
            {
                Schema.Create(_ =>
                {
                    Action remove = () => _.RemoveObject(name);
                    remove.Should().ThrowArgumentExceptionForName(name, reason);
                });
            }
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
                var foo = _.Object("Foo");
                Action rename = () => foo.Name(null!);
                rename.Should().ThrowArgumentNullException("name");
            });
        }


        public class Poco
        {
        }

        [GraphQLName(AnnotatedName)]
        public class PocoNameAnnotated
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName(InvalidName)]
        public class PocoInvalidNameAnnotation
        {
            public const string InvalidName = "abc @#$%^";
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


        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_object_cannot_be_added_with_invalid_name_attribute()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object<PocoInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                        @"Cannot create GraphQL object with CLR class 'GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects.ObjectsTests+PocoInvalidNameAnnotation'. The name specified in the GraphQLNameAttribute (""abc @#$%^"") on the PocoInvalidNameAnnotation class is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
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


        [Spec(nameof(clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_object_with_name_attribute_can_be_renamed()
        {
            var schema = Schema.Create(_ => { _.Object<PocoNameAnnotated>().Name("Foo"); });
            schema.GetObject<PocoNameAnnotated>().Name.Should().Be("Foo");
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
            var schema = Schema.Create(_ => { _.Object("Foo").ClrType(typeof(Poco)); });
            schema.GetObject("Foo").ClrType.Should().Be<Poco>();
        }


        [Spec(nameof(untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "needs impl")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
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
            var schema = Schema.Create(_ => { _.Object<PocoNameAnnotated>().RemoveClrType(); });
            schema.GetObject(nameof(PocoNameAnnotated.AnnotatedName)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_object_with_type_removed_should_retain_clr_type_name()
        {
            // Priority: High
            var schema = Schema.Create(_ => { _.Object<Poco>().RemoveClrType(); });
            schema.GetObject(nameof(Poco)).ClrType.Should().BeNull();
        }


        [Spec(nameof(DEPRECATED_subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_annotation_conflicts
        ))]
        [Fact(Skip = "Needs design")]
        public void
            subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_annotation_conflicts_()
        {
            Schema.Create(_ =>
            {
                _.Object(nameof(PocoNameAnnotated.AnnotatedName));
                Action removeCustomName = () =>
                {
                    // _.Object("Foo").ClrType<PocoNameAnnotated>().RemoveName();
                };
                // TODO: ensure meaningful exception message
                removeCustomName.Should().Throw<InvalidNameException>();
            });
        }


        [Spec(nameof(DEPRECATED_subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_conflicts))]
        [Fact(Skip = "needs design")]
        public void subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_conflicts_()
        {
            Schema.Create(_ =>
            {
                _.Object(nameof(Poco));
                Action removeCustomName = () =>
                {
                    //_.Object("Foo").ClrType<Poco>().RemoveName();
                };
                // TODO: ensure meaningful exception message
                removeCustomName.Should().Throw<DuplicateNameException>();
            });
        }

        [Spec(nameof(clr_typed_item_can_have_clr_type_removed))]
        [Fact(Skip = "needs design")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<Poco>().RemoveClrType();
                _.Object<PocoNameAnnotated>().RemoveClrType();
            });
            schema.HasObject(nameof(Poco)).Should().BeTrue();
            schema.HasObject(PocoNameAnnotated.AnnotatedName).Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Object<Poco>();
                Action change = () => _.Object<Poco>().ClrType(null!);
                change.Should().ThrowArgumentNullException("clrType");
            });
        }
    }
}