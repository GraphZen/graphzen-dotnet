// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
#nullable disable

namespace GraphZen.Objects
{
    public abstract class Schema_Objects : CollectionConfigurationFixture<IObjectTypesContainer,
        IObjectTypesContainerDefinition, IMutableObjectTypesContainerDefinition, ObjectTypeDefinition, ObjectType,
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

        public override NamedCollection<ObjectTypeDefinition> GetCollection(SchemaDefinition parent)
        {
            return parent.GetObjects().ToNamedCollection();
        }

        public override NamedCollection<ObjectType> GetCollection(Schema parent)
        {
            var objects = parent.GetObjects();
            var casted = objects.ToNamedCollection();
            return casted;
        }

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(SchemaDefinition parent, string name)
        {
            return parent.FindIgnoredTypeConfigurationSource(name);
        }
    }
}