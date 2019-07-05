// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem.Internal
{
    public static class MutableFieldsContainerDefinitionFieldAccessorExtensions
    {
        [CanBeNull]
        public static FieldDefinition FindField([NotNull] this IMutableFieldsContainerDefinition fields,
            [NotNull] string name)
            => fields.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField) ? nameField : null;

        public static bool HasField([NotNull] this IMutableFieldsContainerDefinition fields, [NotNull] string name)
            => fields.Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static FieldDefinition GetField([NotNull] this IMutableFieldsContainerDefinition fields,
            [NotNull] string name)
            => fields.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{fields} does not contain a field named '{name}'.");

        public static bool TryGetField([NotNull] this IMutableFieldsContainerDefinition fields, [NotNull] string name,
            out FieldDefinition fieldDefinition)
            => fields.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out fieldDefinition);
    }


    public static class FieldsContainerDefinitionFieldAccessorExtensions
    {
        [CanBeNull]
        public static FieldDefinition FindField([NotNull] this FieldsContainerDefinition fieldsContainerDefinition,
            [NotNull] string name)
            => fieldsContainerDefinition.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField)
                ? nameField
                : null;

        public static bool HasField([NotNull] this FieldsContainerDefinition fieldsContainerDefinition,
            [NotNull] string name)
            => fieldsContainerDefinition.Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static FieldDefinition GetField([NotNull] this FieldsContainerDefinition fieldsContainerDefinition,
            [NotNull] string name)
            => fieldsContainerDefinition.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{fieldsContainerDefinition} does not contain a field named '{name}'.");

        public static bool TryGetField([NotNull] this FieldsContainerDefinition fieldsContainerDefinition,
            [NotNull] string name, out FieldDefinition fieldDefinition)
            => fieldsContainerDefinition.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out fieldDefinition);
    }


    public static class InterfaceTypeDefinitionFieldAccessorExtensions
    {
        [CanBeNull]
        public static FieldDefinition FindField([NotNull] this InterfaceTypeDefinition interfaceTypeDefinition,
            [NotNull] string name)
            => interfaceTypeDefinition.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField)
                ? nameField
                : null;

        public static bool HasField([NotNull] this InterfaceTypeDefinition interfaceTypeDefinition,
            [NotNull] string name)
            => interfaceTypeDefinition.Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static FieldDefinition GetField([NotNull] this InterfaceTypeDefinition interfaceTypeDefinition,
            [NotNull] string name)
            => interfaceTypeDefinition.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{interfaceTypeDefinition} does not contain a field named '{name}'.");

        public static bool TryGetField([NotNull] this InterfaceTypeDefinition interfaceTypeDefinition,
            [NotNull] string name, out FieldDefinition fieldDefinition)
            => interfaceTypeDefinition.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out fieldDefinition);
    }


    public static class ObjectTypeDefinitionFieldAccessorExtensions
    {
        [CanBeNull]
        public static FieldDefinition FindField([NotNull] this ObjectTypeDefinition objectTypeDefinition,
            [NotNull] string name)
            => objectTypeDefinition.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField)
                ? nameField
                : null;

        public static bool HasField([NotNull] this ObjectTypeDefinition objectTypeDefinition, [NotNull] string name)
            => objectTypeDefinition.Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static FieldDefinition GetField([NotNull] this ObjectTypeDefinition objectTypeDefinition,
            [NotNull] string name)
            => objectTypeDefinition.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{objectTypeDefinition} does not contain a field named '{name}'.");

        public static bool TryGetField([NotNull] this ObjectTypeDefinition objectTypeDefinition, [NotNull] string name,
            out FieldDefinition fieldDefinition)
            => objectTypeDefinition.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out fieldDefinition);
    }


    public static class FieldsContainerFieldAccessorExtensions
    {
        [CanBeNull]
        public static Field FindField([NotNull] this IFieldsContainer fields, [NotNull] string name)
            => fields.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField) ? nameField : null;

        public static bool HasField([NotNull] this IFieldsContainer fields, [NotNull] string name)
            => fields.Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static Field GetField([NotNull] this IFieldsContainer fields, [NotNull] string name)
            => fields.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{fields} does not contain a field named '{name}'.");

        public static bool TryGetField([NotNull] this IFieldsContainer fields, [NotNull] string name, out Field field)
            => fields.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out field);
    }


    public static class ObjectTypeFieldAccessorExtensions
    {
        [CanBeNull]
        public static Field FindField([NotNull] this ObjectType objectType, [NotNull] string name)
            => objectType.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField) ? nameField : null;

        public static bool HasField([NotNull] this ObjectType objectType, [NotNull] string name)
            => objectType.Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static Field GetField([NotNull] this ObjectType objectType, [NotNull] string name)
            => objectType.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{objectType} does not contain a field named '{name}'.");

        public static bool TryGetField([NotNull] this ObjectType objectType, [NotNull] string name, out Field field)
            => objectType.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out field);
    }


    public static class InterfaceTypeFieldAccessorExtensions
    {
        [CanBeNull]
        public static Field FindField([NotNull] this InterfaceType interfaceType, [NotNull] string name)
            => interfaceType.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField)
                ? nameField
                : null;

        public static bool HasField([NotNull] this InterfaceType interfaceType, [NotNull] string name)
            => interfaceType.Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static Field GetField([NotNull] this InterfaceType interfaceType, [NotNull] string name)
            => interfaceType.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{interfaceType} does not contain a field named '{name}'.");

        public static bool TryGetField([NotNull] this InterfaceType interfaceType, [NotNull] string name,
            out Field field)
            => interfaceType.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out field);
    }


    public static class EnumTypeDefinitionValueAccessorExtensions
    {
        [CanBeNull]
        public static EnumValueDefinition FindValue([NotNull] this EnumTypeDefinition enumTypeDefinition,
            [NotNull] string name)
            => enumTypeDefinition.ValuesByName.TryGetValue(Check.NotNull(name, nameof(name)), out var nameValue)
                ? nameValue
                : null;

        public static bool HasValue([NotNull] this EnumTypeDefinition enumTypeDefinition, [NotNull] string name)
            => enumTypeDefinition.ValuesByName.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static EnumValueDefinition GetValue([NotNull] this EnumTypeDefinition enumTypeDefinition,
            [NotNull] string name)
            => enumTypeDefinition.FindValue(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{enumTypeDefinition} does not contain a value named '{name}'.");

        public static bool TryGetValue([NotNull] this EnumTypeDefinition enumTypeDefinition, [NotNull] string name,
            out EnumValueDefinition enumValueDefinition)
            => enumTypeDefinition.ValuesByName.TryGetValue(Check.NotNull(name, nameof(name)), out enumValueDefinition);
    }


    public static partial class EnumTypeValueAccessorExtensions
    {
        [CanBeNull]
        public static EnumValue FindValue([NotNull] this EnumType enumType, [NotNull] string name)
            => enumType.ValuesByName.TryGetValue(Check.NotNull(name, nameof(name)), out var nameValue)
                ? nameValue
                : null;

        public static bool HasValue([NotNull] this EnumType enumType, [NotNull] string name)
            => enumType.ValuesByName.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static EnumValue GetValue([NotNull] this EnumType enumType, [NotNull] string name)
            => enumType.FindValue(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{enumType} does not contain a value named '{name}'.");

        public static bool TryGetValue([NotNull] this EnumType enumType, [NotNull] string name, out EnumValue enumValue)
            => enumType.ValuesByName.TryGetValue(Check.NotNull(name, nameof(name)), out enumValue);
    }


    public static partial class EnumTypeValueAccessorExtensions
    {
        [CanBeNull]
        public static EnumValue FindValue([NotNull] this EnumType enumType, [NotNull] object value)
            => enumType.ValuesByValue.TryGetValue(Check.NotNull(value, nameof(value)), out var valueValue)
                ? valueValue
                : null;

        public static bool HasValue([NotNull] this EnumType enumType, [NotNull] object value)
            => enumType.ValuesByValue.ContainsKey(Check.NotNull(value, nameof(value)));

        [NotNull]
        public static EnumValue GetValue([NotNull] this EnumType enumType, [NotNull] object value)
            => enumType.FindValue(Check.NotNull(value, nameof(value))) ??
               throw new Exception($"{enumType} does not contain a value named '{value}'.");

        public static bool TryGetValue([NotNull] this EnumType enumType, [NotNull] object value,
            out EnumValue enumValue)
            => enumType.ValuesByValue.TryGetValue(Check.NotNull(value, nameof(value)), out enumValue);
    }


    public static class MutableArgumentsContainerDefinitionArgumentAccessorExtensions
    {
        [CanBeNull]
        public static ArgumentDefinition FindArgument([NotNull] this IMutableArgumentsContainerDefinition arguments,
            [NotNull] string name)
            => arguments.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument([NotNull] this IMutableArgumentsContainerDefinition arguments,
            [NotNull] string name)
            => arguments.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static ArgumentDefinition GetArgument([NotNull] this IMutableArgumentsContainerDefinition arguments,
            [NotNull] string name)
            => arguments.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{arguments} does not contain a argument named '{name}'.");

        public static bool TryGetArgument([NotNull] this IMutableArgumentsContainerDefinition arguments,
            [NotNull] string name, out ArgumentDefinition argumentDefinition)
            => arguments.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);
    }


    public static class FieldDefinitionArgumentAccessorExtensions
    {
        [CanBeNull]
        public static ArgumentDefinition FindArgument([NotNull] this FieldDefinition fieldDefinition,
            [NotNull] string name)
            => fieldDefinition.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument([NotNull] this FieldDefinition fieldDefinition, [NotNull] string name)
            => fieldDefinition.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static ArgumentDefinition GetArgument([NotNull] this FieldDefinition fieldDefinition,
            [NotNull] string name)
            => fieldDefinition.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{fieldDefinition} does not contain a argument named '{name}'.");

        public static bool TryGetArgument([NotNull] this FieldDefinition fieldDefinition, [NotNull] string name,
            out ArgumentDefinition argumentDefinition)
            => fieldDefinition.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);
    }


    public static class DirectiveDefinitionArgumentAccessorExtensions
    {
        [CanBeNull]
        public static ArgumentDefinition FindArgument([NotNull] this DirectiveDefinition directiveDefinition,
            [NotNull] string name)
            => directiveDefinition.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument([NotNull] this DirectiveDefinition directiveDefinition, [NotNull] string name)
            => directiveDefinition.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static ArgumentDefinition GetArgument([NotNull] this DirectiveDefinition directiveDefinition,
            [NotNull] string name)
            => directiveDefinition.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{directiveDefinition} does not contain a argument named '{name}'.");

        public static bool TryGetArgument([NotNull] this DirectiveDefinition directiveDefinition, [NotNull] string name,
            out ArgumentDefinition argumentDefinition)
            => directiveDefinition.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);
    }


    public static class ArgumentsContainerArgumentAccessorExtensions
    {
        [CanBeNull]
        public static Argument FindArgument([NotNull] this IArgumentsContainer arguments, [NotNull] string name)
            => arguments.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument([NotNull] this IArgumentsContainer arguments, [NotNull] string name)
            => arguments.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static Argument GetArgument([NotNull] this IArgumentsContainer arguments, [NotNull] string name)
            => arguments.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{arguments} does not contain a argument named '{name}'.");

        public static bool TryGetArgument([NotNull] this IArgumentsContainer arguments, [NotNull] string name,
            out Argument argument)
            => arguments.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);
    }


    public static class FieldArgumentAccessorExtensions
    {
        [CanBeNull]
        public static Argument FindArgument([NotNull] this Field field, [NotNull] string name)
            => field.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument([NotNull] this Field field, [NotNull] string name)
            => field.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static Argument GetArgument([NotNull] this Field field, [NotNull] string name)
            => field.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{field} does not contain a argument named '{name}'.");

        public static bool TryGetArgument([NotNull] this Field field, [NotNull] string name, out Argument argument)
            => field.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);
    }


    public static class DirectiveArgumentAccessorExtensions
    {
        [CanBeNull]
        public static Argument FindArgument([NotNull] this Directive directive, [NotNull] string name)
            => directive.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument([NotNull] this Directive directive, [NotNull] string name)
            => directive.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static Argument GetArgument([NotNull] this Directive directive, [NotNull] string name)
            => directive.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{directive} does not contain a argument named '{name}'.");

        public static bool TryGetArgument([NotNull] this Directive directive, [NotNull] string name,
            out Argument argument)
            => directive.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);
    }


    public static class MutableInputObjectTypeDefinitionFieldAccessorExtensions
    {
        [CanBeNull]
        public static InputFieldDefinition FindField(
            [NotNull] this IMutableInputObjectTypeDefinition inputObjectDefinition, [NotNull] string name)
            => inputObjectDefinition.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField)
                ? nameField
                : null;

        public static bool HasField([NotNull] this IMutableInputObjectTypeDefinition inputObjectDefinition,
            [NotNull] string name)
            => inputObjectDefinition.Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static InputFieldDefinition GetField(
            [NotNull] this IMutableInputObjectTypeDefinition inputObjectDefinition, [NotNull] string name)
            => inputObjectDefinition.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{inputObjectDefinition} does not contain a field named '{name}'.");

        public static bool TryGetField([NotNull] this IMutableInputObjectTypeDefinition inputObjectDefinition,
            [NotNull] string name, out InputFieldDefinition inputFieldDefinition)
            => inputObjectDefinition.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out inputFieldDefinition);
    }


    public static class InputObjectTypeFieldAccessorExtensions
    {
        [CanBeNull]
        public static InputField FindField([NotNull] this IInputObjectType inputObject, [NotNull] string name)
            => inputObject.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField) ? nameField : null;

        public static bool HasField([NotNull] this IInputObjectType inputObject, [NotNull] string name)
            => inputObject.Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        [NotNull]
        public static InputField GetField([NotNull] this IInputObjectType inputObject, [NotNull] string name)
            => inputObject.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{inputObject} does not contain a field named '{name}'.");

        public static bool TryGetField([NotNull] this IInputObjectType inputObject, [NotNull] string name,
            out InputField inputField)
            => inputObject.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out inputField);
    }
}