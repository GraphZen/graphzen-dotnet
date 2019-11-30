// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Configuration.Infrastructure;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.Configuration
{
    [NoReorder]
    public class LeafConventionConfigurationTests : TestDataFixtureRunner<ILeafConventionConfigurationFixture>
    {
        public static IEnumerable<object[]> FixtureData { get; } =
            ConfigurationFixtures.GetAll<ILeafConventionConfigurationFixture>().ToTestData();


        [Theory]
        [MemberData(nameof(FixtureData))]
        public void parent_configured_conventionally_optional_configured_by_data_annotation(
            ILeafConventionConfigurationFixture fixture)
        {
            RunFixture(fixture, () =>
            {
                var context = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var parentDef = fixture.GetParent(sb, context.ParentName);
                    fixture.GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                    fixture.TryGetValue(parentDef, out var defVal).Should().BeTrue();
                    defVal.Should().Be(context.DataAnnotationValue);
                });
                var parent = fixture.GetParent(schema, context.ParentName);
                fixture.TryGetValue(parent, out var val).Should().BeTrue();
                val.Should().Be(context.DataAnnotationValue);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void parent_configured_explicitly_then_conventionally_optional_configured_by_data_annotation(
            ILeafConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var context = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, context.ParentName);
                    var parentDef = fixture.GetParent(sb, context.ParentName);
                    fixture.TryGetValue(parentDef, out _).Should().BeFalse();
                    fixture.GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                    fixture.ConfigureContextConventionally(sb);
                    fixture.GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                    fixture.TryGetValue(parentDef, out var defVal).Should().BeTrue();
                    defVal.Should().Be(context.DataAnnotationValue);
                });
                var parent = fixture.GetParent(schema, context.ParentName);
                fixture.TryGetValue(parent, out var val).Should().BeTrue();
                val.Should().Be(context.DataAnnotationValue);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void parent_and_item_configured_explicitly_then_conventionally_optional_configured_by_data_annotation(
            ILeafConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var context = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, context.ParentName);
                    fixture.ConfigureExplicitly(sb, context.ParentName, fixture.ValueA);
                    var parentDef = fixture.GetParent(sb, context.ParentName);
                    fixture.TryGetValue(parentDef, out var defVal1).Should().BeTrue();
                    defVal1.Should().Be(fixture.ValueA);
                    fixture.GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                    fixture.ConfigureContextConventionally(sb);
                    fixture.GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                    fixture.TryGetValue(parentDef, out var defVal2).Should().BeTrue();
                    defVal2.Should().Be(fixture.ValueA);
                });
                var parent = fixture.GetParent(schema, context.ParentName);
                fixture.TryGetValue(parent, out var val).Should().BeTrue();
                val.Should().Be(fixture.ValueA);
            });
        }

        /*
                        public virtual void configured_by_data_annotation()
                        {
                            string parentName = null;
                            var schema = Schema.Create(sb =>
                            {
                                DefineParentConventionallyWithDataAnnotation(sb, out parentName);
                                var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                                TryGetValue(parentDef, out var dataAnnotationValue).Should().BeTrue();
                                dataAnnotationValue.Should().Be(DataAnnotationValue);
                            });
                            var parent = GetParentByName(schema, parentName);
                            TryGetValue(parent, out var finalVal).Should().BeTrue();
                            finalVal.Should().Be(DataAnnotationValue);
                        }

                        public virtual void configured_by_data_annotation_then_reconfigured_explicitly()
                        {
                            string parentName = null;
                            var schema = Schema.Create(sb =>
                            {
                                DefineParentConventionallyWithDataAnnotation(sb, out parentName);
                                var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                                TryGetValue(parentDef, out _).Should().BeTrue();
                                ConfigureExplicitly(sb, parentName, ValueA);
                                TryGetValue(parentDef, out var configuredA).Should().BeTrue();
                                configuredA.Should().Be(ValueA);
                                ConfigureExplicitly(sb, parentName, ValueB);
                                TryGetValue(parentDef, out var configuredB).Should().BeTrue();
                                configuredB.Should().Be(ValueB);
                                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                            });
                            var parent = GetParentByName(schema, parentName);
                            TryGetValue(parent, out var finalVal).Should().BeTrue();
                            finalVal.Should().Be(ValueB);
                        }

                        public virtual void optional_configured_by_data_annotation_then_removed_explicitly()
                        {
                            string parentName = null;
                            var schema = Schema.Create(sb =>
                            {
                                DefineParentConventionallyWithDataAnnotation(sb, out parentName);
                                var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                                TryGetValue(parentDef, out _).Should().BeTrue();
                                RemoveExplicitly(sb, parentName);
                                TryGetValue(parentDef, out _).Should().BeFalse();
                                GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                            });
                            var parent = GetParentByName(schema, parentName);
                            TryGetValue(parent, out _).Should().BeFalse();
                        }

                        public virtual void configure_by_convention()
                        {
                            //var schema = Schema.Create(sb =>
                            //{
                            //    DefineByConvention(sb);
                            //    var parentDef = GetParentDefinitionByName(sb.GetDefinition(), ConventionalParentName);
                            //    GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                            //    TryGetValue(parentDef, out var defVal).Should().BeTrue();
                            //    defVal.Should().Be(ConventionalValue);
                            //});
                            //var parent = GetParentByName(schema, ConventionalParentName);
                            //TryGetValue(parent, out var val).Should().BeTrue();
                            //val.Should().Be(ConventionalValue);
                        }

                        public virtual void define_by_data_annotation()
                        {
                            //var schema = Schema.Create(sb =>
                            //{
                            //    DefineByDataAnnotation(sb);
                            //    var definition = sb.GetDefinition();
                            //    var parentDef = GetParentDefinitionByName(definition, DataAnnotationParentName);
                            //    GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                            //    TryGetValue(parentDef, out var defVal).Should().BeTrue();
                            //    defVal.Should().Be(DataAnnotationValue);
                            //});
                            //var parent = GetParentByName(schema, DataAnnotationParentName);
                            //TryGetValue(parent, out var val).Should().BeTrue();
                            //val.Should().Be(DataAnnotationValue);
                        }

                        public virtual void define_by_data_annotation_overridden_by_explicit_configuration()
                        {
                            //var schema = Schema.Create(sb =>
                            //{
                            //    DefineByDataAnnotation(sb);
                            //    var definition = sb.GetDefinition();
                            //    var parentDef = GetParentDefinitionByName(definition, DataAnnotationParentName);
                            //    GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.DataAnnotation);
                            //    TryGetValue(parentDef, out var defVal).Should().BeTrue();
                            //    defVal.Should().Be(DataAnnotationValue);
                            //    // ConfigureExplicitly(sb, DataAnnotationParentName);
                            //    throw new NotImplementedException();
                            //    //GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                            //    //TryGetValue(parentDef, out var newVal).Should().BeTrue();
                            //    // newVal.Should().Be(ValueA);
                            //});
                            //var parent = GetParentByName(schema, DataAnnotationParentName);
                            //TryGetValue(parent, out var val).Should().BeTrue();
                            //val.Should().Be(ValueA);
                        }
                        */
    }
}