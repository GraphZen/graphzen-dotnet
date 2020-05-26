// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using static GraphZen.Infrastructure.GraphQLName;

namespace GraphZen.Internal
{
    internal static class TypeSystemExceptionMessages
    {
        public static string GetClrTypeKind(this Type clrType) => clrType.IsInterface ? "interface" :
            clrType.IsClass ? "class" :
            clrType.IsEnum ? "enum" : "type";


        public static class DuplicateItemException
        {
            public static string
                CannotChangeClrType<T>(T definition, Type clrType, IMutableClrType existing)
                where T : INamed, IClrType =>
                $"Cannot set CLR type on {definition} to CLR {GetClrTypeKind(clrType)} '{clrType.Name}': {existing} already exists with that CLR type.";


            internal static string CannotCreateTypeWithDuplicateNameAndType<T>(TypeKind kind, string name, Type clrType,
                T named, T typed)
                where T : NamedTypeDefinition =>
                $"Cannot create {kind.ToDisplayStringLower()} {name} with CLR {clrType.GetClrTypeKind()} '{clrType.Name}': both {named} and {typed} (with CLR {typed.ClrType?.GetClrTypeKind()} {typed.ClrType?.Name}) already exist.";

            internal static string CannotRenameField(IFieldDefinition field, string name) =>
                $"Cannot rename {field} to \"{name}\": {field.DeclaringType?.ToString()?.FirstCharToUpper()} already contains a field named \"{name}\".";

            internal static string CannotRenameDirective(IDirectiveDefinition directive, string name) =>
                $"Cannot rename {directive} to \"{name}\": a directive named \"{name}\" already exists.";

            internal static string CannotCreateDirectiveWithConflictingNameAndType(string name, Type clrType,
                IDirectiveDefinition existingNamed, IDirectiveDefinition existingTyped) =>
                $"Cannot create directive {name} with CLR type '{clrType.Name}': both {existingNamed} and {existingTyped} (with CLR type {existingTyped.ClrType?.Name}) already exist.";


            internal static string CannotRenameInputField(IInputFieldDefinition field, string name) =>
                $"Cannot rename {field} to \"{name}\": {field.DeclaringType?.ToString()?.FirstCharToUpper()} already contains a field named \"{name}\".";

            internal static string CannotRenameArgument(IArgumentDefinition argument, string name)
            {
                var parent = argument.DeclaringMember is IFieldDefinition fd
                    ? $"{fd} on {fd.DeclaringType}"
                    : argument.DeclaringMember.ToString();
                return
                    $"Cannot rename {argument} to \"{name}\": {parent?.FirstCharToUpper()} already contains an argument named \"{name}\".";
            }

            internal static string DuplicateEnumValue(IEnumValueDefinition enumValue, string name) =>
                $"Cannot rename enum value {enumValue.DeclaringType.Name}.{enumValue.Name} to \"{name}\": {enumValue.DeclaringType.ToString()?.FirstCharToUpper()} already contains a value named \"{name}\".";
        }

        public static class InvalidNameException
        {
            public static string CannotCreateDirectiveFromClrTypeWithInvalidNameAttribute(Type clrType,
                string annotatedName)
                =>
                    $"Cannot create directive with CLR {GetClrTypeKind(clrType)} '{clrType.Name}'. The name \"{annotatedName}\" specified in the {nameof(GraphQLNameAttribute)} on the {clrType.Name} CLR {GetClrTypeKind(clrType)} is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotGetOrCreateBuilderForClrTypeWithInvalidNameAttribute(Type clrType,
                string annotatedName, TypeKind kind)
                =>
                    $"Cannot get or create GraphQL {kind.ToDisplayStringLower()} type builder with CLR {GetClrTypeKind(clrType)} '{clrType.Name}'. The name \"{annotatedName}\" specified in the {nameof(GraphQLNameAttribute)} on the {clrType.Name} CLR {GetClrTypeKind(clrType)} is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotGetOrCreateTypeBuilderWithInvalidName(string name, TypeKind kind)
                =>
                    $"Cannot get or create GraphQL type builder for {kind.ToDisplayStringLower()} named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotCreateField(string name, FieldDefinition field)
                =>
                    $"Cannot create field named \"{name}\" on {field.DeclaringType}: \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";


            public static string CannotGetOrCreateBuilderForClrTypeWithInvalidName(Type clrType, TypeKind kind)
                =>
                    $"Cannot get or create GraphQL {kind.ToDisplayStringLower()} type builder with CLR {GetClrTypeKind(clrType)} '{clrType.Name}'. The CLR {GetClrTypeKind(clrType)} name '{clrType.Name}' is not a valid GraphQL name. {NameSpecDescription}";


            public static string CannotCreateArgumentWithInvalidName(IArgumentDefinition def, string name)
                =>
                    $"Cannot create argument named \"{name}\" for {def.DeclaringMember}: \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotCreateInputFieldWithInvalidName(IInputFieldDefinition def, string name)
                =>
                    $"Cannot create field named \"{name}\" for {def.DeclaringType}: \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotCreateDirectiveWithInvalidName(string name) =>
                $"Cannot create directive named \"{name}\": \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotCreateEnumValue(string name, IEnumTypeDefinition parent) =>
                $"Cannot add enum value \"{name}\" to {parent}: \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";


            public static string CannotRename(string name, string namedDescription) =>
                $"Cannot rename {namedDescription}: \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotRename(string name, INamed named) => CannotRename(name, named.ToString()!);

            public static string CannotRename(string name, INamed named, INamed parentDef) =>
                CannotRename(name, $"{named} on {parentDef}");

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