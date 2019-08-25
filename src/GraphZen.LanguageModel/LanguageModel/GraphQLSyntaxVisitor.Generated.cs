// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    public abstract partial class GraphQLSyntaxVisitor
    {
        /// <summary>Called when the visitor enters a <see cref="ArgumentSyntax" /> node.</summary>
        public virtual void EnterArgument( ArgumentSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ArgumentSyntax" /> node.</summary>
        public virtual void LeaveArgument( ArgumentSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="BooleanValueSyntax" /> node.</summary>
        public virtual void EnterBooleanValue( BooleanValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="BooleanValueSyntax" /> node.</summary>
        public virtual void LeaveBooleanValue( BooleanValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="DirectiveDefinitionSyntax" /> node.</summary>
        public virtual void EnterDirectiveDefinition( DirectiveDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="DirectiveDefinitionSyntax" /> node.</summary>
        public virtual void LeaveDirectiveDefinition( DirectiveDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="DirectiveSyntax" /> node.</summary>
        public virtual void EnterDirective( DirectiveSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="DirectiveSyntax" /> node.</summary>
        public virtual void LeaveDirective( DirectiveSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="DocumentSyntax" /> node.</summary>
        public virtual void EnterDocument( DocumentSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="DocumentSyntax" /> node.</summary>
        public virtual void LeaveDocument( DocumentSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumTypeDefinitionSyntax" /> node.</summary>
        public virtual void EnterEnumTypeDefinition( EnumTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumTypeDefinitionSyntax" /> node.</summary>
        public virtual void LeaveEnumTypeDefinition( EnumTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumTypeExtensionSyntax" /> node.</summary>
        public virtual void EnterEnumTypeExtension( EnumTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumTypeExtensionSyntax" /> node.</summary>
        public virtual void LeaveEnumTypeExtension( EnumTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumValueDefinitionSyntax" /> node.</summary>
        public virtual void EnterEnumValueDefinition( EnumValueDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumValueDefinitionSyntax" /> node.</summary>
        public virtual void LeaveEnumValueDefinition( EnumValueDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumValueSyntax" /> node.</summary>
        public virtual void EnterEnumValue( EnumValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumValueSyntax" /> node.</summary>
        public virtual void LeaveEnumValue( EnumValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FieldDefinitionSyntax" /> node.</summary>
        public virtual void EnterFieldDefinition( FieldDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FieldDefinitionSyntax" /> node.</summary>
        public virtual void LeaveFieldDefinition( FieldDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FieldSyntax" /> node.</summary>
        public virtual void EnterField( FieldSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FieldSyntax" /> node.</summary>
        public virtual void LeaveField( FieldSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FloatValueSyntax" /> node.</summary>
        public virtual void EnterFloatValue( FloatValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FloatValueSyntax" /> node.</summary>
        public virtual void LeaveFloatValue( FloatValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FragmentDefinitionSyntax" /> node.</summary>
        public virtual void EnterFragmentDefinition( FragmentDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FragmentDefinitionSyntax" /> node.</summary>
        public virtual void LeaveFragmentDefinition( FragmentDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FragmentSpreadSyntax" /> node.</summary>
        public virtual void EnterFragmentSpread( FragmentSpreadSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FragmentSpreadSyntax" /> node.</summary>
        public virtual void LeaveFragmentSpread( FragmentSpreadSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InlineFragmentSyntax" /> node.</summary>
        public virtual void EnterInlineFragment( InlineFragmentSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InlineFragmentSyntax" /> node.</summary>
        public virtual void LeaveInlineFragment( InlineFragmentSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InputObjectTypeDefinitionSyntax" /> node.</summary>
        public virtual void EnterInputObjectTypeDefinition( InputObjectTypeDefinitionSyntax node) =>
            OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InputObjectTypeDefinitionSyntax" /> node.</summary>
        public virtual void LeaveInputObjectTypeDefinition( InputObjectTypeDefinitionSyntax node) =>
            OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InputObjectTypeExtensionSyntax" /> node.</summary>
        public virtual void EnterInputObjectTypeExtension( InputObjectTypeExtensionSyntax node) =>
            OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InputObjectTypeExtensionSyntax" /> node.</summary>
        public virtual void LeaveInputObjectTypeExtension( InputObjectTypeExtensionSyntax node) =>
            OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InputValueDefinitionSyntax" /> node.</summary>
        public virtual void EnterInputValueDefinition( InputValueDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InputValueDefinitionSyntax" /> node.</summary>
        public virtual void LeaveInputValueDefinition( InputValueDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InterfaceTypeDefinitionSyntax" /> node.</summary>
        public virtual void EnterInterfaceTypeDefinition( InterfaceTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InterfaceTypeDefinitionSyntax" /> node.</summary>
        public virtual void LeaveInterfaceTypeDefinition( InterfaceTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InterfaceTypeExtensionSyntax" /> node.</summary>
        public virtual void EnterInterfaceTypeExtension( InterfaceTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InterfaceTypeExtensionSyntax" /> node.</summary>
        public virtual void LeaveInterfaceTypeExtension( InterfaceTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="IntValueSyntax" /> node.</summary>
        public virtual void EnterIntValue( IntValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="IntValueSyntax" /> node.</summary>
        public virtual void LeaveIntValue( IntValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ListTypeSyntax" /> node.</summary>
        public virtual void EnterListType( ListTypeSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ListTypeSyntax" /> node.</summary>
        public virtual void LeaveListType( ListTypeSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ListValueSyntax" /> node.</summary>
        public virtual void EnterListValue( ListValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ListValueSyntax" /> node.</summary>
        public virtual void LeaveListValue( ListValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NamedTypeSyntax" /> node.</summary>
        public virtual void EnterNamedType( NamedTypeSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NamedTypeSyntax" /> node.</summary>
        public virtual void LeaveNamedType( NamedTypeSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NameSyntax" /> node.</summary>
        public virtual void EnterName( NameSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NameSyntax" /> node.</summary>
        public virtual void LeaveName( NameSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NonNullTypeSyntax" /> node.</summary>
        public virtual void EnterNonNullType( NonNullTypeSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NonNullTypeSyntax" /> node.</summary>
        public virtual void LeaveNonNullType( NonNullTypeSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NullValueSyntax" /> node.</summary>
        public virtual void EnterNullValue( NullValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NullValueSyntax" /> node.</summary>
        public virtual void LeaveNullValue( NullValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectFieldSyntax" /> node.</summary>
        public virtual void EnterObjectField( ObjectFieldSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectFieldSyntax" /> node.</summary>
        public virtual void LeaveObjectField( ObjectFieldSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectTypeDefinitionSyntax" /> node.</summary>
        public virtual void EnterObjectTypeDefinition( ObjectTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectTypeDefinitionSyntax" /> node.</summary>
        public virtual void LeaveObjectTypeDefinition( ObjectTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectTypeExtensionSyntax" /> node.</summary>
        public virtual void EnterObjectTypeExtension( ObjectTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectTypeExtensionSyntax" /> node.</summary>
        public virtual void LeaveObjectTypeExtension( ObjectTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectValueSyntax" /> node.</summary>
        public virtual void EnterObjectValue( ObjectValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectValueSyntax" /> node.</summary>
        public virtual void LeaveObjectValue( ObjectValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="OperationDefinitionSyntax" /> node.</summary>
        public virtual void EnterOperationDefinition( OperationDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="OperationDefinitionSyntax" /> node.</summary>
        public virtual void LeaveOperationDefinition( OperationDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="OperationTypeDefinitionSyntax" /> node.</summary>
        public virtual void EnterOperationTypeDefinition( OperationTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="OperationTypeDefinitionSyntax" /> node.</summary>
        public virtual void LeaveOperationTypeDefinition( OperationTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="PunctuatorSyntax" /> node.</summary>
        public virtual void EnterPunctuator( PunctuatorSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="PunctuatorSyntax" /> node.</summary>
        public virtual void LeavePunctuator( PunctuatorSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ScalarTypeDefinitionSyntax" /> node.</summary>
        public virtual void EnterScalarTypeDefinition( ScalarTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ScalarTypeDefinitionSyntax" /> node.</summary>
        public virtual void LeaveScalarTypeDefinition( ScalarTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ScalarTypeExtensionSyntax" /> node.</summary>
        public virtual void EnterScalarTypeExtension( ScalarTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ScalarTypeExtensionSyntax" /> node.</summary>
        public virtual void LeaveScalarTypeExtension( ScalarTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="SchemaDefinitionSyntax" /> node.</summary>
        public virtual void EnterSchemaDefinition( SchemaDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="SchemaDefinitionSyntax" /> node.</summary>
        public virtual void LeaveSchemaDefinition( SchemaDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="SchemaExtensionSyntax" /> node.</summary>
        public virtual void EnterSchemaExtension( SchemaExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="SchemaExtensionSyntax" /> node.</summary>
        public virtual void LeaveSchemaExtension( SchemaExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="SelectionSetSyntax" /> node.</summary>
        public virtual void EnterSelectionSet( SelectionSetSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="SelectionSetSyntax" /> node.</summary>
        public virtual void LeaveSelectionSet( SelectionSetSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="StringValueSyntax" /> node.</summary>
        public virtual void EnterStringValue( StringValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="StringValueSyntax" /> node.</summary>
        public virtual void LeaveStringValue( StringValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="UnionTypeDefinitionSyntax" /> node.</summary>
        public virtual void EnterUnionTypeDefinition( UnionTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="UnionTypeDefinitionSyntax" /> node.</summary>
        public virtual void LeaveUnionTypeDefinition( UnionTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="UnionTypeExtensionSyntax" /> node.</summary>
        public virtual void EnterUnionTypeExtension( UnionTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="UnionTypeExtensionSyntax" /> node.</summary>
        public virtual void LeaveUnionTypeExtension( UnionTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="VariableDefinitionSyntax" /> node.</summary>
        public virtual void EnterVariableDefinition( VariableDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="VariableDefinitionSyntax" /> node.</summary>
        public virtual void LeaveVariableDefinition( VariableDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="VariableSyntax" /> node.</summary>
        public virtual void EnterVariable( VariableSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="VariableSyntax" /> node.</summary>
        public virtual void LeaveVariable( VariableSyntax node) => OnLeave(node);
    }

    public abstract partial class GraphQLSyntaxVisitor<TResult>
    {
        /// <summary>Called when the visitor enters a <see cref="ArgumentSyntax" /> node.</summary>
        public virtual TResult EnterArgument( ArgumentSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ArgumentSyntax" /> node.</summary>
        public virtual TResult LeaveArgument( ArgumentSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="BooleanValueSyntax" /> node.</summary>
        public virtual TResult EnterBooleanValue( BooleanValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="BooleanValueSyntax" /> node.</summary>
        public virtual TResult LeaveBooleanValue( BooleanValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="DirectiveDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterDirectiveDefinition( DirectiveDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="DirectiveDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveDirectiveDefinition( DirectiveDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="DirectiveSyntax" /> node.</summary>
        public virtual TResult EnterDirective( DirectiveSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="DirectiveSyntax" /> node.</summary>
        public virtual TResult LeaveDirective( DirectiveSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="DocumentSyntax" /> node.</summary>
        public virtual TResult EnterDocument( DocumentSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="DocumentSyntax" /> node.</summary>
        public virtual TResult LeaveDocument( DocumentSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterEnumTypeDefinition( EnumTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveEnumTypeDefinition( EnumTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumTypeExtensionSyntax" /> node.</summary>
        public virtual TResult EnterEnumTypeExtension( EnumTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumTypeExtensionSyntax" /> node.</summary>
        public virtual TResult LeaveEnumTypeExtension( EnumTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumValueDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterEnumValueDefinition( EnumValueDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumValueDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveEnumValueDefinition( EnumValueDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumValueSyntax" /> node.</summary>
        public virtual TResult EnterEnumValue( EnumValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumValueSyntax" /> node.</summary>
        public virtual TResult LeaveEnumValue( EnumValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FieldDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterFieldDefinition( FieldDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FieldDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveFieldDefinition( FieldDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FieldSyntax" /> node.</summary>
        public virtual TResult EnterField( FieldSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FieldSyntax" /> node.</summary>
        public virtual TResult LeaveField( FieldSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FloatValueSyntax" /> node.</summary>
        public virtual TResult EnterFloatValue( FloatValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FloatValueSyntax" /> node.</summary>
        public virtual TResult LeaveFloatValue( FloatValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FragmentDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterFragmentDefinition( FragmentDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FragmentDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveFragmentDefinition( FragmentDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FragmentSpreadSyntax" /> node.</summary>
        public virtual TResult EnterFragmentSpread( FragmentSpreadSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FragmentSpreadSyntax" /> node.</summary>
        public virtual TResult LeaveFragmentSpread( FragmentSpreadSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InlineFragmentSyntax" /> node.</summary>
        public virtual TResult EnterInlineFragment( InlineFragmentSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InlineFragmentSyntax" /> node.</summary>
        public virtual TResult LeaveInlineFragment( InlineFragmentSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InputObjectTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterInputObjectTypeDefinition( InputObjectTypeDefinitionSyntax node) =>
            OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InputObjectTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveInputObjectTypeDefinition( InputObjectTypeDefinitionSyntax node) =>
            OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InputObjectTypeExtensionSyntax" /> node.</summary>
        public virtual TResult EnterInputObjectTypeExtension( InputObjectTypeExtensionSyntax node) =>
            OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InputObjectTypeExtensionSyntax" /> node.</summary>
        public virtual TResult LeaveInputObjectTypeExtension( InputObjectTypeExtensionSyntax node) =>
            OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InputValueDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterInputValueDefinition( InputValueDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InputValueDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveInputValueDefinition( InputValueDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InterfaceTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterInterfaceTypeDefinition( InterfaceTypeDefinitionSyntax node) =>
            OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InterfaceTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveInterfaceTypeDefinition( InterfaceTypeDefinitionSyntax node) =>
            OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InterfaceTypeExtensionSyntax" /> node.</summary>
        public virtual TResult EnterInterfaceTypeExtension( InterfaceTypeExtensionSyntax node) =>
            OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InterfaceTypeExtensionSyntax" /> node.</summary>
        public virtual TResult LeaveInterfaceTypeExtension( InterfaceTypeExtensionSyntax node) =>
            OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="IntValueSyntax" /> node.</summary>
        public virtual TResult EnterIntValue( IntValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="IntValueSyntax" /> node.</summary>
        public virtual TResult LeaveIntValue( IntValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ListTypeSyntax" /> node.</summary>
        public virtual TResult EnterListType( ListTypeSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ListTypeSyntax" /> node.</summary>
        public virtual TResult LeaveListType( ListTypeSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ListValueSyntax" /> node.</summary>
        public virtual TResult EnterListValue( ListValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ListValueSyntax" /> node.</summary>
        public virtual TResult LeaveListValue( ListValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NamedTypeSyntax" /> node.</summary>
        public virtual TResult EnterNamedType( NamedTypeSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NamedTypeSyntax" /> node.</summary>
        public virtual TResult LeaveNamedType( NamedTypeSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NameSyntax" /> node.</summary>
        public virtual TResult EnterName( NameSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NameSyntax" /> node.</summary>
        public virtual TResult LeaveName( NameSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NonNullTypeSyntax" /> node.</summary>
        public virtual TResult EnterNonNullType( NonNullTypeSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NonNullTypeSyntax" /> node.</summary>
        public virtual TResult LeaveNonNullType( NonNullTypeSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NullValueSyntax" /> node.</summary>
        public virtual TResult EnterNullValue( NullValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NullValueSyntax" /> node.</summary>
        public virtual TResult LeaveNullValue( NullValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectFieldSyntax" /> node.</summary>
        public virtual TResult EnterObjectField( ObjectFieldSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectFieldSyntax" /> node.</summary>
        public virtual TResult LeaveObjectField( ObjectFieldSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterObjectTypeDefinition( ObjectTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveObjectTypeDefinition( ObjectTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectTypeExtensionSyntax" /> node.</summary>
        public virtual TResult EnterObjectTypeExtension( ObjectTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectTypeExtensionSyntax" /> node.</summary>
        public virtual TResult LeaveObjectTypeExtension( ObjectTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectValueSyntax" /> node.</summary>
        public virtual TResult EnterObjectValue( ObjectValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectValueSyntax" /> node.</summary>
        public virtual TResult LeaveObjectValue( ObjectValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="OperationDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterOperationDefinition( OperationDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="OperationDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveOperationDefinition( OperationDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="OperationTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterOperationTypeDefinition( OperationTypeDefinitionSyntax node) =>
            OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="OperationTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveOperationTypeDefinition( OperationTypeDefinitionSyntax node) =>
            OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="PunctuatorSyntax" /> node.</summary>
        public virtual TResult EnterPunctuator( PunctuatorSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="PunctuatorSyntax" /> node.</summary>
        public virtual TResult LeavePunctuator( PunctuatorSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ScalarTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterScalarTypeDefinition( ScalarTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ScalarTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveScalarTypeDefinition( ScalarTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ScalarTypeExtensionSyntax" /> node.</summary>
        public virtual TResult EnterScalarTypeExtension( ScalarTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ScalarTypeExtensionSyntax" /> node.</summary>
        public virtual TResult LeaveScalarTypeExtension( ScalarTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="SchemaDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterSchemaDefinition( SchemaDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="SchemaDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveSchemaDefinition( SchemaDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="SchemaExtensionSyntax" /> node.</summary>
        public virtual TResult EnterSchemaExtension( SchemaExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="SchemaExtensionSyntax" /> node.</summary>
        public virtual TResult LeaveSchemaExtension( SchemaExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="SelectionSetSyntax" /> node.</summary>
        public virtual TResult EnterSelectionSet( SelectionSetSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="SelectionSetSyntax" /> node.</summary>
        public virtual TResult LeaveSelectionSet( SelectionSetSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="StringValueSyntax" /> node.</summary>
        public virtual TResult EnterStringValue( StringValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="StringValueSyntax" /> node.</summary>
        public virtual TResult LeaveStringValue( StringValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="UnionTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterUnionTypeDefinition( UnionTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="UnionTypeDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveUnionTypeDefinition( UnionTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="UnionTypeExtensionSyntax" /> node.</summary>
        public virtual TResult EnterUnionTypeExtension( UnionTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="UnionTypeExtensionSyntax" /> node.</summary>
        public virtual TResult LeaveUnionTypeExtension( UnionTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="VariableDefinitionSyntax" /> node.</summary>
        public virtual TResult EnterVariableDefinition( VariableDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="VariableDefinitionSyntax" /> node.</summary>
        public virtual TResult LeaveVariableDefinition( VariableDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="VariableSyntax" /> node.</summary>
        public virtual TResult EnterVariable( VariableSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="VariableSyntax" /> node.</summary>
        public virtual TResult LeaveVariable( VariableSyntax node) => OnLeave(node);
    }
}