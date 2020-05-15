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
            .adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type))]
        [Fact(Skip = "TODO")]
        public void adding_clr_typed_item_with_name_annotation_updates_matching_named_items_clr_type_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 7228465489695183390