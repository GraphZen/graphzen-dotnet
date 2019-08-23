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
        public void initial_value_then_reconfigured_explicitly()
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

        /*

                public virtual void optional_not_defined_by_convention_then_configured_explicitly()
                {
                    List<string> parentNames = null;
                    var schema = Schema.Create(sb =>
                    {
                        DefineParents(sb, out parentNames);
                        foreach (var parentName in parentNames)
                        {
                            var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                            GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                            TryGetValue(parentDef, out _).Should().BeFalse();
                            ConfigureExplicitly(sb, parentName, ValueA);
                            TryGetValue(parentDef, out var configuredA).Should().BeTrue();
                            configuredA.Should().Be(ValueA);
                            GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                        }
                    });
                    foreach (var parentName in parentNames)
                    {
                        var parent = GetParentByName(schema, parentName);
                        TryGetValue(parent, out var finalVal).Should().BeTrue();
                        finalVal.Should().Be(ValueA);
                    }
                }

                public virtual void optional_not_defined_by_convention_then_configured_explicitly_then_removed()
                {
                    List<string> parentNames = null;
                    var schema = Schema.Create(sb =>
                    {
                        DefineParents(sb, out parentNames);
                        foreach (var parentName in parentNames)
                        {
                            var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                            GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                            TryGetValue(parentDef, out _).Should().BeFalse();
                            ConfigureExplicitly(sb, parentName, ValueA);
                            TryGetValue(parentDef, out var configuredA).Should().BeTrue();
                            configuredA.Should().Be(ValueA);
                            GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                            RemoveExplicitly(sb, parentName);
                            TryGetValue(parentDef, out var configuredC).Should().BeFalse();
                            GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                            configuredC.Should().BeNull();
                        }
                    });
                    foreach (var parentName in parentNames)
                    {
                        var parent = GetParentByName(schema, parentName);
                        TryGetValue(parent, out var finalVal).Should().BeFalse();
                        finalVal.Should().BeNull();
                    }
                }

                public virtual void configured_explicitly_reconfigured_explicitly()
                {
                    ValueA.Should().NotBe(ValueB);
                    List<string> parentNames = null;
                    var schema = Schema.Create(sb =>
                    {
                        DefineParents(sb, out parentNames);
                        foreach (var parentName in parentNames)
                        {
                            var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
                            GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Convention);
                            TryGetValue(parentDef, out _).Should().BeFalse();
                            ConfigureExplicitly(sb, parentName, ValueA);
                            TryGetValue(parentDef, out var configuredA).Should().BeTrue();
                            configuredA.Should().Be(ValueA);
                            ConfigureExplicitly(sb, parentName, ValueB);
                            TryGetValue(parentDef, out var configuredB).Should().BeTrue();
                            configuredB.Should().Be(ValueB);
                            GetElementConfigurationSource(parentDef).Should().Be(ConfigurationSource.Explicit);
                        }
                    });
                    foreach (var parentName in parentNames)
                    {
                        var parent = GetParentByName(schema, parentName);
                        TryGetValue(parent, out var finalVal).Should().BeTrue();
                        finalVal.Should().Be(ValueB);
                    }
                }

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