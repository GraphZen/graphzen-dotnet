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
            var schema = Schema.Create(_ => { _.Object("Foo"); });
            schema.HasObject("Foo").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_removed))]
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


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed()
        {
            // Priority: High
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo");
                _.Object("Foo").Name("Bar");
            });
            schema.HasObject("Foo").Should().BeFalse();
            schema.HasObject("Bar").Should().BeTrue();
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo");
                Action rename = () => { _.Object("Bar").Name("Foo"); };
                rename.Should().Throw<DuplicateNameException>()
                    .WithMessage(TypeIdentity.GetDuplicateTypeNameErrorMessage("Bar", "Foo"));
            });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_added_with_invalid_name))]
        [Fact]
        public void named_item_cannot_be_added_with_invalid_name()
        {
            foreach (var (name, reason) in GraphQLNameTestHelpers.InvalidGraphQLNames)
            {
                Schema.Create(_ =>
                {
                    Action add = () => _.Object(name);
                    add.Should().Throw<ArgumentException>()
                        .WithMessage(GraphQLName.GetInvalidNameErrorMessage(name) + " (Parameter 'name')", reason)
                        .WithInnerException<InvalidNameException>()
                        .WithMessage(GraphQLName.GetInvalidNameErrorMessage(name), reason);
                });
            }
        }
    }
}