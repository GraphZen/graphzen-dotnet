// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests
{
    public static class ClrTypeUtils
    {


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