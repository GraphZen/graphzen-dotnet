// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.DirectiveAnnotations
{
    [NoReorder]
    public class SchemaBuilderDirectiveAnnotationsTests
    {
        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact(Skip = "generated")]
        public void named_item_can_be_renamed()
        {
            // Priority: Low
            var schema = Schema.Create(_ => { });
        }
    }
}