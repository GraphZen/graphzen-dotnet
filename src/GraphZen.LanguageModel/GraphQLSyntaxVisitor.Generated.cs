using GraphZen.Infrastructure;  
  
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    public abstract partial class GraphQLSyntaxVisitor
    {
        /// <summary>Called when the visitor enters a <see cref="ArgumentSyntax"/> node.</summary>
        public virtual void EnterArgument([NotNull] ArgumentSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ArgumentSyntax"/> node.</summary>
        public virtual void LeaveArgument([NotNull] ArgumentSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="BooleanValueSyntax"/> node.</summary>
        public virtual void EnterBooleanValue([NotNull] BooleanValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="BooleanValueSyntax"/> node.</summary>
        public virtual void LeaveBooleanValue([NotNull] BooleanValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="DirectiveDefinitionSyntax"/> node.</summary>
        public virtual void EnterDirectiveDefinition([NotNull] DirectiveDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="DirectiveDefinitionSyntax"/> node.</summary>
        public virtual void LeaveDirectiveDefinition([NotNull] DirectiveDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="DirectiveSyntax"/> node.</summary>
        public virtual void EnterDirective([NotNull] DirectiveSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="DirectiveSyntax"/> node.</summary>
        public virtual void LeaveDirective([NotNull] DirectiveSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="DocumentSyntax"/> node.</summary>
        public virtual void EnterDocument([NotNull] DocumentSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="DocumentSyntax"/> node.</summary>
        public virtual void LeaveDocument([NotNull] DocumentSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumTypeDefinitionSyntax"/> node.</summary>
        public virtual void EnterEnumTypeDefinition([NotNull] EnumTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumTypeDefinitionSyntax"/> node.</summary>
        public virtual void LeaveEnumTypeDefinition([NotNull] EnumTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumTypeExtensionSyntax"/> node.</summary>
        public virtual void EnterEnumTypeExtension([NotNull] EnumTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumTypeExtensionSyntax"/> node.</summary>
        public virtual void LeaveEnumTypeExtension([NotNull] EnumTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumValueDefinitionSyntax"/> node.</summary>
        public virtual void EnterEnumValueDefinition([NotNull] EnumValueDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumValueDefinitionSyntax"/> node.</summary>
        public virtual void LeaveEnumValueDefinition([NotNull] EnumValueDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumValueSyntax"/> node.</summary>
        public virtual void EnterEnumValue([NotNull] EnumValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumValueSyntax"/> node.</summary>
        public virtual void LeaveEnumValue([NotNull] EnumValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FieldDefinitionSyntax"/> node.</summary>
        public virtual void EnterFieldDefinition([NotNull] FieldDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FieldDefinitionSyntax"/> node.</summary>
        public virtual void LeaveFieldDefinition([NotNull] FieldDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FieldSyntax"/> node.</summary>
        public virtual void EnterField([NotNull] FieldSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FieldSyntax"/> node.</summary>
        public virtual void LeaveField([NotNull] FieldSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FloatValueSyntax"/> node.</summary>
        public virtual void EnterFloatValue([NotNull] FloatValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FloatValueSyntax"/> node.</summary>
        public virtual void LeaveFloatValue([NotNull] FloatValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FragmentDefinitionSyntax"/> node.</summary>
        public virtual void EnterFragmentDefinition([NotNull] FragmentDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FragmentDefinitionSyntax"/> node.</summary>
        public virtual void LeaveFragmentDefinition([NotNull] FragmentDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FragmentSpreadSyntax"/> node.</summary>
        public virtual void EnterFragmentSpread([NotNull] FragmentSpreadSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FragmentSpreadSyntax"/> node.</summary>
        public virtual void LeaveFragmentSpread([NotNull] FragmentSpreadSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InlineFragmentSyntax"/> node.</summary>
        public virtual void EnterInlineFragment([NotNull] InlineFragmentSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InlineFragmentSyntax"/> node.</summary>
        public virtual void LeaveInlineFragment([NotNull] InlineFragmentSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InputObjectTypeDefinitionSyntax"/> node.</summary>
        public virtual void EnterInputObjectTypeDefinition([NotNull] InputObjectTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InputObjectTypeDefinitionSyntax"/> node.</summary>
        public virtual void LeaveInputObjectTypeDefinition([NotNull] InputObjectTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InputObjectTypeExtensionSyntax"/> node.</summary>
        public virtual void EnterInputObjectTypeExtension([NotNull] InputObjectTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InputObjectTypeExtensionSyntax"/> node.</summary>
        public virtual void LeaveInputObjectTypeExtension([NotNull] InputObjectTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InputValueDefinitionSyntax"/> node.</summary>
        public virtual void EnterInputValueDefinition([NotNull] InputValueDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InputValueDefinitionSyntax"/> node.</summary>
        public virtual void LeaveInputValueDefinition([NotNull] InputValueDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InterfaceTypeDefinitionSyntax"/> node.</summary>
        public virtual void EnterInterfaceTypeDefinition([NotNull] InterfaceTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InterfaceTypeDefinitionSyntax"/> node.</summary>
        public virtual void LeaveInterfaceTypeDefinition([NotNull] InterfaceTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InterfaceTypeExtensionSyntax"/> node.</summary>
        public virtual void EnterInterfaceTypeExtension([NotNull] InterfaceTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InterfaceTypeExtensionSyntax"/> node.</summary>
        public virtual void LeaveInterfaceTypeExtension([NotNull] InterfaceTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="IntValueSyntax"/> node.</summary>
        public virtual void EnterIntValue([NotNull] IntValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="IntValueSyntax"/> node.</summary>
        public virtual void LeaveIntValue([NotNull] IntValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ListTypeSyntax"/> node.</summary>
        public virtual void EnterListType([NotNull] ListTypeSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ListTypeSyntax"/> node.</summary>
        public virtual void LeaveListType([NotNull] ListTypeSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ListValueSyntax"/> node.</summary>
        public virtual void EnterListValue([NotNull] ListValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ListValueSyntax"/> node.</summary>
        public virtual void LeaveListValue([NotNull] ListValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NamedTypeSyntax"/> node.</summary>
        public virtual void EnterNamedType([NotNull] NamedTypeSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NamedTypeSyntax"/> node.</summary>
        public virtual void LeaveNamedType([NotNull] NamedTypeSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NameSyntax"/> node.</summary>
        public virtual void EnterName([NotNull] NameSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NameSyntax"/> node.</summary>
        public virtual void LeaveName([NotNull] NameSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NonNullTypeSyntax"/> node.</summary>
        public virtual void EnterNonNullType([NotNull] NonNullTypeSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NonNullTypeSyntax"/> node.</summary>
        public virtual void LeaveNonNullType([NotNull] NonNullTypeSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NullValueSyntax"/> node.</summary>
        public virtual void EnterNullValue([NotNull] NullValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NullValueSyntax"/> node.</summary>
        public virtual void LeaveNullValue([NotNull] NullValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectFieldSyntax"/> node.</summary>
        public virtual void EnterObjectField([NotNull] ObjectFieldSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectFieldSyntax"/> node.</summary>
        public virtual void LeaveObjectField([NotNull] ObjectFieldSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectTypeDefinitionSyntax"/> node.</summary>
        public virtual void EnterObjectTypeDefinition([NotNull] ObjectTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectTypeDefinitionSyntax"/> node.</summary>
        public virtual void LeaveObjectTypeDefinition([NotNull] ObjectTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectTypeExtensionSyntax"/> node.</summary>
        public virtual void EnterObjectTypeExtension([NotNull] ObjectTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectTypeExtensionSyntax"/> node.</summary>
        public virtual void LeaveObjectTypeExtension([NotNull] ObjectTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectValueSyntax"/> node.</summary>
        public virtual void EnterObjectValue([NotNull] ObjectValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectValueSyntax"/> node.</summary>
        public virtual void LeaveObjectValue([NotNull] ObjectValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="OperationDefinitionSyntax"/> node.</summary>
        public virtual void EnterOperationDefinition([NotNull] OperationDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="OperationDefinitionSyntax"/> node.</summary>
        public virtual void LeaveOperationDefinition([NotNull] OperationDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="OperationTypeDefinitionSyntax"/> node.</summary>
        public virtual void EnterOperationTypeDefinition([NotNull] OperationTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="OperationTypeDefinitionSyntax"/> node.</summary>
        public virtual void LeaveOperationTypeDefinition([NotNull] OperationTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="PunctuatorSyntax"/> node.</summary>
        public virtual void EnterPunctuator([NotNull] PunctuatorSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="PunctuatorSyntax"/> node.</summary>
        public virtual void LeavePunctuator([NotNull] PunctuatorSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ScalarTypeDefinitionSyntax"/> node.</summary>
        public virtual void EnterScalarTypeDefinition([NotNull] ScalarTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ScalarTypeDefinitionSyntax"/> node.</summary>
        public virtual void LeaveScalarTypeDefinition([NotNull] ScalarTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ScalarTypeExtensionSyntax"/> node.</summary>
        public virtual void EnterScalarTypeExtension([NotNull] ScalarTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ScalarTypeExtensionSyntax"/> node.</summary>
        public virtual void LeaveScalarTypeExtension([NotNull] ScalarTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="SchemaDefinitionSyntax"/> node.</summary>
        public virtual void EnterSchemaDefinition([NotNull] SchemaDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="SchemaDefinitionSyntax"/> node.</summary>
        public virtual void LeaveSchemaDefinition([NotNull] SchemaDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="SchemaExtensionSyntax"/> node.</summary>
        public virtual void EnterSchemaExtension([NotNull] SchemaExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="SchemaExtensionSyntax"/> node.</summary>
        public virtual void LeaveSchemaExtension([NotNull] SchemaExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="SelectionSetSyntax"/> node.</summary>
        public virtual void EnterSelectionSet([NotNull] SelectionSetSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="SelectionSetSyntax"/> node.</summary>
        public virtual void LeaveSelectionSet([NotNull] SelectionSetSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="StringValueSyntax"/> node.</summary>
        public virtual void EnterStringValue([NotNull] StringValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="StringValueSyntax"/> node.</summary>
        public virtual void LeaveStringValue([NotNull] StringValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="UnionTypeDefinitionSyntax"/> node.</summary>
        public virtual void EnterUnionTypeDefinition([NotNull] UnionTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="UnionTypeDefinitionSyntax"/> node.</summary>
        public virtual void LeaveUnionTypeDefinition([NotNull] UnionTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="UnionTypeExtensionSyntax"/> node.</summary>
        public virtual void EnterUnionTypeExtension([NotNull] UnionTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="UnionTypeExtensionSyntax"/> node.</summary>
        public virtual void LeaveUnionTypeExtension([NotNull] UnionTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="VariableDefinitionSyntax"/> node.</summary>
        public virtual void EnterVariableDefinition([NotNull] VariableDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="VariableDefinitionSyntax"/> node.</summary>
        public virtual void LeaveVariableDefinition([NotNull] VariableDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="VariableSyntax"/> node.</summary>
        public virtual void EnterVariable([NotNull] VariableSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="VariableSyntax"/> node.</summary>
        public virtual void LeaveVariable([NotNull] VariableSyntax node) => OnLeave(node);
    }

    public abstract partial class GraphQLSyntaxVisitor<TResult>
    {
        /// <summary>Called when the visitor enters a <see cref="ArgumentSyntax"/> node.</summary>
        public virtual TResult EnterArgument([NotNull] ArgumentSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ArgumentSyntax"/> node.</summary>
        public virtual TResult LeaveArgument([NotNull] ArgumentSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="BooleanValueSyntax"/> node.</summary>
        public virtual TResult EnterBooleanValue([NotNull] BooleanValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="BooleanValueSyntax"/> node.</summary>
        public virtual TResult LeaveBooleanValue([NotNull] BooleanValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="DirectiveDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterDirectiveDefinition([NotNull] DirectiveDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="DirectiveDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveDirectiveDefinition([NotNull] DirectiveDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="DirectiveSyntax"/> node.</summary>
        public virtual TResult EnterDirective([NotNull] DirectiveSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="DirectiveSyntax"/> node.</summary>
        public virtual TResult LeaveDirective([NotNull] DirectiveSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="DocumentSyntax"/> node.</summary>
        public virtual TResult EnterDocument([NotNull] DocumentSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="DocumentSyntax"/> node.</summary>
        public virtual TResult LeaveDocument([NotNull] DocumentSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterEnumTypeDefinition([NotNull] EnumTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveEnumTypeDefinition([NotNull] EnumTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumTypeExtensionSyntax"/> node.</summary>
        public virtual TResult EnterEnumTypeExtension([NotNull] EnumTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumTypeExtensionSyntax"/> node.</summary>
        public virtual TResult LeaveEnumTypeExtension([NotNull] EnumTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumValueDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterEnumValueDefinition([NotNull] EnumValueDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumValueDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveEnumValueDefinition([NotNull] EnumValueDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="EnumValueSyntax"/> node.</summary>
        public virtual TResult EnterEnumValue([NotNull] EnumValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="EnumValueSyntax"/> node.</summary>
        public virtual TResult LeaveEnumValue([NotNull] EnumValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FieldDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterFieldDefinition([NotNull] FieldDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FieldDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveFieldDefinition([NotNull] FieldDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FieldSyntax"/> node.</summary>
        public virtual TResult EnterField([NotNull] FieldSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FieldSyntax"/> node.</summary>
        public virtual TResult LeaveField([NotNull] FieldSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FloatValueSyntax"/> node.</summary>
        public virtual TResult EnterFloatValue([NotNull] FloatValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FloatValueSyntax"/> node.</summary>
        public virtual TResult LeaveFloatValue([NotNull] FloatValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FragmentDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterFragmentDefinition([NotNull] FragmentDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FragmentDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveFragmentDefinition([NotNull] FragmentDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="FragmentSpreadSyntax"/> node.</summary>
        public virtual TResult EnterFragmentSpread([NotNull] FragmentSpreadSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="FragmentSpreadSyntax"/> node.</summary>
        public virtual TResult LeaveFragmentSpread([NotNull] FragmentSpreadSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InlineFragmentSyntax"/> node.</summary>
        public virtual TResult EnterInlineFragment([NotNull] InlineFragmentSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InlineFragmentSyntax"/> node.</summary>
        public virtual TResult LeaveInlineFragment([NotNull] InlineFragmentSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InputObjectTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterInputObjectTypeDefinition([NotNull] InputObjectTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InputObjectTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveInputObjectTypeDefinition([NotNull] InputObjectTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InputObjectTypeExtensionSyntax"/> node.</summary>
        public virtual TResult EnterInputObjectTypeExtension([NotNull] InputObjectTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InputObjectTypeExtensionSyntax"/> node.</summary>
        public virtual TResult LeaveInputObjectTypeExtension([NotNull] InputObjectTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InputValueDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterInputValueDefinition([NotNull] InputValueDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InputValueDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveInputValueDefinition([NotNull] InputValueDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InterfaceTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterInterfaceTypeDefinition([NotNull] InterfaceTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InterfaceTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveInterfaceTypeDefinition([NotNull] InterfaceTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="InterfaceTypeExtensionSyntax"/> node.</summary>
        public virtual TResult EnterInterfaceTypeExtension([NotNull] InterfaceTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="InterfaceTypeExtensionSyntax"/> node.</summary>
        public virtual TResult LeaveInterfaceTypeExtension([NotNull] InterfaceTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="IntValueSyntax"/> node.</summary>
        public virtual TResult EnterIntValue([NotNull] IntValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="IntValueSyntax"/> node.</summary>
        public virtual TResult LeaveIntValue([NotNull] IntValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ListTypeSyntax"/> node.</summary>
        public virtual TResult EnterListType([NotNull] ListTypeSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ListTypeSyntax"/> node.</summary>
        public virtual TResult LeaveListType([NotNull] ListTypeSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ListValueSyntax"/> node.</summary>
        public virtual TResult EnterListValue([NotNull] ListValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ListValueSyntax"/> node.</summary>
        public virtual TResult LeaveListValue([NotNull] ListValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NamedTypeSyntax"/> node.</summary>
        public virtual TResult EnterNamedType([NotNull] NamedTypeSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NamedTypeSyntax"/> node.</summary>
        public virtual TResult LeaveNamedType([NotNull] NamedTypeSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NameSyntax"/> node.</summary>
        public virtual TResult EnterName([NotNull] NameSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NameSyntax"/> node.</summary>
        public virtual TResult LeaveName([NotNull] NameSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NonNullTypeSyntax"/> node.</summary>
        public virtual TResult EnterNonNullType([NotNull] NonNullTypeSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NonNullTypeSyntax"/> node.</summary>
        public virtual TResult LeaveNonNullType([NotNull] NonNullTypeSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="NullValueSyntax"/> node.</summary>
        public virtual TResult EnterNullValue([NotNull] NullValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="NullValueSyntax"/> node.</summary>
        public virtual TResult LeaveNullValue([NotNull] NullValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectFieldSyntax"/> node.</summary>
        public virtual TResult EnterObjectField([NotNull] ObjectFieldSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectFieldSyntax"/> node.</summary>
        public virtual TResult LeaveObjectField([NotNull] ObjectFieldSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterObjectTypeDefinition([NotNull] ObjectTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveObjectTypeDefinition([NotNull] ObjectTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectTypeExtensionSyntax"/> node.</summary>
        public virtual TResult EnterObjectTypeExtension([NotNull] ObjectTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectTypeExtensionSyntax"/> node.</summary>
        public virtual TResult LeaveObjectTypeExtension([NotNull] ObjectTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ObjectValueSyntax"/> node.</summary>
        public virtual TResult EnterObjectValue([NotNull] ObjectValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ObjectValueSyntax"/> node.</summary>
        public virtual TResult LeaveObjectValue([NotNull] ObjectValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="OperationDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterOperationDefinition([NotNull] OperationDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="OperationDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveOperationDefinition([NotNull] OperationDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="OperationTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterOperationTypeDefinition([NotNull] OperationTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="OperationTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveOperationTypeDefinition([NotNull] OperationTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="PunctuatorSyntax"/> node.</summary>
        public virtual TResult EnterPunctuator([NotNull] PunctuatorSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="PunctuatorSyntax"/> node.</summary>
        public virtual TResult LeavePunctuator([NotNull] PunctuatorSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ScalarTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterScalarTypeDefinition([NotNull] ScalarTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ScalarTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveScalarTypeDefinition([NotNull] ScalarTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="ScalarTypeExtensionSyntax"/> node.</summary>
        public virtual TResult EnterScalarTypeExtension([NotNull] ScalarTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="ScalarTypeExtensionSyntax"/> node.</summary>
        public virtual TResult LeaveScalarTypeExtension([NotNull] ScalarTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="SchemaDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterSchemaDefinition([NotNull] SchemaDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="SchemaDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveSchemaDefinition([NotNull] SchemaDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="SchemaExtensionSyntax"/> node.</summary>
        public virtual TResult EnterSchemaExtension([NotNull] SchemaExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="SchemaExtensionSyntax"/> node.</summary>
        public virtual TResult LeaveSchemaExtension([NotNull] SchemaExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="SelectionSetSyntax"/> node.</summary>
        public virtual TResult EnterSelectionSet([NotNull] SelectionSetSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="SelectionSetSyntax"/> node.</summary>
        public virtual TResult LeaveSelectionSet([NotNull] SelectionSetSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="StringValueSyntax"/> node.</summary>
        public virtual TResult EnterStringValue([NotNull] StringValueSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="StringValueSyntax"/> node.</summary>
        public virtual TResult LeaveStringValue([NotNull] StringValueSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="UnionTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterUnionTypeDefinition([NotNull] UnionTypeDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="UnionTypeDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveUnionTypeDefinition([NotNull] UnionTypeDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="UnionTypeExtensionSyntax"/> node.</summary>
        public virtual TResult EnterUnionTypeExtension([NotNull] UnionTypeExtensionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="UnionTypeExtensionSyntax"/> node.</summary>
        public virtual TResult LeaveUnionTypeExtension([NotNull] UnionTypeExtensionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="VariableDefinitionSyntax"/> node.</summary>
        public virtual TResult EnterVariableDefinition([NotNull] VariableDefinitionSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="VariableDefinitionSyntax"/> node.</summary>
        public virtual TResult LeaveVariableDefinition([NotNull] VariableDefinitionSyntax node) => OnLeave(node);

        /// <summary>Called when the visitor enters a <see cref="VariableSyntax"/> node.</summary>
        public virtual TResult EnterVariable([NotNull] VariableSyntax node) => OnEnter(node);

        /// <summary>Called when the visitor leaves a <see cref="VariableSyntax"/> node.</summary>
        public virtual TResult LeaveVariable([NotNull] VariableSyntax node) => OnLeave(node);
    }
}
