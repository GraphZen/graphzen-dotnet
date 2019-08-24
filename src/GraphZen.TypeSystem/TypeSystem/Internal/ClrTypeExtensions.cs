#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;


// ReSharper disable UnusedMember.Global

namespace GraphZen.TypeSystem.Internal
{
    public static class ClrTypeExtensions
    {
        internal static bool IsFunc([NotNull] this Type clrType)
        {
            Debug.Assert(clrType.FullName != null, "clrType.FullName != null");
            return clrType.Assembly == typeof(Func<>).Assembly && clrType.FullName.StartsWith("System.Func");
        }


        internal static bool TryGetListItemType([NotNull] this Type clrType, out Type itemType)
        {
            itemType = default;


            if (typeof(ICollection).IsAssignableFrom(clrType)
                || clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                || clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>)
                || clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(ICollection<>)
                || clrType.GetInterfaces().Any(i =>
                {
                    // ReSharper disable once PossibleNullReferenceException
                    if (i.IsGenericType)
                    {
                        var def = i.GetGenericTypeDefinition();
                        return def == typeof(IReadOnlyCollection<>) || def == typeof(ICollection<>) ||
                               def == typeof(IEnumerable<>);
                    }

                    return false;
                }))
            {
                if (clrType.IsGenericType)
                {
                    var args = clrType.GetGenericArguments();
                    if (args.Length == 1)
                    {
                        itemType = args[0];
                    }
                }
                else
                {
                    itemType = clrType.GetElementType();
                }

                return itemType != null;
            }

            return false;
        }

        internal static bool TryGetNullableType([NotNull] this Type clrType, out Type nullableClrType)
        {
            nullableClrType = default;
            if (clrType.IsGenericType)
            {
                var genericDef = clrType.GetGenericTypeDefinition();
                if (genericDef == typeof(Nullable<>))
                {
                    nullableClrType = clrType.GetGenericArguments()[0];
                    return true;
                }
            }

            return false;
        }

        internal static bool TryGetTaskResultType([NotNull] this Type clrType, out Type resultClrType)
        {
            resultClrType = default;
            if (clrType.IsGenericType)
            {
                var genericDef = clrType.GetGenericTypeDefinition();
                if (genericDef == typeof(Task<>))
                {
                    resultClrType = clrType.GetGenericArguments()[0];
                    return true;
                }
            }

            return false;
        }

        private static bool TryGetGraphQLTypeInfoRecursive([NotNull] this Type clrType,
            out TypeSyntax typeNode, out Type innerClrType, bool canBeNull = false,
            bool itemCanBeNull = false)
        {
            if (clrType.TryGetTaskResultType(out var resultType))
            {
                return resultType.TryGetGraphQLTypeInfoRecursive(out typeNode,
                    out innerClrType, canBeNull, itemCanBeNull);
            }

            if (clrType.TryGetNullableType(out var nullable))
            {
                return nullable.TryGetGraphQLTypeInfoRecursive(out typeNode, out innerClrType, true);
            }

            if (clrType.TryGetListItemType(out var itemType) &&
                itemType.TryGetGraphQLTypeInfoRecursive(out typeNode, out innerClrType, itemCanBeNull))
            {
                typeNode = canBeNull
                    ? (TypeSyntax)SyntaxFactory.ListType(typeNode)
                    : SyntaxFactory.NonNull(SyntaxFactory.ListType(typeNode));
                return true;
            }

            if (!clrType.IsValidClrType())
            {
                innerClrType = null;
                typeNode = null;
                return false;
            }

            typeNode = canBeNull
                ? (TypeSyntax)SyntaxFactory.NamedType(clrType)
                : SyntaxFactory.NonNull(SyntaxFactory.NamedType(clrType));
            innerClrType = clrType.GetEffectiveClrType();
            return true;
        }

        public static bool TryGetGraphQLTypeInfo(this Type clrType,
            out TypeSyntax typeNode,
            out Type innerClrType, bool canBeNull = false, bool itemCanBeNull = false) =>
            TryGetGraphQLTypeInfoRecursive(
                Check.NotNull(clrType, nameof(clrType)), out typeNode, out innerClrType, canBeNull, itemCanBeNull);

        public static bool TryGetGraphQLTypeInfo(this MethodInfo method, out TypeSyntax typeNode, out Type innerClrType)
            => Check.NotNull(method, nameof(method))
                .ReturnType
                .TryGetGraphQLTypeInfo(
                    out typeNode, out innerClrType,
                    method.CanBeNull(), method.ItemCanBeNull());

