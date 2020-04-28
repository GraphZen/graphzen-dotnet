// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.DirectiveAnnotations
{
    [NoReorder]
    public  class SchemaBuilderDirectiveAnnotationsTests {

// Priority: Low
// Subject Name: DirectiveAnnotations
        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added))]
        [Fact(Skip = "generated")]
        public void named_item_can_be_added() {
            var schema = Schema.Create(_ => {

            });
        }
[Spec(nameof(NamedCollectionSpecs.named_item_can_be_removed))]
[Fact(Skip = "generated")]
public void named_item_can_be_removed() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
}





    }
}