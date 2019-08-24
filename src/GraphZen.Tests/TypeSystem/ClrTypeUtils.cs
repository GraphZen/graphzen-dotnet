// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    public static class ClrTypeUtils
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

        public static Type[] GetImplementedTypes(Type type)
        {
            var types = type.Assembly.GetTypes()
                .Where(type.IsAssignableFrom)
                .Where(s => type.IsAbstract)
                .Where(_ => !_.IsAbstract)
                .OrderBy(s => s.Name).ToArray();
            return types;
        }

        public static IEnumerable<Type> GetTypesImplementingOpenGenericType(Type openGenericType,
            bool? isAbstract = false)
        {
            var assembly = openGenericType.Assembly;
            return from x in assembly.GetTypes()
                   from z in x.GetInterfaces()
                   let y = x.BaseType
                   where
                       y != null && y.IsGenericType &&
                       openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition()) ||
                       z.IsGenericType &&
                       openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition())
                   where !isAbstract.HasValue || x.IsAbstract == isAbstract.Value
                   select x;
        }
    }
}