using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen
{

    public interface IExplicitCollectionConfigurationFixture : ICollectionConfigurationFixture
    {

    }

    public interface IConventionalCollectionConfigurationFixture : ICollectionConfigurationFixture
    {
        CollectionConventionContext ConfigureViaConvention([NotNull] SchemaBuilder sb);
    }


    public interface ICollectionConfigurationFixture : IConfigurationFixture
    {
        Type CollectionItemMemberType { get; }
        Type CollectionItemMemberDefinitionType { get; }
        NamedCollection<IMutableNamed> GetCollection(SchemaBuilder sb, string parentName);
        NamedCollection<INamed> GetCollection(Schema schema, string parentName);
        void AddItem(SchemaBuilder sb, string parentName, string itemName);
        void IgnoreItem(SchemaBuilder sb, string parentName, string itemName);
        void UnignoreItem(SchemaBuilder sb, string parentName, string itemName);
        ConfigurationSource? FindIgnoredItemConfigurationSource(SchemaBuilder sb, string parentName, string itemName);
    }
}