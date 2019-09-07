// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Configuration.Infrastructure;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.Configuration.Enums.EnumValues
{
    public abstract class Enum_Values :
        CollectionConfigurationFixture<IEnumValuesContainer,
            IEnumValuesContainerDefinition, IMutableEnumValuesContainerDefinition, EnumValueDefinition, EnumValue,
            EnumTypeDefinition,
            EnumType>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Enum(parentName);
        }

        public override EnumType GetParent(Schema schema, string parentName) => schema.GetEnum(parentName);

        public override EnumTypeDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetEnum(parentName);

        public override NamedCollection<EnumValueDefinition> GetCollection(EnumTypeDefinition parent) =>
            parent.Values.ToNamedCollection();

        public override NamedCollection<EnumValue> GetCollection(EnumType parent) => parent.Values.ToNamedCollection();

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(EnumTypeDefinition parent,
            string name) =>
            parent.FindIgnoredValueConfigurationSource(name);

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Enum(parentName).Value(name);
        }

        public override void IgnoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Enum(parentName).IgnoreValue(name);
        }

        public override void UnignoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Enum(parentName).UnignoreValue(name);
        }

        public override void RenameItem(SchemaBuilder sb, string parentName, string name, string newName)
        {
            sb.Enum(parentName).Value(name, value => value.Name(newName));
        }
    }
}