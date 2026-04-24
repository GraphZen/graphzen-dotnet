// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;

namespace GraphZen.LanguageModel;

public static class SyntaxFactory
{
    private static NullValueSyntax NullValueInstance { get; } = new();


    private static BooleanValueSyntax TrueBooleanValueInstance { get; } = new(true);


    private static BooleanValueSyntax FalseBooleanValueInstance { get; } = new(false);


    [DebuggerStepThrough]
    public static NullValueSyntax NullValue() => NullValueInstance;

    [DebuggerStepThrough]
    public static SchemaDefinitionSyntax SchemaDefinition() =>
        new(OperationTypeDefinitionSyntax.EmptyList);


    [DebuggerStepThrough]
    public static OperationTypeDefinitionSyntax OperationTypeDefinition(OperationType operationType,
        NamedTypeSyntax type) =>
        new(operationType, type);


    [DebuggerStepThrough]
    public static NamedTypeSyntax NamedType(NameSyntax name) => new(name);


    [DebuggerStepThrough]
    public static NamedTypeSyntax NamedType(Type clrType) =>
        new(Name(Check.NotNull(clrType, nameof(clrType)).GetGraphQLName()));


    [DebuggerStepThrough]
    public static ObjectFieldSyntax ObjectField(NameSyntax name, ValueSyntax value) =>
        new(name, value);


    [DebuggerStepThrough]
    public static EnumValueSyntax EnumValue(NameSyntax name) => new(name);


    [DebuggerStepThrough]
    public static EnumValueDefinitionSyntax EnumValueDefinition(EnumValueSyntax enumValue) =>
        new(enumValue);


    [DebuggerStepThrough]
    public static ListValueSyntax ListValue(params ValueSyntax[] values) => new(values);


    [DebuggerStepThrough]
    public static ListValueSyntax ListValue(IReadOnlyList<ValueSyntax> values) => new(values);


    [DebuggerStepThrough]
    public static ObjectValueSyntax ObjectValue(params ObjectFieldSyntax[] fields) => new(fields);


    [DebuggerStepThrough]
    public static ObjectValueSyntax ObjectValue(IReadOnlyList<ObjectFieldSyntax> fields) =>
        new(fields);


    [DebuggerStepThrough]
    public static BooleanValueSyntax BooleanValue(bool value) =>
        value ? TrueBooleanValueInstance : FalseBooleanValueInstance;


    [DebuggerStepThrough]
    public static StringValueSyntax StringValue(string value, bool isBlockString = false) =>
        new(value, isBlockString);


    [DebuggerStepThrough]
    public static IntValueSyntax IntValue(int value) => new(value);


    [DebuggerStepThrough]
    public static FloatValueSyntax FloatValue(string value) => new(value);


    [DebuggerStepThrough]
    public static FragmentSpreadSyntax FragmentSpread(NameSyntax name) => new(name);


    [DebuggerStepThrough]
    public static InputObjectTypeDefinitionSyntax InputObjectTypeDefinition(NameSyntax name) =>
        new(name);


    [DebuggerStepThrough]
    public static ListTypeSyntax ListType(TypeSyntax type) => new(type);


    [DebuggerStepThrough]
    public static ObjectTypeDefinitionSyntax ObjectTypeDefinition(NameSyntax name) =>
        new(name);


    [DebuggerStepThrough]
    public static InterfaceTypeDefinitionSyntax InterfaceTypeDefinition(NameSyntax name) =>
        new(name);


    [DebuggerStepThrough]
    public static DocumentSyntax Document(params DefinitionSyntax[] definitions) => new(definitions);


    [DebuggerStepThrough]
    public static FieldDefinitionSyntax FieldDefinition(NameSyntax name, TypeSyntax type) =>
        new(name, type);


    [DebuggerStepThrough]
    public static InputObjectTypeExtensionSyntax InputObjectTypeExtension(NameSyntax name) =>
        new(name);


    [DebuggerStepThrough]
    public static ArgumentSyntax Argument(NameSyntax name, ValueSyntax node) =>
        new(name, null, node);


    [DebuggerStepThrough]
    public static VariableSyntax Variable(NameSyntax name) => new(name);


    [DebuggerStepThrough]
    public static VariableDefinitionSyntax VariableDefinition(VariableSyntax variable, TypeSyntax type,
        ValueSyntax? defaultValue = null) =>
        new(variable, type, defaultValue);


    [DebuggerStepThrough]
    public static NameSyntax Name(string name) => new(name);


    [DebuggerStepThrough]
    public static NameSyntax[] Names(params string[] names) =>
        Check.NotNull(names, nameof(names)).Select(Name).ToArray();


    [DebuggerStepThrough]
    public static DirectiveSyntax Directive(NameSyntax name) => new(name);


    [DebuggerStepThrough]
    public static InputValueDefinitionSyntax InputValueDefinition(NameSyntax name, TypeSyntax type) =>
        new(name, type);


    [DebuggerStepThrough]
    public static EnumTypeDefinitionSyntax EnumTypeDefinition(NameSyntax name) =>
        new(name);


    [DebuggerStepThrough]
    public static NonNullTypeSyntax NonNull(NullableTypeSyntax node) => new(node);


    [DebuggerStepThrough]
    public static FieldSyntax Field(NameSyntax name) => new(name);


    [DebuggerStepThrough]
    public static UnionTypeDefinitionSyntax UnionTypeDefinition(NameSyntax name) =>
        new(name);


    [DebuggerStepThrough]
    public static ScalarTypeDefinitionSyntax ScalarTypeDefinition(NameSyntax name,
        StringValueSyntax? description = null) =>
        new(name, description);


    [DebuggerStepThrough]
    public static SelectionSetSyntax SelectionSet(params SelectionSyntax[] selections) =>
        new(selections);
}