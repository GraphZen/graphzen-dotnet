// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.QueryEngine.Validation.Rules;
using Xunit;

namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class UniqueOperationNamesTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.UniqueOperationNames;

        [Fact]
        public void NoOperations() => QueryShouldPass(@"

          fragment fragA on Type {
            field
          }

        ");

        [Fact]
        public void OneAnonymousOperation() => QueryShouldPass(@"

          {
            field
          }

        ");

        [Fact]
        public void OneNameOperation() => QueryShouldPass(@"
        
          query Foo {
            field
          }

        ");

        [Fact]
        public void MultipleOperations() => QueryShouldPass(@" 

          query Foo {
            field
          }
          query Bar {
            field
          }

        ");

        [Fact]
        public void MultipleOperationsOfDifferentTypes() => QueryShouldPass(@" 

          query Foo {
            field
          }
          mutation Bar {
            field
          }
          subscription Baz {
            field
          }

        ");

        [Fact]
        public void FragmentAndOperationNamedTheSame() => QueryShouldPass(@" 

          query Foo {
            ...Foo
          }
          fragment Foo on Type {
            field
          }

        ");

        private static ExpectedError
            DuplicateOp(string operationName, params (int line, int column)[] lineColumnPairs) =>
            Error(UniqueOperationNames.DuplicateOperationNameMessage(operationName), lineColumnPairs);

        [Fact]
        public void MultipleOperationsOfSameNameMutation() => QueryShouldFail(@" 

          query Foo {
            fieldA
          }
          query Foo {
            fieldB
          }

        ", DuplicateOp("Foo", (3, 17), (6, 17)));


        [Fact]
        public void MultipleOperationsOfSameNameSubscription() => QueryShouldFail(@" 
      
          query Foo {
            fieldA
          }
          subscription Foo {
            fieldB
          }

        ", DuplicateOp("Foo", (3, 17), (6, 24)));
    }
}