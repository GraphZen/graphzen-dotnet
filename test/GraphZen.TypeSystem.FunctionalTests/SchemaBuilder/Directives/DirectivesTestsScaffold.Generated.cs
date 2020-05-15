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
        [Spec(nameof(ClrTypedCollectionSpecs
            .clr_typed_item_with_name_annotation_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist
        ))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_with_name_annotation_cannot_be_added_with_custom_name_if_named_and_typed_items_already_exist_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 7952810746529116468