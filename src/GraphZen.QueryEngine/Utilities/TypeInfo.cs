// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen
{
    public class TypeInfo
    {
        private readonly Stack<Maybe<object>> _defaultValueStack = new Stack<Maybe<object>>();

        private readonly Stack<Field> _fieldDefStack = new Stack<Field>();

        private readonly Stack<IGraphQLType> _inputTypeStack = new Stack<IGraphQLType>();

        private readonly Stack<ICompositeType> _parentTypeStack = new Stack<ICompositeType>();

        private readonly Stack<IGraphQLType> _typeStack = new Stack<IGraphQLType>();

        public TypeInfo(Schema schema)
        {
            Schema = Check.NotNull(schema, nameof(schema));
        }


        public Argument Argument { get; private set; }


        public Directive Directive { get; private set; }


        public EnumValue EnumValue { get; private set; }


        protected Schema Schema { get; }


        public Maybe<object> DefaultValue => _defaultValueStack.PeekOrDefault();


        private static Field GetFieldDef(Schema schema, IGraphQLType parentType,
            FieldSyntax node)
        {
            var name = node.Name.Value;

            if (name == Introspection.SchemaMetaFieldDef.Name && schema.QueryType.Equals(parentType))
                return Introspection.SchemaMetaFieldDef;

            if (name == Introspection.TypeMetaFieldDef.Name && schema.QueryType.Equals(parentType))
                return Introspection.TypeMetaFieldDef;

            if (name == Introspection.TypeNameMetaFieldDef.Name && parentType is ICompositeType)
                return Introspection.TypeNameMetaFieldDef;

            if (parentType is ObjectType objectType) return objectType.FindField(name);

            if (parentType is InterfaceType interfaceType) return interfaceType.FindField(name);

            return null;
        }


        public IGraphQLType GetOutputType() => _typeStack.PeekOrDefault();


        public ICompositeType GetParentType() => _parentTypeStack.PeekOrDefault();


        public IGraphQLType GetInputType() => _inputTypeStack.PeekOrDefault();


        public IGraphQLType GetParentInputType() => _inputTypeStack.Count > 1 ? _inputTypeStack.ElementAt(1) : default;


        public Field GetField() => _fieldDefStack.PeekOrDefault();

        public void Enter(SyntaxNode syntaxNode)
        {
            switch (syntaxNode)
            {
                case SelectionSetSyntax _:
                    _parentTypeStack.Push(GetOutputType().GetNamedType() as ICompositeType);
                    break;
                case FieldSyntax node:
                    var parentType = GetParentType();
                    Field fieldDef = null;
                    IGraphQLType fieldType = null;
                    if (parentType != null)
                    {
                        fieldDef = GetFieldDef(Schema, parentType, node);
                        if (fieldDef != null) fieldType = fieldDef.FieldType;
                    }

                    _fieldDefStack.Push(fieldDef);
                    _typeStack.Push(fieldType);
                    break;

                case DirectiveSyntax directive:
                    Directive = Schema.FindDirective(directive.Name.Value);
                    break;
                case OperationDefinitionSyntax node:
                    {
                        IGraphQLType type = null;
                        if (node.OperationType == OperationType.Query)
                            type = Schema.QueryType;
                        else if (node.OperationType == OperationType.Mutation)
                            type = Schema.MutationType;
                        else if (node.OperationType == OperationType.Subscription) type = Schema.SubscriptionType;

                        _typeStack.Push(type);
                        break;
                    }
                case IFragmentTypeConditionSyntax node:
                    {
                        var typeCondition = node.TypeCondition;
                        var type = typeCondition != null
                            ? Schema.GetTypeFromAst(typeCondition)
                            : GetOutputType().GetNamedType();
                        _typeStack.Push(type);
                        break;
                    }
                case VariableDefinitionSyntax node:
                    {
                        var type = Schema.GetTypeFromAst(node.VariableType);
                        _inputTypeStack.Push(type);
                        break;
                    }
                case ArgumentSyntax node:
                    {
                        Argument argDef = null;
                        IGraphQLType argType = null;

                        var fieldOrDirective = (IArgumentsContainer)Directive ?? GetField();
                        if (fieldOrDirective != null)
                        {
                            argDef = fieldOrDirective.FindArgument(node.Name.Value);
                            argType = argDef?.InputType;
                        }

                        Argument = argDef;
                        _defaultValueStack.Push(argDef != null && argDef.HasDefaultValue
                            ? Maybe.Some(argDef.DefaultValue)
                            : Maybe.None<object>());
                        _inputTypeStack.Push(argType);
                        break;
                    }
                case ListValueSyntax _:
                    {
                        var listType = GetInputType().GetNullableType();
                        var itemType = listType is ListType list ? list.OfType : listType;
                        _defaultValueStack.Push(null);
                        _inputTypeStack.Push(itemType);

                        break;
                    }
                case ObjectFieldSyntax node:
                    {
                        var objectType = GetInputType().GetNamedType();
                        IGraphQLType inputFieldType = null;
                        InputField inputField = null;
                        if (objectType is InputObjectType inputObject)
                        {
                            inputField = inputObject.FindField(node.Name.Value);
                            inputFieldType = inputField?.InputType;
                        }

                        _defaultValueStack.Push(inputField != null && inputField.HasDefaultValue
                            ? Maybe.Some(inputField.DefaultValue)
                            : Maybe.None<object>());
                        _inputTypeStack.Push(inputFieldType);
                        break;
                    }
                case EnumValueSyntax node:

                    {
                        var type = GetInputType().GetNamedType();
                        EnumValue = type is EnumType enumType ? enumType.FindValue(node.Value) : null;

                        break;
                    }
            }
        }

        public void Leave(SyntaxNode syntaxNode)
        {
            switch (syntaxNode)
            {
                case SelectionSetSyntax _:
                    _parentTypeStack.Pop();
                    break;
                case FieldSyntax _:
                    _fieldDefStack.Pop();
                    break;
                case DirectiveSyntax _:
                    Directive = null;
                    break;
                case OperationDefinitionSyntax _:
                case InlineFragmentSyntax _:
                case FragmentDefinitionSyntax _:
                    _typeStack.Pop();
                    break;
                case VariableDefinitionSyntax _:
                    _inputTypeStack.Pop();
                    break;
                case ArgumentSyntax _:
                    Argument = null;
                    _defaultValueStack.Pop();
                    _inputTypeStack.Pop();
                    break;

                case ListValueSyntax _:
                case ObjectFieldSyntax _:
                    _defaultValueStack.Pop();
                    _inputTypeStack.Pop();
                    break;
                case EnumValueSyntax _:
                    EnumValue = null;
                    break;
            }
        }
    }
}