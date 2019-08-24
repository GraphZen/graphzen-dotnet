// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using Xunit;

namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class SchemaMustHaveRootObjectTypesTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = DocumentValidationRules.SchemaMustHaveRootObjectTypes;

        [Fact]
        public void AcceptsASchemaWhoseQueryTypeIsAnObjectType()
        {
            SDLShouldPass(@"
              type Query {
                test: String
              }
            ");

            SDLShouldPass(@"
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
            SDLShouldPass(@"
              type Query {
                test: String
              }

              type Mutation {
                test: String
              }
            ");

            SDLShouldPass(@"
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
            SDLShouldPass(@"
              type Query {
                test: String
              }

              type Subscription {
                test: String
              }
            ");

            SDLShouldPass(@"
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
            SDLShouldFail(@"
              type Mutation {
                test: String
              }
            ", Error("Query root type must be provided."));

            SDLShouldFail(@"
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
            SDLShouldFail(@"
              input Query {
                test: String
              }
            ", Error("Query root type must be Object type, it cannot be Query.", (1, 1)));

            SDLShouldFail(@"
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
            SDLShouldFail(@"
              type Query {
                field: String
              }

              input Mutation {
                test: String
              }
            ", Error("Mutation root type must be Object type if provided, it cannot be Mutation.", (5, 1)));

            SDLShouldFail(@"
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
            SDLShouldFail(@"
              type Query {
                field: String
              }

              input Subscription {
                test: String
              }
            ", Error("Subscription root type must be Object type if provided, it cannot be Subscription.", (5, 1)));

            SDLShouldFail(@"
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
}