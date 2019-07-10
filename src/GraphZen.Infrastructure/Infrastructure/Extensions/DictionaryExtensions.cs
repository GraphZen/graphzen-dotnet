// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.Infrastructure
{
    internal static class DictionaryExtensions
    {
        [NotNull]
        public static ICollection<TItem> GetItems<TKey, TItem>(this IDictionary<TKey, ICollection<TItem>> dictionary,
            TKey key)
        {
            Check.NotNull(dictionary, nameof(dictionary));
            Check.NotNull(key, nameof(key));
            if (dictionary.TryGetValue(key, out var collection))
            {
                return collection;
            }

            return Enumerable.Empty<TItem>().ToList();
        }

        public static void AddItem<TKey, TItem>(this IDictionary<TKey, ICollection<TItem>> dictionary, TKey key,
            TItem item)
        {
            Check.NotNull(dictionary, nameof(dictionary));
            Check.NotNull(key, nameof(key));
            if (dictionary.TryGetValue(key, out var collection))
            {
                collection.Add(item);
            }
            else
            {
                dictionary[key] = new List<TItem> { item };
            }
        }


        [NotNull]
        public static IReadOnlyList<DictionaryEntry> GetEntries(this IDictionary dictionary)
        {
            Check.NotNull(dictionary, nameof(dictionary));
            var entries = new List<DictionaryEntry>();
            var enumerator = dictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                entries.Add(enumerator.Entry);
            }

            return entries.AsReadOnly();
        }

        internal static TValue? FindValueOrDefault<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> dictionary, [NotNull] TKey key) where TValue : struct =>
            dictionary.TryGetValue(key, out var val) ? val : (TValue?)null;


        public static void Increment<TKey>(this IDictionary<TKey, int> dictionary, TKey key)
        {
            Check.NotNull(dictionary, nameof(dictionary));
            Check.NotNull(key, nameof(key));
            if (dictionary.TryGetValue(key, out var value))
            {
                dictionary[key] = value + 1;
            }
            else
            {
                dictionary.Add(key, 1);
            }
        }
    }
}