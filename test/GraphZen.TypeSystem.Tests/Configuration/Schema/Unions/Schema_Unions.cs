// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Unions
{
    // ReSharper disable once InconsistentNaming
    public abstract class Schema_Unions : NamedCollectionConfigurationFixture<IUnionTypes,
        IUnionTypesDefinition, IMutableUnionTypesDefinition, MutableUnionType,
        UnionType,
        MutableSchema,
        Schema>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
        }

        public override Schema GetParent(Schema schema, string parentName) => schema;

        public override MutableSchema GetParent(SchemaBuilder sb, string parentName) => sb.GetDefinition();

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Union(name);
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
            sb.Union(itemName).Name(newName);
        }

        public override NamedCollection<MutableUnionType> GetCollection(MutableSchema parent) =>
            parent.GetUnions().ToNamedCollection();

        public override NamedCollection<UnionType> GetCollection(Schema parent) => parent.Unions.ToNamedCollection();

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(MutableSchema parent, string name) =>
            parent.FindIgnoredTypeConfigurationSource(name);
    }
}