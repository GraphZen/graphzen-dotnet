// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
#nullable disable

namespace GraphZen.InputObjects
{
    public abstract class Schema_InputObjects : CollectionConfigurationFixture<IInputObjectTypesContainer,
        IInputObjectTypesContainerDefinition, IMutableInputObjectTypesContainerDefinition, InputObjectTypeDefinition,
        InputObjectType,
        SchemaDefinition,
        Schema>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
        }

        public override Schema GetParent(Schema schema, string parentName)
        {
            return schema;
        }

        public override SchemaDefinition GetParent(SchemaBuilder sb, string parentName)
        {
            return sb.GetDefinition();
        }

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.InputObject(name);
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
            sb.InputObject(itemName).Name(newName);
        }

        public override NamedCollection<InputObjectTypeDefinition> GetCollection(SchemaDefinition parent)
        {
            return parent.GetInputObjects().ToNamedCollection();
        }

        public override NamedCollection<InputObjectType> GetCollection(Schema parent)
        {
            return parent.InputObjects.ToNamedCollection();
        }

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(SchemaDefinition parent, string name)
        {
            return parent.FindIgnoredTypeConfigurationSource(name);
        }
    }
}