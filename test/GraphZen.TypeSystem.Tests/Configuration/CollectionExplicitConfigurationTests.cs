// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;

namespace GraphZen.TypeSystem.Tests.Configuration;

[NoReorder]
public class CollectionExplicitConfigurationTests : TestDataFixtureRunner<ICollectionExplicitConfigurationFixture>
{
    public static IEnumerable<object[]> FixtureData { get; } =
        ConfigurationFixtures.GetAll<ICollectionExplicitConfigurationFixture>().ToTestData();


    [Theory]
    [MemberData(nameof(FixtureData))]
    public void parent_should_be_of_expected_type(ICollectionExplicitConfigurationFixture fixture)
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
    public void when_item_added_explicitly_item_configurationSource_should_be_explicit(
        ICollectionExplicitConfigurationFixture fixture)

    {
        RunFixture(fixture, () =>
        {
            var parentName = "parent";
            var itemName = "addedExplicitly";
            var schema = Schema.Create(sb =>
            {
                fixture.ConfigureParentExplicitly(sb, parentName);
                fixture.AddItem(sb, parentName, itemName);
                var defCollection = fixture.GetCollection(sb, parentName);
                Assert.NotNull(defCollection[itemName]);
                Assert.IsType(fixture.CollectionItemMemberDefinitionType, defCollection[itemName]);
                Assert.Equal(ConfigurationSource.Explicit,
                    defCollection[itemName].GetConfigurationSource());
            });
            var collection = fixture.GetCollection(schema, parentName);
            Assert.NotNull(collection[itemName]);
            Assert.IsType(fixture.CollectionItemMemberType, collection[itemName]);
        });
    }

    [Theory]
    [MemberData(nameof(FixtureData))]
    public void when_item_added_explicitly_item_name_configurationSource_should_be_explicit(
        ICollectionExplicitConfigurationFixture fixture)

    {
        RunFixture(fixture, () =>
        {
            var parentName = "parent";
            var itemName = "addedExplicitly";
            var schema = Schema.Create(sb =>
            {
                fixture.ConfigureParentExplicitly(sb, parentName);
                fixture.AddItem(sb, parentName, itemName);
                var defCollection = fixture.GetCollection(sb, parentName);
                Assert.Equal(ConfigurationSource.Explicit,
                    defCollection[itemName].GetNameConfigurationSource());
                Assert.Equal(itemName, defCollection[itemName].Name);
            });
            var collection = fixture.GetCollection(schema, parentName);
            Assert.Equal(itemName, collection[itemName].Name);
        });
    }

    [Theory]
    [MemberData(nameof(FixtureData))]
    public void
        when_item_added_explicitly_then_ignored_explicitly_item_ignored_configuration_source_should_be_explicit(
            ICollectionExplicitConfigurationFixture fixture)

    {
        RunFixture(fixture, () =>
        {
            var itemName = "addedExplicitly";
            var parentName = "parent";
            Schema.Create(sb =>
            {
                fixture.ConfigureParentExplicitly(sb, parentName);
                fixture.AddItem(sb, parentName, itemName);
                fixture.IgnoreItem(sb, parentName, itemName);
                Assert.Equal(ConfigurationSource.Explicit,
                    fixture.FindIgnoredItemConfigurationSource(sb, parentName, itemName));
            });
        });
    }

    [Theory]
    [MemberData(nameof(FixtureData))]
    public void when_item_added_explicitly_then_ignored_explicitly_item_should_be_removed(
        ICollectionExplicitConfigurationFixture fixture)

    {
        RunFixture(fixture, () =>
        {
            var parentName = "parent";
            var itemName = "addedExplicitly";
            var schema = Schema.Create(sb =>
            {
                fixture.ConfigureParentExplicitly(sb, parentName);
                fixture.AddItem(sb, parentName, itemName);
                var defCollection = fixture.GetCollection(sb, parentName);
                Assert.NotNull(defCollection[itemName]);
                Assert.IsType(fixture.CollectionItemMemberDefinitionType, defCollection[itemName]);
                Assert.Equal(ConfigurationSource.Explicit,
                    defCollection[itemName].GetConfigurationSource());
                fixture.IgnoreItem(sb, parentName, itemName);
                Assert.False(defCollection.ContainsKey(itemName));
            });
            var collection = fixture.GetCollection(schema, parentName);
            Assert.False(collection.ContainsKey(itemName));
        });
    }

