#nullable disable
using System.Collections.Generic;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using Xunit;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute

namespace GraphZen
{
    [NoReorder]
    public class LeafExplicitConfigurationTests : TestDataHelper<ILeafExplicitConfigurationFixture>
    {
        public static IEnumerable<object[]> FixtureData { get; } =
            ConfigurationFixtures.GetAll<ILeafExplicitConfigurationFixture>().ToTestData();


        [Theory]
        [MemberData(nameof(FixtureData))]
        public void parent_should_be_of_expected_type(ILeafExplicitConfigurationFixture fixture)
        {
            TestData(fixture, () =>
            {
                var parentName = "parent";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, parentName);
                    fixture.GetParent(sb, parentName).Should().BeOfType(fixture.ParentMemberDefinitionType);
                });
                fixture.GetParent(schema, parentName).Should().BeOfType(fixture.ParentMemberType);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void initial_value(ILeafExplicitConfigurationFixture fixture)

        {
            TestData(fixture, () =>
            {
                var parentName = "parent";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, parentName);
                    var parentDef = fixture.GetParent(sb, parentName);
                    fixture.GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                    fixture.TryGetValue(parentDef, out _).Should().BeFalse();
                });

                var parent = fixture.GetParent(schema, parentName);
                fixture.TryGetValue(parent, out _).Should().BeFalse();
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void initial_value_then_configured_explicitly(ILeafExplicitConfigurationFixture fixture)

        {
            TestData(fixture, () =>
            {
                var parentName = "parent";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, parentName);
                    var parentDef = fixture.GetParent(sb, parentName);
                    fixture.ConfigureExplicitly(sb, parentName, fixture.ValueA);
                    fixture.GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                    fixture.TryGetValue(parentDef, out var confVal).Should().BeTrue();
                    confVal.Should().Be(fixture.ValueA);
                });

                var parent = fixture.GetParent(schema, parentName);
                fixture.TryGetValue(parent, out var finalVal).Should().BeTrue();
                finalVal.Should().Be(fixture.ValueA);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void initial_value_then_configured_then_reconfigured_explicitly(
            ILeafExplicitConfigurationFixture fixture)

        {
            TestData(fixture, () =>
            {
                var parentName = "parent";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, parentName);
                    var parentDef = fixture.GetParent(sb, parentName);
                    fixture.ConfigureExplicitly(sb, parentName, fixture.ValueA);
                    fixture.ConfigureExplicitly(sb, parentName, fixture.ValueB);
                    fixture.GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                    fixture.TryGetValue(parentDef, out var confVal).Should().BeTrue();
                    confVal.Should().Be(fixture.ValueB);
                });

                var parent = fixture.GetParent(schema, parentName);
                fixture.TryGetValue(parent, out var finalVal).Should().BeTrue();
                finalVal.Should().Be(fixture.ValueB);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void initial_value_then_reconfigured_explicitly_then_removed(ILeafExplicitConfigurationFixture fixture)

        {
            TestData(fixture, () =>
            {
                var parentName = "parent";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, parentName);
                    var parentDef = fixture.GetParent(sb, parentName);
                    fixture.ConfigureExplicitly(sb, parentName, fixture.ValueA);
                    fixture.GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                    fixture.TryGetValue(parentDef, out _).Should().BeTrue();
                    fixture.RemoveValue(sb, parentName);
                    fixture.TryGetValue(parentDef, out _).Should().BeFalse();
                });

                var parent = fixture.GetParent(schema, parentName);
                fixture.TryGetValue(parent, out _).Should().BeFalse();
            });
        }
    }
}