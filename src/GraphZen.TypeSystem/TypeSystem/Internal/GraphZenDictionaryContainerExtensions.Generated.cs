// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem.Internal
{
    

    public static class EnumTypeDefinitionValueAccessorExtensions
    {
        
        public static EnumValueDefinition FindValue( this EnumTypeDefinition enumTypeDefinition,
             string name)
            => enumTypeDefinition.Values.TryGetValue(Check.NotNull(name, nameof(name)), out var nameValue)
                ? nameValue
                : null;

        public static bool HasValue( this EnumTypeDefinition enumTypeDefinition,  string name)
            => enumTypeDefinition.Values.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static EnumValueDefinition GetValue( this EnumTypeDefinition enumTypeDefinition,
             string name)
            => enumTypeDefinition.FindValue(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{enumTypeDefinition} does not contain a value named '{name}'.");

        public static bool TryGetValue( this EnumTypeDefinition enumTypeDefinition,  string name,
            out EnumValueDefinition enumValueDefinition)
            => enumTypeDefinition.Values.TryGetValue(Check.NotNull(name, nameof(name)), out enumValueDefinition);
    }


    public static partial class EnumTypeValueAccessorExtensions
    {
        
        public static EnumValue FindValue( this EnumType enumType,  string name)
            => enumType.Values.TryGetValue(Check.NotNull(name, nameof(name)), out var nameValue)
                ? nameValue
                : null;

        public static bool HasValue( this EnumType enumType,  string name)
            => enumType.Values.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static EnumValue GetValue( this EnumType enumType,  string name)
            => enumType.FindValue(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{enumType} does not contain a value named '{name}'.");

        public static bool TryGetValue( this EnumType enumType,  string name, out EnumValue enumValue)
            => enumType.Values.TryGetValue(Check.NotNull(name, nameof(name)), out enumValue);
    }


    public static partial class EnumTypeValueAccessorExtensions
    {
        
        public static EnumValue FindValue( this EnumType enumType,  object value)
            => enumType.ValuesByValue.TryGetValue(Check.NotNull(value, nameof(value)), out var valueValue)
                ? valueValue
                : null;

        public static bool HasValue( this EnumType enumType,  object value)
            => enumType.ValuesByValue.ContainsKey(Check.NotNull(value, nameof(value)));

        
        public static EnumValue GetValue( this EnumType enumType,  object value)
            => enumType.FindValue(Check.NotNull(value, nameof(value))) ??
               throw new Exception($"{enumType} does not contain a value named '{value}'.");

        public static bool TryGetValue( this EnumType enumType,  object value,
            out EnumValue enumValue)
            => enumType.ValuesByValue.TryGetValue(Check.NotNull(value, nameof(value)), out enumValue);
    }


    public static class MutableArgumentsContainerDefinitionArgumentAccessorExtensions
    {
        
        public static ArgumentDefinition FindArgument( this IMutableArgumentsContainerDefinition arguments,
             string name)
            => arguments.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument( this IMutableArgumentsContainerDefinition arguments,
             string name)
            => arguments.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static ArgumentDefinition GetArgument( this IMutableArgumentsContainerDefinition arguments,
             string name)
            => arguments.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{arguments} does not contain a argument named '{name}'.");

        public static bool TryGetArgument( this IMutableArgumentsContainerDefinition arguments,
             string name, out ArgumentDefinition argumentDefinition)
            => arguments.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);
    }


    public static class FieldDefinitionArgumentAccessorExtensions
    {
        
        public static ArgumentDefinition FindArgument( this FieldDefinition fieldDefinition,
             string name)
            => fieldDefinition.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument( this FieldDefinition fieldDefinition,  string name)
            => fieldDefinition.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static ArgumentDefinition GetArgument( this FieldDefinition fieldDefinition,
             string name)
            => fieldDefinition.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{fieldDefinition} does not contain a argument named '{name}'.");

        public static bool TryGetArgument( this FieldDefinition fieldDefinition,  string name,
            out ArgumentDefinition argumentDefinition)
            => fieldDefinition.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);
    }


    public static class DirectiveDefinitionArgumentAccessorExtensions
    {
        
        public static ArgumentDefinition FindArgument( this DirectiveDefinition directiveDefinition,
             string name)
            => directiveDefinition.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument( this DirectiveDefinition directiveDefinition,  string name)
            => directiveDefinition.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static ArgumentDefinition GetArgument( this DirectiveDefinition directiveDefinition,
             string name)
            => directiveDefinition.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{directiveDefinition} does not contain a argument named '{name}'.");

        public static bool TryGetArgument( this DirectiveDefinition directiveDefinition,  string name,
            out ArgumentDefinition argumentDefinition)
            => directiveDefinition.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argumentDefinition);
    }


    public static class ArgumentsContainerArgumentAccessorExtensions
    {
        
        public static Argument FindArgument( this IArgumentsContainer arguments,  string name)
            => arguments.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument( this IArgumentsContainer arguments,  string name)
            => arguments.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static Argument GetArgument( this IArgumentsContainer arguments,  string name)
            => arguments.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{arguments} does not contain a argument named '{name}'.");

        public static bool TryGetArgument( this IArgumentsContainer arguments,  string name,
            out Argument argument)
            => arguments.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);
    }


    public static class FieldArgumentAccessorExtensions
    {
        
        public static Argument FindArgument( this Field field,  string name)
            => field.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument( this Field field,  string name)
            => field.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static Argument GetArgument( this Field field,  string name)
            => field.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{field} does not contain a argument named '{name}'.");

        public static bool TryGetArgument( this Field field,  string name, out Argument argument)
            => field.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);
    }


    public static class DirectiveArgumentAccessorExtensions
    {
        
        public static Argument FindArgument( this Directive directive,  string name)
            => directive.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var nameArgument)
                ? nameArgument
                : null;

        public static bool HasArgument( this Directive directive,  string name)
            => directive.Arguments.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static Argument GetArgument( this Directive directive,  string name)
            => directive.FindArgument(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{directive} does not contain a argument named '{name}'.");

        public static bool TryGetArgument( this Directive directive,  string name,
            out Argument argument)
            => directive.Arguments.TryGetValue(Check.NotNull(name, nameof(name)), out argument);
    }


    public static class MutableInputObjectTypeDefinitionFieldAccessorExtensions
    {
        
        public static InputFieldDefinition FindField(
             this IMutableInputObjectTypeDefinition inputObjectDefinition,  string name)
            => inputObjectDefinition.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField)
                ? nameField
                : null;

        public static bool HasField( this IMutableInputObjectTypeDefinition inputObjectDefinition,
             string name)
            => inputObjectDefinition.Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static InputFieldDefinition GetField(
             this IMutableInputObjectTypeDefinition inputObjectDefinition,  string name)
            => inputObjectDefinition.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{inputObjectDefinition} does not contain a field named '{name}'.");

        public static bool TryGetField( this IMutableInputObjectTypeDefinition inputObjectDefinition,
             string name, out InputFieldDefinition inputFieldDefinition)
            => inputObjectDefinition.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out inputFieldDefinition);
    }


    public static class InputObjectTypeFieldAccessorExtensions
    {
        
        public static InputField FindField( this IInputObjectType inputObject,  string name)
            => inputObject.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out var nameField) ? nameField : null;

        public static bool HasField( this IInputObjectType inputObject,  string name)
            => inputObject.Fields.ContainsKey(Check.NotNull(name, nameof(name)));

        
        public static InputField GetField( this IInputObjectType inputObject,  string name)
            => inputObject.FindField(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{inputObject} does not contain a field named '{name}'.");

        public static bool TryGetField( this IInputObjectType inputObject,  string name,
            out InputField inputField)
            => inputObject.Fields.TryGetValue(Check.NotNull(name, nameof(name)), out inputField);
    }
}