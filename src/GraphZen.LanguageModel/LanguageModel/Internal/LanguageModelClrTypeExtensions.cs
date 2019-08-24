#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel.Internal
{
    public static class LanguageModelClrTypeExtensions
    {
        public static bool TryGetGraphQLName(this Type clrTYpe, out string name, object source = null)
        {
            name = default;
            if (clrTYpe.TryGetGraphQLNameWithoutValidation(out var maybeInvalidName, source) &&
                maybeInvalidName.IsValidGraphQLName())
            {
                name = maybeInvalidName;
                return true;
            }

            return false;
        }

        [NotNull]
        public static string GetGraphQLName(this Type clrType, object source = null)
        {
            if (clrType.TryGetGraphQLNameWithoutValidation(out var maybeInvalidName, source))
            {
                if (maybeInvalidName.IsValidGraphQLName())
                {
                    return maybeInvalidName;
                }

                throw new Exception(
                    $"Failed to get a valid GraphQL name for CLR type '{clrType}' because it was invalid. The invalid name was '{maybeInvalidName}'.");
            }

            throw new InvalidOperationException($"Failed to get a valid GraphQL name for CLR type '{clrType}'.");
        }

        private static bool TryGetGraphQLNameWithoutValidation(this Type clrType, out string name, object source = null)
        {
            Check.NotNull(clrType, nameof(clrType));
            name = default;

            if (clrType.TryGetGraphQLNameFromDataAnnotation(out name))
            {
                return true;
            }

            if (source != null)
            {
                var typenameProperty = clrType.GetProperty("__typename");
                if (typenameProperty != null && typenameProperty.PropertyType == typeof(string))
                {
                    var typename = typenameProperty.GetValue(source);

                    if (typename != null)
                    {
                        name = (string)typename;
                        return true;
                    }

                    return false;
                }
            }

            name = clrType.Name;
            return true;
        }


        public static bool HasValidGraphQLName(this Type clrType, object source = null) =>
            clrType.TryGetGraphQLName(out _, source);


        public static bool TryGetGraphQLNameFromDataAnnotation([NotNull] this Type clrType, out string name)
        {
            name = clrType.GetCustomAttributes(typeof(GraphQLNameAttribute), false).Cast<GraphQLNameAttribute>().SingleOrDefault()?.Name;
            return name != null;
        }
    }
}