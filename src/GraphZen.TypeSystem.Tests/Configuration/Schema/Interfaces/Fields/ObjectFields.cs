// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Interfaces.Fields
{
    public abstract class InterfaceFields : 
        CollectionElementConfigurationFixture<IFieldsContainer,
        IFieldsContainerDefinition, IMutableFieldsContainerDefinition, FieldDefinition, Field, InterfaceTypeDefinition,
        InterfaceType>
    {
        public override void DefineParent(SchemaBuilder sb, string parentName) => sb.Interface(parentName);

        public override InterfaceType GetParent(Schema schema, string parentName) => schema.GetInterface(parentName);

        public override InterfaceTypeDefinition GetParent(SchemaBuilder schemaBuilder, string parentName) =>
            schemaBuilder.GetDefinition().GetInterface(parentName);

        public override IReadOnlyDictionary<string, FieldDefinition> GetCollection(InterfaceTypeDefinition parent) =>
            parent.Fields;

        public override IReadOnlyDictionary<string, Field> GetCollection(InterfaceType parent) =>
            parent.Fields;

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(InterfaceTypeDefinition parent,
            string name) => parent.FindIgnoredFieldConfigurationSource(name);

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Interface(parentName).Field(name, "String");
        }

        public override void IgnoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Interface(parentName).IgnoreField(name);
        }

        public override void UnignoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Interface(parentName).UnignoreField(name);
        }

        public override void RenameItem(SchemaBuilder sb, string parentName, string name, string newName)
        {
            sb.Interface(parentName).Field(name, field => field?.Name(newName));
        }
    }
}