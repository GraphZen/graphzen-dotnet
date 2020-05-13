// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
    {
        // ReSharper disable once InconsistentNaming
        private interface PlainInterface
        {
        }

        [GraphQLName(AnnotatedName)]
        // ReSharper disable once InconsistentNaming
        private interface PlainInterfaceAnnotatedName
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName("abc &*(")]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedType.Local
        private interface PlainInterfaceInvalidNameAnnotation
        {
        }


        [Spec(nameof(clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<PlainInterface>().ClrType(typeof(PlainInterfaceAnnotatedName));
            });
            schema.HasInterface<PlainInterface>().Should().BeFalse();
            schema.HasInterface<PlainInterfaceAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var poci = _.Interface<PlainInterface>();
                Action remove = () => poci.ClrType(null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(untyped_item_can_have_clr_type_added))]
        [Fact]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").ClrType<PlainInterface>(); });
            schema.GetInterface("Foo").ClrType.Should().Be<PlainInterface>();
        }


        [Spec(nameof(untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "needs impl")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            Schema.Create(_ =>
            {
                _.Interface<PlainInterface>();
                var foo = _.Interface("Foo");
                Action add = () => foo.ClrType<PlainInterface>();
                add.Should().Throw<DuplicateClrTypeException>().WithMessage("x");
            });
        }
    }
}