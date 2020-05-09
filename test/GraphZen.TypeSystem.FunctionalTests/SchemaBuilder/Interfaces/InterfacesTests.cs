// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces
{
    [NoReorder]
    public class InterfacesTests
    {
        // ReSharper disable once InconsistentNaming
        private interface IPoci
        {
        }

        [GraphQLName(AnnotatedName)]
        // ReSharper disable once InconsistentNaming
        private interface IPociAnnotatedName
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName("abc &*(")]
        // ReSharper disable once InconsistentNaming
        private interface IPociInvalidNameAnnotation
        {
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "needs design/impl")]
        public void named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("input", "Bar");
                Action add = () => _.Interface("Bar");
                add.Should().Throw<Exception>();
            });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io))]
        // [Fact(Skip = "needs design/impl")]
        [Fact]
        public void named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("input", "Bar");
                var baz = _.Interface("Baz");
                Action rename = () => baz.Name("Bar");
                rename.Should().Throw<Exception>();
            });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "needs design/implementation")]
        public void clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("input", "Bar");
                var poci = _.Interface<IPoci>();
                Action rename = () => poci.Name("Bar");
                rename.Should().Throw<Exception>().WithMessage("something about input/output type");
            });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "needs design/impl")]
        public void
            clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("input", IPociAnnotatedName.AnnotatedName);
                Action add = () => _.Interface<IPociAnnotatedName>();
                add.Should().Throw<Exception>().WithMessage("something about input/output type");
            });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"interface Foo"); });
            schema.HasInterface("Foo").Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "needs design/impl")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"extend interface Foo"); });
            schema.HasInterface("Foo").Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo"); });
            schema.HasInterface("Foo").Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Interface((string)null!);
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
                    Action add = () => _.Interface(name);
                    add.Should().ThrowArgumentExceptionForName(name, reason);
                });
            }
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").Name("Bar"); });
            schema.HasInterface("Bar").Should().BeTrue();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var foo = _.Interface("Foo");
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
                    var foo = _.Interface("Foo");
                    Action rename = () => foo.Name(name);
                    var def = _.GetDefinition().GetInterface("Foo");
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
                _.Interface("Foo");
                var bar = _.Interface("Bar");
                Action rename = () => bar.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    @"Cannot rename interface Bar to ""Foo"", interface Foo already exists. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface("Foo");
                _.RemoveInterface("Foo");
            });
            schema.HasInterface("Foo").Should().BeFalse();
        }


        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo");
                Action remove = () => _.RemoveInterface((string)null!);
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
                    Action remove = () => _.RemoveInterface(name);
                    remove.Should().ThrowArgumentExceptionForName(name, reason);
                });
            }
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Interface(typeof(IPoci)); });
            schema.HasInterface<IPoci>().Should().BeTrue();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Interface<IPoci>(); });
            schema.HasInterface<IPoci>().Should().BeTrue();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Interface((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Interface<IPociInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    @"Cannot create GraphQL interface with CLR interface 'GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfacesTests+IPociInvalidNameAnnotation'. The name specified in the GraphQLNameAttribute (""abc &*("") on the IPociInvalidNameAnnotation interface is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_removed))]
        [Fact(Skip = "needs design/impl")]
        public void clr_typed_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<IPoci>();
                _.RemoveInterface(typeof(IPoci));
            });
            schema.HasInterface<IPoci>().Should().BeFalse();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_removed_via_type_param))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<IPoci>();
                _.RemoveInterface<IPoci>();
            });
            schema.HasInterface<IPoci>().Should().BeFalse();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveInterface((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ => { _.Interface<IPoci>().ClrType(typeof(IPociAnnotatedName)); });
            schema.HasInterface<IPoci>().Should().BeFalse();
            schema.HasInterface<IPociAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var poci = _.Interface<IPoci>();
                Action remove = () => poci.ClrType(null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_have_clr_type_removed))]
        [Fact(Skip = "needs design/impl")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ => { _.Interface<IPoci>().RemoveClrType(); });
            schema.GetInterface(nameof(IPoci)).ClrType.Should().BeNull();
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "needs design/impl")]
        public void clr_typed_item_with_type_removed_should_retain_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Interface<IPoci>().RemoveClrType(); });
            schema.HasInterface(nameof(IPoci)).Should().BeTrue();
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "needs design/impl")]
        public void clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.Interface<IPociAnnotatedName>().RemoveClrType(); });
            schema.HasInterface(IPociAnnotatedName.AnnotatedName).Should().BeTrue();
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Interface<IPoci>().Name("Foo"); });
            schema.GetInterface<IPoci>().Name.Should().Be("Foo");
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Interface<IPociAnnotatedName>().Name("Foo"); });
            schema.GetInterface<IPociAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_()
        {
            foreach (var (name, reason) in GraphQLNameTestHelpers.InvalidGraphQLNames)
            {
                Schema.Create(_ =>
                {
                    var poci = _.Interface<IPoci>();
                    Action rename = () => poci.Name(name);
                    var def = _.GetDefinition().GetInterface<IPoci>();
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
                _.Interface("Foo");
                var poci = _.Interface<IPoci>();
                Action rename = () => poci.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    @"Cannot rename interface IPoci to ""Foo"", interface Foo already exists. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.untyped_item_can_have_clr_type_added))]
        [Fact]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType<IPoci>(); });
            schema.GetInterface("Foo").ClrType.Should().Be<IPoci>();
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "needs impl")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            Schema.Create(_ =>
            {
                _.Interface<IPoci>();
                var foo = _.Interface("Foo");
                Action add = () => foo.ClrType<IPoci>();
                add.Should().Throw<DuplicateClrTypeException>().WithMessage("x");
            });
        }


      

        [Spec(nameof(ClrTypedCollectionSpecs
            .DEPRECATED_subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_conflicts))]
        [Fact(Skip = "needs design")]
        public void subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_conflicts_()
        {
            //var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs
            .DEPRECATED_subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_annotation_conflicts))]
        [Fact(Skip = "needs design")]
        public void
            subsequently_clr_typed_item_cannot_have_custom_named_removed_if_clr_type_name_annotation_conflicts_()
        {
            //var schema = Schema.Create(_ => { });
        }
    }
}