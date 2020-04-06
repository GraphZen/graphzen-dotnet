using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable restore

namespace GraphZen.LanguageModel
{
    public static partial class SyntaxFactory
    {
        public static ArgumentSyntax Argument(NameSyntax name, ValueSyntax value, StringValueSyntax? description = null,
            SyntaxLocation? location = null) => new ArgumentSyntax(name, value, description, location);

        public static DirectiveDefinitionSyntax DirectiveDefinition(NameSyntax name,
            IReadOnlyList<NameSyntax> locations, IReadOnlyList<InputValueDefinitionSyntax>? arguments = null,
            StringValueSyntax? description = null, SyntaxLocation? location = null) =>
            new DirectiveDefinitionSyntax(name, locations, arguments, description, location);

        public static DirectiveSyntax Directive(NameSyntax name, IReadOnlyList<ArgumentSyntax>? arguments = null,
            SyntaxLocation? location = null) => new DirectiveSyntax(name, arguments, location);

        public static DocumentSyntax Document(params DefinitionSyntax[] definitions) => new DocumentSyntax(definitions);

        public static DocumentSyntax Document(IReadOnlyList<DefinitionSyntax> definitions,
            SyntaxLocation? location = null) => new DocumentSyntax(definitions, location);

        public static EnumTypeDefinitionSyntax EnumTypeDefinition(NameSyntax name,
            StringValueSyntax? description = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<EnumValueDefinitionSyntax>? values = null, SyntaxLocation? location = null) =>
            new EnumTypeDefinitionSyntax(name, description, directives, values, location);

        public static EnumTypeExtensionSyntax EnumTypeExtension(NameSyntax name,
            IReadOnlyList<DirectiveSyntax>? directives = null, IReadOnlyList<EnumValueDefinitionSyntax>? values = null,
            SyntaxLocation? location = null) => new EnumTypeExtensionSyntax(name, directives, values, location);

        public static EnumValueDefinitionSyntax EnumValueDefinition(EnumValueSyntax value,
            StringValueSyntax? description = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            SyntaxLocation? location = null) => new EnumValueDefinitionSyntax(value, description, directives, location);

        public static EnumValueSyntax EnumValue(NameSyntax value) => new EnumValueSyntax(value);

        public static FieldDefinitionSyntax FieldDefinition(NameSyntax name, TypeSyntax type,
            StringValueSyntax? description = null, IReadOnlyList<InputValueDefinitionSyntax>? arguments = null,
            IReadOnlyList<DirectiveSyntax>? directives = null, SyntaxLocation? location = null) =>
            new FieldDefinitionSyntax(name, type, description, arguments, directives, location);

        public static FieldSyntax Field(NameSyntax name, NameSyntax? alias = null,
            IReadOnlyList<ArgumentSyntax>? arguments = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            SelectionSetSyntax? selectionSet = null, SyntaxLocation? location = null) =>
            new FieldSyntax(name, alias, arguments, directives, selectionSet, location);

        public static FloatValueSyntax FloatValue(string value, SyntaxLocation? location = null) =>
            new FloatValueSyntax(value, location);

        public static FragmentDefinitionSyntax FragmentDefinition(NameSyntax name, NamedTypeSyntax typeCondition,
            SelectionSetSyntax selectionSet, IReadOnlyList<DirectiveSyntax>? directives = null,
            SyntaxLocation? location = null) =>
            new FragmentDefinitionSyntax(name, typeCondition, selectionSet, directives, location);

        public static FragmentSpreadSyntax FragmentSpread(NameSyntax name,
            IReadOnlyList<DirectiveSyntax>? directives = null, SyntaxLocation? location = null) =>
            new FragmentSpreadSyntax(name, directives, location);

        public static InlineFragmentSyntax InlineFragment(SelectionSetSyntax selectionSet,
            NamedTypeSyntax? typeCondition = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            SyntaxLocation? location = null) =>
            new InlineFragmentSyntax(selectionSet, typeCondition, directives, location);

        public static InputObjectTypeDefinitionSyntax InputObjectTypeDefinition(NameSyntax name,
            StringValueSyntax? description = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<InputValueDefinitionSyntax>? fields = null, SyntaxLocation? location = null) =>
            new InputObjectTypeDefinitionSyntax(name, description, directives, fields, location);

        public static InputObjectTypeExtensionSyntax InputObjectTypeExtension(NameSyntax name,
            IReadOnlyList<DirectiveSyntax>? directives = null, IReadOnlyList<InputValueDefinitionSyntax>? fields = null,
            SyntaxLocation? location = null) => new InputObjectTypeExtensionSyntax(name, directives, fields, location);

        public static InputValueDefinitionSyntax InputValueDefinition(NameSyntax name, TypeSyntax type,
            StringValueSyntax? description = null, ValueSyntax? defaultValue = null,
            IReadOnlyList<DirectiveSyntax>? directives = null, SyntaxLocation? location = null) =>
            new InputValueDefinitionSyntax(name, type, description, defaultValue, directives, location);

        public static InterfaceTypeDefinitionSyntax InterfaceTypeDefinition(NameSyntax name,
            StringValueSyntax? description = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<FieldDefinitionSyntax>? fields = null, SyntaxLocation? location = null) =>
            new InterfaceTypeDefinitionSyntax(name, description, directives, fields, location);

        public static InterfaceTypeExtensionSyntax InterfaceTypeExtension(NameSyntax name,
            IReadOnlyList<DirectiveSyntax>? directives = null, IReadOnlyList<FieldDefinitionSyntax>? fields = null,
            SyntaxLocation? location = null) => new InterfaceTypeExtensionSyntax(name, directives, fields, location);

