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
            internal static string DuplicateField(IFieldDefinition field, string name) => $"Cannot rename {field} to \"{name}\": {field.DeclaringType?.ToString()?.FirstCharToUpper()} already contains a field named \"{name}\".";
            internal static string DuplicateInputField(IInputFieldDefinition field, string name) => $"Cannot rename {field} to \"{name}\": {field.DeclaringMember?.ToString()?.FirstCharToUpper()} already contains a field named \"{name}\".";
            internal static string DuplicateArgument(IArgumentDefinition argument, string name)
            {
                var parent = argument.DeclaringMember is IFieldDefinition fd ? $"{fd} on {fd.DeclaringType}" : argument.DeclaringMember.ToString();
                return $"Cannot rename {argument} to \"{name}\": {parent?.FirstCharToUpper()} already contains an argument named \"{name}\".";
            }
        }

        public static class InvalidNameException
        {
            private static string GetClrTypeDisplay(Type clrType) => clrType.IsInterface ? "interface" :
                clrType.IsClass ? "class" :
                clrType.IsEnum ? "enum" : "type";


            public static string CannotGetOrCreateBuilderForClrTypeWithInvalidNameAttribute(Type clrType,
                string annotatedName, TypeKind kind)
                =>
                    $"Cannot get or create GraphQL {kind.ToDisplayStringLower()} type builder with CLR {GetClrTypeDisplay(clrType)} '{clrType.Name}'. The name \"{annotatedName}\" specified in the {nameof(GraphQLNameAttribute)} on the {clrType.Name} CLR {GetClrTypeDisplay(clrType)} is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotGetOrCreateTypeBuilderWithInvalidName(string name, TypeKind kind)
                =>
                    $"Cannot get or create GraphQL type builder for {kind.ToDisplayStringLower()} named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotGetOrCreateFieldBuilderWithInvalidName(string name,
                IFieldsDefinition declaringType)
                =>
                    $"Cannot get or create GraphQL field builder for field \"{name}\" on {declaringType}. The field name \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";


            public static string CannotGetOrCreateBuilderForClrTypeWithInvalidName(Type clrType, TypeKind kind)
                =>
                    $"Cannot get or create GraphQL {kind.ToDisplayStringLower()} type builder with CLR {GetClrTypeDisplay(clrType)} '{clrType.Name}'. The CLR {GetClrTypeDisplay(clrType)} name '{clrType.Name}' is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotCreateInputValueWithInvalidName(IInputValueDefinition def, string name)
            {
                var type = def is IArgumentDefinition ? "argument" : def is IInputFieldDefinition ? "field" : throw new NotImplementedException();
                return $"Cannot create {type} named \"{name}\" for {def.DeclaringMember}: \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";
            }

            public static string CannotRename(string name, INamed named) =>
                $"Cannot rename {named}. \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotRename(string name, INamed named, INamed parentDef) =>
                $"Cannot rename {named} on {parentDef}: \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotRenameArgument(IArgumentDefinition argument, string name)
            {
                var parent = argument.DeclaringMember is IFieldDefinition fd
                    ? $"{fd} on {fd.DeclaringType}"
                    : argument.DeclaringMember.ToString();
                return
                    $"Cannot rename {argument} on {parent}: \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";
            }


            public static string CannotRemove(IMutableNamedTypeDefinition named) =>
                $"Cannot remove name from {named}. Only custom names can be removed from GraphQL {named.Kind.ToDisplayStringLower()} with a CLR type.";
        }
    }
}