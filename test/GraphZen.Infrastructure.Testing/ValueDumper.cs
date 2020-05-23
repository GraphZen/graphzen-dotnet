// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class ValueDumper
    {
        public static IEnumerable<Type> DumpTypes(this IEnumerable<Type> types)
        {
            // ReSharper disable once UnusedVariable
            var dumpTypes = types as Type[] ?? types.ToArray();
            dumpTypes
                .OrderBy(_ => _.Name)
                .Select(_ => new StringLiteral($"typeof({_.Name})")).Dump("types", true);
            return dumpTypes;
        }

        public static T Dump<T, TR>(this T value, Func<T, TR> selector, string prefix = "")
        {
            selector(value).Dump(prefix);
            return value;
        }

        public static T XDump<T>(this T value) => value;
        // ReSharper disable once MethodOverloadWithOptionalParameter
        public static T XDump<T>(this T value, string label = "value", bool expanded = false) => value;

        public static T Dump<T>(this T value, string label = "value", bool expanded = false)
        {
            if (expanded)
            {
                Console.WriteLine($"= {label} =\n{value.Inspect(true)}");
            }
            else
            {
                Console.WriteLine($"\t\t{label} \t\t-> {value.Inspect()}");
            }

            return value;
        }
    }
}