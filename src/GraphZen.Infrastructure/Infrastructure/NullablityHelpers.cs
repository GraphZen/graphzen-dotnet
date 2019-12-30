// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public static class NullablityHelpers
    {
        public static Type GetMemberType(this MemberInfo memberInfo)
            => (memberInfo as PropertyInfo)?.PropertyType ?? ((FieldInfo) memberInfo)?.FieldType!;

        public const string NullableAttributeFullName = "System.Runtime.CompilerServices.NullableAttribute";

        public const string NullableContextAttributeFullName =
            "System.Runtime.CompilerServices.NullableContextAttribute";

        private class NonNullablityState
        {
            public Type? NullableAttrType;
            public Type? NullableContextAttrType;
            public FieldInfo? NullableFlagsFieldInfo;
            public FieldInfo? NullableContextFlagFieldInfo;
            public Dictionary<Type, bool> TypeCache { get; } = new Dictionary<Type, bool>();
        }

        private static readonly NonNullablityState State = new NonNullablityState();


        public static bool IsNonNullableReferenceTypeDepr(MemberInfo memberInfo)
        {
            if (memberInfo.GetMemberType().IsValueType) return false;


            // First check for [MaybeNull] on the return value. If it exists, the member is nullable.
            var isMaybeNull = memberInfo switch
            {
                FieldInfo f => f.GetCustomAttribute<MaybeNullAttribute>() != null,
                PropertyInfo p => p.GetMethod?.ReturnParameter?.GetCustomAttribute<MaybeNullAttribute>() != null,
                _ => false
            };

            if (isMaybeNull) return false;

            // For C# 8.0 nullable types, the C# currently synthesizes a NullableAttribute that expresses nullability into assemblies
            // it produces. If the model is spread across more than one assembly, there will be multiple versions of this attribute,
            // so look for it by name, caching to avoid reflection on every check.
            // Note that this may change - if https://github.com/dotnet/corefx/issues/36222 is done we can remove all of this.

            // First look for NullableAttribute on the member itself
            if (Attribute.GetCustomAttributes(memberInfo)
                .FirstOrDefault(a => a.GetType().FullName == NullableAttributeFullName) is { } attribute)
            {
                var attributeType = attribute.GetType();

                if (attributeType != State.NullableAttrType)
                {
                    State.NullableFlagsFieldInfo = attributeType.GetField("NullableFlags");
                    State.NullableAttrType = attributeType;
                }

                if (State.NullableFlagsFieldInfo?.GetValue(attribute) is byte[] flags)
                    return flags.FirstOrDefault() == 1;
            }

            // No attribute on the member, try to find a NullableContextAttribute on the declaring type
            var type = memberInfo.DeclaringType;
            if (type != null)
            {
                if (State.TypeCache.TryGetValue(type, out var cachedTypeNonNullable)) return cachedTypeNonNullable;

                if (Attribute.GetCustomAttributes(type)
                        .FirstOrDefault(a => a.GetType().FullName == NullableContextAttributeFullName) is Attribute
                    contextAttr)
                {
                    var attributeType = contextAttr.GetType();

                    if (attributeType != State.NullableContextAttrType)
                    {
                        State.NullableContextFlagFieldInfo = attributeType.GetField("Flag");
                        State.NullableContextAttrType = attributeType;
                    }

                    if (State.NullableContextFlagFieldInfo?.GetValue(contextAttr) is byte flag)
                        return State.TypeCache[type] = flag == 1;
                }

                return State.TypeCache[type] = false;
            }

            return false;
        }
    }
}