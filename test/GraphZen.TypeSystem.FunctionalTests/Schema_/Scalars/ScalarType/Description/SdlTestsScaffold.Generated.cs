// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Scalars.ScalarType.Description
{
// testFile: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\Scalars\ScalarType\Description\SdlTests.cs
// testFileExists: False
// fileDir: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\Scalars\ScalarType\Description

    [NoReorder]
    public abstract class SdlTests
    {
// SpecId: item_can_be_defined_by_sdl
// isTestImplemented: False
        [Spec(nameof(SdlSpec.item_can_be_defined_by_sdl))]
        [Fact(Skip = "TODO")]
        public void item_can_be_defined_by_sdl_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move SdlTests into a separate file to start writing tests
    [NoReorder]
    public class SdlTestsScaffold
    {
    }
}
// Source Hash Code: 11968639547164699134