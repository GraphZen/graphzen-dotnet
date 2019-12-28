// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests.Configuration
{
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
                    fixture.GetParent(sb, parentName).Should().BeOfType(fixture.ParentMemberDefinitionType);
                });
                fixture.GetParent(schema, parentName).Should().BeOfType(fixture.ParentMemberType);
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
                    defCollection[itemName].Should().NotBeNull();
                    defCollection[itemName].Should().BeOfType(fixture.CollectionItemMemberDefinitionType);
                    defCollection[itemName].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                });
                var collection = fixture.GetCollection(schema, parentName);
                collection[itemName].Should().NotBeNull();
                collection[itemName].Should().BeOfType(fixture.CollectionItemMemberType);
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
                    defCollection[itemName].GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                    defCollection[itemName].Name.Should().Be(itemName);
                });
                var collection = fixture.GetCollection(schema, parentName);
                collection[itemName].Name.Should().Be(itemName);
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
                    fixture.FindIgnoredItemConfigurationSource(sb, parentName, itemName).Should()
                        .Be(ConfigurationSource.Explicit);
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
                    defCollection[itemName].Should().NotBeNull();
                    defCollection[itemName].Should().BeOfType(fixture.CollectionItemMemberDefinitionType);
                    defCollection[itemName].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                    fixture.IgnoreItem(sb, parentName, itemName);
                    defCollection.ContainsKey(itemName).Should().BeFalse();
                });
                var collection = fixture.GetCollection(schema, parentName);
                collection.ContainsKey(itemName).Should().BeFalse();
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
                    defCollection[itemName].Should().NotBeNull();
                    defCollection[itemName].Should().BeOfType(fixture.CollectionItemMemberDefinitionType);
                    defCollection[itemName].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                });
                var collection = fixture.GetCollection(schema, parentName);
                collection[itemName].Name.Should().Be(itemName);
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
                    fixture.FindIgnoredItemConfigurationSource(sb, parentName, itemName).Should().BeNull();
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
                    defCollection[itemName].Should().NotBeNull();
                    defCollection[itemName].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                    defCollection[itemName].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                });
                var collection = fixture.GetCollection(schema, parentName);
                collection[itemName].Should().NotBeNull();
                collection[itemName].Name.Should().Be(itemName);
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
                    defCollection[initialItemName].Should().NotBeNull();
                    defCollection[initialItemName].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                    defCollection[initialItemName].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                    fixture.RenameItem(sb, parentName, initialItemName, changedItemName);
                    defCollection.ContainsKey(initialItemName).Should().BeFalse();
                    defCollection[changedItemName].Should().NotBeNull();
                    defCollection[changedItemName].GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                    defCollection[changedItemName].GetNameConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                    var finalItem = defCollection[changedItemName];
                    finalItem.Should().Be(initialItem);
                });
                var collection = fixture.GetCollection(schema, parentName);
                collection[changedItemName].Should().NotBeNull();
                collection[changedItemName].Name.Should().Be(changedItemName);
            });
        }
    }
}