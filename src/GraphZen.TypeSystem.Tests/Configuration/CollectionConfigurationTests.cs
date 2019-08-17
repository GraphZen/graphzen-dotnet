// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.Objects.Fields;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using Xunit;

// ReSharper disable PossibleNullReferenceException

namespace GraphZen
{
    public class CollectionConfigurationTests
    {
        [NotNull]
        private IEnumerable<ICollectionElementConfigurationFixture> GetFixtures()
        {
            yield return new ObjectFields_Explicit();
        }

        private void ForEachFixture(
            [NotNull] Action<ICollectionElementConfigurationFixture> test)
        {
            Check.NotNull(test, nameof(test));
            foreach (var fixture in GetFixtures())
            {
                try
                {
                    test(fixture);
                }
                catch (Exception ex)
                {
                    throw new Exception($@"Failed fixture:

  {fixture.GetType().Name}  

Inner exception: 

{ex.GetType()}: {ex.Message}", ex);
                }
            }
        }


        [Fact]
        public void when_item_added_explicitly_item_configurationSource_should_be_explicit()
        {
            ForEachFixture(fixture =>
            {
                var parentName = "parent";
                var itemName = "addedExplicitly";
                var schema = Schema.Create(sb =>
                {
                    fixture.DefineParent(sb, parentName);
                    fixture.AddItem(sb, parentName, itemName);
                    var defCollection = fixture.GetCollection(sb, parentName);
                    defCollection[itemName].Should().NotBeNull();
                    defCollection[itemName].Should().BeOfType(fixture.CollectionItemMemberDefinitionType);
                    (defCollection[itemName] as MemberDefinition).GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                    defCollection.Count.Should().Be(1);
                });
                var collection = fixture.GetCollection(schema, parentName);
                collection.Count.Should().Be(1);
                collection[itemName].Should().NotBeNull();
                collection[itemName].Should().BeOfType(fixture.CollectionItemMemberType);
            });
        }

        [Fact]
        public void when_item_added_explicitly_item_name_configurationSource_should_be_explicit()
        {
            ForEachFixture(fixture =>
            {
                var parentName = "parent";
                var itemName = "addedExplicitly";
                var schema = Schema.Create(sb =>
                {
                    fixture.DefineParent(sb, parentName);
                    fixture.AddItem(sb, parentName, itemName);
                    var defCollection = fixture.GetCollection(sb, parentName);
                    defCollection[itemName].GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                    defCollection[itemName].Name.Should().Be(itemName);
                });
                var collection = fixture.GetCollection(schema, parentName);
                collection[itemName].Name.Should().Be(itemName);
            });
        }

        [Fact]
        public void
            when_item_added_explicitly_then_ignored_explicitly_item_ignored_configuration_source_should_be_explicit()
        {
            ForEachFixture(fixture =>
            {
                var itemName = "addedExplicitly";
                var parentName = "parent";
                Schema.Create(sb =>
                {
                    fixture.DefineParent(sb, parentName);
                    fixture.AddItem(sb, parentName, itemName);
                    fixture.IgnoreItem(sb, parentName, itemName);
                    fixture.FindIgnoredItemConfigurationSource(sb, parentName, itemName).Should()
                        .Be(ConfigurationSource.Explicit);
                });
            });
        }

        [Fact]
        public void when_item_added_explicitly_then_ignored_explicitly_item_should_be_removed()
        {
            ForEachFixture(fixture =>
            {
                var parentName = "parent";
                var itemName = "addedExplicitly";
                var schema = Schema.Create(sb =>
                {
                    fixture.DefineParent(sb, parentName);
                    fixture.AddItem(sb, parentName, itemName);
                    var defCollection = fixture.GetCollection(sb, parentName);
                    defCollection[itemName].Should().NotBeNull();
                    defCollection[itemName].Should().BeOfType(fixture.CollectionItemMemberDefinitionType);
                    (defCollection[itemName] as MemberDefinition).GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                    defCollection.Count.Should().Be(1);
                    fixture.IgnoreItem(sb, parentName, itemName);
                    defCollection.ContainsKey(itemName).Should().BeFalse();
                });
                var collection = fixture.GetCollection(schema, parentName);
                collection.Count.Should().Be(0);
            });
        }

        [Fact]
        public void when_item_added_explicitly_then_ignored_then_unignored_and_re_added_should_exist()
        {
            ForEachFixture(fixture =>
            {
                var parentName = "parent";
                var itemName = "addedExplicitly";
                var schema = Schema.Create(sb =>
                {
                    fixture.DefineParent(sb, parentName);
                    fixture.AddItem(sb, parentName, itemName);
                    fixture.IgnoreItem(sb, parentName, itemName);
                    fixture.UnignoreItem(sb, parentName, itemName);
                    fixture.AddItem(sb, parentName, itemName);
                    var defCollection = fixture.GetCollection(sb, parentName);
                    defCollection.Count.Should().Be(1);
                    defCollection[itemName].Should().NotBeNull();
                    defCollection[itemName].Should().BeOfType(fixture.CollectionItemMemberDefinitionType);
                    (defCollection[itemName] as MemberDefinition).GetConfigurationSource().Should()
                        .Be(ConfigurationSource.Explicit);
                });
                var collection = fixture.GetCollection(schema, parentName);
                collection[itemName].Name.Should().Be(itemName);
            });
        }

        [Fact]
        public void
            when_item_added_explicitly_then_ignored_then_unignored_explicitly_ignored_configuration_source_should_be_null()
        {
            ForEachFixture(fixture =>
            {
                var itemName = "addedExplicitly";
                var parentName = "parent";
                Schema.Create(sb =>
                {
                    fixture.DefineParent(sb, parentName);
                    fixture.AddItem(sb, parentName, itemName);
                    fixture.IgnoreItem(sb, parentName, itemName);
                    fixture.UnignoreItem(sb, parentName, itemName);
                    fixture.FindIgnoredItemConfigurationSource(sb, parentName, itemName).Should().BeNull();
                });
            });
        }

        [Fact]
        public void when_parent_defined_explicitly_collection_is_empty() =>
            ForEachFixture(fixture =>
            {
                var parentName = "test";
                var schema = Schema.Create(sb =>
                {
                    fixture.DefineParent(sb, parentName);
                    var defCollection = fixture.GetCollection(sb, parentName);
                    defCollection.Should().BeEmpty();
                });
                var collection = fixture.GetCollection(schema, parentName);
                collection.Should().BeEmpty();
            });
    }
}