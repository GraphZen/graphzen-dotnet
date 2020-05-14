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

        [Spec(nameof(clr_type_can_be_changed))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnum>().ClrType<PlainEnumAnnotatedName>(); });
            schema.HasEnum<PlainEnum>().Should().BeFalse();
            schema.HasEnum<PlainEnumAnnotatedName>().Should().BeTrue();
        }


        [Spec(nameof(clr_type_cannot_be_null))]
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


        [Spec(nameof(clr_type_should_be_unique))]
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

        [Spec(nameof(clr_type_can_be_removed))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnum>().RemoveClrType(); });
            schema.GetEnum(nameof(PlainEnum)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_when_type_removed_should_retain_name))]
        [Fact(Skip = "nees impl")]
        public void clr_typed_item_with_type_removed_should_retain_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnum>().RemoveClrType(); });
            schema.HasEnum(nameof(PlainEnum)).Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_when_clr_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.Enum<PlainEnumAnnotatedName>().RemoveClrType(); });
            schema.HasEnum(AnnotatedName).Should().BeTrue();
        }
    }
}