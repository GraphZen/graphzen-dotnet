// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives
{
    [NoReorder]
    public abstract class ClrTypedCollectionTestsScaffold
    {
        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_nameschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotation))]
        [Fact(Skip = "TODO")]
        public void
            adding_clr_typed_item_with_custom_name_does_not_update_item_matching_clr_type_name_annotationschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 1069205526146831742