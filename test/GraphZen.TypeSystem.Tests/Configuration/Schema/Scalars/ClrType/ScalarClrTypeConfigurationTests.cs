// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests.Configuration.Scalars.ClrType
{
    [NoReorder]
    public class ScalarClrTypeConfigurationTests
    {
        public class ExampleScalar
        {
        }

        [Fact]
        public void scalar_added_explicitly_subsequently_referenced_by_matching_clr_type_should_have_clr_type_set()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Scalar(nameof(ExampleScalar));
                var def = sb.GetDefinition().GetScalar(nameof(ExampleScalar));
                sb.Scalar<ExampleScalar>();
                def.ClrType.Should().Be<ExampleScalar>();
            });
            schema.GetScalar<ExampleScalar>().ClrType.Should().Be<ExampleScalar>();
        }

        [Fact]
        public void
            scalar_added_explicitly_subsequently_referenced_by_matching_clr_type_via_field_should_have_clr_type_set()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Scalar(nameof(ExampleScalar));
                sb.Object("Parent").Field<ExampleScalar>("field");
                var def = sb.GetDefinition().GetScalar(nameof(ExampleScalar));
                sb.Scalar<ExampleScalar>();
                def.ClrType.Should().Be<ExampleScalar>();
            });
            schema.GetScalar<ExampleScalar>().ClrType.Should().Be<ExampleScalar>();
        }
    }
}