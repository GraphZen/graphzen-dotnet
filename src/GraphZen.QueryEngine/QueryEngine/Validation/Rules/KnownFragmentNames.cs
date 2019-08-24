// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.QueryEngine.Validation.Rules
{
    public class KnownFragmentNames : QueryValidationRuleVisitor
    {
        public KnownFragmentNames(QueryValidationContext context) : base(context)
        {
        }

        public static string UnknownFragmentMessage(string fragmentName) => $"Unknown fragment \"{fragmentName}\".";

        public override VisitAction EnterFragmentSpread(FragmentSpreadSyntax node)
        {
            var fragmentName = node.Name.Value;
            if (!Context.Fragments.ContainsKey(fragmentName))
            {
                ReportError(UnknownFragmentMessage(fragmentName), node.Name);
            }

            return VisitAction.Continue;
        }
    }
}