    [Theory]
    [MemberData(nameof(FixtureData))]
    public void when_item_added_explicitly_then_ignored_then_re_added_explicitly_should_exist(
        ICollectionExplicitConfigurationFixture fixture)

    {
        RunFixture(fixture, () =>
        {
            var parentName = "parent";
            var itemName = "addedExplicitly";
            var schema = Schema.Create(sb =>
            {
                fixture.ConfigureParentExplicitly(sb, parentName);
                fixture.AddItem(sb, parentName, itemName);
                fixture.IgnoreItem(sb, parentName, itemName);
                fixture.AddItem(sb, parentName, itemName);
                var defCollection = fixture.GetCollection(sb, parentName);
                Assert.NotNull(defCollection[itemName]);
                Assert.IsType(fixture.CollectionItemMemberDefinitionType, defCollection[itemName]);
                Assert.Equal(ConfigurationSource.Explicit,
                    defCollection[itemName].GetConfigurationSource());
            });
            var collection = fixture.GetCollection(schema, parentName);
            Assert.Equal(itemName, collection[itemName].Name);
        });
    }

    [Theory]
    [MemberData(nameof(FixtureData))]
    public void
        when_item_added_explicitly_then_ignored_then_unignored_explicitly_ignored_configuration_source_should_be_null(
            ICollectionExplicitConfigurationFixture fixture)

    {
        RunFixture(fixture, () =>
        {
            var itemName = "addedExplicitly";
            var parentName = "parent";
            Schema.Create(sb =>
            {
                fixture.ConfigureParentExplicitly(sb, parentName);
                fixture.AddItem(sb, parentName, itemName);
                fixture.IgnoreItem(sb, parentName, itemName);
                fixture.UnignoreItem(sb, parentName, itemName);
                Assert.Null(fixture.FindIgnoredItemConfigurationSource(sb, parentName, itemName));
            });
        });
    }

    [Theory]
    [MemberData(nameof(FixtureData))]
    public void item_added_explicitly(ICollectionExplicitConfigurationFixture fixture)

    {
        RunFixture(fixture, () =>
        {
            var parentName = "test";
            var itemName = "item";
            var schema = Schema.Create(sb =>
            {
                fixture.ConfigureParentExplicitly(sb, parentName);
                fixture.AddItem(sb, parentName, itemName);
                var defCollection = fixture.GetCollection(sb, parentName);
                Assert.NotNull(defCollection[itemName]);
                Assert.Equal(ConfigurationSource.Explicit,
                    defCollection[itemName].GetConfigurationSource());
                Assert.Equal(ConfigurationSource.Explicit,
                    defCollection[itemName].GetNameConfigurationSource());
            });
            var collection = fixture.GetCollection(schema, parentName);
            Assert.NotNull(collection[itemName]);
            Assert.Equal(itemName, collection[itemName].Name);
        });
    }

    [Theory]
    [MemberData(nameof(FixtureData))]
    public void item_added_explicitly_then_renamed_explicitly(ICollectionExplicitConfigurationFixture fixture)

    {
        RunFixture(fixture, () =>
        {
            var parentName = "test";
            var initialItemName = "item";
            var changedItemName = "itemFinal";
            var schema = Schema.Create(sb =>
            {
                fixture.ConfigureParentExplicitly(sb, parentName);
                fixture.AddItem(sb, parentName, initialItemName);
                var defCollection = fixture.GetCollection(sb, parentName);
                var initialItem = defCollection[initialItemName];
                Assert.NotNull(defCollection[initialItemName]);
                Assert.Equal(ConfigurationSource.Explicit,
                    defCollection[initialItemName].GetConfigurationSource());
                Assert.Equal(ConfigurationSource.Explicit,
                    defCollection[initialItemName].GetNameConfigurationSource());
                fixture.RenameItem(sb, parentName, initialItemName, changedItemName);
                Assert.False(defCollection.ContainsKey(initialItemName));
                Assert.NotNull(defCollection[changedItemName]);
                Assert.Equal(ConfigurationSource.Explicit,
                    defCollection[changedItemName].GetConfigurationSource());
                Assert.Equal(ConfigurationSource.Explicit,
                    defCollection[changedItemName].GetNameConfigurationSource());
                var finalItem = defCollection[changedItemName];
                Assert.Equal(initialItem, finalItem);
            });
            var collection = fixture.GetCollection(schema, parentName);
            Assert.NotNull(collection[changedItemName]);
            Assert.Equal(changedItemName, collection[changedItemName].Name);
        });
    }
}
