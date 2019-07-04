// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;


namespace GraphZen.LanguageModel
{
    public static class SyntaxFactory
    {
        [NotNull]
        private static NullValueSyntax NullValueInstance { get; } = new NullValueSyntax();

        [NotNull]
        private static BooleanValueSyntax TrueBooleanValueInstance { get; } = new BooleanValueSyntax(true);

        [NotNull]
        private static BooleanValueSyntax FalseBooleanValueInstance { get; } = new BooleanValueSyntax(false);

        [NotNull]
        [DebuggerStepThrough]
        public static NullValueSyntax NullValue() => NullValueInstance;

        [DebuggerStepThrough]
        public static SchemaDefinitionSyntax SchemaDefinition() =>
            new SchemaDefinitionSyntax(OperationTypeDefinitionSyntax.EmptyList);

        [NotNull]
        [DebuggerStepThrough]
        public static OperationTypeDefinitionSyntax OperationTypeDefinition(OperationType operationType,
            NamedTypeSyntax type) => new OperationTypeDefinitionSyntax(operationType, type);

        [NotNull]
        [DebuggerStepThrough]
        public static NamedTypeSyntax NamedType(NameSyntax name) => new NamedTypeSyntax(name);

        [NotNull]
        [DebuggerStepThrough]
        public static NamedTypeSyntax NamedType(Type clrType) =>
            new NamedTypeSyntax(Name(Check.NotNull(clrType, nameof(clrType)).GetGraphQLName()));


        [NotNull]
        [DebuggerStepThrough]
        public static ObjectFieldSyntax ObjectField(NameSyntax name, ValueSyntax value) =>
            new ObjectFieldSyntax(name, value);

        [NotNull]
        [DebuggerStepThrough]
        public static EnumValueSyntax EnumValue(NameSyntax name) => new EnumValueSyntax(name);

        [NotNull]
        [DebuggerStepThrough]
        public static EnumValueDefinitionSyntax EnumValueDefinition(EnumValueSyntax enumValue) =>
            new EnumValueDefinitionSyntax(enumValue);

        [NotNull]
        [DebuggerStepThrough]
        public static ListValueSyntax ListValue(params ValueSyntax[] values) => new ListValueSyntax(values);

        [NotNull]
        [DebuggerStepThrough]
        public static ListValueSyntax ListValue(IReadOnlyList<ValueSyntax> values) => new ListValueSyntax(values);


        [NotNull]
        [DebuggerStepThrough]
        public static ObjectValueSyntax ObjectValue(params ObjectFieldSyntax[] fields) =>
            new ObjectValueSyntax(fields);

        [NotNull]
        [DebuggerStepThrough]
        public static ObjectValueSyntax ObjectValue(IReadOnlyList<ObjectFieldSyntax> fields) =>
            new ObjectValueSyntax(fields);

        [NotNull]
        [DebuggerStepThrough]
        public static BooleanValueSyntax BooleanValue(bool value) =>
            value ? TrueBooleanValueInstance : FalseBooleanValueInstance;

        [NotNull]
        [DebuggerStepThrough]
        public static StringValueSyntax StringValue(string value, bool isBlockString = false) =>
            new StringValueSyntax(value, isBlockString);

        [NotNull]
        [DebuggerStepThrough]
        public static IntValueSyntax IntValue(int value) => new IntValueSyntax(value);

        [NotNull]
        [DebuggerStepThrough]
        public static FloatValueSyntax FloatValue(string value) => new FloatValueSyntax(value);

        [NotNull]
        [DebuggerStepThrough]
        public static FragmentSpreadSyntax FragmentSpread(NameSyntax name) => new FragmentSpreadSyntax(name);

        [NotNull]
        [DebuggerStepThrough]
        public static InputObjectTypeDefinitionSyntax InputObjectTypeDefinition(NameSyntax name) =>
            new InputObjectTypeDefinitionSyntax(name);

        [NotNull]
        [DebuggerStepThrough]
        public static ListTypeSyntax ListType(TypeSyntax type) => new ListTypeSyntax(type);

        [NotNull]
        [DebuggerStepThrough]
        public static ObjectTypeDefinitionSyntax ObjectTypeDefinition(NameSyntax name) =>
            new ObjectTypeDefinitionSyntax(name);

        [NotNull]
        [DebuggerStepThrough]
        public static InterfaceTypeDefinitionSyntax InterfaceTypeDefinition(NameSyntax name) =>
            new InterfaceTypeDefinitionSyntax(name);

        [NotNull]
        [DebuggerStepThrough]
        public static DocumentSyntax Document(params DefinitionSyntax[] definitions) => new DocumentSyntax(definitions);

        [NotNull]
        [DebuggerStepThrough]
        public static FieldDefinitionSyntax FieldDefinition(NameSyntax name, TypeSyntax type) =>
            new FieldDefinitionSyntax(name, type);

        [NotNull]
        [DebuggerStepThrough]
        public static InputObjectTypeExtensionSyntax InputObjectTypeExtension(NameSyntax name) =>
            new InputObjectTypeExtensionSyntax(name);


        [NotNull]
        [DebuggerStepThrough]
        public static ArgumentSyntax Argument(NameSyntax name, ValueSyntax node) =>
            new ArgumentSyntax(name, null, node);

        [NotNull]
        [DebuggerStepThrough]
        public static VariableSyntax Variable(NameSyntax name) => new VariableSyntax(name);

        [NotNull]
        [DebuggerStepThrough]
        public static VariableDefinitionSyntax VariableDefinition(VariableSyntax variable, TypeSyntax type,
            ValueSyntax defaultValue = null) =>
            new VariableDefinitionSyntax(variable, type, defaultValue);

        [NotNull]
        [DebuggerStepThrough]
        public static NameSyntax Name(string name) => new NameSyntax(name);

        [NotNull]
        [DebuggerStepThrough]
        public static NameSyntax[] Names(params string[] names) =>
            Check.NotNull(names, nameof(names)).Select(Name).ToArray();

        [NotNull]
        [DebuggerStepThrough]
        public static DirectiveSyntax Directive(NameSyntax name) => new DirectiveSyntax(name);

        [NotNull]
        [DebuggerStepThrough]
        public static InputValueDefinitionSyntax InputValueDefinition(NameSyntax name, TypeSyntax type) =>
            new InputValueDefinitionSyntax(name, type);

        [NotNull]
        [DebuggerStepThrough]
        public static EnumTypeDefinitionSyntax EnumTypeDefinition(NameSyntax name) =>
            new EnumTypeDefinitionSyntax(name);


        [NotNull]
        [DebuggerStepThrough]
        public static NonNullTypeSyntax NonNull(NullableTypeSyntax node) => new NonNullTypeSyntax(node);

        [NotNull]
        [DebuggerStepThrough]
        public static FieldSyntax Field(NameSyntax name) => new FieldSyntax(name);


        [NotNull]
        [DebuggerStepThrough]
        public static UnionTypeDefinitionSyntax UnionTypeDefinition(NameSyntax name) =>
            new UnionTypeDefinitionSyntax(name);

        [NotNull]
        [DebuggerStepThrough]
        public static ScalarTypeDefinitionSyntax ScalarTypeDefinition(NameSyntax name) =>
            new ScalarTypeDefinitionSyntax(name);


        [NotNull]
        [DebuggerStepThrough]
        public static SelectionSetSyntax SelectionSet(params SelectionSyntax[] selections) =>
            new SelectionSetSyntax(selections);
    }
}