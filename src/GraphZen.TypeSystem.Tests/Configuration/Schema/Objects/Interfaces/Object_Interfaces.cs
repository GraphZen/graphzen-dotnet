// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.Objects.Interfaces
{
    public abstract class Object_Interfaces :
        CollectionConfigurationFixture<IInterfacesContainer,
            IInterfacesContainerDefinition, IMutableInterfacesContainerDefinition, InterfaceTypeDefinition,
            InterfaceType, ObjectTypeDefinition,
            ObjectType>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Object(parentName);
        }

        public override ObjectType GetParent(Schema schema, string parentName)
        {
            return schema.GetObject(parentName);
        }

        public override ObjectTypeDefinition GetParent(SchemaBuilder sb, string parentName)
        {
            return sb.GetDefinition().GetObject(parentName);
        }

        public override NamedCollection<InterfaceTypeDefinition> GetCollection(ObjectTypeDefinition parent)
        {
            return parent.GetInterfaces().ToNamedCollection();
        }

        public override NamedCollection<InterfaceType> GetCollection(ObjectType parent)
        {
            return parent.Interfaces.ToNamedCollection();
        }

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(ObjectTypeDefinition parent,
            string name)
        {
            return parent.FindIgnoredInterfaceConfigurationSource(name);
        }

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Object(parentName).ImplementsInterface(name);
        }

        public override void IgnoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Object(parentName).IgnoreInterface(name);
        }

        public override void UnignoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Object(parentName).UnignoreInterface(name);
        }

        public override void RenameItem(SchemaBuilder sb, string parentName, string name, string newName)
        {
            sb.Interface(name).Name(newName);
        }
    }
}