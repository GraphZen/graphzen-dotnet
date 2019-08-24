#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;

namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class ScalarLeafsTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.ScalarLeafs;
    }
}