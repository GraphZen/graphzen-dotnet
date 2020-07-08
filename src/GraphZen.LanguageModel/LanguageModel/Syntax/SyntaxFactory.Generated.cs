// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.LanguageModel
{
    public static partial class SyntaxFactory
    {
        #region SyntaxFactoryGenerator

        public static ArgumentSyntax Argument(NameSyntax name, ValueSyntax value, StringValueSyntax? description = null,
            SyntaxLocation? location = null) => new ArgumentSyntax(name, value, description, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static DirectiveDefinitionSyntax DirectiveDefinition(NameSyntax name,
            IReadOnlyList<NameSyntax> locations, IReadOnlyList<InputValueDefinitionSyntax>? arguments = null,
            StringValueSyntax? description = null, SyntaxLocation? location = null) =>
            new DirectiveDefinitionSyntax(name, locations, arguments, description, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static DirectiveSyntax Directive(NameSyntax name, IReadOnlyList<ArgumentSyntax>? arguments = null,
            SyntaxLocation? location = null) => new DirectiveSyntax(name, arguments, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static DocumentSyntax Document(params DefinitionSyntax[] definitions) => new DocumentSyntax(definitions);

        #endregion

        #region SyntaxFactoryGenerator

        public static DocumentSyntax Document(IReadOnlyList<DefinitionSyntax> definitions,
            SyntaxLocation? location = null) => new DocumentSyntax(definitions, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static EnumTypeDefinitionSyntax EnumTypeDefinition(NameSyntax name,
            StringValueSyntax? description = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<EnumValueDefinitionSyntax>? values = null, SyntaxLocation? location = null) =>
            new EnumTypeDefinitionSyntax(name, description, directives, values, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static EnumTypeExtensionSyntax EnumTypeExtension(NameSyntax name,
            IReadOnlyList<DirectiveSyntax>? directives = null, IReadOnlyList<EnumValueDefinitionSyntax>? values = null,
            SyntaxLocation? location = null) => new EnumTypeExtensionSyntax(name, directives, values, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static EnumValueDefinitionSyntax EnumValueDefinition(EnumValueSyntax value,
            StringValueSyntax? description = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            SyntaxLocation? location = null) => new EnumValueDefinitionSyntax(value, description, directives, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static EnumValueSyntax EnumValue(NameSyntax value) => new EnumValueSyntax(value);

        #endregion

        #region SyntaxFactoryGenerator

        public static FieldDefinitionSyntax FieldDefinition(NameSyntax name, TypeSyntax type,
            StringValueSyntax? description = null, IReadOnlyList<InputValueDefinitionSyntax>? arguments = null,
            IReadOnlyList<DirectiveSyntax>? directives = null, SyntaxLocation? location = null) =>
            new FieldDefinitionSyntax(name, type, description, arguments, directives, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static FieldSyntax Field(NameSyntax name, NameSyntax? alias = null,
            IReadOnlyList<ArgumentSyntax>? arguments = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            SelectionSetSyntax? selectionSet = null, SyntaxLocation? location = null) =>
            new FieldSyntax(name, alias, arguments, directives, selectionSet, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static FloatValueSyntax FloatValue(string value, SyntaxLocation? location = null) =>
            new FloatValueSyntax(value, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static FragmentDefinitionSyntax FragmentDefinition(NameSyntax name, NamedTypeSyntax typeCondition,
            SelectionSetSyntax selectionSet, IReadOnlyList<DirectiveSyntax>? directives = null,
            SyntaxLocation? location = null) =>
            new FragmentDefinitionSyntax(name, typeCondition, selectionSet, directives, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static FragmentSpreadSyntax FragmentSpread(NameSyntax name,
            IReadOnlyList<DirectiveSyntax>? directives = null, SyntaxLocation? location = null) =>
            new FragmentSpreadSyntax(name, directives, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static InlineFragmentSyntax InlineFragment(SelectionSetSyntax selectionSet,
            NamedTypeSyntax? typeCondition = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            SyntaxLocation? location = null) =>
            new InlineFragmentSyntax(selectionSet, typeCondition, directives, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static InputObjectTypeDefinitionSyntax InputObjectTypeDefinition(NameSyntax name,
            StringValueSyntax? description = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<InputValueDefinitionSyntax>? fields = null, SyntaxLocation? location = null) =>
            new InputObjectTypeDefinitionSyntax(name, description, directives, fields, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static InputObjectTypeExtensionSyntax InputObjectTypeExtension(NameSyntax name,
            IReadOnlyList<DirectiveSyntax>? directives = null, IReadOnlyList<InputValueDefinitionSyntax>? fields = null,
            SyntaxLocation? location = null) => new InputObjectTypeExtensionSyntax(name, directives, fields, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static InputValueDefinitionSyntax InputValueDefinition(NameSyntax name, TypeSyntax type,
            StringValueSyntax? description = null, ValueSyntax? defaultValue = null,
            IReadOnlyList<DirectiveSyntax>? directives = null, SyntaxLocation? location = null) =>
            new InputValueDefinitionSyntax(name, type, description, defaultValue, directives, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static InterfaceTypeDefinitionSyntax InterfaceTypeDefinition(NameSyntax name,
            StringValueSyntax? description = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<FieldDefinitionSyntax>? fields = null, SyntaxLocation? location = null) =>
            new InterfaceTypeDefinitionSyntax(name, description, directives, fields, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static InterfaceTypeExtensionSyntax InterfaceTypeExtension(NameSyntax name,
            IReadOnlyList<DirectiveSyntax>? directives = null, IReadOnlyList<FieldDefinitionSyntax>? fields = null,
            SyntaxLocation? location = null) => new InterfaceTypeExtensionSyntax(name, directives, fields, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static IntValueSyntax IntValue(int value, SyntaxLocation? location = null) =>
            new IntValueSyntax(value, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static ListTypeSyntax ListType(TypeSyntax type, SyntaxLocation? location = null) =>
            new ListTypeSyntax(type, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static ListValueSyntax ListValue(params ValueSyntax[] values) => new ListValueSyntax(values);

        #endregion

        #region SyntaxFactoryGenerator

        public static ListValueSyntax ListValue(IReadOnlyList<ValueSyntax> values, SyntaxLocation? location = null) =>
            new ListValueSyntax(values, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static NamedTypeSyntax NamedType(NameSyntax name, SyntaxLocation? location = null) =>
            new NamedTypeSyntax(name, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static NameSyntax Name(string value, SyntaxLocation? location = null) => new NameSyntax(value, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static NonNullTypeSyntax NonNullType(NullableTypeSyntax type, SyntaxLocation? location = null) =>
            new NonNullTypeSyntax(type, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static ObjectFieldSyntax
            ObjectField(NameSyntax name, ValueSyntax value, SyntaxLocation? location = null) =>
            new ObjectFieldSyntax(name, value, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static ObjectTypeDefinitionSyntax ObjectTypeDefinition(NameSyntax name,
            StringValueSyntax? description = null, IReadOnlyList<NamedTypeSyntax>? interfaces = null,
            IReadOnlyList<DirectiveSyntax>? directives = null, IReadOnlyList<FieldDefinitionSyntax>? fields = null,
            SyntaxLocation? location = null) =>
            new ObjectTypeDefinitionSyntax(name, description, interfaces, directives, fields, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static ObjectTypeExtensionSyntax ObjectTypeExtension(NameSyntax name,
            IReadOnlyList<NamedTypeSyntax>? interfaces = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<FieldDefinitionSyntax>? fields = null, SyntaxLocation? location = null) =>
            new ObjectTypeExtensionSyntax(name, interfaces, directives, fields, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static ObjectValueSyntax ObjectValue(params ObjectFieldSyntax[] fields) => new ObjectValueSyntax(fields);

        #endregion

        #region SyntaxFactoryGenerator

        public static ObjectValueSyntax ObjectValue(IReadOnlyList<ObjectFieldSyntax> fields,
            SyntaxLocation? location = null) => new ObjectValueSyntax(fields, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static OperationDefinitionSyntax OperationDefinition(OperationType type, SelectionSetSyntax selectionSet,
            NameSyntax? name = null, IReadOnlyList<VariableDefinitionSyntax>? variableDefinitions = null,
            IReadOnlyList<DirectiveSyntax>? directives = null, SyntaxLocation? location = null) =>
            new OperationDefinitionSyntax(type, selectionSet, name, variableDefinitions, directives, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static OperationTypeDefinitionSyntax OperationTypeDefinition(OperationType operationType,
            NamedTypeSyntax type, SyntaxLocation? location = null) =>
            new OperationTypeDefinitionSyntax(operationType, type, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static PunctuatorSyntax Punctuator(SyntaxLocation location) => new PunctuatorSyntax(location);

        #endregion

        #region SyntaxFactoryGenerator

        public static ScalarTypeDefinitionSyntax ScalarTypeDefinition(NameSyntax name,
            StringValueSyntax? description = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            SyntaxLocation? location = null) => new ScalarTypeDefinitionSyntax(name, description, directives, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static ScalarTypeExtensionSyntax ScalarTypeExtension(NameSyntax name,
            IReadOnlyList<DirectiveSyntax> directives, SyntaxLocation? location = null) =>
            new ScalarTypeExtensionSyntax(name, directives, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static SchemaDefinitionSyntax SchemaDefinition(
            IReadOnlyList<OperationTypeDefinitionSyntax>? operationTypes = null,
            IReadOnlyList<DirectiveSyntax>? directives = null, SyntaxLocation? location = null) =>
            new SchemaDefinitionSyntax(operationTypes, directives, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static SchemaExtensionSyntax SchemaExtension(IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<OperationTypeDefinitionSyntax>? operationTypes = null, SyntaxLocation? location = null) =>
            new SchemaExtensionSyntax(directives, operationTypes, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static SelectionSetSyntax SelectionSet(IReadOnlyList<SelectionSyntax> selections,
            SyntaxLocation? location = null) => new SelectionSetSyntax(selections, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static SelectionSetSyntax SelectionSet(params SelectionSyntax[] selections) =>
            new SelectionSetSyntax(selections);

        #endregion

        #region SyntaxFactoryGenerator

        public static StringValueSyntax StringValue(string value, bool isBlockString = false,
            SyntaxLocation? location = null) => new StringValueSyntax(value, isBlockString, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static UnionTypeDefinitionSyntax UnionTypeDefinition(NameSyntax name,
            StringValueSyntax? description = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<NamedTypeSyntax>? types = null, SyntaxLocation? location = null) =>
            new UnionTypeDefinitionSyntax(name, description, directives, types, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static UnionTypeExtensionSyntax UnionTypeExtension(NameSyntax name,
            IReadOnlyList<DirectiveSyntax>? directives = null, IReadOnlyList<NamedTypeSyntax>? types = null,
            SyntaxLocation? location = null) => new UnionTypeExtensionSyntax(name, directives, types, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static VariableDefinitionSyntax VariableDefinition(VariableSyntax variable, TypeSyntax type,
            ValueSyntax? defaultValue = null, SyntaxLocation? location = null) =>
            new VariableDefinitionSyntax(variable, type, defaultValue, location);

        #endregion

        #region SyntaxFactoryGenerator

        public static VariableSyntax Variable(NameSyntax name, SyntaxLocation? location = null) =>
            new VariableSyntax(name, location);

        #endregion
    }
}
// Source Hash Code: 18290394137405157956