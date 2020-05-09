// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Interfaces
{
    [NoReorder]
    public abstract class InterfacesTests
    {
        [Spec(nameof(NamedTypeSetSpecs.set_item_can_be_added))]
        [Fact(Skip = "TODO")]
        public void set_item_can_be_added_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedTypeSetSpecs.set_item_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void set_item_can_be_removed_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(NamedTypeSetSpecs.set_item_must_be_valid_name))]
        [Fact(Skip = "TODO")]
        public void set_item_must_be_valid_name_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move InterfacesTests into a separate file to start writing tests
    [NoReorder]
    public class InterfacesTestsScaffold
    {
    }
}
// Source Hash Code: 9849013722709211105