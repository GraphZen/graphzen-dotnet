// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.InputObjects.InputObjectType.Fields.InputField
{
    [NoReorder]
    public abstract class InputFieldSdlExtensionSpec
    {
        [Spec(nameof(SdlExtensionSpec.item_can_be_defined_by_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void item_can_be_defined_by_sdl_extension_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move InputFieldSdlExtensionSpec into a separate file to start writing tests
    [NoReorder]
    public class InputFieldSdlExtensionSpecScaffold
    {
    }
}
// Source Hash Code: 11267827273099307680