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
    public class KnownFragmentNamesTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = QueryValidationRules.KnownFragmentNames;

        [Fact]
        public void KnownFragmentNamesAreValid() => QueryShouldPass(@"

          {
            human(id: 4) {
              ...HumanFields1
              ... on Human {
                ...HumanFields2
              }
              ... {
                name
              }
            }
          }
          fragment HumanFields1 on Human {
            name
            ...HumanFields3
          }
          fragment HumanFields2 on Human {
            name
          }
          fragment HumanFields3 on Human {
            name
          }

        ");

        [Fact]
        public void UnknownFragmentNames() => QueryShouldFail(@"

          {
            human(id: 4) {
              ...UnknownFragment1
              ... on Human {
                ...UnknownFragment2
              }
            }
          }

          fragment HumanFields on Human {
            name
            ...UnknownFragment3
          }

        ",
            UndefinedFragment("UnknownFragment1", 5, 18),
            UndefinedFragment("UnknownFragment2", 7, 20),
            UndefinedFragment("UnknownFragment3", 14, 16)
        );

        private static ExpectedError UndefinedFragment(string fragmentName, int line, int column) =>
            Error(KnownFragmentNames.UnknownFragmentMessage(fragmentName), (line, column));
    }
}