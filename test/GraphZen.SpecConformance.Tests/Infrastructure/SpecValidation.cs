// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Internal;
using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.Tests.Validation.Rules;
using GraphZen.TypeSystem;

namespace GraphZen.SpecConformance.Tests.Infrastructure;

public record ExpectedError(string Message, int Line, int Column);

public class ValidationResult
{
    private readonly IReadOnlyCollection<GraphQLServerError> _errors;

    public ValidationResult(IReadOnlyCollection<GraphQLServerError> errors) => _errors = errors;

    public void ToDeepEqual(params ExpectedError[] expected)
    {
        Assert.Equal(expected.Length, _errors.Count);
        var errorsList = _errors.ToList();
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i].Message, errorsList[i].Message);
            Assert.NotNull(errorsList[i].Locations);
            var location = errorsList[i].Locations![0];
            Assert.Equal(expected[i].Line, location.Line);
            Assert.Equal(expected[i].Column, location.Column);
        }
    }
}

public static class SpecValidation
{
    public static Schema TestSchema => ValidationRuleHarness.TestSchema;

    public static ValidationResult ExpectErrors(ValidationRule rule, string query) =>
        ExpectErrors(TestSchema, rule, query);

    public static ValidationResult ExpectErrors(Schema schema, ValidationRule rule, string query)
    {
        var document = Parser.ParseDocument(query);
        var errors = new QueryValidator([rule]).Validate(schema, document);
        return new ValidationResult(errors);
    }

    public static void ExpectValid(ValidationRule rule, string query) =>
        ExpectErrors(rule, query).ToDeepEqual();

    public static void ExpectValid(Schema schema, ValidationRule rule, string query) =>
        ExpectErrors(schema, rule, query).ToDeepEqual();
}
