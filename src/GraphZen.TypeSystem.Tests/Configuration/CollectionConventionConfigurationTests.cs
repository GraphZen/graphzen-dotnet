// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute

namespace GraphZen
{
    public class Object_ViaClrClass_Description : Object_Description, ILeafConventionConfigurationFixture
    {
    }

    public class Object_Explicit_Description : Object_Description, ILeafExplicitConfigurationFixture
    {
    }



    public class Object_Description : LeafElementConfigurationFixture<IDescription, IDescription, IMutableDescription, string, ObjectTypeDefinition, ObjectType>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            throw new NotImplementedException();
        }

        public override ObjectType GetParent(Schema schema, string parentName) => throw new NotImplementedException();

        public override ObjectTypeDefinition GetParent(SchemaBuilder schemaBuilder, string parentName) =>
            throw new NotImplementedException();


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();
    }

    [NoReorder]
    public class LeafConventionConfigurationTests : FixtureRunner<ILeafConventionConfigurationFixture>
    {
        protected override IEnumerable<ILeafConventionConfigurationFixture> GetFixtures() =>
            ConfigurationFixtures.GetAll<ILeafConventionConfigurationFixture>();
    }

    [NoReorder]
    public class LeafExplicitConfigurationTests : FixtureRunner<ILeafExplicitConfigurationFixture>
    {
        protected override IEnumerable<ILeafExplicitConfigurationFixture> GetFixtures() =>
            ConfigurationFixtures.GetAll<ILeafExplicitConfigurationFixture>();


        [Fact]
        public void optional_not_defined_by_convention_when_parent_configured_explicitly()
        {
            TestFixtures(fixture =>
            {
                string parentName = "parent";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, parentName);

                });
            });
        }

        /*
                public virtual void optional_not_defined_by_convention_when_parent_configured_explicitly()
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
                        }
                    });
                    foreach (var parentName in parentNames)
                    {
                        var parent = GetParentByName(schema, parentName);
                        TryGetValue(parent, out _).Should().BeFalse();
                    }
                }

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


    [NoReorder]
    public class CollectionConventionConfigurationTests : FixtureRunner<ICollectionConventionConfigurationFixture>
    {
        protected override IEnumerable<ICollectionConventionConfigurationFixture> GetFixtures() =>
            ConfigurationFixtures.GetAll<ICollectionConventionConfigurationFixture>();

        [Fact]
        public void item_added_by_convention_with_name_configured_by_convention()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();

                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByConvention].Name.Should().Be(ctx.ItemNamedByConvention);
                    defCollection[ctx.ItemNamedByConvention].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByConvention].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                    defCollection[ctx.ItemNamedByConvention].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[ctx.ItemNamedByConvention].Should().NotBeNull();
                collection[ctx.ItemNamedByConvention].Name.Should().Be(ctx.ItemNamedByConvention);
            });
        }

        [Fact]
        public void item_added_by_convention_with_name_configured_by_convention_renamed_explicitly()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();

                var explicitName = "ExplicitName";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByConvention].Name.Should().Be(ctx.ItemNamedByConvention);
                    defCollection[ctx.ItemNamedByConvention].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByConvention].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                    defCollection[ctx.ItemNamedByConvention].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                    fixture.RenameItem(sb, ctx.ParentName, ctx.ItemNamedByConvention, explicitName);
                    defCollection.ContainsKey(ctx.ItemNamedByConvention).Should().BeFalse();
                    defCollection[explicitName].Should().NotBeNull();
                    defCollection[explicitName].GetConfigurationSource().Should().Be(ConfigurationSource.Convention);
                    defCollection[explicitName].GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                    defCollection[explicitName].Name.Should().Be(explicitName);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[explicitName].Should().NotBeNull();
                collection[explicitName].Name.Should().Be(explicitName);
            });
        }

        [Fact]
        public void item_added_by_convention_with_name_configured_by_data_annotation()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();

                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByDataAnnotation].Name.Should().Be(ctx.ItemNamedByDataAnnotation);
                    defCollection[ctx.ItemNamedByDataAnnotation].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByDataAnnotation].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                    defCollection[ctx.ItemNamedByDataAnnotation].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.DataAnnotation);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[ctx.ItemNamedByDataAnnotation].Should().NotBeNull();
                collection[ctx.ItemNamedByDataAnnotation].Name.Should().Be(ctx.ItemNamedByDataAnnotation);
            });
        }

        [Fact]
        public void item_added_by_convention_with_name_configured_by_data_annotation_renamed_explicitly()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();
                var explicitName = "ExplicitName";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection[ctx.ItemNamedByDataAnnotation].Name.Should().Be(ctx.ItemNamedByDataAnnotation);
                    defCollection[ctx.ItemNamedByDataAnnotation].Should().NotBeNull();
                    defCollection[ctx.ItemNamedByDataAnnotation].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Convention);
                    defCollection[ctx.ItemNamedByDataAnnotation].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.DataAnnotation);
                    fixture.RenameItem(sb, ctx.ParentName, ctx.ItemNamedByConvention, explicitName);
                    defCollection.ContainsKey(ctx.ItemNamedByConvention).Should().BeFalse();
                    defCollection[explicitName].Should().NotBeNull();
                    defCollection[explicitName].GetConfigurationSource().Should().Be(ConfigurationSource.Convention);
                    defCollection[explicitName].GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                    defCollection[explicitName].Name.Should().Be(explicitName);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[explicitName].Should().NotBeNull();
                collection[explicitName].Name.Should().Be(explicitName);
            });
        }

        [Fact]
        public void item_ignored_by_convention()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByConvention).Should().BeFalse();

                    //fixture.FindIgnoredItemConfigurationSource(sb, ctx.ParentName,
                    //    ctx.ItemIgnoredByConvention).Should().Be(ConfigurationSource.Convention);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection.ContainsKey(ctx.ItemIgnoredByConvention).Should().BeFalse();
            });
        }

        [Fact]
        public void item_ignored_by_convention_added_by_explicit_configuration()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByConvention).Should().BeFalse();
                    fixture.AddItem(sb, ctx.ParentName, ctx.ItemIgnoredByConvention);
                    defCollection[ctx.ItemIgnoredByConvention].Should().NotBeNull();
                    defCollection[ctx.ItemIgnoredByConvention].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                    defCollection[ctx.ItemIgnoredByConvention].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[ctx.ItemIgnoredByConvention].Should().NotBeNull();
                collection[ctx.ItemIgnoredByConvention].Name.Should().Be(ctx.ItemIgnoredByConvention);
            });
        }

        [Fact]
        public void item_ignored_by_data_annotation()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByDataAnnotation).Should().BeFalse();
                    fixture.FindIgnoredItemConfigurationSource(sb, ctx.ParentName,
                        ctx.ItemIgnoredByDataAnnotation).Should().Be(ConfigurationSource.DataAnnotation);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection.ContainsKey(ctx.ItemIgnoredByDataAnnotation).Should().BeFalse();
            });
        }

        [Fact]
        public void item_ignored_by_data_annotation_added_by_explicit_configuration()
        {
            TestFixtures(fixture =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName);
                    defCollection.ContainsKey(ctx.ItemIgnoredByDataAnnotation).Should().BeFalse();
                    fixture.AddItem(sb, ctx.ParentName, ctx.ItemIgnoredByDataAnnotation);
                    defCollection[ctx.ItemIgnoredByDataAnnotation].Should().NotBeNull();
                    defCollection[ctx.ItemIgnoredByDataAnnotation].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                    defCollection[ctx.ItemIgnoredByDataAnnotation].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName);
                collection[ctx.ItemIgnoredByDataAnnotation].Should().NotBeNull();
                collection[ctx.ItemIgnoredByDataAnnotation].Name.Should().Be(ctx.ItemIgnoredByDataAnnotation);
            });
        }
    }
}