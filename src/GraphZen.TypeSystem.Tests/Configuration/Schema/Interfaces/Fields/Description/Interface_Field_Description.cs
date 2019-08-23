using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
// ReSharper disable PossibleNullReferenceException

namespace GraphZen.Interfaces.Fields.Description
{
    public abstract class Interface_Field_Description : LeafElementConfigurationFixture<IDescription, IDescription, IMutableDescription,
        string, FieldDefinition, Field>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Interface(Grandparent).Field(parentName);
        }

        public override Field GetParent(Schema schema, string parentName) =>
            schema.GetInterface(Grandparent).GetField(parentName);

        public override FieldDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetInterface(Grandparent).GetField(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.Interface(Grandparent).Field(parentName, v => v.Description(value));
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Interface(Grandparent).Field(parentName, v => v.Description(null));
        }

        public override bool TryGetValue(Field parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(FieldDefinition parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}