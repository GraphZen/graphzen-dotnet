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
    public static class SyntaxFactory
    {
        private static NullValueSyntax NullValueInstance { get; } = new NullValueSyntax();


        private static BooleanValueSyntax TrueBooleanValueInstance { get; } = new BooleanValueSyntax(true);


        private static BooleanValueSyntax FalseBooleanValueInstance { get; } = new BooleanValueSyntax(false);


        [DebuggerStepThrough]
        public static NullValueSyntax NullValue()
        {
            return NullValueInstance;
        }

        [DebuggerStepThrough]
        public static SchemaDefinitionSyntax SchemaDefinition()
        {
            return new SchemaDefinitionSyntax(OperationTypeDefinitionSyntax.EmptyList);
        }


        [DebuggerStepThrough]
        public static OperationTypeDefinitionSyntax OperationTypeDefinition(OperationType operationType,
            NamedTypeSyntax type)
        {
            return new OperationTypeDefinitionSyntax(operationType, type);
        }


        [DebuggerStepThrough]
        public static NamedTypeSyntax NamedType(NameSyntax name)
        {
            return new NamedTypeSyntax(name);
        }


        [DebuggerStepThrough]
        public static NamedTypeSyntax NamedType(Type clrType)
        {
            return new NamedTypeSyntax(Name(Check.NotNull(clrType, nameof(clrType)).GetGraphQLName()));
        }


        [DebuggerStepThrough]
        public static ObjectFieldSyntax ObjectField(NameSyntax name, ValueSyntax value)
        {
            return new ObjectFieldSyntax(name, value);
        }


        [DebuggerStepThrough]
        public static EnumValueSyntax EnumValue(NameSyntax name)
        {
            return new EnumValueSyntax(name);
        }


        [DebuggerStepThrough]
        public static EnumValueDefinitionSyntax EnumValueDefinition(EnumValueSyntax enumValue)
        {
            return new EnumValueDefinitionSyntax(enumValue);
        }


        [DebuggerStepThrough]
        public static ListValueSyntax ListValue(params ValueSyntax[] values)
        {
            return new ListValueSyntax(values);
        }


        [DebuggerStepThrough]
        public static ListValueSyntax ListValue(IReadOnlyList<ValueSyntax> values)
        {
            return new ListValueSyntax(values);
        }


        [DebuggerStepThrough]
        public static ObjectValueSyntax ObjectValue(params ObjectFieldSyntax[] fields)
        {
            return new ObjectValueSyntax(fields);
        }


        [DebuggerStepThrough]
        public static ObjectValueSyntax ObjectValue(IReadOnlyList<ObjectFieldSyntax> fields)
        {
            return new ObjectValueSyntax(fields);
        }


        [DebuggerStepThrough]
        public static BooleanValueSyntax BooleanValue(bool value)
        {
            return value ? TrueBooleanValueInstance : FalseBooleanValueInstance;
        }


        [DebuggerStepThrough]
        public static StringValueSyntax StringValue(string value, bool isBlockString = false)
        {
            return new StringValueSyntax(value, isBlockString);
        }


        [DebuggerStepThrough]
        public static IntValueSyntax IntValue(int value)
        {
            return new IntValueSyntax(value);
        }


        [DebuggerStepThrough]
        public static FloatValueSyntax FloatValue(string value)
        {
            return new FloatValueSyntax(value);
        }


        [DebuggerStepThrough]
        public static FragmentSpreadSyntax FragmentSpread(NameSyntax name)
        {
            return new FragmentSpreadSyntax(name);
        }


        [DebuggerStepThrough]
        public static InputObjectTypeDefinitionSyntax InputObjectTypeDefinition(NameSyntax name)
        {
            return new InputObjectTypeDefinitionSyntax(name);
        }


        [DebuggerStepThrough]
        public static ListTypeSyntax ListType(TypeSyntax type)
        {
            return new ListTypeSyntax(type);
        }


        [DebuggerStepThrough]
        public static ObjectTypeDefinitionSyntax ObjectTypeDefinition(NameSyntax name)
        {
            return new ObjectTypeDefinitionSyntax(name);
        }


        [DebuggerStepThrough]
        public static InterfaceTypeDefinitionSyntax InterfaceTypeDefinition(NameSyntax name)
        {
            return new InterfaceTypeDefinitionSyntax(name);
        }


        [DebuggerStepThrough]
        public static DocumentSyntax Document(params DefinitionSyntax[] definitions)
        {
            return new DocumentSyntax(definitions);
        }


        [DebuggerStepThrough]
        public static FieldDefinitionSyntax FieldDefinition(NameSyntax name, TypeSyntax type)
        {
            return new FieldDefinitionSyntax(name, type);
        }


        [DebuggerStepThrough]
        public static InputObjectTypeExtensionSyntax InputObjectTypeExtension(NameSyntax name)
        {
            return new InputObjectTypeExtensionSyntax(name);
        }


        [DebuggerStepThrough]
        public static ArgumentSyntax Argument(NameSyntax name, ValueSyntax node)
        {
            return new ArgumentSyntax(name, null, node);
        }


        [DebuggerStepThrough]
        public static VariableSyntax Variable(NameSyntax name)
        {
            return new VariableSyntax(name);
        }


        [DebuggerStepThrough]
        public static VariableDefinitionSyntax VariableDefinition(VariableSyntax variable, TypeSyntax type,
            ValueSyntax defaultValue = null)
        {
            return new VariableDefinitionSyntax(variable, type, defaultValue);
        }


        [DebuggerStepThrough]
        public static NameSyntax Name(string name)
        {
            return new NameSyntax(name);
        }


        [DebuggerStepThrough]
        public static NameSyntax[] Names(params string[] names)
        {
            return Check.NotNull(names, nameof(names)).Select(Name).ToArray();
        }


        [DebuggerStepThrough]
        public static DirectiveSyntax Directive(NameSyntax name)
        {
            return new DirectiveSyntax(name);
        }


        [DebuggerStepThrough]
        public static InputValueDefinitionSyntax InputValueDefinition(NameSyntax name, TypeSyntax type)
        {
            return new InputValueDefinitionSyntax(name, type);
        }


        [DebuggerStepThrough]
        public static EnumTypeDefinitionSyntax EnumTypeDefinition(NameSyntax name)
        {
            return new EnumTypeDefinitionSyntax(name);
        }


        [DebuggerStepThrough]
        public static NonNullTypeSyntax NonNull(NullableTypeSyntax node)
        {
            return new NonNullTypeSyntax(node);
        }


        [DebuggerStepThrough]
        public static FieldSyntax Field(NameSyntax name)
        {
            return new FieldSyntax(name);
        }


        [DebuggerStepThrough]
        public static UnionTypeDefinitionSyntax UnionTypeDefinition(NameSyntax name)
        {
            return new UnionTypeDefinitionSyntax(name);
        }


        [DebuggerStepThrough]
        public static ScalarTypeDefinitionSyntax ScalarTypeDefinition(NameSyntax name,
            StringValueSyntax description = null)
        {
            return new ScalarTypeDefinitionSyntax(name, description);
        }


        [DebuggerStepThrough]
        public static SelectionSetSyntax SelectionSet(params SelectionSyntax[] selections)
        {
            return new SelectionSetSyntax(selections);
        }
    }
}