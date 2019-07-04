// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using FluentAssertions;
using GraphZen.Infrastructure;

using Xunit;

namespace GraphZen.TypeSystem
{
    [NoReorder]
    [UsedImplicitly]
    public abstract class NamedContainerItemConfigurationTests : NameConfigurationByExplicitConfigurationTests
    {
        [Fact]
        public void name_set_by_convention_overridden_by_explicit_configuration_with_conflicting_name_throws_exception()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMemberNamedByConvention(_);
                Action setConflictingName = () => { SetNameOnMemberNamedByConvention(_, "CustomName"); };
                setConflictingName.Should().Throw<InvalidOperationException>().WithMessage(
                    $"Cannot rename * {ConventionalName} to 'CustomName'. * already contains a * named 'CustomName'.");
            });
            GetMemberNamedByConvention(schema).Name.Should().Be(ConventionalName);
            GetMemberNamedByDataAnnotation(schema).Name.Should().Be("CustomName");
        }


        [Fact]
        public void
            name_set_by_data_annotation_overridden_by_explicit_configuration_with_conflicting_name_throws_exception()
        {
            var schema = Schema.Create(_ =>
            {
                CreateMemberWithCustomNameAttribute(_);
                Action setConflictingName = () => { SetNameOnMemberNamedByDataAnnotation(_, ConventionalName); };
                setConflictingName.Should().Throw<InvalidOperationException>()
                    .WithMessage(
                        $"Cannot rename * CustomName to '{ConventionalName}'. * already contains a * named '{ConventionalName}'.");
            });
            GetMemberNamedByConvention(schema).Name.Should().Be(ConventionalName);
            GetMemberNamedByDataAnnotation(schema).Name.Should().Be("CustomName");
        }
    }
}