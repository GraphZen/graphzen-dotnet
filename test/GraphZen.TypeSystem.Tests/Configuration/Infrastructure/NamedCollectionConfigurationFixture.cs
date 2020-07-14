// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Infrastructure
{
    public abstract class NamedCollectionConfigurationFixture<
        TMarker,
        TDefMarker,
        TMutableDefMarker,
        TCollectionItemDefinition,
        TCollectionItem,
        TParentMemberDefinition,
        TParentMember
    > :
        ConfigurationFixture<TMarker, TDefMarker, TMutableDefMarker,
            TParentMemberDefinition, TParentMember>, ICollectionConfigurationFixture
        where TMutableDefMarker : TDefMarker
        where TParentMemberDefinition : MutableMember, TMutableDefMarker
        where TParentMember : MutableMember, TMarker
        where TCollectionItemDefinition : MutableMember, IMutableName
        where TCollectionItem : MutableMember, IName
        where TMarker : TDefMarker
    {
        public Type CollectionItemMemberType { get; } = typeof(TCollectionItem);
        public Type CollectionItemMemberDefinitionType { get; } = typeof(TCollectionItemDefinition);

        public NamedCollection<IMutableName>
            GetCollection(SchemaBuilder sb, string parentName) =>
            GetCollection(GetParent(sb, parentName))
                .ToNamedCollection<IMutableName, TCollectionItemDefinition>();

        public NamedCollection<IName> GetCollection(Schema schema, string parentName)
        {
            var collection = GetCollection(GetParent(schema, parentName));
            var casted = collection.ToNamedCollection<IName, TCollectionItem>();
            return casted;
        }


        public abstract void AddItem(SchemaBuilder sb, string parentName, string name);
        public abstract void IgnoreItem(SchemaBuilder sb, string parentName, string name);

        public abstract void UnignoreItem(SchemaBuilder sb, string parentName,
            string name);

        public abstract void RenameItem(SchemaBuilder sb, string parentName, string itemName, string newName);

        public ConfigurationSource? FindIgnoredItemConfigurationSource(SchemaBuilder sb,
            string parentName,
            string itemName) =>
            FindIgnoredItemConfigurationSource(GetParent(sb, parentName), itemName);


        public abstract NamedCollection<TCollectionItemDefinition> GetCollection(
            TParentMemberDefinition parent);


        public abstract NamedCollection<TCollectionItem> GetCollection(TParentMember parent);

        public abstract ConfigurationSource? FindIgnoredItemConfigurationSource(
            TParentMemberDefinition parent, string name);

        //public class CollectionWrapper<T>
        //{
        //    public CollectionWrapper(IReadOnlyDictionary<string, TInner> innerDictionary)
        //    {
        //        InnerDictionary = innerDictionary;
        //    }

        //    private IReadOnlyDictionary<string, TInner> InnerDictionary { get; }


        //    public int Count => InnerDictionary.Count;
        //    public bool ContainsKey(string key) => throw new NotImplementedException();

        //    public bool TryGetValue(string key, out T value) => throw new NotImplementedException();

        //    public T this[string key] => throw new NotImplementedException();
        //}
    }
}