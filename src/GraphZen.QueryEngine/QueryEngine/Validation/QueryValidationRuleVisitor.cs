#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;

namespace GraphZen.QueryEngine.Validation
{
    public abstract class QueryValidationRuleVisitor : ValidationRuleVisitor
    {
        protected QueryValidationRuleVisitor(QueryValidationContext context) : base(context)
        {
            Context = Check.NotNull(context, nameof(context));
        }

        [NotNull]
        public new QueryValidationContext Context { get; }

        [NotNull]
        public TypeInfo TypeInfo => Context.TypeInfo;
    }
}