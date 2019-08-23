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
    public class LeafExplicitConfigurationTests : FixtureRunner<ILeafExplicitConfigurationFixture>
    {
        protected override IEnumerable<ILeafExplicitConfigurationFixture> GetFixtures() =>
            ConfigurationFixtures.GetAll<ILeafExplicitConfigurationFixture>();


        [Fact]
        public void parent_should_be_of_expected_type()
        {
            TestFixtures(fixture =>
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

        [Fact]
        public void initial_value()
        {
            TestFixtures(fixture =>
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

        [Fact]
        public void initial_value_then_configured_explicitly()
        {
            TestFixtures(fixture =>
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

        [Fact]
        public void initial_value_then_configured_then_reconfigured_explicitly()
        {
            TestFixtures(fixture =>
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

        [Fact]
        public void initial_value_then_reconfigured_explicitly_then_removed()
        {
            TestFixtures(fixture =>
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