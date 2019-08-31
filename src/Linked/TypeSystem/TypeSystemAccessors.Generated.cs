// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global
// ReSharper disable PartialTypeWithSinglePart

#nullable restore

namespace GraphZen.TypeSystem
{
    public static partial class InterfaceTypeDefinitionFieldsAccessorExtensions
    {
        public static FieldDefinition? FindField(this InterfaceTypeDefinition source, string name)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField) ? nameField : null;

        public static bool HasField(this InterfaceTypeDefinition source, string name)
            => source.Fields.ContainsKey(Check.NotNull(name, nameof(name)));


        public static FieldDefinition GetField(this InterfaceTypeDefinition source, string name)
            => source.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a field named '{name}'.");

        public static bool TryGetField(this InterfaceTypeDefinition source, string name,
            [NotNullWhen(true)] out FieldDefinition? fieldDefinition)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out fieldDefinition);
    }


    public static partial class ObjectTypeDefinitionFieldsAccessorExtensions
    {
        public static FieldDefinition? FindField(this ObjectTypeDefinition source, string name)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField) ? nameField : null;

        public static bool HasField(this ObjectTypeDefinition source, string name)
            => source.Fields.ContainsKey(Check.NotNull(name, nameof(name)));


        public static FieldDefinition GetField(this ObjectTypeDefinition source, string name)
            => source.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a field named '{name}'.");

        public static bool TryGetField(this ObjectTypeDefinition source, string name,
            [NotNullWhen(true)] out FieldDefinition? fieldDefinition)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out fieldDefinition);
    }


    public static partial class InputObjectTypeDefinitionFieldsAccessorExtensions
    {
        public static InputFieldDefinition? FindField(this InputObjectTypeDefinition source, string name)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField) ? nameField : null;

        public static bool HasField(this InputObjectTypeDefinition source, string name)
            => source.Fields.ContainsKey(Check.NotNull(name, nameof(name)));


        public static InputFieldDefinition GetField(this InputObjectTypeDefinition source, string name)
            => source.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a field named '{name}'.");

        public static bool TryGetField(this InputObjectTypeDefinition source, string name,
            [NotNullWhen(true)] out InputFieldDefinition? inputFieldDefinition)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out inputFieldDefinition);
    }


    public static partial class FieldsContainerDefinitionFieldsAccessorExtensions
    {
        public static FieldDefinition? FindField(this FieldsContainerDefinition source, string name)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField) ? nameField : null;

        public static bool HasField(this FieldsContainerDefinition source, string name)
            => source.Fields.ContainsKey(Check.NotNull(name, nameof(name)));


        public static FieldDefinition GetField(this FieldsContainerDefinition source, string name)
            => source.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a field named '{name}'.");

        public static bool TryGetField(this FieldsContainerDefinition source, string name,
            [NotNullWhen(true)] out FieldDefinition? fieldDefinition)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out fieldDefinition);
    }


    public static partial class InterfaceTypeFieldsAccessorExtensions
    {
        public static Field? FindField(this InterfaceType source, string name)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField) ? nameField : null;

        public static bool HasField(this InterfaceType source, string name)
            => source.Fields.ContainsKey(Check.NotNull(name, nameof(name)));


        public static Field GetField(this InterfaceType source, string name)
            => source.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a field named '{name}'.");

        public static bool TryGetField(this InterfaceType source, string name, [NotNullWhen(true)] out Field? field)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out field);
    }


    public static partial class ObjectTypeFieldsAccessorExtensions
    {
        public static Field? FindField(this ObjectType source, string name)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField) ? nameField : null;

        public static bool HasField(this ObjectType source, string name)
            => source.Fields.ContainsKey(Check.NotNull(name, nameof(name)));


        public static Field GetField(this ObjectType source, string name)
            => source.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a field named '{name}'.");

        public static bool TryGetField(this ObjectType source, string name, [NotNullWhen(true)] out Field? field)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out field);
    }


    public static partial class InputObjectTypeFieldsAccessorExtensions
    {
        public static InputField? FindField(this InputObjectType source, string name)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField) ? nameField : null;

        public static bool HasField(this InputObjectType source, string name)
            => source.Fields.ContainsKey(Check.NotNull(name, nameof(name)));


        public static InputField GetField(this InputObjectType source, string name)
            => source.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a field named '{name}'.");

        public static bool TryGetField(this InputObjectType source, string name,
            [NotNullWhen(true)] out InputField? inputField)
            => source.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out inputField);
    }


    public static partial class EnumTypeDefinitionValuesAccessorExtensions
    {
        public static EnumValueDefinition? FindValue(this EnumTypeDefinition source, string name)
            => source.Values.TryGetValue(Check.NotNull(name, nameof(name)), out var nameValue) ? nameValue : null;

        public static bool HasValue(this EnumTypeDefinition source, string name)
            => source.Values.ContainsKey(Check.NotNull(name, nameof(name)));


        public static EnumValueDefinition GetValue(this EnumTypeDefinition source, string name)
            => source.FindValue(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a value named '{name}'.");

        public static bool TryGetValue(this EnumTypeDefinition source, string name,
            [NotNullWhen(true)] out EnumValueDefinition? enumValueDefinition)
            => source.Values.TryGetValue(Check.NotNull(name, nameof(name)), out enumValueDefinition);
    }


    public static partial class EnumTypeValuesAccessorExtensions
    {
        public static EnumValue? FindValue(this EnumType source, string name)
            => source.Values.TryGetValue(Check.NotNull(name, nameof(name)), out var nameValue) ? nameValue : null;

        public static bool HasValue(this EnumType source, string name)
            => source.Values.ContainsKey(Check.NotNull(name, nameof(name)));


        public static EnumValue GetValue(this EnumType source, string name)
            => source.FindValue(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a value named '{name}'.");

        public static bool TryGetValue(this EnumType source, string name, [NotNullWhen(true)] out EnumValue? enumValue)
            => source.Values.TryGetValue(Check.NotNull(name, nameof(name)), out enumValue);
    }


    public static partial class EnumTypeValuesByValueAccessorExtensions
    {
        public static EnumValue? FindValue(this EnumType source, object value)
            => source.ValuesByValue.TryGetValue(Check.NotNull(value, nameof(value)), out var valueValue)
                ? valueValue
                : null;

        public static bool HasValue(this EnumType source, object value)
            => source.ValuesByValue.ContainsKey(Check.NotNull(value, nameof(value)));


        public static EnumValue GetValue(this EnumType source, object value)
            => source.FindValue(Check.NotNull(value, nameof(value))) ??
               throw new Exception($"{source} does not contain a value named '{value}'.");

        public static bool TryGetValue(this EnumType source, object value, [NotNullWhen(true)] out EnumValue? enumValue)
            => source.ValuesByValue.TryGetValue(Check.NotNull(value, nameof(value)), out enumValue);
    }


    public static partial class FieldDefinitionArgumentsAccessorExtensions
    {
        public static ArgumentDefinition? FindArgument(this FieldDefinition source, string name)
            => source.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument(this FieldDefinition source, string name)
            => source.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));


        public static ArgumentDefinition GetArgument(this FieldDefinition source, string name)
            => source.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a argument named '{name}'.");

        public static bool TryGetArgument(this FieldDefinition source, string name,
            [NotNullWhen(true)] out ArgumentDefinition? argumentDefinition)
            => source.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);
    }


    public static partial class DirectiveDefinitionArgumentsAccessorExtensions
    {
        public static ArgumentDefinition? FindArgument(this DirectiveDefinition source, string name)
            => source.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument(this DirectiveDefinition source, string name)
            => source.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));


        public static ArgumentDefinition GetArgument(this DirectiveDefinition source, string name)
            => source.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a argument named '{name}'.");

        public static bool TryGetArgument(this DirectiveDefinition source, string name,
            [NotNullWhen(true)] out ArgumentDefinition? argumentDefinition)
            => source.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);
    }


    public static partial class FieldArgumentsAccessorExtensions
    {
        public static Argument? FindArgument(this Field source, string name)
            => source.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument(this Field source, string name)
            => source.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));


        public static Argument GetArgument(this Field source, string name)
            => source.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a argument named '{name}'.");

        public static bool TryGetArgument(this Field source, string name, [NotNullWhen(true)] out Argument? argument)
            => source.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);
    }


    public static partial class IArgumentsContainerArgumentsAccessorExtensions
    {
        public static Argument? FindArgument(this IArguments source, string name)
            => source.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument(this IArguments source, string name)
            => source.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));


        public static Argument GetArgument(this IArguments source, string name)
            => source.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{source} does not contain a argument named '{name}'.");

        public static bool TryGetArgument(this IArguments source, string name,
            [NotNullWhen(true)] out Argument? argument)
            => source.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);
    }
}