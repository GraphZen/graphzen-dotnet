// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Enums.EnumType.ClrType
{
    [NoReorder]
    public class ClrTypeTests
    {
        public const string AnnotatedName = nameof(AnnotatedName);

        private enum PlainEnum
        {
        }

        [GraphQLName(AnnotatedName)]
        private enum PlainEnumAnnotatedName
        {
        }

        [GraphQLName("abc ()(*322*&%^")]
        // ReSharper disable once UnusedType.Local
        private enum PlainEnumInvalidNameAnnotation
        {
        }

        [Spec(nameof(clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnum>().ClrType<PlainEnumAnnotatedName>(); });
            schema.HasEnum<PlainEnum>().Should().BeFalse();
            schema.HasEnum<PlainEnumAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var poce = _.Enum<PlainEnum>();
                Action change = () => poce.ClrType(null!);
                change.Should().ThrowArgumentNullException("clrType");
            });
        }

        [Spec(nameof(untyped_item_can_have_clr_type_added))]
        [Fact]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.Enum("Foo").ClrType<PlainEnum>(); });
            schema.GetEnum("Foo").ClrType.Should().Be<PlainEnum>();
        }


        [Spec(nameof(untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "needs impl")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            Schema.Create(_ =>
            {
                _.Enum<PlainEnum>();
                var foo = _.Enum("Foo");
                Action add = () => foo.ClrType<PlainEnum>();
                add.Should().Throw<DuplicateClrTypeException>();
            });
        }
    }
}