// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.QueryEngine.Validation.Rules
{
    public class ExecutableDefinitions : QueryValidationRuleVisitor
    {
        public ExecutableDefinitions(QueryValidationContext context) : base(context)
        {
        }

        public static string NonExecutableDefinitionMessage(string definitionName)
        {
            return $"The {definitionName} definition is not executable.";
        }

        public override VisitAction EnterDocument(DocumentSyntax node)
        {
            foreach (var definition in node.Definitions)
                if (definition.Kind != SyntaxKind.OperationDefinition &&
                    definition.Kind != SyntaxKind.FragmentDefinition)
                {
                    var defName = definition is INamedSyntax named ? named.Name.Value : "schema";
                    ReportError(new GraphQLError(NonExecutableDefinitionMessage(defName), new[] { definition }));
                }

            return false;
        }
    }
}