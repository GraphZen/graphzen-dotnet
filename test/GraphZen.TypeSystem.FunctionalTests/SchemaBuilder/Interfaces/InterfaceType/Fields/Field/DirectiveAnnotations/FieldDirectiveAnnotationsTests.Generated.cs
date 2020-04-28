// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable PartialTypeWithSinglePart
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Fields.Field.DirectiveAnnotations
{
    public partial class FieldDirectiveAnnotationsTests
    {
// Priority: Low
// Subject Name: DirectiveAnnotations
        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added()
        {
            var schema = Schema.Create(_ => { });
        }


// Priority: Low
// Subject Name: DirectiveAnnotations
        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed()
        {
            var schema = Schema.Create(_ => { });
        }


// Priority: Low
// Subject Name: DirectiveAnnotations
        [Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists()
        {
            var schema = Schema.Create(_ => { });
        }


// Priority: Low
// Subject Name: DirectiveAnnotations
        [Spec(nameof(NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact]
        public void named_item_can_be_removed()
        {
            var schema = Schema.Create(_ => { });
        }


// Priority: Low
// Subject Name: DirectiveAnnotations
        [Spec(nameof(NamedCollectionSpecs.named_item_name_must_be_valid_name))]
        [Fact]
        public void named_item_name_must_be_valid_name()
        {
            var schema = Schema.Create(_ => { });
        }
    }
}