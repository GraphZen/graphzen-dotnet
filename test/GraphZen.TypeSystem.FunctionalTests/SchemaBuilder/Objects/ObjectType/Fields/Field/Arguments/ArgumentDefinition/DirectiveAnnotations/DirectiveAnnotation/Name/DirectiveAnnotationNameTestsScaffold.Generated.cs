// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects.ObjectType.Fields.Field.Arguments.ArgumentDefinition
    .DirectiveAnnotations.DirectiveAnnotation.Name
{
    [NoReorder]
    public abstract class DirectiveAnnotationNameTests
    {
        [Spec(nameof(RequiredSpecs.parent_cannot_be_created_without_required_item))]
        [Fact]
        public void parent_cannot_be_created_without_required_item()
        {
            // Priority: High
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(RequiredSpecs.required_item_cannot_be_removed))]
        [Fact]
        public void required_item_cannot_be_removed()
        {
            // Priority: High
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }


        [Spec(nameof(UpdateableSpecs.updateable_item_can_be_updated))]
        [Fact]
        public void updateable_item_can_be_updated()
        {
            // Priority: High
            var schema = Schema.Create(_ => { });
            throw new NotImplementedException();
        }
    }

// Move DirectiveAnnotationNameTests into a separate file to start writing tests
    [NoReorder]
    public class DirectiveAnnotationNameTestsScaffold
    {
    }
}