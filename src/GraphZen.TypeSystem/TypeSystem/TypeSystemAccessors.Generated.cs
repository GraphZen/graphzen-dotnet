// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable restore

namespace GraphZen.TypeSystem
{
    public static class InterfaceTypeDefinitionFieldAccessorExtensions
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


    public static class ObjectTypeDefinitionFieldAccessorExtensions
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


    public static class InputObjectTypeDefinitionFieldAccessorExtensions
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


    public static class FieldsContainerDefinitionFieldAccessorExtensions
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


    public static class InterfaceTypeFieldAccessorExtensions
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


    public static class ObjectTypeFieldAccessorExtensions
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


    public static class InputObjectTypeFieldAccessorExtensions
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
}