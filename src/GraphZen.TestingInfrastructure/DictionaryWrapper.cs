using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen
{
    public class DictionaryWrapper<TInner, T> : NamedCollection<T> where TInner : T
    {
        public DictionaryWrapper(IReadOnlyDictionary<string, TInner> innerDictionary)
        {
            InnerDictionary = Check.NotNull(innerDictionary, nameof(innerDictionary));
        }

        [NotNull]
        public IReadOnlyDictionary<string, TInner> InnerDictionary { get; }


        public override int Count => InnerDictionary.Count;
        public override bool ContainsKey(string key) => InnerDictionary.ContainsKey(key);
        public override bool TryGetValue([NotNull]string key, out T value)
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
}