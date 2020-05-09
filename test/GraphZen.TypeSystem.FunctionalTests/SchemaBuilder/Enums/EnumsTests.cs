// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Enums
{
    [NoReorder]
    public class EnumsTests
    {
        public const string AnnotatedName = nameof(AnnotatedName);

        private enum Poce
        {
        }

        [GraphQLName(AnnotatedName)]
        private enum PoceAnnotatedName
        {
        }

        [GraphQLName("abc ()(*322*&%^")]
        private enum PoceInvalidNameAnnoation
        {
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_added_if_name_matches_input_type_identity))]
        [Fact]
        public void named_item_can_be_added_if_name_matches_input_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                _.Enum("Bar");
            });
            schema.HasEnum("Bar").Should().BeTrue();
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_added_if_name_matches_output_type_identity))]
        [Fact]
        public void named_item_can_be_added_if_name_matches_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "Bar");
                _.Enum("Bar");
            });
            schema.HasEnum("Bar").Should().BeTrue();
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_renamed_to_name_with_input_type_identity))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_renamed_to_name_with_input_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                _.Enum("Baz").Name("Bar");
            });
            schema.HasEnum("Bar").Should().BeTrue();
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_renamed_to_name_with_output_type_identity))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_renamed_to_name_with_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "Bar");
                _.Enum("Baz").Name("Bar");
            });
            schema.HasEnum("Bar").Should().BeTrue();
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_can_be_renamed_if_name_matches_input_type_identity))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_renamed_if_name_matches_input_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                _.Enum<Poce>().Name("Bar");
            });
            schema.HasEnum("Bar").Should().BeTrue();
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_can_be_renamed_if_name_matches_output_type_identity))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_renamed_if_name_matches_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "Bar");
                _.Enum<Poce>().Name("Bar");
            });
            schema.HasEnum("Bar").Should().BeTrue();
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_input_type_identity))]
        [Fact]
        public void
            clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_input_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", AnnotatedName);
                _.Enum<PoceAnnotatedName>();
            });
            schema.HasEnum(AnnotatedName).Should().BeTrue();
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_output_type_identity))]
        [Fact]
        public void
            clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_output_type_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", AnnotatedName);
                _.Enum<PoceAnnotatedName>();
            });
            schema.HasEnum(AnnotatedName).Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => { _.FromSchema("enum Foo"); });
            schema.HasEnum("Foo").Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { _.FromSchema("extend enum Foo"); });

            schema.HasEnum("Foo").Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo"); });
            schema.HasEnum("Foo").Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Enum((string)null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_added_with_invalid_name))]
        [Fact]
        public void named_item_cannot_be_added_with_invalid_name_()
        {
            foreach (var (name, reason) in GraphQLNameTestHelpers.InvalidGraphQLNames)
            {
                Schema.Create(_ =>
                {
                    Action add = () => _.Enum(name);
                    add.Should().ThrowArgumentExceptionForName(name, reason);
                });
            }
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").Name("Bar"); });
            schema.HasEnum("Foo").Should().BeFalse();
            schema.HasEnum("Bar").Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Enum("Foo");
                Action rename = () => foo.Name(null!);
                rename.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_an_invalid_name))]
        [Fact]
        public void named_item_cannot_be_renamed_with_an_invalid_name_()
        {
            foreach (var (name, reason) in GraphQLNameTestHelpers.InvalidGraphQLNames)
            {
                Schema.Create(_ =>
                {
                    var foo = _.Enum("Foo");
                    Action rename = () => foo.Name(name);
                    var def = _.GetDefinition().GetEnum("Foo");
                    rename.Should()
                        .Throw<InvalidNameException>()
                        .WithMessage(TypeSystemExceptionMessages.InvalidNameException.CannotRename(name, def), reason);
                });
            }
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo");
                var bar = _.Enum("Bar");
                Action rename = () => bar.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    @"Cannot rename enum Bar to ""Foo"", enum Foo already exists. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum("Foo");
                _.RemoveEnum("Foo");
            });
            schema.HasEnum("Foo").Should().BeFalse();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveEnum((string)null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_invalid_name))]
        [Fact]
        public void named_item_cannot_be_removed_with_invalid_name_()
        {
            foreach (var (name, reason) in GraphQLNameTestHelpers.InvalidGraphQLNames)
            {
                Schema.Create(_ =>
                {
                    Action remove = () => _.RemoveEnum(name);
                    remove.Should().ThrowArgumentExceptionForName(name, reason);
                });
            }
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Enum(typeof(Poce)); });
            schema.HasEnum<Poce>();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Enum<Poce>(); });
            schema.HasEnum<Poce>();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Enum((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Enum<PoceInvalidNameAnnoation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    @"Cannot create GraphQL enum with CLR enum 'GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Enums.EnumsTests+PoceInvalidNameAnnoation'. The name specified in the GraphQLNameAttribute (""abc ()(*322*&%^"") on the PoceInvalidNameAnnoation enum is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_removed))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum<Poce>();
                _.RemoveEnum(typeof(Poce));
            });
            schema.HasEnum<Poce>().Should().BeFalse();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_removed_via_type_param))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Enum<Poce>();
                _.RemoveEnum<Poce>();
            });
            schema.HasEnum<Poce>().Should().BeFalse();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Enum<Poce>();
                Action remove = () => _.RemoveEnum((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ => { _.Enum<Poce>().ClrType<PoceAnnotatedName>(); });
            schema.HasEnum<Poce>().Should().BeFalse();
            schema.HasEnum<PoceAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var poce = _.Enum<Poce>();
                Action change = () => poce.ClrType(null!);
                change.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_removed))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ => { _.Enum<Poce>().RemoveClrType(); });
            schema.GetEnum(nameof(Poce)).ClrType.Should().BeNull();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "nees impl")]
        public void clr_typed_item_with_type_removed_should_retain_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Enum<Poce>().RemoveClrType(); });
            schema.HasEnum(nameof(Poce)).Should().BeTrue();
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.Enum<PoceAnnotatedName>().RemoveClrType(); });
            schema.HasEnum(AnnotatedName).Should().BeTrue();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Enum<Poce>().Name("Foo"); });
            schema.GetEnum<Poce>().Name.Should().Be("Foo");
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Enum<PoceAnnotatedName>().Name("Foo"); });
            schema.GetEnum<PoceAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_()
        {
            foreach (var (name, reason) in GraphQLNameTestHelpers.InvalidGraphQLNames)
            {
                Schema.Create(_ =>
                {
                    var poce = _.Enum<Poce>();
                    Action rename = () => poce.Name(name);
                    var def = _.GetDefinition().GetEnum<Poce>();
                    rename.Should().Throw<InvalidNameException>()
                        .WithMessage(TypeSystemExceptionMessages.InvalidNameException.CannotRename(name, def), reason);
                });
            }
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Enum("Foo");
                var poce = _.Enum<Poce>();
                Action rename = () => poce.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    @"Cannot rename enum Poce to ""Foo"", enum Foo already exists. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.untyped_item_can_have_clr_type_added))]
        [Fact]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType<Poce>(); });
            schema.GetEnum("Foo").ClrType.Should().Be<Poce>();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "needs impl")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            Schema.Create(_ =>
            {
                _.Enum<Poce>();
                var foo = _.Enum("Foo");
                Action add = () => foo.ClrType<Poce>();
                add.Should().Throw<DuplicateClrTypeException>();
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .DEPRECATED_subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_conflicts))]
        [Fact(Skip = "TODO")]
        public void subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_conflicts_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .DEPRECATED_subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_annotation_conflicts))]
        [Fact(Skip = "TODO")]
        public void
            subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_annotation_conflicts_()
        {
            var schema = Schema.Create(_ => { });
        }
    }

    // Move EnumsTests into a separate file to start writing tests
}
// Source Hash Code: 8762128340584621188