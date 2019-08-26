// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation;
using GraphZen.QueryEngine.Validation.Rules;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class KnownDirectivesTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.KnownDirectives;


        [Fact]
        public void WithNoDirectives()
        {
            QueryShouldPass(@"

          query Foo {
            name
            ...Frag
          }

          fragment Frag on Dog {
            name
          }

        ");
        }

        [Fact]
        public void WithKnownDirectives()
        {
            QueryShouldPass(@"

          {
            dog @include(if: true) {
              name
            }
            human @skip(if: false) {
              name
            }
          }

        ");
        }


        private static ExpectedError UnknownDirective(string directiveName, int line, int column)
        {
            return Error(KnownDirectives.UnknownDirectiveMessage(directiveName), (line, column));
        }

        [Fact]
        public void WithUnknownDirectives()
        {
            QueryShouldFail(@"

          {
            dog @unknown(directive: ""value"") {
              name
            }
          }

        ", UnknownDirective("unknown", 4, 17));
        }

        [Fact]
        public void WithManyUnknownDirectives()
        {
            QueryShouldFail(@"

          {
            dog @unknown(directive: ""value"") {
              name
            }
            human @unknown(directive: ""value"") {
              name
              pets @unknown(directive: ""value"") {
                name
              }
            }
          }

        ",
                UnknownDirective("unknown", 4, 17),
                UnknownDirective("unknown", 7, 19),
                UnknownDirective("unknown", 9, 20)
            );
        }

        [Fact]
        public void WithWellPlacedDirectives()
        {
            QueryShouldPass(@"

          query Foo($var: Boolean) @onQuery {
            name @include(if: $var)
            ...Frag @include(if: true)
            skippedField @skip(if: true)
            ...SkippedFrag @skip(if: true)
          }

          mutation Bar @onMutation {
            someField
          }

        ");
        }

        [Fact(Skip = "not implemented")]
        public void ExperimentalWithWellPlacedVariableDefinitionDirectives()
        {
            QueryShouldPass(@"

          query Foo($var: Boolean @onVariableDefinition) {
            name
          }

        ");
        }

        private static ExpectedError MisplacedDirective(string directiveName, string placement, int line, int column)
        {
            return Error(KnownDirectives.MisplacedDirectiveMessage(directiveName, placement), (line, column));
        }

        [Fact]
        public void WithMisplacedDirectives()
        {
            QueryShouldFail(@"

          query Foo($var: Boolean) @include(if: true) {
            name @onQuery @include(if: $var)
            ...Frag @onQuery
          }

          mutation Bar @onQuery {
            someField
          }

        ",
                MisplacedDirective("include", "QUERY", 3, 36),
                MisplacedDirective("onQuery", "FIELD", 4, 18),
                MisplacedDirective("onQuery", "FRAGMENT_SPREAD", 5, 21),
                MisplacedDirective("onQuery", "MUTATION", 8, 24)
            );
        }
    }
}