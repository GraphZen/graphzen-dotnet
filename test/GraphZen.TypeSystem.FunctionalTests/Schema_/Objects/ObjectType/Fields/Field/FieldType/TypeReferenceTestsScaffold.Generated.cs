// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.TypeReferenceSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType.Fields.Field.FieldType
{
    [NoReorder]
    public abstract class TypeReferenceTests
    {
        [Spec(nameof(it_can_be_created_if_type_matches_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void it_can_be_created_if_type_matches_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(it_can_be_created_with_clr_type_if_type_matches_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void it_can_be_created_with_clr_type_if_type_matches_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(it_cannot_be_created_if_type_conflicts_with_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void it_cannot_be_created_if_type_conflicts_with_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(it_cannot_be_created_with_clr_type_if_type_conflicts_with_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void it_cannot_be_created_with_clr_type_if_type_conflicts_with_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_can_be_set_if_type_matches_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void type_can_be_set_if_type_matches_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_can_be_set_with_clr_type_if_type_matches_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void type_can_be_set_with_clr_type_if_type_matches_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_cannot_be_set_if_type_conflicts_with_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void type_cannot_be_set_if_type_conflicts_with_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(type_cannot_be_set__with_clr_type_if_type_conflicts_with_own_io_identity))]
        [Fact(Skip = "TODO")]
        public void type_cannot_be_set__with_clr_type_if_type_conflicts_with_own_io_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move TypeReferenceTests into a separate file to start writing tests
    [NoReorder]
    public class TypeReferenceTestsScaffold
    {
    }
}
// Source Hash Code: 16889767536924516338