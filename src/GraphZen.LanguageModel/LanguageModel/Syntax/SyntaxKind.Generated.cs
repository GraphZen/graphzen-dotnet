#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel {
public enum SyntaxKind {
/// <summary>Indicates an <see cref="ArgumentSyntax"/> node.</summary>
Argument,
/// <summary>Indicates an <see cref="BooleanValueSyntax"/> node.</summary>
BooleanValue,
/// <summary>Indicates an <see cref="DirectiveDefinitionSyntax"/> node.</summary>
DirectiveDefinition,
/// <summary>Indicates an <see cref="DirectiveSyntax"/> node.</summary>
Directive,
/// <summary>Indicates an <see cref="DocumentSyntax"/> node.</summary>
Document,
/// <summary>Indicates an <see cref="EnumTypeDefinitionSyntax"/> node.</summary>
EnumTypeDefinition,
/// <summary>Indicates an <see cref="EnumTypeExtensionSyntax"/> node.</summary>
EnumTypeExtension,
/// <summary>Indicates an <see cref="EnumValueDefinitionSyntax"/> node.</summary>
EnumValueDefinition,
/// <summary>Indicates an <see cref="EnumValueSyntax"/> node.</summary>
EnumValue,
/// <summary>Indicates an <see cref="FieldDefinitionSyntax"/> node.</summary>
FieldDefinition,
/// <summary>Indicates an <see cref="FieldSyntax"/> node.</summary>
Field,
/// <summary>Indicates an <see cref="FloatValueSyntax"/> node.</summary>
FloatValue,
/// <summary>Indicates an <see cref="FragmentDefinitionSyntax"/> node.</summary>
FragmentDefinition,
/// <summary>Indicates an <see cref="FragmentSpreadSyntax"/> node.</summary>
FragmentSpread,
/// <summary>Indicates an <see cref="InlineFragmentSyntax"/> node.</summary>
InlineFragment,
/// <summary>Indicates an <see cref="InputObjectTypeDefinitionSyntax"/> node.</summary>
InputObjectTypeDefinition,
/// <summary>Indicates an <see cref="InputObjectTypeExtensionSyntax"/> node.</summary>
InputObjectTypeExtension,
/// <summary>Indicates an <see cref="InputValueDefinitionSyntax"/> node.</summary>
InputValueDefinition,
/// <summary>Indicates an <see cref="InterfaceTypeDefinitionSyntax"/> node.</summary>
InterfaceTypeDefinition,
/// <summary>Indicates an <see cref="InterfaceTypeExtensionSyntax"/> node.</summary>
InterfaceTypeExtension,
/// <summary>Indicates an <see cref="IntValueSyntax"/> node.</summary>
IntValue,
/// <summary>Indicates an <see cref="ListTypeSyntax"/> node.</summary>
ListType,
/// <summary>Indicates an <see cref="ListValueSyntax"/> node.</summary>
ListValue,
/// <summary>Indicates an <see cref="NamedTypeSyntax"/> node.</summary>
NamedType,
/// <summary>Indicates an <see cref="NameSyntax"/> node.</summary>
Name,
/// <summary>Indicates an <see cref="NonNullTypeSyntax"/> node.</summary>
NonNullType,
/// <summary>Indicates an <see cref="NullValueSyntax"/> node.</summary>
NullValue,
/// <summary>Indicates an <see cref="ObjectFieldSyntax"/> node.</summary>
ObjectField,
/// <summary>Indicates an <see cref="ObjectTypeDefinitionSyntax"/> node.</summary>
ObjectTypeDefinition,
/// <summary>Indicates an <see cref="ObjectTypeExtensionSyntax"/> node.</summary>
ObjectTypeExtension,
/// <summary>Indicates an <see cref="ObjectValueSyntax"/> node.</summary>
ObjectValue,
/// <summary>Indicates an <see cref="OperationDefinitionSyntax"/> node.</summary>
OperationDefinition,
/// <summary>Indicates an <see cref="OperationTypeDefinitionSyntax"/> node.</summary>
OperationTypeDefinition,
/// <summary>Indicates an <see cref="PunctuatorSyntax"/> node.</summary>
Punctuator,
/// <summary>Indicates an <see cref="ScalarTypeDefinitionSyntax"/> node.</summary>
ScalarTypeDefinition,
/// <summary>Indicates an <see cref="ScalarTypeExtensionSyntax"/> node.</summary>
ScalarTypeExtension,
/// <summary>Indicates an <see cref="SchemaDefinitionSyntax"/> node.</summary>
SchemaDefinition,
/// <summary>Indicates an <see cref="SchemaExtensionSyntax"/> node.</summary>
SchemaExtension,
/// <summary>Indicates an <see cref="SelectionSetSyntax"/> node.</summary>
SelectionSet,
/// <summary>Indicates an <see cref="StringValueSyntax"/> node.</summary>
StringValue,
/// <summary>Indicates an <see cref="UnionTypeDefinitionSyntax"/> node.</summary>
UnionTypeDefinition,
/// <summary>Indicates an <see cref="UnionTypeExtensionSyntax"/> node.</summary>
UnionTypeExtension,
/// <summary>Indicates an <see cref="VariableDefinitionSyntax"/> node.</summary>
VariableDefinition,
/// <summary>Indicates an <see cref="VariableSyntax"/> node.</summary>
Variable,
}
}
// Source Hash Code: -118589120