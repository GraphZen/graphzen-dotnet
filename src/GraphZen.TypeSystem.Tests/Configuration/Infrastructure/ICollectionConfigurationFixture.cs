// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen
{
    public interface ICollectionConfigurationFixture : IConfigurationFixture
    {
        Type CollectionItemMemberType { get; }
        Type CollectionItemMemberDefinitionType { get; }
        NamedCollection<IMutableNamed> GetCollection( SchemaBuilder sb,  string parentName);
        NamedCollection<INamed> GetCollection( Schema schema,  string parentName);
        void AddItem( SchemaBuilder sb,  string parentName,  string itemName);
        void IgnoreItem( SchemaBuilder sb,  string parentName,  string itemName);
        void UnignoreItem( SchemaBuilder sb,  string parentName,  string itemName);

        void RenameItem( SchemaBuilder sb,  string parentName,  string itemName,
             string newName);

        ConfigurationSource? FindIgnoredItemConfigurationSource( SchemaBuilder sb,  string parentName,
             string itemName);
    }
}