// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Internal;
using GraphZen.QueryEngine.Validation;
using GraphZen.Tests.Validation.Rules;
using GraphZen.TypeSystem;

namespace GraphZen.SpecConformance.Tests.Infrastructure;

public abstract class SpecValidationRuleHarness : ValidationRuleHarness
{
    protected void QueryShouldPass(Schema schema, string query)
    {
        var document = Parser.ParseDocument(query);
        var result = new QueryValidator(new[] { RuleUnderTest }).Validate(schema, document);
        Assert.Empty(result);
    }

    protected void QueryShouldFail(string query)
    {
        var document = Parser.ParseDocument(query);
        var result = new QueryValidator(new[] { RuleUnderTest }).Validate(TestSchema, document);
        Assert.NotEmpty(result);
    }

    protected void QueryShouldFail(string query, int errorCount)
    {
        var document = Parser.ParseDocument(query);
        var result = new QueryValidator(new[] { RuleUnderTest }).Validate(TestSchema, document);
        Assert.Equal(errorCount, result.Count);
    }

    protected void QueryShouldFail(Schema schema, string query)
    {
        var document = Parser.ParseDocument(query);
        var result = new QueryValidator(new[] { RuleUnderTest }).Validate(schema, document);
        Assert.NotEmpty(result);
    }

    protected void QueryShouldFail(Schema schema, string query, int errorCount)
    {
        var document = Parser.ParseDocument(query);
        var result = new QueryValidator(new[] { RuleUnderTest }).Validate(schema, document);
        Assert.Equal(errorCount, result.Count);
    }
}
