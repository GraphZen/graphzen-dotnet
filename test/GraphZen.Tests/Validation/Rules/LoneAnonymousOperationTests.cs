// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

using Xunit;
using static GraphZen.Validation.Rules.LoneAnonymousOperation;

namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class LoneAnonymousOperationTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = ValidationRules.LoneAnonymousOperation;

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
        public void MultipleNamedOperations() => QueryShouldPass(@"

          query Foo {
            field
          }

          query Bar {
            field
          }

        ");

        [Fact]
        public void MultipleAnonymousOperations() => QueryShouldFail(@"

          {
            fieldA
          }
          {
            fieldB
          }

        ",
            Error(AnonymousOperationNotAloneMessage, (3, 11)),
            Error(AnonymousOperationNotAloneMessage, (6, 11)));

        [Fact]
        public void AnonymousOperationWithAMutation() => QueryShouldFail(@"

          {
            fieldA
          }
          mutation Foo {
            fieldB
          }

        ",
            Error(AnonymousOperationNotAloneMessage, (3, 11)));

        [Fact]
        public void AnonymousOperationWithASubscription() => QueryShouldFail(@"

          {
            fieldA
          }
          subscription Foo {
            fieldB
          }

        ",
            Error(AnonymousOperationNotAloneMessage, (3, 11)));
    }
}