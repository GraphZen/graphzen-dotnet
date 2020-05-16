// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NamedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces.InterfaceType.Fields.Field.Arguments
{
    [NoReorder]
    public abstract class NamedCollectionTests
    {
        [Spec(nameof(named_item_can_be_added))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(named_item_cannot_be_added_with_null_value))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_added_with_null_value_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(named_item_cannot_be_added_with_invalid_name))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_added_with_invalid_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(named_item_can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_renamed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(named_item_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_removed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(named_item_cannot_be_removed_with_null_value))]
        [Fact(Skip = "TODO")]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move NamedCollectionTests into a separate file to start writing tests
    [NoReorder]
    public class NamedCollectionTestsScaffold
    {
    }
}
// Source Hash Code: 6133616178736271004