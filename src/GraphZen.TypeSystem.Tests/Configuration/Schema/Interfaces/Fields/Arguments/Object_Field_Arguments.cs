#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable PossibleNullReferenceException

namespace GraphZen.Interfaces.Fields.Arguments
{
    public abstract class Interface_Field_Arguments : CollectionConfigurationFixture<IArgumentsContainer,
        IArgumentsContainerDefinition, IMutableArgumentsContainerDefinition, ArgumentDefinition, Argument,
        FieldDefinition, Field>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Interface(Grandparent).Field(parentName, "String");
        }

        public override Field GetParent(Schema schema, string parentName) =>
            schema.GetInterface(Grandparent).GetField(parentName);

        public override FieldDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetInterface(Grandparent).GetField(parentName);

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Interface(Grandparent).Field(parentName, f => f.Argument(name, "String"));
        }

        public override void IgnoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Interface(Grandparent).Field(parentName, f => f.IgnoreArgument(name));
        }

        public override void UnignoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Interface(Grandparent).Field(parentName, f => f.UnignoreArgument(name));
        }

        public override NamedCollection<ArgumentDefinition> GetCollection(FieldDefinition parent) =>
            parent.Arguments.ToNamedCollection();

        public override NamedCollection<Argument> GetCollection(Field parent) => parent.Arguments.ToNamedCollection();

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(FieldDefinition parent, string name) =>
            parent.FindIgnoredArgumentConfigurationSource(name);

        public override void RenameItem(SchemaBuilder sb, string parentName, string name, string newName)
        {
            sb.Interface(Grandparent).Field(parentName, f => f.Argument(name, arg => arg.Name(newName)));
        }
    }
}