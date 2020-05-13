// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Scalars.ScalarType.ClrType
{
    [NoReorder]
    public abstract class ClrTypeTestsScaffold
    {
        [Spec(nameof(ClrTypeSpecs.cannot_add_clr_type_to_item_via_type_param_with_conflicting_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_via_type_param_with_conflicting_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.cannot_add_clr_type_to_item_with_invalid_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_invalid_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.cannot_add_clr_type_to_item_via_type_param_with_invalid_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_via_type_param_with_invalid_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.cannot_add_clr_type_to_item_with_null_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_null_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(ClrTypeSpecs.cannot_add_clr_type_to_item_via_type_param_with_null_custom_name))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_via_type_param_with_null_custom_name_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }
}
// Source Hash Code: 16279705336847473529