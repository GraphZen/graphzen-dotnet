// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.SdlSpec;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Directives.Directive.Description
{
    [NoReorder]
    public abstract class SdlTestsScaffold
    {
        [Spec(nameof(item_can_be_defined_by_sdl))]
        [Fact(Skip = "TODO")]
        public void item_can_be_defined_by_sdl_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 11263769167738868283