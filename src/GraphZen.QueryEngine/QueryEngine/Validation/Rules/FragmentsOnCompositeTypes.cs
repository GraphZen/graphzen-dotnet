// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.QueryEngine.Validation.Rules
{
    public class FragmentsOnCompositeTypes : QueryValidationRuleVisitor
    {
        public FragmentsOnCompositeTypes(QueryValidationContext context) : base(context)
        {
        }

        public static string InlineFragmentOnNonCompositeErrorMessage(string type) =>
            $"Fragment cannot condition on non composite type \"{type}\".";

        public static string FragmentOnNonCompositeErrorMessage(string fragmentName, string type) =>
            $"Fragment \"{fragmentName}\" cannot condition on non composite type \"{type}\".";

        public override VisitAction EnterInlineFragment(InlineFragmentSyntax node)
        {
            var typeCondition = node.TypeCondition;
            if (typeCondition != null)
            {
                var type = Context.Schema.GetTypeFromAst(typeCondition);
                if (type != null && !(type is ICompositeType))
                    ReportError(InlineFragmentOnNonCompositeErrorMessage(typeCondition.ToSyntaxString()),
                        typeCondition);
            }

            return VisitAction.Continue;
        }

        public override VisitAction EnterFragmentDefinition(FragmentDefinitionSyntax node)
        {
            var type = Context.Schema.GetTypeFromAst(node.TypeCondition);
            {
                if (type != null && !(type is ICompositeType))
                    ReportError(
                        FragmentOnNonCompositeErrorMessage(node.Name.Value, node.TypeCondition.ToSyntaxString()),
                        node.TypeCondition);

                return VisitAction.Continue;
            }
        }
    }
}