// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces.InterfaceType.Name
{
// testFile: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\Interfaces\InterfaceType\Name\NameTests.cs
// testFileExists: True
// fileDir: .\test\GraphZen.TypeSystem.FunctionalTests\Schema_\Interfaces\InterfaceType\Name

    [NoReorder]
    public abstract class NameTestsScaffold
    {
// SpecId: can_be_renamed
// isTestImplemented: False
        [Spec(nameof(NameSpecs.can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void can_be_renamed_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 839491083127940036