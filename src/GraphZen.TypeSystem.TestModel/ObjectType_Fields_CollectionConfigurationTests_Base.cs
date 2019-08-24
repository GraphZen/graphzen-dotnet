#nullable disable
using System;
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen
{
    public abstract class ObjectType_Fields_CollectionConfigurationTests_Base : CollectionElementConfigurationTests<
        IFieldsContainerDefinition,
        IMutableFieldsContainerDefinition,
        ObjectTypeDefinition, ObjectType, FieldDefinition, Field
    >
    {
        public override void DefineParentExplicitly(SchemaBuilder sb, out string parentName)
        {
            parentName = "ExplicitObject";
            sb.Object(parentName);
        }

        public override ObjectTypeDefinition GetParentDefinitionByName([NotNull]SchemaDefinition schemaDefinition,
            [NotNull]string parentName) => schemaDefinition.GetObject(parentName);

        public override ObjectType GetParentByName([NotNull]Schema schema, [NotNull] string parentName) => schema.GetObject(parentName);

        public override IReadOnlyDictionary<string, FieldDefinition> GetDefinitionCollection(
            ObjectTypeDefinition parent) => parent.Fields;

        public override IReadOnlyDictionary<string, Field> GetCollection(ObjectType parent) => parent.Fields;

        public override ConfigurationSource? FindItemIgnoredConfigurationSource(ObjectTypeDefinition parent,
            [NotNull]string name) => parent.FindIgnoredFieldConfigurationSource(name);

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
            => sb.Object(parentName).Field(name);

        public override void IgnoreItem(SchemaBuilder sb, string parentName, string name)
            => sb.Object(parentName).IgnoreField(name);

        public override void UnignoreItem(SchemaBuilder sb, string parentName, string name)
            => sb.Object(parentName).UnignoreField(name);

        public override void RenameItem(SchemaBuilder sb, string parentName, string name, string newName)
        {
            throw new NotImplementedException();
        }
    }
}