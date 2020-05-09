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
    }
}
// Source Hash Code: 9877114025107505354