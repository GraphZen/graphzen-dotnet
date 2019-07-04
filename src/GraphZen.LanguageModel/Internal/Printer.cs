// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GraphZen.LanguageModel.Internal
{
    public class Printer : IPrinter
    {
        [NotNull] private readonly StringBuilder _outputBuilder = new StringBuilder();

        private int _indentLevel;


        public string Print(SyntaxNode node)
        {
            PrintNode(node);
            var result = _outputBuilder.ToString();
            _outputBuilder.Clear();

            return result;
        }

        private void PrintNode(SyntaxNode node)
        {
            if (node == null)
            {
                return;
            }

            switch (node)
            {
                case NameSyntax name:
                    Append(name.Value);
                    break;
                case VariableSyntax variable:
                    Wrap("$", variable.Name);
                    break;

                case DocumentSyntax doc:
                    Join(doc.Definitions, string.Concat(Environment.NewLine, Environment.NewLine));
                    Append(Environment.NewLine);
                    break;
                case OperationDefinitionSyntax def:
                    if (def.Name == null && !def.Directives.Any() && def.OperationType == OperationType.Query)
                    {
                        PrintNode(def.SelectionSet);
                    }
                    else
                    {
                        // Operation type and name
                        Wrap($"{def.OperationType.ToString().ToLower()} ", def.Name);
                        // Variable definitions
                        if (def.VariableDefinitions.Any())
                        {
                            Wrap("(", () => Join(def.VariableDefinitions, ", "), ")");
                        }

                        // Directives
                        Append(' ');
                        PrintDirectives(def.Directives);
                        PrintNode(def.SelectionSet);
                    }

                    break;
                case SelectionSetSyntax set:
                    Block(set.Selections);
                    break;
                case FieldSyntax field:
                    if (field.Alias != null)
                    {
                        Wrap((string)"", (SyntaxNode)field.Alias, ": ");
                    }

                    PrintNode(field.Name);
                    PrintArguments((IReadOnlyList<ArgumentSyntax>)field.Arguments);
                    PrintDirectives(field.Directives);
                    PrintNode(field.SelectionSet);
                    break;
                case ArgumentSyntax arg:
                    PrintDescription(arg);
                    PrintNode(arg.Name);
                    Wrap(": ", arg.Value);
                    break;
                case VariableDefinitionSyntax varDef:
                    PrintNode(varDef.Variable);
                    Append(": ");
                    PrintNode(varDef.VariableType);
                    if (varDef.DefaultValue != null)
                    {
                        Wrap(" = ", varDef.DefaultValue);
                    }

                    break;
                case FragmentSpreadSyntax fragmentSpread:
                    Wrap((string)"... ", (SyntaxNode)fragmentSpread.Name, " ");
                    PrintDirectives(fragmentSpread.Directives);
                    break;
                case InlineFragmentSyntax inlineFragment:
                    Append("... ");
                    if (inlineFragment.TypeCondition != null)
                    {
                        Wrap((string)"on ", (SyntaxNode)inlineFragment.TypeCondition, " ");
                    }

                    PrintDirectives(inlineFragment.Directives);
                    PrintNode(inlineFragment.SelectionSet);
                    break;
                case FragmentDefinitionSyntax fragmentDefinition:
                    Wrap("fragment ", fragmentDefinition.Name);
                    Wrap((string)" on ", (SyntaxNode)fragmentDefinition.TypeCondition, " ");
                    PrintDirectives(fragmentDefinition.Directives);
                    PrintNode(fragmentDefinition.SelectionSet);
                    break;
                case IntValueSyntax intValue:
                    Append((string)intValue.Value.ToString());
                    break;
                case FloatValueSyntax floatValue:
                    Append((string)floatValue.Value);
                    break;
                case StringValueSyntax stringValue:
                    PrintStringValue(stringValue, false);
                    break;
                case BooleanValueSyntax booleanValue:
                    Append(JsonConvert.SerializeObject(booleanValue.Value));
                    break;
                case NullValueSyntax _:
                    Append("null");
                    break;
                case EnumValueSyntax enumValue:
                    Append((string)enumValue.Value);
                    break;
                case ListValueSyntax listValue:
                    Wrap("[", () => Join(listValue.Values, ", "), "]");
                    break;
                case ObjectValueSyntax objectValue:
                    Wrap("{", () => Join(objectValue.Fields, ", "), "}");
                    break;
                case ObjectFieldSyntax objectField:
                    PrintNode(objectField.Name);
                    Append(": ");
                    PrintNode(objectField.Value);
                    break;
                case DirectiveSyntax directive:
                    Wrap("@", directive.Name);
                    PrintArguments((IReadOnlyList<ArgumentSyntax>)directive.Arguments);
                    break;
                case NamedTypeSyntax namedType:
                    Append((string)namedType.Name.Value);
                    break;
                case ListTypeSyntax listType:
                    Wrap((string)"[", (SyntaxNode)listType.OfType, "]");
                    break;
                case NonNullTypeSyntax nonNull:
                    Wrap((string)"", (SyntaxNode)nonNull.OfType, "!");
                    break;
                case SchemaDefinitionSyntax schemaDef:
                    Append("schema ");
                    PrintDirectives(schemaDef.Directives);
                    Block(schemaDef.RootOperationTypes);
                    break;
                case OperationTypeDefinitionSyntax opTypeDef:
                    Wrap($"{opTypeDef.OperationType.ToString().ToLower()}: ", opTypeDef.Type);
                    break;

                case ScalarTypeDefinitionSyntax scalarDef:
                    PrintDescription(scalarDef);
                    Wrap("scalar ", scalarDef.Name);
                    PrintDirectives(scalarDef.Directives);
                    break;
                case ObjectTypeDefinitionSyntax objectDef:
                    PrintDescription(objectDef);
                    Append("type ");
                    PrintNode(objectDef.Name);
                    if (Enumerable.Any<NamedTypeSyntax>(objectDef.Interfaces))
                    {
                        Wrap(" implements ", () => Join(objectDef.Interfaces, " & "));
                    }

                    PrintDirectives(objectDef.Directives);
                    if (Enumerable.Any<FieldDefinitionSyntax>(objectDef.Fields))
                    {
                        Append(" ");
                    }

                    Block(objectDef.Fields);
                    break;
                case FieldDefinitionSyntax fieldDef:
                    PrintDescription(fieldDef);
                    PrintNode(fieldDef.Name);
                    PrintArguments((IReadOnlyList<InputValueDefinitionSyntax>)fieldDef.Arguments);
                    Wrap(": ", fieldDef.FieldType);
                    PrintDirectives(fieldDef.Directives);
                    break;
                case InputValueDefinitionSyntax inputValueDef:
                    PrintDescription(inputValueDef);
                    PrintNode(inputValueDef.Name);
                    Wrap(": ", inputValueDef.Type);
                    if (inputValueDef.DefaultValue != null)
                    {
                        Append(" = ");
                        PrintNode(inputValueDef.DefaultValue);
                    }

                    PrintDirectives(inputValueDef.Directives);
                    break;
                case InterfaceTypeDefinitionSyntax interfaceDef:
                    PrintDescription(interfaceDef);
                    Wrap("interface ", interfaceDef.Name);
                    PrintDirectives(interfaceDef.Directives);
                    if (Enumerable.Any<FieldDefinitionSyntax>(interfaceDef.Fields))
                    {
                        Append(" ");
                    }

                    Block(interfaceDef.Fields);
                    break;
                case UnionTypeDefinitionSyntax unionType:
                    PrintDescription(unionType);
                    Wrap("union ", unionType.Name);
                    PrintDirectives(unionType.Directives);
                    if (Enumerable.Any<NamedTypeSyntax>(unionType.MemberTypes))
                    {
                        Append(" = ");
                        Join(unionType.MemberTypes, " | ");
                    }

                    break;
                case EnumTypeDefinitionSyntax enumType:
                    PrintDescription(enumType);
                    Wrap("enum ", enumType.Name);
                    PrintDirectives(enumType.Directives);
                    if (Enumerable.Any<EnumValueDefinitionSyntax>(enumType.Values))
                    {
                        Append(" ");
                    }

                    Block(enumType.Values);
                    break;
                case InputObjectTypeDefinitionSyntax inputObjectDef:
                    PrintDescription(inputObjectDef);
                    Wrap("input ", inputObjectDef.Name);
                    PrintDirectives(inputObjectDef.Directives);
                    if (Enumerable.Any<InputValueDefinitionSyntax>(inputObjectDef.Fields))
                    {
                        Append(" ");
                    }

                    Block(inputObjectDef.Fields);
                    break;
                case EnumValueDefinitionSyntax enumValuDef:
                    PrintDescription(enumValuDef);
                    PrintNode(enumValuDef.Value);
                    PrintDirectives(enumValuDef.Directives);
                    break;
                case DirectiveDefinitionSyntax directiveDef:
                    PrintDescription(directiveDef);
                    Wrap("directive @", directiveDef.Name);
                    PrintArguments((IReadOnlyList<InputValueDefinitionSyntax>)directiveDef.Arguments);
                    Append(" on ");
                    Join(directiveDef.Locations, " | ");
                    break;
                case EnumTypeExtensionSyntax enumExtension:
                    Wrap("extend enum ", enumExtension.Name);
                    PrintDirectives(enumExtension.Directives);
                    if (Enumerable.Any<EnumValueDefinitionSyntax>(enumExtension.Values))
                    {
                        Append(" ");
                    }

                    Block(enumExtension.Values);
                    break;
                case InterfaceTypeExtensionSyntax ifaceExt:
                    Wrap("extend interface ", ifaceExt.Name);
                    PrintDirectives(ifaceExt.Directives);
                    if (Enumerable.Any<FieldDefinitionSyntax>(ifaceExt.Fields))
                    {
                        Append(" ");
                    }

                    Block(ifaceExt.Fields);
                    break;
                case ScalarTypeExtensionSyntax scalarExt:
                    Wrap("extend scalar ", scalarExt.Name);
                    PrintDirectives(scalarExt.Directives);
                    break;

                case ObjectTypeExtensionSyntax objectExt:
                    Wrap("extend type ", objectExt.Name);
                    if (Enumerable.Any<NamedTypeSyntax>(objectExt.Interfaces))
                    {
                        Wrap(" implements ", () => Join(objectExt.Interfaces, " & "));
                    }

                    PrintDirectives(objectExt.Directives);
                    if (Enumerable.Any<FieldDefinitionSyntax>(objectExt.Fields))
                    {
                        Append(" ");
                    }

                    Block(objectExt.Fields);
                    break;

                case SchemaExtensionSyntax schemaExt:
                    Append("extend schema");
                    PrintDirectives(schemaExt.Directives);
                    if (Enumerable.Any<OperationTypeDefinitionSyntax>(schemaExt.OperationTypes))
                    {
                        Append(" ");
                    }

                    Block(schemaExt.OperationTypes);
                    break;
                case UnionTypeExtensionSyntax unionExt:
                    Wrap("extend union ", unionExt.Name);
                    PrintDirectives(unionExt.Directives);
                    if (Enumerable.Any<NamedTypeSyntax>(unionExt.Types))
                    {
                        Append(" = ");
                        Join(unionExt.Types, " | ");
                    }

                    break;
                case InputObjectTypeExtensionSyntax inputExt:
                    Wrap("extend input ", inputExt.Name);
                    PrintDirectives(inputExt.Directives);
                    if (Enumerable.Any<InputValueDefinitionSyntax>(inputExt.Fields))
                    {
                        Append(" ");
                    }

                    Block(inputExt.Fields);
                    break;
                default:
                    throw new InvalidOperationException($"{node.GetType().Name} is not printable.");
            }
        }


        private void PrintDirectives([NotNull] IReadOnlyList<DirectiveSyntax> directives)
        {
            if (directives.Any())
            {
                Append(" ");
                Join((IReadOnlyList<SyntaxNode>)directives, " ");
            }
        }

        private void PrintStringValue([NotNull] StringValueSyntax stringValue, bool isDescription)
        {
            if (!stringValue.IsBlockString)
            {
                Append(JsonConvert.SerializeObject(stringValue.Value));
            }
            else
            {
                var bq = "\"\"\"";
                var escaped = stringValue.Value;
                if (char.IsWhiteSpace(escaped[0]) && !LanguageHelpers.HasNewline(stringValue.Value))
                {
                    Append($"{bq}{escaped}{bq}");
                }
                else
                {
                    if (isDescription)
                    {
                        Append(bq);
                        _outputBuilder.AppendLine();
                        AppendCurrentIndent();
                        Append((string)escaped);
                        _outputBuilder.AppendLine();
                        AppendCurrentIndent();
                        Append(bq);
                    }
                    else
                    {
                        Append(bq);
                        _outputBuilder.AppendLine();
                        _indentLevel++;
                        Append(GetCurrentIndent() +
                               escaped.Replace(Environment.NewLine, Environment.NewLine + GetCurrentIndent()));
                        _indentLevel--;
                        _outputBuilder.AppendLine();
                        AppendCurrentIndent();
                        Append(bq);
                    }
                }
            }
        }

        private void PrintDescription([NotNull] IDescribedSyntax node)
        {
            if (node.Description != null)
            {
                PrintStringValue(node.Description, true);
                AppendLine();
                AppendCurrentIndent();
            }
        }

        private void PrintArguments([NotNull] [ItemNotNull] IReadOnlyList<InputValueDefinitionSyntax> arguments)
        {
            if (arguments.Any())
            {
                if (arguments.All(_ => _.Description == null))
                {
                    Append("(");
                    Join((IReadOnlyList<SyntaxNode>)arguments, ", ");
                    Append(")");
                }
                else
                {
                    Append("(");
                    _indentLevel++;
                    foreach (var arg in arguments)
                    {
                        AppendLine();
                        AppendCurrentIndent();
                        PrintNode(arg);
                    }

                    _indentLevel--;
                    AppendLine();
                    AppendCurrentIndent();
                    Append(")");
                }
            }
        }

        private void PrintArguments([NotNull] IReadOnlyList<ArgumentSyntax> arguments)
        {
            if (arguments.Any())
            {
                Wrap("(", () => { Join((IReadOnlyList<SyntaxNode>)arguments, ", "); }, ")");
            }
        }


        #region  Helper Methods

        private void Block(IReadOnlyList<SyntaxNode> nodes)
        {
            if (nodes != null && nodes.Any())
            {
                Append('{');
                _indentLevel++;
                foreach (var node in nodes)
                {
                    AppendLine();
                    AppendCurrentIndent();
                    PrintNode(node);
                }

                _indentLevel--;
                AppendLine();
                Append("}");
            }
        }


        public void Wrap(string start, [NotNull] Action action, string end = "")
        {
            Append(start);
            action();
            Append(end);
        }

        public void Wrap(string start, SyntaxNode node, string end = "")
        {
            Append(start);
            PrintNode(node);
            Append(end);
        }


        public void Append(char value) => _outputBuilder.Append(value);

        public void Append(string value) => _outputBuilder.Append(value);

        public void AppendLine(string value = null)
        {
            _outputBuilder.AppendLine(value);
        }

        public void AppendCurrentIndent()
        {
            _outputBuilder.Append(GetCurrentIndent());
        }

        private string GetCurrentIndent() => string.Concat(Enumerable.Range(0, _indentLevel).Select(_ => "  "));

        public void Join([NotNull] IReadOnlyList<SyntaxNode> nodes, Action seperatorAction = null)
        {
            var hasSeperator = seperatorAction != null;

            var i = 1;
            foreach (var node in nodes)
            {
                PrintNode(node);
                var isLastElement = i == nodes.Count;
                if (hasSeperator && !isLastElement)
                {
                    seperatorAction();
                }

                i++;
            }
        }

        private void Join([NotNull] IReadOnlyList<SyntaxNode> nodes, string seperator = null)
        {
            Join(nodes, seperator != null ? () => { Append(seperator); } : (Action)null);
        }

        #endregion
    }
}