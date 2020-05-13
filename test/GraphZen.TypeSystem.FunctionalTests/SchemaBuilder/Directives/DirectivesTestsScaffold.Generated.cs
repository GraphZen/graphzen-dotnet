// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Directives
{
    [NoReorder]
    public abstract class DirectivesTestsScaffold
    {
        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_null_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_added_with_null_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_invalid_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_added_with_invalid_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_added_with_duplicate_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_added_with_duplicate_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_renamed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 8823435487479449050