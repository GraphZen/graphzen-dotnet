// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.DirectiveAnnotations
{
    [NoReorder]
    public class SchemaBuilderDirectiveAnnotationsTests {

        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added() {
            // Priority: Low
            var schema = Schema.Create(_ => {

            });
            throw new NotImplementedException();
        }



        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact]
        public void named_item_can_be_removed() {
            // Priority: Low
            var schema = Schema.Create(_ => {

            });
            throw new NotImplementedException();
        }



        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed() {
            // Priority: Low
            var schema = Schema.Create(_ => {

            });
            throw new NotImplementedException();
        }



        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists() {
            // Priority: Low
            var schema = Schema.Create(_ => {

            });
            throw new NotImplementedException();
        }



        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_name_must_be_valid_name))]
        [Fact]
        public void named_item_name_must_be_valid_name() {
            // Priority: Low
            var schema = Schema.Create(_ => {

            });
            throw new NotImplementedException();
        }


    }
}