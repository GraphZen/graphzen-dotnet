// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using System.Collections.Generic;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;

namespace GraphZen
{
    public abstract class CollectionElementConfigurationTests<
        TMarker,
        TMutableMarker,
        TParentMemberDefinition,
        TParentMember,
        TCollectionItemDefinition,
        TCollectionItem> :
        ElementConfigurationTests<TMarker, TMutableMarker,
            TParentMemberDefinition, TParentMember>
        where TMutableMarker : TMarker
        where TParentMemberDefinition : MemberDefinition, TMutableMarker
        where TParentMember : Member, TMarker
        where TCollectionItemDefinition : MemberDefinition, IMutableNamed
        where TCollectionItem : Member, INamed
    {
        public abstract IReadOnlyDictionary<string, TCollectionItemDefinition> GetDefinitionCollection(
            TParentMemberDefinition parent);

        public IReadOnlyDictionary<string, TCollectionItemDefinition> GetDefinitionCollection(SchemaBuilder sb,
            string parentName)
        {
            var parentDef = GetParentDefinitionByName(sb.GetDefinition(), parentName);
            return GetDefinitionCollection(parentDef);
        }

        public abstract IReadOnlyDictionary<string, TCollectionItem> GetCollection(TParentMember parent);
        public IReadOnlyDictionary<string, TCollectionItem> GetCollection(Schema schema, string parentName)
        {
            var parent = GetParentByName(schema, parentName);
            return GetCollection(parent);
        }

        public abstract ConfigurationSource? FindItemIgnoredConfigurationSource(TParentMemberDefinition parent, string name);

        public ConfigurationSource? FindItemIgnoredConfigurationSource(SchemaBuilder sb, string parentName, string name)
        {
            var parent = GetParentDefinitionByName(sb.GetDefinition(), parentName);
            return FindItemIgnoredConfigurationSource(parent, name);
        }


        public abstract void AddItem(SchemaBuilder sb, string parentName, string name);
        public abstract void IgnoreItem(SchemaBuilder sb, string parentName, string name);
        public abstract void UnignoreItem(SchemaBuilder sb, string parentName, string name);
        public abstract void RenameItem(SchemaBuilder sb, string parentName, string name, string newName);


        [Fact]
        public void when_parent_defined_explicitly_collection_is_empty()
        {
            string parentName = null;
            var schema = Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out parentName);
                var defCollection = GetDefinitionCollection(sb, parentName);
                defCollection.Should().BeEmpty();
            });
            var collection = GetCollection(schema, parentName);
            collection.Should().BeEmpty();
        }


        [Fact]
        public void when_item_added_explicitly_item_configurationSource_should_be_explicit()
        {
            string parentName = null;
            var itemName = "addedExplicitly";
            var schema = Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out parentName);
                AddItem(sb, parentName, itemName);
                var defCollection = GetDefinitionCollection(sb, parentName);
                defCollection[itemName].Should().NotBeNull();
                defCollection[itemName].Should().BeOfType<TCollectionItemDefinition>();
                defCollection[itemName].GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                defCollection.Count.Should().Be(1);
            });
            var parent = GetParentByName(schema, parentName);
            var collection = GetCollection(parent);
            collection.Count.Should().Be(1);
            collection[itemName].Should().NotBeNull();
            collection[itemName].Should().BeOfType<TCollectionItem>();
        }

        [Fact]
        public void when_item_added_explicitly_item_name_configurationSource_should_be_explicit()
        {
            string parentName = null;
            var itemName = "addedExplicitly";
            var schema = Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out parentName);
                AddItem(sb, parentName, itemName);
                var defCollection = GetDefinitionCollection(sb, parentName);
                defCollection[itemName].GetNameConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                defCollection[itemName].Name.Should().Be(itemName);
            });
            var parent = GetParentByName(schema, parentName);
            var collection = GetCollection(parent);
            collection[itemName].Name.Should().Be(itemName);
        }

        [Fact]
        public void when_item_added_explicitly_then_ignored_explicitly_item_ignored_configuration_source_should_be_explicit()
        {
            var itemName = "addedExplicitly";
            Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out var parentName);
                AddItem(sb, parentName, itemName);
                IgnoreItem(sb, parentName, itemName);
                FindItemIgnoredConfigurationSource(sb, parentName, itemName).Should().Be(ConfigurationSource.Explicit);
            });
        }
        [Fact]
        public void when_item_added_explicitly_then_ignored_explicitly_item_should_be_removed()
        {
            string parentName = null;
            var itemName = "addedExplicitly";
            var schema = Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out parentName);
                AddItem(sb, parentName, itemName);
                var defCollection = GetDefinitionCollection(sb, parentName);
                defCollection[itemName].Should().NotBeNull();
                defCollection[itemName].Should().BeOfType<TCollectionItemDefinition>();
                defCollection[itemName].GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
                defCollection.Count.Should().Be(1);
                IgnoreItem(sb, parentName, itemName);
                defCollection.ContainsKey(itemName).Should().BeFalse();

            });
            var parent = GetParentByName(schema, parentName);
            var collection = GetCollection(parent);
            collection.Count.Should().Be(0);
        }
        [Fact]
        public void when_item_added_explicitly_then_ignored_then_unignored_explicitly_ignored_configuration_source_should_be_null()
        {
            var itemName = "addedExplicitly";
            Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out var parentName);
                AddItem(sb, parentName, itemName);
                IgnoreItem(sb, parentName, itemName);
                UnignoreItem(sb, parentName, itemName);
                FindItemIgnoredConfigurationSource(sb, parentName, itemName).Should().BeNull();
            });
        }

        [Fact]
        public void when_item_added_explicitly_then_ignored_then_unignored_and_re_added_should_exist()
        {
            string parentName = null;
            var itemName = "addedExplicitly";
            var schema = Schema.Create(sb =>
            {
                DefineParentExplicitly(sb, out parentName);
                AddItem(sb, parentName, itemName);
                IgnoreItem(sb, parentName, itemName);
                UnignoreItem(sb, parentName, itemName);
                AddItem(sb, parentName, itemName);
                var defCollection = GetDefinitionCollection(sb, parentName);
                defCollection.Count.Should().Be(1);
                defCollection[itemName].Should().NotBeNull();
                defCollection[itemName].Should().BeOfType<TCollectionItemDefinition>();
                defCollection[itemName].GetConfigurationSource().Should().Be(ConfigurationSource.Explicit);
            });
            var parent = GetParentByName(schema, parentName);
            var collection = GetCollection(parent);
            collection[itemName].Name.Should().Be(itemName);
        }
    }
}