// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel.Validation;

namespace GraphZen.Tests.Validation.Rules;

[NoReorder]
public class SchemaMustHaveRootObjectTypesTests : ValidationRuleHarness
{
    public override ValidationRule RuleUnderTest { get; } = DocumentValidationRules.SchemaMustHaveRootObjectTypes;

    [Fact]
    public void AcceptsASchemaWhoseQueryTypeIsAnObjectType()
    {
        SdlShouldPass(@"
              type Query {
                test: String
              }
            ");

        SdlShouldPass(@"
              schema {
                query: QueryRoot
              }

              type QueryRoot {
                test: String
              }
            ");
    }

    [Fact]
    public void AcceptsASchemaWhoseQueryAndMutationTypesAreObjectTypes()
    {
        SdlShouldPass(@"
              type Query {
                test: String
              }

              type Mutation {
                test: String
              }
            ");

        SdlShouldPass(@"
              schema {
                query: QueryRoot
                mutation: MutationRoot
              }

              type QueryRoot {
                test: String
              }

              type MutationRoot {
                test: String
              }
            ");
    }

    [Fact]
    public void AcceptsASchemaWhoseQueryAndSubscriptionTypesAreObjectTypes()
    {
        SdlShouldPass(@"
              type Query {
                test: String
              }

              type Subscription {
                test: String
              }
            ");

        SdlShouldPass(@"
              schema {
                query: QueryRoot
                subscription: SubscriptionRoot
              }

              type QueryRoot {
                test: String
              }

              type SubscriptionRoot {
                test: String
              }
            ");
    }

    [Fact]
    public void RejectsASchemaWithoutAQueryType()
    {
        SdlShouldFail(@"
              type Mutation {
                test: String
              }
            ", Error("Query root type must be provided."));

        SdlShouldFail(@"
              schema {
                mutation: MutationRoot
              }

              type MutationRoot {
                test: String
              }"
            , Error("Query root type must be provided.", (1, 1)));
    }

    [Fact]
    public void RejectsSchemaWhoseQueryTypeIsNotAnObjectType()
    {
        SdlShouldFail(@"
              input Query {
                test: String
              }
            ", Error("Query root type must be Object type, it cannot be Query.", (1, 1)));

        SdlShouldFail(@"
              schema {
                query: SomeInputObject
              }

              input SomeInputObject {
                test: String
              }
            ", Error("Query root type must be Object type, it cannot be SomeInputObject.", (2, 10)));
    }

    [Fact]
    public void RejectsASchemaWhoseMutationTypeIsAnInputType()
    {
        SdlShouldFail(@"
              type Query {
                field: String
              }

              input Mutation {
                test: String
              }
            ", Error("Mutation root type must be Object type if provided, it cannot be Mutation.", (5, 1)));

        SdlShouldFail(@"
              schema {
                query: Query
                mutation: SomeInputObject
              }

              type Query {
                field: String
              }

              input SomeInputObject {
                test: String
              }
            ", Error("Mutation root type must be Object type if provided, it cannot be SomeInputObject.", (3, 13)));
    }

    [Fact]
    public void RejectsASchemaWhoseSubscriptionTypeIsAnInputType()
    {
        SdlShouldFail(@"
              type Query {
                field: String
              }

              input Subscription {
                test: String
              }
            ", Error("Subscription root type must be Object type if provided, it cannot be Subscription.", (5, 1)));

        SdlShouldFail(@"
              schema {
                query: Query
                subscription: SomeInputObject
              }

              type Query {
                field: String
              }

              input SomeInputObject {
                test: String
              }
            ",
            Error("Subscription root type must be Object type if provided, it cannot be SomeInputObject.",
                (3, 17)));
    }

    [Fact(Skip = "TODO")]
    public void RejectsASchemaExtendedWithInvalidRootTypes()
    {
    }
}
