// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.


using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.InputObjects.InputObjectType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
    {
        private class PlainClass
        {
        }

        [GraphQLName(AnnotatedName)]
        private class PlainClassAnnotatedName
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName(InvalidName)]
        // ReSharper disable once UnusedType.Local
        private class PlainClassInvalidNameAnnotation
        {
            public const string InvalidName = "abc @#$%^";
        }

        [Spec(nameof(clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ => { _.InputObject<PlainClass>().ClrType<PlainClassAnnotatedName>(); });
            schema.HasInputObject<PlainClassAnnotatedName>().Should().BeTrue();
            schema.HasInputObject<PlainClass>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var poco = _.InputObject<PlainClass>();
                Action remove = () => poco.ClrType(null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(untyped_item_can_have_clr_type_added))]
        [Fact]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").ClrType<PlainClass>(); });
            schema.HasInputObject<PlainClass>().Should().BeTrue();
        }


        [Spec(nameof(untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "needs implementation")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            Schema.Create(_ =>
            {
                _.InputObject<PlainClass>();
                var foo = _.InputObject("Foo");
                Action add = () => foo.ClrType<PlainClass>();
                add.Should().Throw<DuplicateClrTypeException>();
            });
        }
    }
}