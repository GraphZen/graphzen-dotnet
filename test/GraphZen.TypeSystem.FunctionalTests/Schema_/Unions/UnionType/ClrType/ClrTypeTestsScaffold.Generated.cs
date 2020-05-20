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
        [Spec(nameof(setting_clr_type_and_inferring_name_name_should_be_valid))]
        [Fact(Skip = "TODO")]
        public void setting_clr_type_and_inferring_name_name_should_be_valid_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(setting_clr_type_does_not_change_name))]
        [Fact(Skip = "TODO")]
        public void setting_clr_type_does_not_change_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(setting_clr_type_with_name_annotation_does_not_change_name))]
        [Fact(Skip = "TODO")]
        public void setting_clr_type_with_name_annotation_does_not_change_name_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 5751137359992631200