        public static bool TryGetGraphQLTypeInfo(this PropertyInfo property, out TypeSyntax typeNode,
            out Type innerClrType)
            => Check.NotNull(property, nameof(property))
                .PropertyType
                .TryGetGraphQLTypeInfo(
                    out typeNode, out innerClrType,
                    property.CanBeNull(), property.ItemCanBeNull());

        public static bool TryGetGraphQLTypeInfo(this ParameterInfo parameter, out TypeSyntax typeNode,
            out Type innerClrType)
            => Check.NotNull(parameter, nameof(parameter)).ParameterType
                .TryGetGraphQLTypeInfo(
                    out typeNode, out innerClrType,
                    parameter.CanBeNull(), parameter.ItemCanBeNull());


        [NotNull]
        public static Type GetEffectiveClrType([NotNull] this Type clrType) =>
            clrType.GetCustomAttribute<GraphQLTypeAttribute>()?.ClrType ?? clrType;


        public static bool IsSameOrSubclass([NotNull] this Type potentialSubClass, [NotNull] Type potentialBase) =>
            potentialSubClass.IsSubclassOf(potentialBase) || potentialBase == potentialSubClass;

        public static bool TryGetOutputTypeKind([NotNull] this Type clrType, out TypeKind? kind)
        {
            kind = null;
            if (!IsValidClrType(clrType))
            {
                return false;
            }

            if (clrType.IsEnum)
            {
                kind = TypeKind.Enum;
                return true;
            }

            if (clrType.IsValueType)
            {
                kind = TypeKind.Scalar;
                return true;
            }

            if (clrType.IsInterface)
            {
                kind = TypeKind.Interface;
                return true;
            }

            kind = TypeKind.Object;
            return true;
        }

        public static bool TryGetInputTypeKind([NotNull] this Type clrType, out TypeKind? kind)
        {
            kind = null;
            if (!IsValidClrType(clrType))
            {
                return false;
            }

            if (clrType.IsEnum)
            {
                kind = TypeKind.Enum;
                return true;
            }

            if (clrType.IsValueType)
            {
                kind = TypeKind.Scalar;
                return true;
            }

            kind = TypeKind.InputObject;
            return false;
        }


        public static bool IsValidClrType([NotNull] this Type clrType)
        {
            if (clrType.IsGenericType)
            {
                return false;
            }

            return clrType.HasValidGraphQLName();
        }

        public static IEnumerable<MemberInfo> GetTargetingInterfaceProperties([NotNull] this PropertyInfo property)
        {
            var methodInfo = property.GetGetMethod();
            // ReSharper disable once PossibleNullReferenceException
            foreach (var iface in property.DeclaringType.GetInterfaces())
            {
                var mapping = property.DeclaringType.GetInterfaceMap(iface);
                for (var i = 0; i < mapping.InterfaceMethods.Length; i++)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    if (mapping.TargetMethods[i] == methodInfo)
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        var interfaceMethod = mapping.InterfaceMethods[i];
                        // ReSharper disable twice PossibleNullReferenceException
                        yield return interfaceMethod.DeclaringType.GetProperty(property.Name);
                    }
                }
            }
        }


        public static IEnumerable<MemberInfo> GetTargetingInterfaceMethods([NotNull] this MethodInfo method
        )
        {
            // ReSharper disable once PossibleNullReferenceException
            foreach (var iface in method.DeclaringType.GetInterfaces())
            {
                var mapping = method.DeclaringType.GetInterfaceMap(iface);
                for (var i = 0; i < mapping.InterfaceMethods.Length; i++)
                {
                    if (mapping.TargetMethods[i] == method)
                    {
                        var interfaceMethod = mapping.InterfaceMethods[i];
                        yield return interfaceMethod;
                    }
                }
            }
        }

        [NotNull]
        [ItemNotNull]
        public static IEnumerable<Type> GetImplementingTypes([NotNull] this Type clrType)
        {
            // ReSharper disable once PossibleNullReferenceException
            var referencedAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                // ReSharper disable once PossibleNullReferenceException
                .Where(_ => referencedAssemblies.Contains(_.GetName())).Concat(new List<Assembly> { clrType.Assembly });
            foreach (var assembly in assemblies)
            {
                // ReSharper disable once PossibleNullReferenceException
                foreach (var type in assembly.DefinedTypes)
                {
                    if (type != clrType)
                    {
                        if (clrType.IsInterface && clrType.IsAssignableFrom(type))
                        {
                            yield return type;
                        }
                        else if (clrType.IsClass && type.IsSubclassOf(clrType))
                        {
                            yield return type;
                        }
                    }
                }
            }
        }
    }
}