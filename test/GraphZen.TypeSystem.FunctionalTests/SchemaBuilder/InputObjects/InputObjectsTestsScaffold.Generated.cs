// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.InputObjects
{
    [NoReorder]
    public abstract class InputObjectsTestsScaffold
    {
        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.adding_clr_type_with_name_annotation_to_item_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_with_name_annotation_to_item_changes_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_with_custom_name_if_name_is_null))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_custom_name_if_name_is_null_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.cannot_add_clr_type_to_item_with_custom_name_if_name_is_invalid))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_custom_name_if_name_is_invalid_()
        {
            var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 9036299587457867226