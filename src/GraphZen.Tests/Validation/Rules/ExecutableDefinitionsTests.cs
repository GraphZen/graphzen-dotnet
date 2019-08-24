#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.QueryEngine.Validation.Rules;
using Xunit;

namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class ExecutableDefinitionsTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.ExecutableDefinitions;

        private static ExpectedError NonExecutableDefinition(string defName, int line, int column) =>
            Error(ExecutableDefinitions.NonExecutableDefinitionMessage(defName), (line, column));

        [Fact]
        public void WithOnlyOperation() => QueryShouldPass(@"

          query Foo {
            dog {
              name
            }
          }

        ");

        [Fact]
        public void WithOperationAndFragment() => QueryShouldPass(@"

          query Foo {
            dog {
              name
              ...Frag
            }
          }

          fragment Frag on Dog {
            name
          }

        ");

        [Fact]
        public void WithTypeDefinition() => QueryShouldFail(@"

          query Foo {
            dog {
              name
            }
          }

          type Cow {
            name: String
          }

          extend type Dog {
            color: String
          }

        ",
            NonExecutableDefinition("Cow", 9, 11),
            NonExecutableDefinition("Dog", 13, 11)
        );

        [Fact]
        public void WithSchemaDefinition() => QueryShouldFail(@"

          schema {
            query: Query
          }

          type Query {
            test: String
          }

          extend schema @directive

        ",
            NonExecutableDefinition("schema", 3, 11),
            NonExecutableDefinition("Query", 7, 11),
            NonExecutableDefinition("schema", 11, 11)
        );
    }
}