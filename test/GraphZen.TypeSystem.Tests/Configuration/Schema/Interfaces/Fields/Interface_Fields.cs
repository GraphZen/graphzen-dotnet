// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Interfaces.Fields
{
    // ReSharper disable once InconsistentNaming
    public abstract class Interface_Fields :
        NamedCollectionConfigurationFixture<IFields,
            IFields, IMutableFields, MutableField, Field,
            MutableInterfaceType,
            InterfaceType>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Interface(parentName);
        }

        public override InterfaceType GetParent(Schema schema, string parentName) => schema.GetInterface(parentName);

        public override MutableInterfaceType GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetInterface(parentName);

        public override NamedCollection<MutableField> GetCollection(MutableInterfaceType parent) =>
            parent.Fields.ToNamedCollection();

        public override NamedCollection<Field> GetCollection(InterfaceType parent) => parent.Fields.ToNamedCollection();

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(MutableInterfaceType parent,
            string name) =>
            parent.FindIgnoredFieldConfigurationSource(name);

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

        public override void RenameItem(SchemaBuilder sb, string parentName, string itemName, string newName)
        {
            sb.Interface(parentName).Field(itemName, field => field?.Name(newName));
        }
    }
}