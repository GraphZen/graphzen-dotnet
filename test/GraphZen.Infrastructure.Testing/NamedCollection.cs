// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class NamedCollection
    {
        public static NamedCollection<T> ToNamedCollection<T>(this IReadOnlyDictionary<string, T> source)
            where T : class, INamed =>
            ToNamedCollection<T, T>(source);


        public static NamedCollection<T> ToNamedCollection<T>(this IEnumerable<T> source) where T : class, INamed =>
            ToNamedCollection<T, T>(source);


        public static NamedCollection<TOuter> ToNamedCollection<TOuter, TInner>(
            this IReadOnlyDictionary<string, TInner> source) where TInner : TOuter where TOuter : class, INamed =>
            new DictionaryWrapper<TInner, TOuter>(source);


        public static NamedCollection<TOuter> ToNamedCollection<TOuter, TInner>(this IEnumerable<TInner> source)
            where TInner : TOuter where TOuter : class, INamed =>
            new EnumerableWrapper<TInner, TOuter>(source);

        private class EnumerableWrapper<TInner, T> : NamedCollection<T> where T : class, INamed where TInner : T, INamed
        {
            public EnumerableWrapper(IEnumerable<TInner> innerEnumerable)
            {
                InnerEnumerable = Check.NotNull(innerEnumerable, nameof(innerEnumerable));
            }


            public IEnumerable<TInner> InnerEnumerable { get; }

            public override int Count => InnerEnumerable.Count();

            public override T this[string key]
            {
                get
                {
                    if (TryGetValue(key, out var value)) return value;

                    throw new InvalidOperationException($"Item named '{key}' does not exist in this collection");
                }
            }

            public override bool ContainsKey(string key) => InnerEnumerable.Any(_ => _.Name == key);

            public override bool TryGetValue(string key, [NotNullWhen(true)] out T? value)
            {
                value = InnerEnumerable.SingleOrDefault(_ => _.Name == key);
                return value != null;
            }

            public override IEnumerator<T> GetEnumerator() => InnerEnumerable.Cast<T>().GetEnumerator();
        }

        private class DictionaryWrapper<TInner, T> : NamedCollection<T> where TInner : T where T : class, INamed
        {
            public DictionaryWrapper(IReadOnlyDictionary<string, TInner> innerDictionary)
            {
                InnerDictionary = Check.NotNull(innerDictionary, nameof(innerDictionary));
            }


            public IReadOnlyDictionary<string, TInner> InnerDictionary { get; }


            public override int Count => InnerDictionary.Count;

            public override T this[string key] => InnerDictionary[key];

            public override bool ContainsKey(string key) => InnerDictionary.ContainsKey(key);

            public override bool TryGetValue(string key, [NotNullWhen(true)] out T? value)
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


    public abstract class NamedCollection<T> : IEnumerable<T> where T : class, INamed
    {
        public abstract int Count { get; }

        public abstract T this[string key] { get; }

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public abstract bool ContainsKey(string key);
        public abstract bool TryGetValue(string key, [NotNullWhen(true)] out T? value);

        public override string ToString()
        {
            // ReSharper disable once PossibleNullReferenceException
            return string.Join(", ", this.Select(_ => _.Name));
        }
    }
}