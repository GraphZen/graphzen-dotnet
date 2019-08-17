using System;
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.MetaModel
{
    public interface ICollectionElementConfigurationFixture : IElementConfigurationFixture
    {
        Type CollectionItemMemberType { get; }
        Type CollectionItemMemberDefinitionType { get; }
        IReadOnlyDictionary<string, IMutableNamed> GetCollection(SchemaBuilder sb, string parentName);
        IReadOnlyDictionary<string, INamed> GetCollection(Schema schema, string parentName);
        void AddItem(SchemaBuilder sb, string parentName, string itemName);
        void IgnoreItem(SchemaBuilder sb, string parentName, string itemName);
        void UnignoreItem(SchemaBuilder sb, string parentName, string itemName);
        ConfigurationSource? FindIgnoredItemConfigurationSource(SchemaBuilder sb, string parentName, string itemName);
    }
}