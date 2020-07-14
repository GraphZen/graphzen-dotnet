// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Enums.EnumValues
{
    // ReSharper disable once InconsistentNaming
    public abstract class Enum_Values :
        NamedCollectionConfigurationFixture<IEnumValues,
            IEnumValuesDefinition, IMutableEnumValuesDefinition, MutableEnumValue, EnumValue,
            MutableEnumType,
            EnumType>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Enum(parentName);
        }

        public override EnumType GetParent(Schema schema, string parentName) => schema.GetEnum(parentName);

        public override MutableEnumType GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetEnum(parentName);

        public override NamedCollection<MutableEnumValue> GetCollection(MutableEnumType parent) =>
            parent.Values.ToNamedCollection();

        public override NamedCollection<EnumValue> GetCollection(EnumType parent) => parent.Values.ToNamedCollection();

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(MutableEnumType parent,
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