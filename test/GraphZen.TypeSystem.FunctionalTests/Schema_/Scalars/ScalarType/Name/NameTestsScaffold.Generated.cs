// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NameSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Scalars.ScalarType.Name
{
    [NoReorder]
    public abstract class NameTestsScaffold
    {
        [Spec(nameof(can_be_renamed))]
        [Fact(Skip = "TODO")]
        public void can_be_renamed_()
        {
            // var schema = Schema.Create(schemaBuilder => { });
        }
    }
}
// Source Hash Code: 9199628806425098234