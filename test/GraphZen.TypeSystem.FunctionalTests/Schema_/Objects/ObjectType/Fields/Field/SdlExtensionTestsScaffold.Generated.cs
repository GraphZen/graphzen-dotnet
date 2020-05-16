// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType.Fields.Field
{
// testFile: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\Objects\ObjectType\Fields\Field\SdlExtensionTests.cs
// testFileExists: False
// fileDir: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\Objects\ObjectType\Fields\Field

    [NoReorder]
    public abstract class SdlExtensionTests
    {
// SpecId: item_can_be_defined_by_sdl_extension
// isTestImplemented: False
// subject.Path: Schema_.Objects.ObjectType.Fields.Field
        [Spec(nameof(SdlExtensionSpec.item_can_be_defined_by_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void item_can_be_defined_by_sdl_extension_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move SdlExtensionTests into a separate file to start writing tests
    [NoReorder]
    public class SdlExtensionTestsScaffold
    {
    }
}
// Source Hash Code: 14592783773542913966