#nullable disable
using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen
{
    public interface ICollectionConfigurationFixture : IConfigurationFixture
    {
        Type CollectionItemMemberType { get; }
        Type CollectionItemMemberDefinitionType { get; }
        NamedCollection<IMutableNamed> GetCollection([NotNull] SchemaBuilder sb, [NotNull] string parentName);
        NamedCollection<INamed> GetCollection([NotNull] Schema schema, [NotNull] string parentName);
        void AddItem([NotNull] SchemaBuilder sb, [NotNull] string parentName, [NotNull] string itemName);
        void IgnoreItem([NotNull] SchemaBuilder sb, [NotNull] string parentName, [NotNull] string itemName);
        void UnignoreItem([NotNull] SchemaBuilder sb, [NotNull] string parentName, [NotNull] string itemName);

        void RenameItem([NotNull] SchemaBuilder sb, [NotNull] string parentName, [NotNull] string itemName,
            [NotNull] string newName);

        ConfigurationSource? FindIgnoredItemConfigurationSource([NotNull] SchemaBuilder sb, [NotNull] string parentName,
            [NotNull] string itemName);
    }
}