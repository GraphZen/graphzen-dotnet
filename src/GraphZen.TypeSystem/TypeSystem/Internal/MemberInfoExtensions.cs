#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Internal
{
    public static class MemberInfoExtensions
    {
        public static bool TryGetDescriptionFromDataAnnotation([NotNull] this MemberInfo memberInfo,
            out string description)
        {
            description = memberInfo.GetCustomAttribute<DescriptionAttribute>()?.Description;

            return description != null;
        }

        public static bool TryGetDescriptionFromDataAnnotation([NotNull] this ParameterInfo parameterInfo,
            out string description)
        {
            description = parameterInfo.GetCustomAttribute<DescriptionAttribute>()?.Description;

            return description != null;
        }


        public static bool NotIgnored([NotNull] this ICustomAttributeProvider memberInfo)
        {
            Check.NotNull(memberInfo, nameof(memberInfo));
            var ignoredAttribute =
                memberInfo.GetCustomAttributes(typeof(GraphQLIgnoreAttribute), false).SingleOrDefault();
            return ignoredAttribute == null;
        }

        public static bool IsIgnoredByDataAnnotation([NotNull] this PropertyInfo property)
        {
            var ignoredAttribute = property
                .GetCustomAttribute<GraphQLIgnoreAttribute>();
            return ignoredAttribute != null;
        }

        public static bool IsIgnoredByDataAnnotation([NotNull] this Type clrType)
        {
            var ignoredAttribute = clrType.GetCustomAttributes(typeof(GraphQLIgnoreAttribute), false).SingleOrDefault();
            return ignoredAttribute != null;
        }

        public static bool IsIgnoredByDataAnnotation([NotNull] this MethodInfo method)
        {
            var ignoredAttribute = method
                .GetCustomAttribute<GraphQLIgnoreAttribute>();
            return ignoredAttribute != null;
        }

        public static bool IsIgnoredByDataAnnotation([NotNull] this ParameterInfo parameterInfo)
        {
            Check.NotNull(parameterInfo, nameof(parameterInfo));
            var ignoredAttribute = parameterInfo
                .GetCustomAttribute<GraphQLIgnoreAttribute>();
            return ignoredAttribute != null;
        }


        public static (string name, ConfigurationSource nameConfigurationSource) GetGraphQLFieldName(
            [NotNull] this MemberInfo member)
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
            // ReSharper disable once AssignNullToNotNullAttribute
            var customName = parameter.GetCustomAttribute<GraphQLNameAttribute>()?.Name;
            return customName != null
                ? (customName, ConfigurationSource.DataAnnotation)
                : (parameter.Name, ConfigurationSource.Convention);
        }


        private static (string name, ConfigurationSource nameConfigurationSource) GetGraphQLFieldName(
            [NotNull] this MemberInfo member, [NotNull] Type fieldClrType)
        {
            var customName = member.GetCustomAttribute<GraphQLNameAttribute>()?.Name;
            if (customName != null)
            {
                return (customName, ConfigurationSource.DataAnnotation);
            }

            if (fieldClrType.IsGenericType && fieldClrType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                return (member.Name.TrimAsyncSuffix().FirstCharToLower(), ConfigurationSource.Convention);
            }

            return (member.Name.FirstCharToLower(), ConfigurationSource.Convention);
        }

        public static (string name, ConfigurationSource configurationSource) GetGraphQLNameForEnumValue(
            this MemberInfo member)
        {
            Check.NotNull(member, nameof(member));

            var customName = member.GetCustomAttribute<GraphQLNameAttribute>()?.Name;
            if (customName != null)
            {
                return (customName, ConfigurationSource.DataAnnotation);
            }

            return (member.Name, ConfigurationSource.Convention);
        }
    }
}