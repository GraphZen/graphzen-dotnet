// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
#nullable disable

namespace GraphZen.Interfaces
{
    public abstract class Schema_Interfaces : CollectionConfigurationFixture<IInterfaceTypesContainer,
        IInterfaceTypesContainerDefinition, IMutableInterfaceTypesContainerDefinition, InterfaceTypeDefinition,
        InterfaceType,
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
            sb.Interface(name);
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
            sb.Interface(itemName).Name(newName);
        }

        public override NamedCollection<InterfaceTypeDefinition> GetCollection(SchemaDefinition parent)
        {
            return parent.GetInterfaces().ToNamedCollection();
        }

        public override NamedCollection<InterfaceType> GetCollection(Schema parent)
        {
            return parent.Interfaces.ToNamedCollection();
        }

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(SchemaDefinition parent, string name)
        {
            return parent.FindIgnoredTypeConfigurationSource(name);
        }
    }
}