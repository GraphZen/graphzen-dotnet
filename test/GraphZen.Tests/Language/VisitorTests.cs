﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.Language.Internal;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.Language
{
    [NoReorder]
    public class VisitorTests
    {
        public class KitchenSinkVisitor : GraphQLSyntaxWalker
        {
            public KitchenSinkVisitor(List<(string enterOrLeave, Type type)> visited)
            {
                Visited = visited;
            }

            private List<(string enterOrLeave, Type type)> Visited { get; }

            public override void OnEnter(SyntaxNode node) => Visited.Add(("enter", node.GetType()));

            public override void OnLeave(SyntaxNode node) => Visited.Add(("leave", node.GetType()));
        }

        [Fact]
        public void ValidatesPathArgument()
        {
            var kitchenSink = File.ReadAllText("./Language/kitchen-sink.graphql");
            var ast = Parser.ParseDocument(kitchenSink);
            var visited = new List<(string enterOrLeave, Type type)>();
            var visitor = new KitchenSinkVisitor(visited);
            var expected = new List<(string enterOrLeave, Type type)>
            {
                ("enter", typeof(DocumentSyntax)),
                ("enter", typeof(OperationDefinitionSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(VariableDefinitionSyntax)),
                ("enter", typeof(VariableSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(VariableSyntax)),
                ("enter", typeof(NamedTypeSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(NamedTypeSyntax)),
                ("leave", typeof(VariableDefinitionSyntax)),
                ("enter", typeof(VariableDefinitionSyntax)),
                ("enter", typeof(VariableSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(VariableSyntax)),
                ("enter", typeof(NamedTypeSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(NamedTypeSyntax)),
                ("enter", typeof(EnumValueSyntax)),
                ("leave", typeof(EnumValueSyntax)),
                ("leave", typeof(VariableDefinitionSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(ListValueSyntax)),
                ("enter", typeof(IntValueSyntax)),
                ("leave", typeof(IntValueSyntax)),
                ("enter", typeof(IntValueSyntax)),
                ("leave", typeof(IntValueSyntax)),
                ("leave", typeof(ListValueSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("enter", typeof(InlineFragmentSyntax)),
                ("enter", typeof(NamedTypeSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(NamedTypeSyntax)),
                ("enter", typeof(DirectiveSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(DirectiveSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(IntValueSyntax)),
                ("leave", typeof(IntValueSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(VariableSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(VariableSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("enter", typeof(DirectiveSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(VariableSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(VariableSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("leave", typeof(DirectiveSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("enter", typeof(FragmentSpreadSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(FragmentSpreadSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(InlineFragmentSyntax)),
                ("enter", typeof(InlineFragmentSyntax)),
                ("enter", typeof(DirectiveSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(VariableSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(VariableSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("leave", typeof(DirectiveSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(InlineFragmentSyntax)),
                ("enter", typeof(InlineFragmentSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(InlineFragmentSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(OperationDefinitionSyntax)),
                ("enter", typeof(OperationDefinitionSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(IntValueSyntax)),
                ("leave", typeof(IntValueSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("enter", typeof(DirectiveSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(DirectiveSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(OperationDefinitionSyntax)),
                ("enter", typeof(OperationDefinitionSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(VariableDefinitionSyntax)),
                ("enter", typeof(VariableSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(VariableSyntax)),
                ("enter", typeof(NamedTypeSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(NamedTypeSyntax)),
                ("leave", typeof(VariableDefinitionSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(VariableSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(VariableSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(OperationDefinitionSyntax)),
                ("enter", typeof(FragmentDefinitionSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(NamedTypeSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(NamedTypeSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(VariableSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(VariableSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(VariableSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(VariableSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(ObjectValueSyntax)),
                ("enter", typeof(ObjectFieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(StringValueSyntax)),
                ("leave", typeof(StringValueSyntax)),
                ("leave", typeof(ObjectFieldSyntax)),
                ("enter", typeof(ObjectFieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(StringValueSyntax)),
                ("leave", typeof(StringValueSyntax)),
                ("leave", typeof(ObjectFieldSyntax)),
                ("leave", typeof(ObjectValueSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(FragmentDefinitionSyntax)),
                ("enter", typeof(OperationDefinitionSyntax)),
                ("enter", typeof(SelectionSetSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(BooleanValueSyntax)),
                ("leave", typeof(BooleanValueSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(BooleanValueSyntax)),
                ("leave", typeof(BooleanValueSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("enter", typeof(ArgumentSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("enter", typeof(NullValueSyntax)),
                ("leave", typeof(NullValueSyntax)),
                ("leave", typeof(ArgumentSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("enter", typeof(FieldSyntax)),
                ("enter", typeof(NameSyntax)),
                ("leave", typeof(NameSyntax)),
                ("leave", typeof(FieldSyntax)),
                ("leave", typeof(SelectionSetSyntax)),
                ("leave", typeof(OperationDefinitionSyntax)),
                ("leave", typeof(DocumentSyntax))
            };
            visitor.Visit(ast);

            visited.Should().BeEquivalentTo(expected, _ => _.WithStrictOrdering());
        }
    }
}