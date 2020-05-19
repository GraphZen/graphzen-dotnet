// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.InputXorOutputTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces
{
    [NoReorder]
    public abstract class InputXorOutputTypeTestsScaffold
    {
        [Spec(nameof(clr_type_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void
            clr_type_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_ioschemaBuilder()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(
            clr_type_cannot_be_added_via_type_param_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            clr_type_cannot_be_added_via_type_param_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_ioschemaBuilder()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_ioschemaBuilder()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }


        [Spec(nameof(clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void
            clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_ioschemaBuilder()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }
    }
}
// Source Hash Code: 14308986584620059202