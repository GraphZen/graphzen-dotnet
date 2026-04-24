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
    public class
        CollectionConventionConfigurationTests : TestDataFixtureRunner<ICollectionConventionConfigurationFixture>
    {
        public static IEnumerable<object[]> FixtureData =
            ConfigurationFixtures.GetAll<ICollectionConventionConfigurationFixture>().ToTestData();


        [Theory]
        [MemberData(nameof(FixtureData))]
        public void conventional_parent_should_be_of_expected_type(ICollectionConventionConfigurationFixture fixture)
        {
            RunFixture(fixture, () =>
            {
                var context = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    Assert.IsType(fixture.ParentMemberDefinitionType, fixture.GetParent(sb, context.ParentName!));
                });
                Assert.IsType(fixture.ParentMemberType, fixture.GetParent(schema, context.ParentName!));
            });
        }


        [Theory]
        [MemberData(nameof(FixtureData))]
        public void conventional_item_added_explicitly_via_clr_member_should_have_clr_member(
            ICollectionConventionConfigurationFixture fixture)
        {
            RunFixture(fixture, () =>
            {
                var context = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, context.ParentName!);
                    Assert.IsType(fixture.CollectionItemMemberDefinitionType,
                        defCollection[context.ItemNamedByConvention!]);
                    Assert.IsType(fixture.CollectionItemMemberDefinitionType,
                        defCollection[context.ItemNamedByDataAnnotation!]);
                });
                Assert.IsType(fixture.ParentMemberType, fixture.GetParent(schema, context.ParentName!));
                var collection = fixture.GetCollection(schema, context.ParentName!);
                Assert.IsType(fixture.CollectionItemMemberType,
                    collection[context.ItemNamedByConvention!]);
                Assert.IsType(fixture.CollectionItemMemberType,
                    collection[context.ItemNamedByDataAnnotation!]);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void conventional_item_should_be_of_expected_type(ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var context = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, context.ParentName!);
                    Assert.IsType(fixture.CollectionItemMemberDefinitionType,
                        defCollection[context.ItemNamedByConvention!]);
                    Assert.IsType(fixture.CollectionItemMemberDefinitionType,
                        defCollection[context.ItemNamedByDataAnnotation!]);
                });
                Assert.IsType(fixture.ParentMemberType, fixture.GetParent(schema, context.ParentName!));
                var collection = fixture.GetCollection(schema, context.ParentName!);
                Assert.IsType(fixture.CollectionItemMemberType,
                    collection[context.ItemNamedByConvention!]);
                Assert.IsType(fixture.CollectionItemMemberType,
                    collection[context.ItemNamedByDataAnnotation!]);
            });
        }


        [Theory]
        [MemberData(nameof(FixtureData))]
        public void parent_should_be_of_expected_type(ICollectionConventionConfigurationFixture fixture)

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
        public void item_added_by_conventional_name(ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();

                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    //fixture.GetParent(sb, ctx.ParentName!).GetType().Should().Be(fixture.Pa)
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName!);
                    Assert.Equal(ctx.ItemNamedByConvention!, defCollection[ctx.ItemNamedByConvention!].Name);
                    Assert.NotNull(defCollection[ctx.ItemNamedByConvention!]);
                    Assert.Equal(ctx.DefaultItemConfigurationSource ?? ConfigurationSource.Convention,
                        defCollection[ctx.ItemNamedByConvention!].GetConfigurationSource());
                    Assert.Equal(ConfigurationSource.Convention,
                        defCollection[ctx.ItemNamedByConvention!].GetNameConfigurationSource());
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName!);
                Assert.NotNull(collection[ctx.ItemNamedByConvention!]);
                Assert.Equal(ctx.ItemNamedByConvention!, collection[ctx.ItemNamedByConvention!].Name);
            });
        }


        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_added_by_convention_with_name_configured_by_convention_renamed_explicitly(
            ICollectionConventionConfigurationFixture fixture)
        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var explicitName = "ExplicitName";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName!);
                    Assert.Equal(ctx.ItemNamedByConvention!, defCollection[ctx.ItemNamedByConvention!].Name);
                    Assert.NotNull(defCollection[ctx.ItemNamedByConvention!]);
                    Assert.Equal(ctx.DefaultItemConfigurationSource ?? ConfigurationSource.Convention,
                        defCollection[ctx.ItemNamedByConvention!].GetConfigurationSource());
                    Assert.Equal(ConfigurationSource.Convention,
                        defCollection[ctx.ItemNamedByConvention!].GetNameConfigurationSource());
                    fixture.RenameItem(sb, ctx.ParentName!, ctx.ItemNamedByConvention!, explicitName);
                    Assert.False(defCollection.ContainsKey(ctx.ItemNamedByConvention!));
                    Assert.NotNull(defCollection[explicitName]);
                    Assert.Equal(ConfigurationSource.Explicit,
                        defCollection[explicitName].GetNameConfigurationSource());
                    Assert.Equal(explicitName, defCollection[explicitName].Name);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName!);
                Assert.NotNull(collection[explicitName]);
                Assert.Equal(explicitName, collection[explicitName].Name);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_added_by_convention_with_name_configured_by_data_annotation(
            ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();

                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName!);
                    Assert.Equal(ctx.ItemNamedByDataAnnotation, defCollection[ctx.ItemNamedByDataAnnotation!].Name);
                    Assert.NotNull(defCollection[ctx.ItemNamedByDataAnnotation!]);
                    Assert.Equal(ctx.DefaultItemConfigurationSource ?? ConfigurationSource.Convention,
                        defCollection[ctx.ItemNamedByDataAnnotation!].GetConfigurationSource());
                    Assert.Equal(ConfigurationSource.DataAnnotation,
                        defCollection[ctx.ItemNamedByDataAnnotation!].GetNameConfigurationSource());
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName!);
                Assert.NotNull(collection[ctx.ItemNamedByDataAnnotation!]);
                Assert.Equal(ctx.ItemNamedByDataAnnotation, collection[ctx.ItemNamedByDataAnnotation!].Name);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_added_by_convention_with_name_configured_by_data_annotation_renamed_explicitly(
            ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var explicitName = "ExplicitName";
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName!);
                    Assert.Equal(ctx.ItemNamedByDataAnnotation, defCollection[ctx.ItemNamedByDataAnnotation!].Name);
                    Assert.NotNull(defCollection[ctx.ItemNamedByDataAnnotation!]);
                    Assert.Equal(ctx.DefaultItemConfigurationSource ?? ConfigurationSource.Convention,
                        defCollection[ctx.ItemNamedByDataAnnotation!].GetConfigurationSource());
                    Assert.Equal(ConfigurationSource.DataAnnotation,
                        defCollection[ctx.ItemNamedByDataAnnotation!].GetNameConfigurationSource());
                    fixture.RenameItem(sb, ctx.ParentName!, ctx.ItemNamedByConvention!, explicitName);
                    Assert.False(defCollection.ContainsKey(ctx.ItemNamedByConvention!));
                    Assert.NotNull(defCollection[explicitName]);
                    Assert.Equal(ConfigurationSource.Explicit,
                        defCollection[explicitName].GetNameConfigurationSource());
                    Assert.Equal(explicitName, defCollection[explicitName].Name);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName!);
                Assert.NotNull(collection[explicitName]);
                Assert.Equal(explicitName, collection[explicitName].Name);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_ignored_by_convention(ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName!);
                    Assert.False(defCollection.ContainsKey(ctx.ItemIgnoredByConvention!));

                    //fixture.FindIgnoredItemConfigurationSource(sb, ctx.ParentName!,
                    //    ctx.ItemIgnoredByConvention!).Should().Be(ConfigurationSource.Convention);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName!);
                Assert.False(collection.ContainsKey(ctx.ItemIgnoredByConvention!));
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_ignored_by_convention_added_by_explicit_configuration(
            ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName!);
                    Assert.False(defCollection.ContainsKey(ctx.ItemIgnoredByConvention!));
                    fixture.AddItem(sb, ctx.ParentName!, ctx.ItemIgnoredByConvention!);
                    Assert.NotNull(defCollection[ctx.ItemIgnoredByConvention!]);
                    Assert.Equal(ConfigurationSource.Explicit,
                        defCollection[ctx.ItemIgnoredByConvention!].GetConfigurationSource());
                    Assert.Equal(ConfigurationSource.Explicit,
                        defCollection[ctx.ItemIgnoredByConvention!].GetConfigurationSource());
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName!);
                Assert.NotNull(collection[ctx.ItemIgnoredByConvention!]);
                Assert.Equal(ctx.ItemIgnoredByConvention, collection[ctx.ItemIgnoredByConvention!].Name);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_ignored_by_data_annotation(ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName!);
                    Assert.False(defCollection.ContainsKey(ctx.ItemIgnoredByDataAnnotation!));
                    Assert.Equal(ConfigurationSource.DataAnnotation,
                        fixture.FindIgnoredItemConfigurationSource(sb, ctx.ParentName!,
                            ctx.ItemIgnoredByDataAnnotation!));
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName!);
                Assert.False(collection.ContainsKey(ctx.ItemIgnoredByDataAnnotation!));
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void item_ignored_by_data_annotation_added_by_explicit_configuration(
            ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureContextConventionally(sb);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName!);
                    Assert.False(defCollection.ContainsKey(ctx.ItemIgnoredByDataAnnotation!));
                    fixture.AddItem(sb, ctx.ParentName!, ctx.ItemIgnoredByDataAnnotation!);
                    Assert.NotNull(defCollection[ctx.ItemIgnoredByDataAnnotation!]);
                    Assert.Equal(ConfigurationSource.Explicit,
                        defCollection[ctx.ItemIgnoredByDataAnnotation!].GetConfigurationSource());
                    Assert.Equal(ConfigurationSource.Explicit,
                        defCollection[ctx.ItemIgnoredByDataAnnotation!].GetConfigurationSource());
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName!);
                Assert.NotNull(collection[ctx.ItemIgnoredByDataAnnotation!]);
                Assert.Equal(ctx.ItemIgnoredByDataAnnotation, collection[ctx.ItemIgnoredByDataAnnotation!].Name);
            });
        }

        [Theory]
        [MemberData(nameof(FixtureData))]
        public void when_parent_configured_explicitly_then_clr_member_set_items_added_by_convention(
            ICollectionConventionConfigurationFixture fixture)

        {
            RunFixture(fixture, () =>
            {
                var ctx = fixture.GetContext();
                var schema = Schema.Create(sb =>
                {
                    fixture.ConfigureParentExplicitly(sb, ctx.ParentName!);
                    fixture.ConfigureClrContext(sb, ctx.ParentName!);
                    var defCollection = fixture.GetCollection(sb, ctx.ParentName!);
                    Assert.False(defCollection.ContainsKey(ctx.ItemIgnoredByDataAnnotation!));
                    Assert.False(defCollection.ContainsKey(ctx.ItemIgnoredByConvention!));
                    Assert.NotNull(defCollection[ctx.ItemNamedByConvention!]);
                    Assert.NotNull(defCollection[ctx.ItemNamedByDataAnnotation!]);
                    Assert.Equal(ConfigurationSource.DataAnnotation,
                        fixture.FindIgnoredItemConfigurationSource(sb, ctx.ParentName!, ctx.ItemIgnoredByDataAnnotation!));
                    //fixture.FindIgnoredItemConfigurationSource(sb, ctx.ParentName!, ctx.ItemIgnoredByConvention!).Should()
                    //                        .Be(ConfigurationSource.Convention);
                });
                var collection = fixture.GetCollection(schema, ctx.ParentName!);
                Assert.False(collection.ContainsKey(ctx.ItemIgnoredByDataAnnotation!));
                Assert.False(collection.ContainsKey(ctx.ItemIgnoredByConvention!));
                Assert.NotNull(collection[ctx.ItemNamedByConvention!]);
                Assert.NotNull(collection[ctx.ItemNamedByDataAnnotation!]);
            });
        }
    }
}
