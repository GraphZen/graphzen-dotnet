// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class ReflectionCodeGenerator
    {
        private static IReadOnlyList<Type> CodeGenMemberAttributes { get; } =
            ImmutableArray.Create(typeof(GenAccessorExtensionsAttribute));

        public static IReadOnlyList<Type> GetSourceTypes<T>() =>
            typeof(T).Assembly.GetTypes().Where(_ => _.Namespace?.Contains(nameof(GraphZen)) ?? false).ToList()
                .AsReadOnly();


        public static IEnumerable<(Type targetType, string contents)> GetTargetTypeAndContent(Type type) =>
            throw new NotImplementedException();

        public static IReadOnlyList<Type> GetCodeGenSourceTypes(Assembly assembly) =>
            assembly.GetTypes().Where(t =>
                    t.GetMembers().Any(m =>
                        m.GetCustomAttributes().Any(a => CodeGenMemberAttributes.Contains(a.GetType()))))
                .ToReadOnlyList();
    }
}