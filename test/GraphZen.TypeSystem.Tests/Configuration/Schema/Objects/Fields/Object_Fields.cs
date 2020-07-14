// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Objects.Fields
{
    // ReSharper disable once InconsistentNaming
    public abstract class Object_Fields :
        NamedCollectionConfigurationFixture<IFields,
            IFields, IMutableFields, MutableField, Field, MutableObjectType,
            ObjectType>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Object(parentName);
        }

        public override ObjectType GetParent(Schema schema, string parentName) => schema.GetObject(parentName);

        public override MutableObjectType GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetObject(parentName);

        public override NamedCollection<MutableField> GetCollection(MutableObjectType parent) =>
            parent.Fields.ToNamedCollection();

        public override NamedCollection<Field> GetCollection(ObjectType parent) => parent.Fields.ToNamedCollection();

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(MutableObjectType parent,
            string name) =>
            parent.FindIgnoredFieldConfigurationSource(name);

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