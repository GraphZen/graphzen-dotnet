// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
{
    public static partial class SyntaxFactory
    {
        private static NullValueSyntax NullValueInstance { get; } = new NullValueSyntax();



        [DebuggerStepThrough]
        public static NullValueSyntax NullValue() => NullValueInstance;

        [DebuggerStepThrough]
        public static SchemaDefinitionSyntax SchemaDefinition() =>
            new SchemaDefinitionSyntax(OperationTypeDefinitionSyntax.EmptyList);


        [DebuggerStepThrough]
        public static OperationTypeDefinitionSyntax OperationTypeDefinition(OperationType operationType,
            NamedTypeSyntax type) =>
            new OperationTypeDefinitionSyntax(operationType, type);


        [DebuggerStepThrough]
        public static NamedTypeSyntax NamedType(NameSyntax name) => new NamedTypeSyntax(name);


        [DebuggerStepThrough]
        public static NamedTypeSyntax NamedType(Type clrType) =>
            new NamedTypeSyntax(Name(Check.NotNull(clrType, nameof(clrType)).GetGraphQLName()));


        [DebuggerStepThrough]
        public static ObjectFieldSyntax ObjectField(NameSyntax name, ValueSyntax value) =>
            new ObjectFieldSyntax(name, value);


        [DebuggerStepThrough]
        public static EnumValueSyntax EnumValue(NameSyntax name) => new EnumValueSyntax(name);


        [DebuggerStepThrough]
        public static EnumValueDefinitionSyntax EnumValueDefinition(EnumValueSyntax enumValue) =>
            new EnumValueDefinitionSyntax(enumValue);


        [DebuggerStepThrough]
        public static ListValueSyntax ListValue(params ValueSyntax[] values) => new ListValueSyntax(values);


        [DebuggerStepThrough]
        public static ListValueSyntax ListValue(IReadOnlyList<ValueSyntax> values) => new ListValueSyntax(values);


        [DebuggerStepThrough]
        public static ObjectValueSyntax ObjectValue(params ObjectFieldSyntax[] fields) => new ObjectValueSyntax(fields);


        [DebuggerStepThrough]
        public static ObjectValueSyntax ObjectValue(IReadOnlyList<ObjectFieldSyntax> fields) =>
            new ObjectValueSyntax(fields);


        [DebuggerStepThrough]
        public static BooleanValueSyntax BooleanValue(bool value) => BooleanValueSyntax.Create(value);



        [DebuggerStepThrough]
        public static StringValueSyntax StringValue(string value, bool isBlockString = false) =>
            new StringValueSyntax(value, isBlockString);


        [DebuggerStepThrough]
        public static IntValueSyntax IntValue(int value) => new IntValueSyntax(value);


        [DebuggerStepThrough]
        public static FloatValueSyntax FloatValue(string value) => new FloatValueSyntax(value);


        [DebuggerStepThrough]
        public static FragmentSpreadSyntax FragmentSpread(NameSyntax name) => new FragmentSpreadSyntax(name);


        [DebuggerStepThrough]
        public static InputObjectTypeDefinitionSyntax InputObjectTypeDefinition(NameSyntax name) =>
            new InputObjectTypeDefinitionSyntax(name);


        [DebuggerStepThrough]
        public static ListTypeSyntax ListType(TypeSyntax type) => new ListTypeSyntax(type);


        [DebuggerStepThrough]
        public static ObjectTypeDefinitionSyntax ObjectTypeDefinition(NameSyntax name) =>
            new ObjectTypeDefinitionSyntax(name);


        [DebuggerStepThrough]
        public static InterfaceTypeDefinitionSyntax InterfaceTypeDefinition(NameSyntax name) =>
            new InterfaceTypeDefinitionSyntax(name);


        [DebuggerStepThrough]
        public static DocumentSyntax Document(params DefinitionSyntax[] definitions) => new DocumentSyntax(definitions);


        [DebuggerStepThrough]
        public static FieldDefinitionSyntax FieldDefinition(NameSyntax name, TypeSyntax type) =>
            new FieldDefinitionSyntax(name, type);


        [DebuggerStepThrough]
        public static InputObjectTypeExtensionSyntax InputObjectTypeExtension(NameSyntax name) =>
            new InputObjectTypeExtensionSyntax(name);


        [DebuggerStepThrough]
        public static ArgumentSyntax Argument(NameSyntax name, ValueSyntax node) =>
            new ArgumentSyntax(name, null, node);


        [DebuggerStepThrough]
        public static VariableSyntax Variable(NameSyntax name) => new VariableSyntax(name);


        [DebuggerStepThrough]
        public static VariableDefinitionSyntax VariableDefinition(VariableSyntax variable, TypeSyntax type,
            ValueSyntax defaultValue = null) =>
            new VariableDefinitionSyntax(variable, type, defaultValue);


        [DebuggerStepThrough]
        public static NameSyntax Name(string name) => new NameSyntax(name);


        [DebuggerStepThrough]
        public static NameSyntax[] Names(params string[] names) =>
            Check.NotNull(names, nameof(names)).Select(Name).ToArray();


        [DebuggerStepThrough]
        public static DirectiveSyntax Directive(NameSyntax name) => new DirectiveSyntax(name);


        [DebuggerStepThrough]
        public static InputValueDefinitionSyntax InputValueDefinition(NameSyntax name, TypeSyntax type) =>
            new InputValueDefinitionSyntax(name, type);


        [DebuggerStepThrough]
        public static EnumTypeDefinitionSyntax EnumTypeDefinition(NameSyntax name) =>
            new EnumTypeDefinitionSyntax(name);


        [DebuggerStepThrough]
        public static NonNullTypeSyntax NonNull(NullableTypeSyntax node) => new NonNullTypeSyntax(node);


        [DebuggerStepThrough]
        public static FieldSyntax Field(NameSyntax name) => new FieldSyntax(name);


        [DebuggerStepThrough]
        public static UnionTypeDefinitionSyntax UnionTypeDefinition(NameSyntax name) =>
            new UnionTypeDefinitionSyntax(name);


        [DebuggerStepThrough]
        public static ScalarTypeDefinitionSyntax ScalarTypeDefinition(NameSyntax name,
            StringValueSyntax description = null) =>
            new ScalarTypeDefinitionSyntax(name, description);


        [DebuggerStepThrough]
        public static SelectionSetSyntax SelectionSet(params SelectionSyntax[] selections) =>
            new SelectionSetSyntax(selections);
    }
}