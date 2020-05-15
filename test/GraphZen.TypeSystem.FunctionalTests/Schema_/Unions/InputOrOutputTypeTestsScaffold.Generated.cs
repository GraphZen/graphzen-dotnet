// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.InputOrOutputTypeSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Unions
{
    [NoReorder]
    public abstract class InputOrOutputTypeTests
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

        [Spec(nameof(named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "needs impl")]
        public void named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                Action add = () => _.Union("Bar");
                add.Should().Throw<Exception>().WithMessage("x");
            });
        }


        [Spec(nameof(named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                var baz = _.Union("Baz");
                Action rename = () => baz.Name("Bar");
                rename.Should().Throw<Exception>().WithMessage("x");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "todo")]
        public void clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                var union = _.Union<UnionsTests.PlainAbstractClass>();
                Action rename = () => union.Name("Bar");
                rename.Should().Throw<Exception>().WithMessage("TODO: error message specific to input/output error");
            });
        }


        [Spec(nameof(
            clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", UnionsTests.PlainAbstractClassAnnotatedName.AnnotatedName);
                Action add = () => _.Union<UnionsTests.PlainAbstractClassAnnotatedName>();
                add.Should().Throw<Exception>("x");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("inputField", "Bar");
                Action add = () => _.Union<UnionsTests.PlainAbstractClassAnnotatedName>("Bar");
                add.Should().Throw<Exception>("x");
            });
        }


        [Spec(nameof(clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move InputOrOutputTypeTests into a separate file to start writing tests
    [NoReorder]
    public class InputOrOutputTypeTestsScaffold
    {
    }
}
// Source Hash Code: 13050180112267868518