// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.Infrastructure
{
    internal static class EnumerableExtensions
    {
        [NotNull]
        public static IReadOnlyList<TSource> ToReadOnlyList<TSource>([NotNull] this IEnumerable<TSource> source) =>
            source.ToList().AsReadOnly();


        public static bool TryGetDuplicateValueBy<TSource, TValue>(this IEnumerable<TSource> source,
            Func<TSource, TValue> selector, out TSource duplicate)
        {
            Check.NotNull(selector, nameof(selector));
            Check.NotNull(source, nameof(source));
            var dupeEntry = source.GroupBy(selector).FirstOrDefault(_ =>
            {
                Debug.Assert(_ != null, nameof(_) + " != null");
                return _.Count() > 1;
            });
            if (dupeEntry != null)
            {
                duplicate = dupeEntry.FirstOrDefault();
                return true;
            }

            duplicate = default;
            return false;
        }

        public static bool TryGetDuplicateKeyBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, out TKey duplicate)
        {
            Check.NotNull(selector, nameof(selector));
            if (source.TryGetDuplicateValueBy(selector, out var dupe))
            {
                duplicate = selector(dupe);
                return true;
            }

            duplicate = default;
            return false;
        }


        [NotNull]
        public static IReadOnlyList<T> ToReadOnlyListWithMutations<T>(this IEnumerable<T> source,
            Action<List<T>> listConfigurator)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(listConfigurator, nameof(listConfigurator));
            var list = source.ToList();
            listConfigurator(list);
            return list.AsReadOnly();
        }


        [NotNull]
        public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue, TSource>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TValue> valueSelector = null)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(keySelector, nameof(keySelector));
            Check.NotNull(valueSelector, nameof(valueSelector));

            return new ReadOnlyDictionary<TKey, TValue>(source.ToDictionary(keySelector, valueSelector));
        }


        [NotNull]
        public static IReadOnlyDictionary<TKey, TSource> ToReadOnlyDictionaryIgnoringDuplicates<TKey, TSource>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(keySelector, nameof(keySelector));

            // ReSharper disable once PossibleNullReferenceException
            // ReSharper disable once AssignNullToNotNullAttribute
            var dict = source.GroupBy(keySelector).ToDictionary(_ => _.Key, _ => _.First());
            return new ReadOnlyDictionary<TKey, TSource>(dict);
        }


        [NotNull]
        internal static IEnumerable<TSource> ToEnumerable<TSource>(this TSource value)
        {
            yield return value;
        }
    }
}