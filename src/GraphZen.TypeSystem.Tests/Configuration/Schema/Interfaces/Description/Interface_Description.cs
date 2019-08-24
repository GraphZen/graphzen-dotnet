#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Interfaces.Description
{
    public abstract class Interface_Description : LeafElementConfigurationFixture<IDescription, IDescription, IMutableDescription,
        string, InterfaceTypeDefinition, InterfaceType>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Interface(parentName);
        }

        public override InterfaceType GetParent(Schema schema, string parentName) => schema.GetInterface(parentName);

        public override InterfaceTypeDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetInterface(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.Interface(parentName).Description(value);
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Interface(parentName).Description(null);
        }

        public override bool TryGetValue(InterfaceType parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(InterfaceTypeDefinition parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}