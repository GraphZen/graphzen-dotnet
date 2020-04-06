// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel.Validation
{
    public class ParallelValidationVisitor : ParallelSyntaxWalker
    {
        private readonly ValidationContext _validationContext;

        public ParallelValidationVisitor(ValidationContext validationContext,
            IReadOnlyCollection<ValidationRuleVisitor> visitors) : base(visitors)
        {
            _validationContext = validationContext;
        }

        public override void OnEnter(SyntaxNode node)
        {
            _validationContext.Enter(node);
            base.OnEnter(node);
        }

        public override void OnLeave(SyntaxNode node)
        {
            base.OnLeave(node);
            _validationContext.Leave(node);
        }
    }
}