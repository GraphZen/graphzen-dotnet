#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.LanguageModel {
public static partial class SyntaxFactory {
#region SyntaxFactoryGenerator
public static ArgumentSyntax Argument( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.ValueSyntax value ,  GraphZen.LanguageModel.StringValueSyntax? description  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new ArgumentSyntax(name, value, description, location);
#endregion
#region SyntaxFactoryGenerator
public static DirectiveDefinitionSyntax DirectiveDefinition( GraphZen.LanguageModel.NameSyntax name ,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.NameSyntax> locations ,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.InputValueDefinitionSyntax>? arguments  = null,  GraphZen.LanguageModel.StringValueSyntax? description  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new DirectiveDefinitionSyntax(name, locations, arguments, description, location);
#endregion
#region SyntaxFactoryGenerator
public static DirectiveSyntax Directive( GraphZen.LanguageModel.NameSyntax name ,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.ArgumentSyntax>? arguments  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new DirectiveSyntax(name, arguments, location);
#endregion
#region SyntaxFactoryGenerator
public static DocumentSyntax Document(params GraphZen.LanguageModel.DefinitionSyntax[] definitions ) => new DocumentSyntax(definitions);
#endregion
#region SyntaxFactoryGenerator
public static DocumentSyntax Document( System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DefinitionSyntax> definitions ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new DocumentSyntax(definitions, location);
#endregion
#region SyntaxFactoryGenerator
public static EnumTypeDefinitionSyntax EnumTypeDefinition( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.StringValueSyntax? description  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.EnumValueDefinitionSyntax>? values  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new EnumTypeDefinitionSyntax(name, description, directives, values, location);
#endregion
#region SyntaxFactoryGenerator
public static EnumTypeExtensionSyntax EnumTypeExtension( GraphZen.LanguageModel.NameSyntax name ,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.EnumValueDefinitionSyntax>? values  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new EnumTypeExtensionSyntax(name, directives, values, location);
#endregion
#region SyntaxFactoryGenerator
public static EnumValueDefinitionSyntax EnumValueDefinition( GraphZen.LanguageModel.EnumValueSyntax value ,  GraphZen.LanguageModel.StringValueSyntax? description  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new EnumValueDefinitionSyntax(value, description, directives, location);
#endregion
#region SyntaxFactoryGenerator
public static EnumValueSyntax EnumValue( GraphZen.LanguageModel.NameSyntax value ) => new EnumValueSyntax(value);
#endregion
#region SyntaxFactoryGenerator
public static FieldDefinitionSyntax FieldDefinition( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.TypeSyntax type ,  GraphZen.LanguageModel.StringValueSyntax? description  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.InputValueDefinitionSyntax>? arguments  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new FieldDefinitionSyntax(name, type, description, arguments, directives, location);
#endregion
#region SyntaxFactoryGenerator
public static FieldSyntax Field( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.NameSyntax? alias  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.ArgumentSyntax>? arguments  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  GraphZen.LanguageModel.SelectionSetSyntax? selectionSet  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new FieldSyntax(name, alias, arguments, directives, selectionSet, location);
#endregion
#region SyntaxFactoryGenerator
public static FloatValueSyntax FloatValue( System.String value ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new FloatValueSyntax(value, location);
#endregion
#region SyntaxFactoryGenerator
public static FragmentDefinitionSyntax FragmentDefinition( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.NamedTypeSyntax typeCondition ,  GraphZen.LanguageModel.SelectionSetSyntax selectionSet ,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new FragmentDefinitionSyntax(name, typeCondition, selectionSet, directives, location);
#endregion
#region SyntaxFactoryGenerator
public static FragmentSpreadSyntax FragmentSpread( GraphZen.LanguageModel.NameSyntax name ,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new FragmentSpreadSyntax(name, directives, location);
#endregion
#region SyntaxFactoryGenerator
public static InlineFragmentSyntax InlineFragment( GraphZen.LanguageModel.SelectionSetSyntax selectionSet ,  GraphZen.LanguageModel.NamedTypeSyntax? typeCondition  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new InlineFragmentSyntax(selectionSet, typeCondition, directives, location);
#endregion
#region SyntaxFactoryGenerator
public static InputObjectTypeDefinitionSyntax InputObjectTypeDefinition( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.StringValueSyntax? description  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.InputValueDefinitionSyntax>? fields  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new InputObjectTypeDefinitionSyntax(name, description, directives, fields, location);
#endregion
#region SyntaxFactoryGenerator
public static InputObjectTypeExtensionSyntax InputObjectTypeExtension( GraphZen.LanguageModel.NameSyntax name ,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.InputValueDefinitionSyntax>? fields  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new InputObjectTypeExtensionSyntax(name, directives, fields, location);
#endregion
#region SyntaxFactoryGenerator
public static InputValueDefinitionSyntax InputValueDefinition( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.TypeSyntax type ,  GraphZen.LanguageModel.StringValueSyntax? description  = null,  GraphZen.LanguageModel.ValueSyntax? defaultValue  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new InputValueDefinitionSyntax(name, type, description, defaultValue, directives, location);
#endregion
#region SyntaxFactoryGenerator
public static InterfaceTypeDefinitionSyntax InterfaceTypeDefinition( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.StringValueSyntax? description  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.FieldDefinitionSyntax>? fields  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new InterfaceTypeDefinitionSyntax(name, description, directives, fields, location);
#endregion
#region SyntaxFactoryGenerator
public static InterfaceTypeExtensionSyntax InterfaceTypeExtension( GraphZen.LanguageModel.NameSyntax name ,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.FieldDefinitionSyntax>? fields  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new InterfaceTypeExtensionSyntax(name, directives, fields, location);
#endregion
#region SyntaxFactoryGenerator
public static IntValueSyntax IntValue( System.Int32 value ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new IntValueSyntax(value, location);
#endregion
#region SyntaxFactoryGenerator
public static ListTypeSyntax ListType( GraphZen.LanguageModel.TypeSyntax type ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new ListTypeSyntax(type, location);
#endregion
#region SyntaxFactoryGenerator
public static ListValueSyntax ListValue(params GraphZen.LanguageModel.ValueSyntax[] values ) => new ListValueSyntax(values);
#endregion
#region SyntaxFactoryGenerator
public static ListValueSyntax ListValue( System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.ValueSyntax> values ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new ListValueSyntax(values, location);
#endregion
#region SyntaxFactoryGenerator
public static NamedTypeSyntax NamedType( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new NamedTypeSyntax(name, location);
#endregion
#region SyntaxFactoryGenerator
public static NameSyntax Name( System.String value ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new NameSyntax(value, location);
#endregion
#region SyntaxFactoryGenerator
public static NonNullTypeSyntax NonNullType( GraphZen.LanguageModel.NullableTypeSyntax type ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new NonNullTypeSyntax(type, location);
#endregion
#region SyntaxFactoryGenerator
public static ObjectFieldSyntax ObjectField( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.ValueSyntax value ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new ObjectFieldSyntax(name, value, location);
#endregion
#region SyntaxFactoryGenerator
public static ObjectTypeDefinitionSyntax ObjectTypeDefinition( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.StringValueSyntax? description  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.NamedTypeSyntax>? interfaces  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.FieldDefinitionSyntax>? fields  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new ObjectTypeDefinitionSyntax(name, description, interfaces, directives, fields, location);
#endregion
#region SyntaxFactoryGenerator
public static ObjectTypeExtensionSyntax ObjectTypeExtension( GraphZen.LanguageModel.NameSyntax name ,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.NamedTypeSyntax>? interfaces  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.FieldDefinitionSyntax>? fields  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new ObjectTypeExtensionSyntax(name, interfaces, directives, fields, location);
#endregion
#region SyntaxFactoryGenerator
public static ObjectValueSyntax ObjectValue(params GraphZen.LanguageModel.ObjectFieldSyntax[] fields ) => new ObjectValueSyntax(fields);
#endregion
#region SyntaxFactoryGenerator
public static ObjectValueSyntax ObjectValue( System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.ObjectFieldSyntax> fields ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new ObjectValueSyntax(fields, location);
#endregion
#region SyntaxFactoryGenerator
public static OperationDefinitionSyntax OperationDefinition( GraphZen.LanguageModel.OperationType type ,  GraphZen.LanguageModel.SelectionSetSyntax selectionSet ,  GraphZen.LanguageModel.NameSyntax? name  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.VariableDefinitionSyntax>? variableDefinitions  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new OperationDefinitionSyntax(type, selectionSet, name, variableDefinitions, directives, location);
#endregion
#region SyntaxFactoryGenerator
public static OperationTypeDefinitionSyntax OperationTypeDefinition( GraphZen.LanguageModel.OperationType operationType ,  GraphZen.LanguageModel.NamedTypeSyntax type ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new OperationTypeDefinitionSyntax(operationType, type, location);
#endregion
#region SyntaxFactoryGenerator
public static PunctuatorSyntax Punctuator( GraphZen.LanguageModel.SyntaxLocation location ) => new PunctuatorSyntax(location);
#endregion
#region SyntaxFactoryGenerator
public static ScalarTypeDefinitionSyntax ScalarTypeDefinition( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.StringValueSyntax? description  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new ScalarTypeDefinitionSyntax(name, description, directives, location);
#endregion
#region SyntaxFactoryGenerator
public static ScalarTypeExtensionSyntax ScalarTypeExtension( GraphZen.LanguageModel.NameSyntax name ,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax> directives ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new ScalarTypeExtensionSyntax(name, directives, location);
#endregion
#region SyntaxFactoryGenerator
public static SchemaDefinitionSyntax SchemaDefinition( System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.OperationTypeDefinitionSyntax>? operationTypes  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new SchemaDefinitionSyntax(operationTypes, directives, location);
#endregion
#region SyntaxFactoryGenerator
public static SchemaExtensionSyntax SchemaExtension( System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.OperationTypeDefinitionSyntax>? operationTypes  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new SchemaExtensionSyntax(directives, operationTypes, location);
#endregion
#region SyntaxFactoryGenerator
public static SelectionSetSyntax SelectionSet( System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.SelectionSyntax> selections ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new SelectionSetSyntax(selections, location);
#endregion
#region SyntaxFactoryGenerator
public static SelectionSetSyntax SelectionSet(params GraphZen.LanguageModel.SelectionSyntax[] selections ) => new SelectionSetSyntax(selections);
#endregion
#region SyntaxFactoryGenerator
public static StringValueSyntax StringValue( System.String value ,  System.Boolean isBlockString  = false,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new StringValueSyntax(value, isBlockString, location);
#endregion
#region SyntaxFactoryGenerator
public static UnionTypeDefinitionSyntax UnionTypeDefinition( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.StringValueSyntax? description  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.NamedTypeSyntax>? types  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new UnionTypeDefinitionSyntax(name, description, directives, types, location);
#endregion
#region SyntaxFactoryGenerator
public static UnionTypeExtensionSyntax UnionTypeExtension( GraphZen.LanguageModel.NameSyntax name ,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives  = null,  System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.NamedTypeSyntax>? types  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new UnionTypeExtensionSyntax(name, directives, types, location);
#endregion
#region SyntaxFactoryGenerator
public static VariableDefinitionSyntax VariableDefinition( GraphZen.LanguageModel.VariableSyntax variable ,  GraphZen.LanguageModel.TypeSyntax type ,  GraphZen.LanguageModel.ValueSyntax? defaultValue  = null,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new VariableDefinitionSyntax(variable, type, defaultValue, location);
#endregion
#region SyntaxFactoryGenerator
public static VariableSyntax Variable( GraphZen.LanguageModel.NameSyntax name ,  GraphZen.LanguageModel.SyntaxLocation? location  = null) => new VariableSyntax(name, location);
#endregion
}
}
// Source Hash Code: 7105663997576846713