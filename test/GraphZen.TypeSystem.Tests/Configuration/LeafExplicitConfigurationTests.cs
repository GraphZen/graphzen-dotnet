// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests.Configuration
{
    [NoReorder]
    public class LeafExplicitConfigurationTests : TestDataFixtureRunner<ILeafExplicitConfigurationFixture>
    {
        public static IEnumerable<object[]> FixtureData { get; } =
            ConfigurationFixtures.GetAll<ILeafExplicitConfigurationFixture>().ToTestData();


        [Theory]
        [MemberData(nameof(FixtureData))]
        public void parent_should_be_of_expected_type(ILeafExplicitConfigurationFixture fixture)
        {
            RunFixture(fixture, () =>
            {
                var parentName = "parent";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, parentName);
                    Assert.IsType(fixture.ParentMemberDefinitionType, fixture.GetParent(sb, parentName));
                });
                Assert.IsType(fixture.ParentMemberType, fixture.GetParent(schema, parentName));
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void initial_value(ILeafExplicitConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var parentName = "parent";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, parentName);
                    var parentDef = fixture.GetParent(sb, parentName);
                    Assert.Equal(ConfigurationSource.Convention, fixture.GetElementConfigurationSource(parentDef));
                    Assert.False(fixture.TryGetValue(parentDef, out _));
                });

                var parent = fixture.GetParent(schema, parentName);
                Assert.False(fixture.TryGetValue(parent, out _));
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void initial_value_then_configured_explicitly(ILeafExplicitConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var parentName = "parent";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, parentName);
                    var parentDef = fixture.GetParent(sb, parentName);
                    fixture.ConfigureExplicitly(sb, parentName, fixture.ValueA);
                    Assert.Equal(ConfigurationSource.Explicit, fixture.GetElementConfigurationSource(parentDef));
                    Assert.True(fixture.TryGetValue(parentDef, out var confVal));
                    Assert.Equal(fixture.ValueA, confVal);
                });

                var parent = fixture.GetParent(schema, parentName);
                Assert.True(fixture.TryGetValue(parent, out var finalVal));
                Assert.Equal(fixture.ValueA, finalVal);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void initial_value_then_configured_then_reconfigured_explicitly(
            ILeafExplicitConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var parentName = "parent";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, parentName);
                    var parentDef = fixture.GetParent(sb, parentName);
                    fixture.ConfigureExplicitly(sb, parentName, fixture.ValueA);
                    fixture.ConfigureExplicitly(sb, parentName, fixture.ValueB);
                    Assert.Equal(ConfigurationSource.Explicit, fixture.GetElementConfigurationSource(parentDef));
                    Assert.True(fixture.TryGetValue(parentDef, out var confVal));
                    Assert.Equal(fixture.ValueB, confVal);
                });

                var parent = fixture.GetParent(schema, parentName);
                Assert.True(fixture.TryGetValue(parent, out var finalVal));
                Assert.Equal(fixture.ValueB, finalVal);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void initial_value_then_reconfigured_explicitly_then_removed(ILeafExplicitConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var parentName = "parent";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, parentName);
                    var parentDef = fixture.GetParent(sb, parentName);
                    fixture.ConfigureExplicitly(sb, parentName, fixture.ValueA);
                    Assert.Equal(ConfigurationSource.Explicit, fixture.GetElementConfigurationSource(parentDef));
                    Assert.True(fixture.TryGetValue(parentDef, out _));
                    fixture.RemoveValue(sb, parentName);
                    Assert.False(fixture.TryGetValue(parentDef, out _));
                });

                var parent = fixture.GetParent(schema, parentName);
                Assert.False(fixture.TryGetValue(parent, out _));
            });
        }
    }
}
