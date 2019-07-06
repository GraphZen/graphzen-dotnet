﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel.Validation
{
    public abstract class DocumentValidationRuleVisitor : ValidationRuleVisitor
    {
        protected DocumentValidationRuleVisitor(DocumentValidationContext context) : base(context)
        {
        }
    }
}