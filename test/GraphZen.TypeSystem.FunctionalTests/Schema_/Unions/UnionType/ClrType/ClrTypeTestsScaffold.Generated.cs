// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Unions.UnionType.ClrType
{
    [NoReorder]
    public abstract class ClrTypeTestsScaffold
    {
        [Spec(nameof(setting_clr_type_does_not_change_name))]
        [Fact(Skip = "TODO")]
        public void setting_clr_type_does_not_change_nameschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(setting_clr_type_with_name_annotation_does_not_change_name))]
        [Fact(Skip = "TODO")]
        public void setting_clr_type_with_name_annotation_does_not_change_nameschemaBuilder()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 3435538804031307666