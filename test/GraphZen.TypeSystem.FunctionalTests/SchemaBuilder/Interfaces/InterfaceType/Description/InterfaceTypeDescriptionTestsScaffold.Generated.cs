// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Description
{
    [NoReorder]
    public abstract class InterfaceTypeDescriptionTests
    {
        [Spec(nameof(UpdateableSpecs.it_can_be_updated))]
        [Fact]
        public void it_can_be_updated()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
        [Fact]
        public void optional_item_can_be_removed()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(OptionalSpecs.parent_can_be_created_without))]
        [Fact]
        public void parent_can_be_created_without()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }
    }

// Move InterfaceTypeDescriptionTests into a separate file to start writing tests
    [NoReorder]
    public class InterfaceTypeDescriptionTestsScaffold
    {
    }
}