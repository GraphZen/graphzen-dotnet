// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable PartialTypeWithSinglePart
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects.ObjectType.Fields.Field.Arguments.ArgumentDefinition
    .InputTypeRef
{
    public partial class ArgumentDefinitionInputTypeRefTests
    {
// Priority: Low
// Subject Name: InputTypeRef
        [Spec(nameof(UpdateableSpecs.it_can_be_updated))]
        [Fact]
        public void it_can_be_updated()
        {
            var schema = Schema.Create(_ => { });
        }


// Priority: Low
// Subject Name: InputTypeRef
        [Spec(nameof(RequiredSpecs.required_item_cannot_be_removed))]
        [Fact]
        public void required_item_cannot_be_removed()
        {
            var schema = Schema.Create(_ => { });
        }
    }
}