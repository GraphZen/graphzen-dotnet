// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;

namespace GraphZen.Configuration
{
    public abstract class InputObjectTypeNameTestsBase : LeafElementConfigurationTests<INamed, IMutableNamed,
        InputObjectTypeDefinition, InputObjectType>
    {
        [Fact]
        public override void define_by_data_annotation() => base.define_by_data_annotation();

        [Fact]
        public override void defined_by_convention() => base.defined_by_convention();
    }
}