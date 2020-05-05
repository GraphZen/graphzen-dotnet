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
        public static class InvalidNameException
        {
            public static string CannotCreateAnnotatedType(Type clrType, string annotatedName, TypeKind kind) =>
                $"Cannot create {kind.ToDisplayString()} type with CLR type '{clrType}'. It is annotated with the name \"{annotatedName}\", which is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotCreateType(Type clrType, TypeKind kind) =>
                $"Cannot create {kind.ToDisplayString()} type with CLR type '{clrType}'. The CLR type name '{clrType.Name}' is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotRename(string name, INamed named) =>
                $"Cannot rename {named}. \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotRemove(IMutableNamedTypeDefinition named) => $"Cannot remove name from {named}. Only custom names can be removed from GraphQL {named.Kind.ToDisplayStringLower()} with a CLR type.";
        }
    }
}