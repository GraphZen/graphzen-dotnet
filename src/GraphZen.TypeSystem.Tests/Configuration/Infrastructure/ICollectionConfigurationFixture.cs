using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen
{

    public interface ILeafConfigurationFixture : IConfigurationFixture
    {
        ConfigurationSource GetElementConfigurationSource(MemberDefinition parent);
    }

    public interface ILeafExplicitConfigurationFixture : ILeafConfigurationFixture { }
    public interface ILeafConventionConfigurationFixture : ILeafConfigurationFixture { }


    public interface ICollectionConfigurationFixture : IConfigurationFixture
    {
        Type CollectionItemMemberType { get; }
        Type CollectionItemMemberDefinitionType { get; }
        NamedCollection<IMutableNamed> GetCollection(SchemaBuilder sb, string parentName);
        NamedCollection<INamed> GetCollection(Schema schema, string parentName);
        void AddItem(SchemaBuilder sb, string parentName, string itemName);
        void IgnoreItem(SchemaBuilder sb, string parentName, string itemName);
        void UnignoreItem(SchemaBuilder sb, string parentName, string itemName);
        void RenameItem([NotNull]SchemaBuilder sb, string parentName, string itemName, string newName);
        ConfigurationSource? FindIgnoredItemConfigurationSource(SchemaBuilder sb, string parentName, string itemName);
    }
}