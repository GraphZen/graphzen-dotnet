﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen
{

    public static class NamedCollection
    {
        [NotNull]
        public static NamedCollection<T> ToNamedCollection<T>(this IReadOnlyDictionary<string, T> source) where T : INamed =>
            ToNamedCollection<T, T>(source);

        [NotNull]
        public static NamedCollection<T> ToNamedCollection<T>(this IEnumerable<T> source) where T : INamed =>
            ToNamedCollection<T, T>(source);


        [NotNull]
        public static NamedCollection<TOuter> ToNamedCollection<TOuter, TInner>(this IReadOnlyDictionary<string, TInner> source) where TInner : TOuter where TOuter : INamed => new DictionaryWrapper<TInner, TOuter>(source);

        [NotNull]
        public static NamedCollection<TOuter> ToNamedCollection<TOuter, TInner>(this IEnumerable<TInner> source) where TInner : TOuter where TOuter : INamed => new EnumerableWrapper<TInner, TOuter>(source);

        private class EnumerableWrapper<TInner, T> : NamedCollection<T> where T : INamed where TInner : T, INamed
        {
            public EnumerableWrapper(IEnumerable<TInner> innerEnumerable)
            {
                InnerEnumerable = Check.NotNull(innerEnumerable, nameof(innerEnumerable));
            }

            [NotNull]
            [ItemNotNull]
            public IEnumerable<TInner> InnerEnumerable { get; }

            public override int Count => InnerEnumerable.Count();

            public override T this[string key]
            {
                get
                {
                    if (TryGetValue(key, out var value))
                    {
                        return value;
                    }

                    throw new InvalidOperationException($"Item named '{key}' does not exist in this collection");
                }
            }

            public override bool ContainsKey(string key) => InnerEnumerable.Any(_ => _.Name == key);

            public override bool TryGetValue(string key, out T value)
            {
                value = InnerEnumerable.SingleOrDefault(_ => _.Name == key);
                return value != null;
            }

            public override IEnumerator<T> GetEnumerator() => InnerEnumerable.Cast<T>().GetEnumerator();
        }

        private class DictionaryWrapper<TInner, T> : NamedCollection<T> where TInner : T where T : INamed
        {
            public DictionaryWrapper(IReadOnlyDictionary<string, TInner> innerDictionary)
            {
                InnerDictionary = Check.NotNull(innerDictionary, nameof(innerDictionary));
            }

            [NotNull]
            public IReadOnlyDictionary<string, TInner> InnerDictionary { get; }


            public override int Count => InnerDictionary.Count;

            public override T this[string key] => InnerDictionary[key];
            public override bool ContainsKey(string key) => InnerDictionary.ContainsKey(key);

            public override bool TryGetValue([NotNull] string key, out T value)
            {
                if (InnerDictionary.TryGetValue(key, out var innerVal))
                {
                    value = innerVal;
                    return true;
                }

                value = default;
                return false;
            }

            public override IEnumerator<T> GetEnumerator() => InnerDictionary.Values.Cast<T>().GetEnumerator();
        }
    }



    public abstract class NamedCollection<T> : IEnumerable<T> where T : INamed
    {
        public abstract int Count { get; }
        public abstract bool ContainsKey(string key);
        public abstract bool TryGetValue(string key, out T value);

        public abstract T this[string key] { get; }

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            // ReSharper disable once PossibleNullReferenceException
            return string.Join(", ", this.Select(_ => _.Name));
        }

    }
}