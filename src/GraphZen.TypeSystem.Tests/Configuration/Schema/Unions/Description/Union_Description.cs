using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Unions.Description
{
    public abstract class Union_Description : LeafElementConfigurationFixture<IDescription, IDescription, IMutableDescription,
        string, UnionTypeDefinition, UnionType>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Union(parentName);
        }

        public override UnionType GetParent(Schema schema, string parentName) => schema.GetUnion(parentName);

        public override UnionTypeDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetUnion(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.Union(parentName).Description(value);
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Union(parentName).Description(null);
        }

        public override bool TryGetValue(UnionType parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(UnionTypeDefinition parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}