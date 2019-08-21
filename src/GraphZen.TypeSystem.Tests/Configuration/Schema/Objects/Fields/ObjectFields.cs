// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Objects.Fields
{
    public abstract class ObjectFields : 
        CollectionConfigurationFixture<IFieldsContainer,
        IFieldsContainerDefinition, IMutableFieldsContainerDefinition, FieldDefinition, Field, ObjectTypeDefinition,
        ObjectType>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName) => sb.Object(parentName);

        public override ObjectType GetParent(Schema schema, string parentName) => schema.GetObject(parentName);

        public override ObjectTypeDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetObject(parentName);

        public override NamedCollection<FieldDefinition> GetCollection(ObjectTypeDefinition parent) =>
            parent.Fields.ToNamedCollection();

        public override NamedCollection<Field> GetCollection(ObjectType parent) =>
            parent.Fields.ToNamedCollection();

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(ObjectTypeDefinition parent,
            string name) => parent.FindIgnoredFieldConfigurationSource(name);

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Object(parentName).Field(name, "String");
        }

        public override void IgnoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Object(parentName).IgnoreField(name);
        }

        public override void UnignoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Object(parentName).UnignoreField(name);
        }

        public override void RenameItem(SchemaBuilder sb, string parentName, string name, string newName)
        {
            sb.Object(parentName).Field(name, field => field?.Name(newName));
        }
    }
}