        public static IntValueSyntax IntValue(int value, SyntaxLocation? location = null) =>
            new IntValueSyntax(value, location);

        public static ListTypeSyntax ListType(TypeSyntax type, SyntaxLocation? location = null) =>
            new ListTypeSyntax(type, location);

        public static ListValueSyntax ListValue(params ValueSyntax[] values) => new ListValueSyntax(values);

        public static ListValueSyntax ListValue(IReadOnlyList<ValueSyntax> values, SyntaxLocation? location = null) =>
            new ListValueSyntax(values, location);

        public static NamedTypeSyntax NamedType(NameSyntax name, SyntaxLocation? location = null) =>
            new NamedTypeSyntax(name, location);

        public static NameSyntax Name(string value, SyntaxLocation? location = null) => new NameSyntax(value, location);

        public static NonNullTypeSyntax NonNullType(NullableTypeSyntax type, SyntaxLocation? location = null) =>
            new NonNullTypeSyntax(type, location);

        public static ObjectFieldSyntax
            ObjectField(NameSyntax name, ValueSyntax value, SyntaxLocation? location = null) =>
            new ObjectFieldSyntax(name, value, location);

        public static ObjectTypeDefinitionSyntax ObjectTypeDefinition(NameSyntax name,
            StringValueSyntax? description = null, IReadOnlyList<NamedTypeSyntax>? interfaces = null,
            IReadOnlyList<DirectiveSyntax>? directives = null, IReadOnlyList<FieldDefinitionSyntax>? fields = null,
            SyntaxLocation? location = null) =>
            new ObjectTypeDefinitionSyntax(name, description, interfaces, directives, fields, location);

        public static ObjectTypeExtensionSyntax ObjectTypeExtension(NameSyntax name,
            IReadOnlyList<NamedTypeSyntax>? interfaces = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<FieldDefinitionSyntax>? fields = null, SyntaxLocation? location = null) =>
            new ObjectTypeExtensionSyntax(name, interfaces, directives, fields, location);

        public static ObjectValueSyntax ObjectValue(params ObjectFieldSyntax[] fields) => new ObjectValueSyntax(fields);

        public static ObjectValueSyntax ObjectValue(IReadOnlyList<ObjectFieldSyntax> fields,
            SyntaxLocation? location = null) => new ObjectValueSyntax(fields, location);

        public static OperationDefinitionSyntax OperationDefinition(OperationType type, SelectionSetSyntax selectionSet,
            NameSyntax? name = null, IReadOnlyList<VariableDefinitionSyntax>? variableDefinitions = null,
            IReadOnlyList<DirectiveSyntax>? directives = null, SyntaxLocation? location = null) =>
            new OperationDefinitionSyntax(type, selectionSet, name, variableDefinitions, directives, location);

        public static OperationTypeDefinitionSyntax OperationTypeDefinition(OperationType operationType,
            NamedTypeSyntax type, SyntaxLocation? location = null) =>
            new OperationTypeDefinitionSyntax(operationType, type, location);

        public static PunctuatorSyntax Punctuator(SyntaxLocation location) => new PunctuatorSyntax(location);

        public static ScalarTypeDefinitionSyntax ScalarTypeDefinition(NameSyntax name,
            StringValueSyntax? description = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            SyntaxLocation? location = null) => new ScalarTypeDefinitionSyntax(name, description, directives, location);

        public static ScalarTypeExtensionSyntax ScalarTypeExtension(NameSyntax name,
            IReadOnlyList<DirectiveSyntax> directives, SyntaxLocation? location = null) =>
            new ScalarTypeExtensionSyntax(name, directives, location);

        public static SchemaDefinitionSyntax SchemaDefinition(
            IReadOnlyList<OperationTypeDefinitionSyntax>? operationTypes = null,
            IReadOnlyList<DirectiveSyntax>? directives = null, SyntaxLocation? location = null) =>
            new SchemaDefinitionSyntax(operationTypes, directives, location);

        public static SchemaExtensionSyntax SchemaExtension(IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<OperationTypeDefinitionSyntax>? operationTypes = null, SyntaxLocation? location = null) =>
            new SchemaExtensionSyntax(directives, operationTypes, location);

        public static SelectionSetSyntax SelectionSet(IReadOnlyList<SelectionSyntax> selections,
            SyntaxLocation? location = null) => new SelectionSetSyntax(selections, location);

        public static SelectionSetSyntax SelectionSet(params SelectionSyntax[] selections) =>
            new SelectionSetSyntax(selections);

        public static StringValueSyntax StringValue(string value, bool isBlockString = false,
            SyntaxLocation? location = null) => new StringValueSyntax(value, isBlockString, location);

        public static UnionTypeDefinitionSyntax UnionTypeDefinition(NameSyntax name,
            StringValueSyntax? description = null, IReadOnlyList<DirectiveSyntax>? directives = null,
            IReadOnlyList<NamedTypeSyntax>? types = null, SyntaxLocation? location = null) =>
            new UnionTypeDefinitionSyntax(name, description, directives, types, location);

        public static UnionTypeExtensionSyntax UnionTypeExtension(NameSyntax name,
            IReadOnlyList<DirectiveSyntax>? directives = null, IReadOnlyList<NamedTypeSyntax>? types = null,
            SyntaxLocation? location = null) => new UnionTypeExtensionSyntax(name, directives, types, location);

        public static VariableDefinitionSyntax VariableDefinition(VariableSyntax variable, TypeSyntax type,
            ValueSyntax? defaultValue = null, SyntaxLocation? location = null) =>
            new VariableDefinitionSyntax(variable, type, defaultValue, location);

        public static VariableSyntax Variable(NameSyntax name, SyntaxLocation? location = null) =>
            new VariableSyntax(name, location);
    }
}