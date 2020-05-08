// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces
{
    [NoReorder]
    public abstract class InterfacesTestsScaffold
    {
        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .subsequently_clr_typed_item_cannot_have_custom_name_removed_if_clr_type_name_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            subsequently_clr_typed_item_cannot_have_custom_name_removed_if_clr_type_name_conflicts_with_type_identity_of_opposite_io_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .subsequently_clr_typed_item_cannot_have_custom_name_removed_if_clr_type_name_annotation_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "TODO")]
        public void
            subsequently_clr_typed_item_cannot_have_custom_name_removed_if_clr_type_name_annotation_conflicts_with_type_identity_of_opposite_io_()
        {
            var schema = Schema.Create(_ => { });
        }
    }
}