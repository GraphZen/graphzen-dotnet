// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Scalars
{
    public abstract class Schema_Scalars : CollectionConfigurationFixture<IScalarTypesContainer,
        IScalarTypesContainerDefinition, IMutableScalarTypesContainerDefinition, ScalarTypeDefinition,
        ScalarType,
        SchemaDefinition,
        Schema>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
        }

        public override Schema GetParent(Schema schema, string parentName) => schema;
        public override SchemaDefinition GetParent(SchemaBuilder sb, string parentName) => sb.GetDefinition();

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Scalar(name);
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
            sb.Scalar(itemName).Name(newName);
        }

        public override NamedCollection<ScalarTypeDefinition> GetCollection(SchemaDefinition parent) =>
            parent.GetScalars().ToNamedCollection();

        public override NamedCollection<ScalarType> GetCollection(Schema parent) =>
            parent.Scalars.ToNamedCollection();

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(SchemaDefinition parent, string name) =>
            parent.FindIgnoredTypeConfigurationSource(name);
    }
}