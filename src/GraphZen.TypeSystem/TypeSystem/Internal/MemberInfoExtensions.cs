// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public static class MemberInfoExtensions
    {
        public const string NullableAttributeFullName = "System.Runtime.CompilerServices.NullableAttribute";

        public const string NullableContextAttributeFullName =
            "System.Runtime.CompilerServices.NullableContextAttribute";

        public const byte Oblivious = 0;
        public const byte NotAnnotated = 1;
        public const byte Annotated = 2;


        private static bool TryGetNullableContextFlag(this IEnumerable<Attribute> attributes,
            [NotNullWhen(true)] out byte? flag)
        {
            flag = default;
            var nullableContextAttr =
                attributes.SingleOrDefault(_ => _.GetType().FullName == NullableContextAttributeFullName);
            if (nullableContextAttr != null)
            {
                var type = nullableContextAttr.GetType();
                var fieldInfo = type.GetField("Flag");
                if (fieldInfo?.GetValue(nullableContextAttr) is byte value) flag = value;
            }

            return flag != null;
        }

        private static bool TryGetNullableAttributeFlags(this IEnumerable<Attribute> attributes,
            [NotNullWhen(true)] out byte[]? flags)
        {
            flags = default;
            if (attributes.SingleOrDefault(_ => _.GetType().FullName == NullableAttributeFullName) is { } attr)
            {
                var type = attr.GetType();
                var fieldInfo = type.GetField("NullableFlags");
                if (fieldInfo?.GetValue(attr) is byte[] value)
                {
                    flags = value;
                    return true;
                }
            }

            return false;
        }

        public static bool HasNullableReferenceType(this MethodInfo method)
        {
            if (method.ReturnType.IsValueType) return false;

            if (method.GetCustomAttributes().TryGetNullableContextFlag(out var flag))
            {
                if (flag == Annotated) return true;
                if (flag == NotAnnotated) return false;
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            if (method.ReturnParameter.GetCustomAttributes().TryGetNullableAttributeFlags(out var flags))
                return flags.FirstOrDefault() == Annotated;

            return false;
        }

        public static bool HasNullableReferenceType(this ParameterInfo parameterInfo)
        {
            if (parameterInfo.ParameterType.IsValueType) return false;
            if (parameterInfo.GetCustomAttributes().TryGetNullableAttributeFlags(out var flags))
            {
                var flag = flags.FirstOrDefault();
                if (flag == Annotated) return true;
                if (flag == NotAnnotated) return false;
            }

            if (parameterInfo.Member.GetCustomAttributes().TryGetNullableContextFlag(out var contextFlag))
            {
                if (contextFlag == Annotated) return true;
                if (contextFlag == NotAnnotated) return false;
            }

            return false;
        }

        public static bool HasNullableReferenceType(this PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.IsValueType) return false;
            if (propertyInfo.GetCustomAttributes().TryGetNullableAttributeFlags(out var flags))
            {
                var flag = flags.FirstOrDefault();
                if (flag == Annotated) return true;
                if (flag == NotAnnotated) return false;
            }

            return false;
        }

        public static bool HasNullableReferenceType(this MemberInfo member) =>
            member switch
            {
                MethodInfo method => method.HasNullableReferenceType(),
                PropertyInfo property => property.HasNullableReferenceType(),
                _ => throw new NotImplementedException()
            };


        public static bool TryGetDescriptionFromDataAnnotation(this MemberInfo memberInfo,
            [NotNullWhen(true)] out string? description)
        {
            description = memberInfo.GetCustomAttribute<DescriptionAttribute>()?.Description;

            return description != null;
        }

        public static bool TryGetDescriptionFromDataAnnotation(this ParameterInfo parameterInfo,
            [NotNullWhen(true)] out string? description)
        {
            description = parameterInfo.GetCustomAttribute<DescriptionAttribute>()?.Description;

            return description != null;
        }

        public static bool NotIgnored(this ICustomAttributeProvider memberInfo)
        {
            Check.NotNull(memberInfo, nameof(memberInfo));
            var ignoredAttribute =
                memberInfo.GetCustomAttributes(typeof(GraphQLIgnoreAttribute), false).SingleOrDefault();
            return ignoredAttribute == null;
        }

        public static bool IsIgnoredByDataAnnotation(this PropertyInfo property)
        {
            var ignoredAttribute = property
                .GetCustomAttribute<GraphQLIgnoreAttribute>();
            return ignoredAttribute != null;
        }

        public static bool IsIgnoredByDataAnnotation(this Type clrType)
        {
            var ignoredAttribute = clrType.GetCustomAttributes(typeof(GraphQLIgnoreAttribute), false).SingleOrDefault();
            return ignoredAttribute != null;
        }

        public static bool IsIgnoredByDataAnnotation(this MethodInfo method)
        {
            var ignoredAttribute = method
                .GetCustomAttribute<GraphQLIgnoreAttribute>();
            return ignoredAttribute != null;
        }

        public static bool IsIgnoredByDataAnnotation(this ParameterInfo parameterInfo)
        {
            Check.NotNull(parameterInfo, nameof(parameterInfo));
            var ignoredAttribute = parameterInfo
                .GetCustomAttribute<GraphQLIgnoreAttribute>();
            return ignoredAttribute != null;
        }

        public static bool IsIgnoredByDataAnnotation(this MemberInfo memberInfo)
        {
            var ignoredAttribute = memberInfo
                .GetCustomAttribute<GraphQLIgnoreAttribute>();
            return ignoredAttribute != null;
        }


        public static (string name, ConfigurationSource nameConfigurationSource) GetGraphQLFieldName(
            this MemberInfo member)
        {
            switch (member)
            {
                case PropertyInfo property:
                    return property.GetGraphQLFieldName();
                case MethodInfo method:
                    return method.GetGraphQLFieldName();
            }

            throw new InvalidOperationException();
        }


        public static (string name, ConfigurationSource nameConfigurationSource) GetGraphQLFieldName(
            this PropertyInfo property) =>
            Check.NotNull(property, nameof(property)).GetGraphQLFieldName(property.PropertyType);

        public static (string name, ConfigurationSource configurationSource)
            GetGraphQLFieldName(this MethodInfo method) =>
            Check.NotNull(method, nameof(method)).GetGraphQLFieldName(method.ReturnType);

        public static (string name, ConfigurationSource configurationSource) GetGraphQLArgumentName(
            this ParameterInfo parameter)
        {
            Check.NotNull(parameter, nameof(parameter));

            var customName = parameter.GetCustomAttribute<GraphQLNameAttribute>()?.Name;
            return customName != null
                ? (customName, ConfigurationSource.DataAnnotation)
                : (parameter.Name!, ConfigurationSource.Convention);
        }


        private static (string name, ConfigurationSource nameConfigurationSource) GetGraphQLFieldName(
            this MemberInfo member, Type fieldClrType)
        {
            var customName = member.GetCustomAttribute<GraphQLNameAttribute>()?.Name;
            if (customName != null) return (customName, ConfigurationSource.DataAnnotation);

            if (fieldClrType.IsGenericType && fieldClrType.GetGenericTypeDefinition() == typeof(Task<>))
                return (member.Name.TrimAsyncSuffix().FirstCharToLower(), ConfigurationSource.Convention);

            return (member.Name.FirstCharToLower(), ConfigurationSource.Convention);
        }

        public static (string name, ConfigurationSource configurationSource) GetGraphQLNameForEnumValue(
            this MemberInfo member)
        {
            Check.NotNull(member, nameof(member));

            var customName = member.GetCustomAttribute<GraphQLNameAttribute>()?.Name;
            if (customName != null) return (customName, ConfigurationSource.DataAnnotation);

            return (member.Name, ConfigurationSource.Convention);
        }
    }
}