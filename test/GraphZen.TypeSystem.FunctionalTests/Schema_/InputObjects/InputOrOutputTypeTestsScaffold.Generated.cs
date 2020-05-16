// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.InputObjects
{
    [NoReorder]
    public abstract class InputOrOutputTypeTests
    {
// SpecId: named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io
// fileDir: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\InputObjects

        [Spec(nameof(InputOrOutputTypeSpecs
            .named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }


// SpecId: named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io
// fileDir: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\InputObjects

        [Spec(nameof(InputOrOutputTypeSpecs
            .named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }


// SpecId: clr_type_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io
// fileDir: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\InputObjects

        [Spec(nameof(InputOrOutputTypeSpecs
            .clr_type_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void clr_type_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }


// SpecId: clr_type_cannot_be_added_via_type_param_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io
// fileDir: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\InputObjects

        [Spec(nameof(InputOrOutputTypeSpecs
            .clr_type_cannot_be_added_via_type_param_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            clr_type_cannot_be_added_via_type_param_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }


// SpecId: clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io
// fileDir: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\InputObjects

        [Spec(nameof(InputOrOutputTypeSpecs
            .clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }


// SpecId: clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io
// fileDir: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\InputObjects

        [Spec(nameof(InputOrOutputTypeSpecs
            .clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }


// SpecId: clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io
// fileDir: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\InputObjects

        [Spec(nameof(InputOrOutputTypeSpecs
            .clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_cannot_be_added_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }


// SpecId: clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io
// fileDir: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\InputObjects

        [Spec(nameof(InputOrOutputTypeSpecs
            .clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "TODO")]
        public void clr_type_cannot_be_changed_with_custom_name_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move InputOrOutputTypeTests into a separate file to start writing tests
    [NoReorder]
    public class InputOrOutputTypeTestsScaffold
    {
    }
}
// Source Hash Code: 17004709358923462989