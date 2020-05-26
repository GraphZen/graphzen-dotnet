// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.InputXorOutputTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.InputObjects
{
    [NoReorder]
    public class InputXorOutputTypeTests
    {
        // ReSharper disable once UnusedType.Local
        private class PlainClass
        {
        }

        [GraphQLName(AnnotatedNameValue)]
        // ReSharper disable once UnusedType.Local
        private class PlainClassAnnotatedName
        {
            public const string AnnotatedNameValue = nameof(AnnotatedNameValue);
        }

        [GraphQLName(InvalidName)]
        // ReSharper disable once UnusedType.Local
        private class PlainClassInvalidNameAnnotation
        {
            public const string InvalidName = "abc @#$%^";
        }


        [Spec(nameof(cannot_create_type_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "needs design")]
        public void named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo")
                    .Field("outputField", "OutputType");

                Action add = () => _.InputObject("OutputType");
                add.Should().Throw<Exception>()
                    .WithMessage(
                        "Cannot add input object OutputType because OutputType is already identified as an output type.");
            });
        }


        [Spec(nameof(cannot_create_type_via_clr_type_if_name_annotation_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "needs impl")]
        public void named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "OutputType");
                var bar = _.InputObject("Bar");
                Action rename = () => bar.Name("OutputType");
                rename.Should().Throw<Exception>()
                    .WithMessage(
                        @"Cannot rename input object Bar to ""OutputTYpe"" because OutputType is already identified as an output type.");
            });
        }







    }
}