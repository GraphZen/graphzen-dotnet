using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen
{

    public abstract class NamedCollection<T> : IEnumerable<T>
    {
        public abstract int Count { get; }
        public abstract bool ContainsKey(string key);
        public abstract bool TryGetValue(string key, out T value);

        public abstract T this[string key] { get; }

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class DictionaryWrapper<TInner, T> : NamedCollection<T> where TInner : T
    {
        public DictionaryWrapper(IReadOnlyDictionary<string, TInner> innerDictionary)
        {
            InnerDictionary = innerDictionary;
        }

        public IReadOnlyDictionary<string, TInner> InnerDictionary { get; }


        public override int Count => InnerDictionary.Count;
        public override bool ContainsKey(string key) => InnerDictionary.ContainsKey(key);
        public override bool TryGetValue(string key, out T value)
        {
            if (InnerDictionary.TryGetValue(key, out var innerVal))
            {
                value = innerVal;
                return true;
            }
            value = default;
            return false;
        }

        public override T this[string key] => InnerDictionary[key];
        public override IEnumerator<T> GetEnumerator() => InnerDictionary.Values.Cast<T>().GetEnumerator();

    }


    public interface ICollectionElementConfigurationFixture : IElementConfigurationFixture
    {
        Type CollectionItemMemberType { get; }
        Type CollectionItemMemberDefinitionType { get; }
        NamedCollection<IMutableNamed> GetCollection(SchemaBuilder sb, string parentName);
        NamedCollection<INamed> GetCollection(Schema schema, string parentName);
        void AddItem(SchemaBuilder sb, string parentName, string itemName);
        void IgnoreItem(SchemaBuilder sb, string parentName, string itemName);
        void UnignoreItem(SchemaBuilder sb, string parentName, string itemName);
        ConfigurationSource? FindIgnoredItemConfigurationSource(SchemaBuilder sb, string parentName, string itemName);
    }
}