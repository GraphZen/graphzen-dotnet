// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel.Internal
{
    public static class LanguageModelClrTypeExtensions
    {

        public static string GetGraphQLName(this Type clrType, Action<string> onInvalidNameAnnotation,
            Action<string> onInvalidClrTypeName)
        {
            if (clrType.TryGetGraphQLNameFromDataAnnotation(out var annotated))
            {
                if (annotated.IsValidGraphQLName())
                {
                    return annotated;
                }

                onInvalidNameAnnotation(annotated);
                throw new InvalidNameException(
                    $"Failed to get a valid GraphQL name for CLR type '{clrType}' because it was invalid. The invalid name was '{annotated}'.");
            }

            if (!clrType.Name.IsValidGraphQLName())
            {
                onInvalidNameAnnotation(clrType.Name);
                throw new InvalidNameException($"Failed to get a valid GraphQL name for CLR type '{clrType}'.");
            }

            return clrType.Name;
        }

        public static string GetGraphQLNameAnnotation(this Type clrType, object? source = null)
        {
            if (clrType.TryGetGraphQLName(out var maybeInvalidName, source))
            {
                if (maybeInvalidName.IsValidGraphQLName())
                {
                    return maybeInvalidName;
                }

                throw new Exception(
                    $"Failed to get a valid GraphQL name for CLR type '{clrType}' because it was invalid. The invalid name was '{maybeInvalidName}'.");
            }

            throw new Exception($"Failed to get a valid GraphQL name for CLR type '{clrType}'.");
        }

        public static bool TryGetGraphQLName(this Type clrType, [NotNullWhen(true)] out string? name,
            object? source = null)
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





        public static bool TryGetGraphQLNameFromDataAnnotation(this Type clrType, [NotNullWhen(true)] out string? name)
        {
            name = clrType.GetCustomAttributes(typeof(GraphQLNameAttribute), false).Cast<GraphQLNameAttribute>()
                .SingleOrDefault()?.Name;
            return name != null;
        }

    }
}