// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Validation.Rules
{
    public class ValuesOfCorrectType : QueryValidationRuleVisitor
    {
        public ValuesOfCorrectType(QueryValidationContext context) : base(context)
        {
        }
    }
}