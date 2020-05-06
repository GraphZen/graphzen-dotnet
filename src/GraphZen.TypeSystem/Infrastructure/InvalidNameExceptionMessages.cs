// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using static GraphZen.Infrastructure.GraphQLName;

namespace GraphZen.Infrastructure
{
    public static class TypeSystemExceptionMessages
    {
        public static class DuplicateNameException
        {
            internal static string DuplicateType(TypeIdentity identity, string newName, TypeIdentity existing) =>
                        $"Cannot rename {identity.Definition?.ToString() ?? identity.Name} to \"{newName}\", {existing.Definition?.ToString() ?? existing.ToString()} already exists. All GraphQL type names must be unique.";

        }
        public static class InvalidNameException
        {
            private static string GetClrTypeDisplay(Type clrType) => clrType.IsInterface ? "interface" :
                                clrType.IsClass ? "class" :
                                clrType.IsEnum ? "enum" : "type";


            public static string CannotCreateDefinitionForTypeWithInvalidNameAttribute(Type clrType, string annotatedName, TypeKind kind)
                => $"Cannot create GraphQL {kind.ToDisplayStringLower()} with CLR {GetClrTypeDisplay(clrType)} '{clrType}'. The name specified in the {nameof(GraphQLNameAttribute)} (\"{annotatedName}\") on the {clrType.Name} {GetClrTypeDisplay(clrType)} is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotCreateDefinitionForTypeWithInvalidName(Type clrType, TypeKind kind)
                => $"Cannot create GraphQL {kind.ToDisplayStringLower()} with CLR {GetClrTypeDisplay(clrType)} '{clrType}'. The {GetClrTypeDisplay(clrType)} name '{clrType.Name}' is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotRename(string name, INamed named) =>
                $"Cannot rename {named}. \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotRemove(IMutableNamedTypeDefinition named) =>
                $"Cannot remove name from {named}. Only custom names can be removed from GraphQL {named.Kind.ToDisplayStringLower()} with a CLR type.";
        }
    }
}