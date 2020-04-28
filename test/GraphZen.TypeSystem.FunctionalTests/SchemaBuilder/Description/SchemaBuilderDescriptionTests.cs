// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Description
{
    [NoReorder] //
    public class SchemaBuilderDescriptionTests
    {
        // Move me into a separate file to start writing tests

        // Priority: Low
        // Subject Name: Description
        [Spec(nameof(TypeSystemSpecs.UpdateableSpecs.it_can_be_updated))]
        [Fact]
        public void it_can_be_updated()
        {
            var schema = Schema.Create(_ =>
            {

            });
        }
    }
}