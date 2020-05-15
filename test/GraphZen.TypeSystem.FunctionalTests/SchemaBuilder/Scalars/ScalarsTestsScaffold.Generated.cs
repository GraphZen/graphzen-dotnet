// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Scalars
{
    [NoReorder]
    public abstract class ScalarsTestsScaffold
    {
        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_can_be_added_via_type_param))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_uses_clr_type_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_uses_clr_type_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypedCollectionSpecs.clr_typed_item_with_name_annotation_uses_clr_type_name_annotation))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_with_name_annotation_uses_clr_type_name_annotation_()
        {
            // var schema = Schema.Create(_ => { });
        }


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


            }
}
// Source Hash Code: 16957263938736570200