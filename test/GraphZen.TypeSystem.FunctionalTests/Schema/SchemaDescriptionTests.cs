// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.FunctionalTests
{
    [SpecSubject(nameof(Schema.Description))]
    public class SchemaDescriptionTests : TypeSystemSpecTests
    {
        [Spec(nameof(ConfigurableItemSpecs.Hello))]
        public void sometest()
        {
        }
    }
}