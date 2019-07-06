// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.QueryEngine.Validation.Rules
{
    public class OverlappingFieldsCanBeMerged : QueryValidationRuleVisitor
    {
        public OverlappingFieldsCanBeMerged(QueryValidationContext context) : base(context)
        {
        }
    }
}