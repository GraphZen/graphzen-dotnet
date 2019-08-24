// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.QueryEngine.Validation.Rules
{
    public class LoneAnonymousOperation : QueryValidationRuleVisitor
    {
        public const string AnonymousOperationNotAloneMessage =
            "This anonymous operation must be the only defined operation.";

        private int _operationCount;

        public LoneAnonymousOperation(QueryValidationContext context) : base(context)
        {
        }

        public override VisitAction EnterDocument(DocumentSyntax node)
        {
            _operationCount = node.Definitions.OfType<OperationDefinitionSyntax>().Count();
            return VisitAction.Continue;
        }

        public override VisitAction EnterOperationDefinition(OperationDefinitionSyntax node)
        {
            if (node.Name == null && _operationCount > 1)
            {
                ReportError(AnonymousOperationNotAloneMessage, node);
            }

            return VisitAction.Continue;
        }
    }
}