// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.InputObjects.InputObjectType.Fields.InputField.FieldType
{
    [NoReorder]
    public class TypeReferenceTests
    {
        [Spec(nameof(TypeSystemSpecs.TypeReferenceSpecs.it_can_be_created_if_type_matches_own_io_identity))]
        [Fact]
        public void it_can_be_created_if_type_matches_own_io_identity_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo");
                _.InputObject("Bar").Field("field", "Foo");
            });
            var foo = schema.GetInputObject("Foo");
            schema.GetInputObject("Bar").GetField("field").FieldType.GetNamedType().Should().Be(foo);
        }


        [Spec(nameof(TypeSystemSpecs.TypeReferenceSpecs
            .it_can_be_created_with_clr_type_if_type_matches_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void it_can_be_created_with_clr_type_if_type_matches_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.TypeReferenceSpecs.it_cannot_be_created_if_type_conflicts_with_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void it_cannot_be_created_if_type_conflicts_with_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.TypeReferenceSpecs
            .it_cannot_be_created_with_clr_type_if_type_conflicts_with_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void it_cannot_be_created_with_clr_type_if_type_conflicts_with_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.TypeReferenceSpecs.type_can_be_set_if_type_matches_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void type_can_be_set_if_type_matches_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.TypeReferenceSpecs.type_can_be_set_with_clr_type_if_type_matches_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void type_can_be_set_with_clr_type_if_type_matches_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.TypeReferenceSpecs.type_cannot_be_set_if_type_conflicts_with_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void type_cannot_be_set_if_type_conflicts_with_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.TypeReferenceSpecs
            .type_cannot_be_set__with_clr_type_if_type_conflicts_with_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void type_cannot_be_set__with_clr_type_if_type_conflicts_with_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}