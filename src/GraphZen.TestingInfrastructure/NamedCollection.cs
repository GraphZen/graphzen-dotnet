using System.Collections;
using System.Collections.Generic;
using GraphZen.Infrastructure;

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
}