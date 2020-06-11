// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using static GraphZen.Infrastructure.GraphQLName;

namespace GraphZen.Internal
{
    internal static class TypeSystemExceptions
    {
        public static string GetClrTypeKind(this Type clrType) => clrType.IsInterface ? "interface" :
            clrType.IsClass ? "class" :
            clrType.IsEnum ? "enum" : "type";


        public static class DuplicateItemException
        {
            public static Infrastructure.DuplicateItemException ForRename(IMutableDefinition definition, string name)
            {
                var parent = definition.GetParentMember();
                var parentDescription = parent is SchemaDefinition ? "The schema" : parent?.ToString()?.FirstCharToUpper();
                return new Infrastructure.DuplicateItemException(
                    $"Cannot rename {definition} to \"{name}\". {parentDescription} already contains a {definition.GetTypeDisplayName()} named \"{name}\".");
            }

            public static string
                CannotChangeClrType<T>(T definition, Type clrType, IMutableClrType existing)
                where T : INamed, IClrType =>
                $"Cannot set CLR type on {definition} to CLR {GetClrTypeKind(clrType)} '{clrType.Name}': {existing} already exists with that CLR type.";


            internal static string CannotCreateTypeWithDuplicateNameAndType<T>(TypeKind kind, string name, Type clrType,
                T named, T typed)
                where T : NamedTypeDefinition =>
                $"Cannot create {kind.ToDisplayStringLower()} {name} with CLR {clrType.GetClrTypeKind()} '{clrType.Name}': both {named} and {typed} already exist.";



            internal static string CannotCreateDirectiveWithConflictingNameAndType(string name, Type clrType,
                IDirectiveDefinition existingNamed, IDirectiveDefinition existingTyped) =>
                $"Cannot create directive {name} with CLR type '{clrType.Name}': both {existingNamed} and {existingTyped} already exist.";


            internal static string CannotRenameInputField(IInputFieldDefinition field, string name) =>
                $"Cannot rename {field} to \"{name}\". {field.DeclaringType?.ToString()?.FirstCharToUpper()} already contains a field named \"{name}\".";


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


            public static string CannotCreateArgumentWithInvalidName(IMutableArgumentDefinition def, string name)
                =>
                    $"Cannot create argument named \"{name}\" for {def.DeclaringMember}: \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotCreateInputFieldWithInvalidName(IInputFieldDefinition def, string name)
                =>
                    $"Cannot create field named \"{name}\" for {def.DeclaringType}: \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotCreateDirectiveWithInvalidName(string name) =>
                $"Cannot create directive named \"{name}\": \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";

            public static string CannotCreateEnumValue(string name, IEnumTypeDefinition parent) =>
                $"Cannot add enum value \"{name}\" to {parent}: \"{name}\" is not a valid GraphQL name. {NameSpecDescription}";
        }
    }
}