using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen
{
    public class Schema_Objects : CollectionConfigurationFixture<IObjectTypesContainer,
        IObjectTypesContainerDefinition, IMutableObjectTypesContainerDefinition, ObjectTypeDefinition, ObjectType,
        SchemaDefinition,
        Schema>

    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
        }

        public override Schema GetParent(Schema schema, string parentName) => schema;

        public override SchemaDefinition GetParent(SchemaBuilder sb, string parentName) => sb.GetDefinition();

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Object(name);
        }

        public override void IgnoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.IgnoreType(name);
        }

        public override void UnignoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.UnignoreType(name);
        }

        public override void RenameItem(SchemaBuilder sb, string parentName, string itemName, string newName)
        {
            sb.Object(itemName).Name(newName);
        }

        public override NamedCollection<ObjectTypeDefinition> GetCollection(SchemaDefinition parent) =>
            parent.GetObjects().ToNamedCollection();

        public override NamedCollection<ObjectType> GetCollection(Schema parent) =>
            parent.GetObjects().ToNamedCollection();

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(SchemaDefinition parent, string name) =>
            parent.FindIgnoredTypeConfigurationSource(name);
    }
}