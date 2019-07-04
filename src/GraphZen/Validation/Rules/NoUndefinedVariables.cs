﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;


namespace GraphZen.Validation.Rules
{
    public class NoUndefinedVariables : QueryValidationRuleVisitor
    {
        public NoUndefinedVariables(QueryValidationContext context) : base(context)
        {
        }
    }
}