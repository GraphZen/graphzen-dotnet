#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Scalars.Description
{
    public abstract class Scalar_Description : LeafElementConfigurationFixture<IDescription, IDescription, IMutableDescription,
        string, ScalarTypeDefinition, ScalarType>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Scalar(parentName);
        }

        public override ScalarType GetParent(Schema schema, string parentName) => schema.GetScalar(parentName);

        public override ScalarTypeDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetScalar(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.Scalar(parentName).Description(value);
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Scalar(parentName).Description(null);
        }

        public override bool TryGetValue(ScalarType parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(ScalarTypeDefinition parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}