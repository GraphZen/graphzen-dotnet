// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects
{
    [NoReorder]
    public class SchemaBuilderObjectsTests
    {

        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void object_can_be_added_to_schema()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo");
            });
            schema.HasObject("Foo").Should().BeTrue();
        }



        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact]
        public void object_can_be_removed_from_schema()
        {
            // Priority: High
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo");
                _.IgnoreType("Foo");

            });
            schema.HasObject("Foo").Should().BeFalse();
        }



        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed()
        {
            // Priority: High
            var schema = Schema.Create(_ =>
            {

            });
            throw new NotImplementedException();
        }



        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists()
        {
            // Priority: High
            var schema = Schema.Create(_ =>
            {

            });
            throw new NotImplementedException();
        }



        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_name_must_be_valid_name))]
        [Fact]
        public void named_item_name_must_be_valid_name()
        {
            // Priority: High
            var schema = Schema.Create(_ =>
            {

            });
            throw new NotImplementedException();
        }


    }
}