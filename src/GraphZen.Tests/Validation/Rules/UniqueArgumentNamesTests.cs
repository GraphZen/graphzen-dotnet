// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class UniqueArgumentNamesTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.UniqueArgumentNames;
    }
}