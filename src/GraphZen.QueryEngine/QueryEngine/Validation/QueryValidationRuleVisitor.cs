// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using GraphZen.Utilities;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.QueryEngine.Validation
{
    public abstract class QueryValidationRuleVisitor : ValidationRuleVisitor
    {
        protected QueryValidationRuleVisitor(QueryValidationContext context) : base(context)
        {
            Context = Check.NotNull(context, nameof(context));
        }


        public new QueryValidationContext Context { get; }


        public TypeInfo TypeInfo => Context.TypeInfo;
    }
}