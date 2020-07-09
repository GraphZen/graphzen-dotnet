// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.Infrastructure
{
    internal static class EnumerableExtensions
    {
        public static bool TryGetDuplicateValueBy<TSource, TValue>(this IEnumerable<TSource> source,
            Func<TSource, TValue> selector, [NotNullWhen(true)] out TSource duplicate) where TSource : class
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
            Func<TSource, TKey> selector, out TKey duplicate) where TSource : class
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


        public static IReadOnlyList<T> ToReadOnlyListWithMutations<T>(this IEnumerable<T> source,
            Action<List<T>> listConfigurator)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(listConfigurator, nameof(listConfigurator));
            var list = source.ToList();
            listConfigurator(list);
            return list.AsReadOnly();
        }


        public static IReadOnlyDictionary<TKey, TSource> ToReadOnlyDictionary<TKey, TSource>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector
        )
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(keySelector, nameof(keySelector));
            return new ReadOnlyDictionary<TKey, TSource>(source.ToDictionary(keySelector, v => v));
        }


        public static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue, TSource>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TValue> valueSelector)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(keySelector, nameof(keySelector));
            Check.NotNull(valueSelector, nameof(ValueInspector));
            return new ReadOnlyDictionary<TKey, TValue>(source.ToDictionary(keySelector, valueSelector));
        }


        public static IReadOnlyDictionary<TKey, TSource> ToReadOnlyDictionaryIgnoringDuplicates<TKey, TSource>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(keySelector, nameof(keySelector));


            var dict = source.GroupBy(keySelector).ToDictionary(_ => _.Key, _ => _.First());
            return new ReadOnlyDictionary<TKey, TSource>(dict);
        }


        internal static IEnumerable<TSource> ToEnumerable<TSource>(this TSource value)
        {
            yield return value;
        }
    }
}