// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
#nullable disable

namespace GraphZen.Enums
{
    public abstract class Schema_Enums : CollectionConfigurationFixture<IEnumTypesContainer,
        IEnumTypesContainerDefinition, IMutableEnumTypesContainerDefinition, EnumTypeDefinition,
        EnumType,
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
            sb.Enum(name);
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
            sb.Enum(itemName).Name(newName);
        }

        public override NamedCollection<EnumTypeDefinition> GetCollection(SchemaDefinition parent)
        {
            return parent.GetEnums().ToNamedCollection();
        }

        public override NamedCollection<EnumType> GetCollection(Schema parent)
        {
            return parent.Enums.ToNamedCollection();
        }

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(SchemaDefinition parent, string name)
        {
            return parent.FindIgnoredTypeConfigurationSource(name);
        }
    }
}