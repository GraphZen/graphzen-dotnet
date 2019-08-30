// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.QueryEngine.Validation.Rules
{
    public class KnownTypeNames : QueryValidationRuleVisitor
    {
        public KnownTypeNames(QueryValidationContext context) : base(context)
        {
        }

        public static string UnknownTypeMessage(string typeName, IReadOnlyList<string> suggestedTypes)
        {
            var message = $"Unknown type \"{typeName}\".";
            if (suggestedTypes.Any()) return $"{message} Did you mean {suggestedTypes.QuotedOrList()}?";

            return message;
        }

        public override VisitAction EnterObjectTypeDefinition(ObjectTypeDefinitionSyntax node) => false;

        public override VisitAction EnterInterfaceTypeDefinition(InterfaceTypeDefinitionSyntax node) => false;

        public override VisitAction EnterUnionTypeDefinition(UnionTypeDefinitionSyntax node) => false;

        public override VisitAction EnterInputObjectTypeDefinition(InputObjectTypeDefinitionSyntax node) => false;

        public override VisitAction EnterNamedType(NamedTypeSyntax node)
        {
            var typeName = node.Name.Value;
            if (!Context.Schema.Types.ContainsKey(typeName))
            {
                var suggestions = StringUtils.GetSuggestionList(typeName, Context.Schema.Types.Keys);
                ReportError(UnknownTypeMessage(typeName, suggestions), node);
            }

            return VisitAction.Continue;
        }
    }
}