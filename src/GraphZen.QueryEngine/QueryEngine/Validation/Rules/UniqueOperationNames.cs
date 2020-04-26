// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.QueryEngine.Validation.Rules
{
    public class UniqueOperationNames : QueryValidationRuleVisitor
    {
        private readonly Dictionary<string, NameSyntax> _knownOperationNames =
            new Dictionary<string, NameSyntax>();

        public UniqueOperationNames(QueryValidationContext context) : base(context)
        {
        }

        public static string DuplicateOperationNameMessage(string operationName) =>
            $"There can be only one operation named \"{operationName}\".";

        public override VisitAction EnterOperationDefinition(OperationDefinitionSyntax node)
        {
            var operationName = node.Name;
            if (operationName != null)
            {
                if (_knownOperationNames.ContainsKey(operationName.Value))
                {
                    ReportError(DuplicateOperationNameMessage(operationName.Value),
                        _knownOperationNames[operationName.Value], operationName);
                }
                else
                {
                    _knownOperationNames.Add(operationName.Value, operationName);
                }
            }

            return false;
        }

        public override VisitAction EnterFragmentDefinition(FragmentDefinitionSyntax node) => false;
    }
}