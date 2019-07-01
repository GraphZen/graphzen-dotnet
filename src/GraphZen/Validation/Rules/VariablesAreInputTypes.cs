﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Validation.Rules
{
    public class VariablesAreInputTypes : QueryValidationRuleVisitor
    {
        public VariablesAreInputTypes(QueryValidationContext context) : base(context)
        {
        }
    }
}