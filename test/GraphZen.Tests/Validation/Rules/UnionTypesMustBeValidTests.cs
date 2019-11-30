// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Validation.Rules
{
    [NoReorder]
    public class UnionTypesMustBeValidTests : ValidationRuleHarness
    {
        public override ValidationRule RuleUnderTest { get; } = DocumentValidationRules.UnionTypesMustBeValid;

        [Fact]
        public void AcceptsUnionTypeWithMemberTypes()
        {
            SDLShouldPass(@"
          type Query {
            test: GoodUnion
          }

          type TypeA {
            field: String
          }

          type TypeB {
            field: String
          }

          union GoodUnion =
            | TypeA
            | TypeB
        ");
        }

        [Fact]
        public void RejectsUnionTypeWithEmptyTypes()
        {
            SDLShouldFail(@"
          type Query {
            test: BadUnion
          }

          union BadUnion
            
          directive @test on UNION

          extend union BadUnion @test
        ", Error("Union type BadUnion must define one or more member types.", (5, 1), (9, 1)));
        }

        [Fact]
        public void RejectsAUnionTypeWithDuplicatedMemberType()
        {
            SDLShouldFail(@"
              type Query {
                test: BadUnion
              }

              type TypeA {
                field: String
              }

              type TypeB {
                field: String
              }

              union BadUnion =
                | TypeA
                | TypeB
                | TypeA

              extend union BadUnion = TypeB
            ", Error("Union type BadUnion can only include type TypeA once.", (14, 5), (16, 5)),
                Error("Union type BadUnion can only include type TypeB once.", (15, 5), (18, 25)));
        }

        [Fact]
        public void ItRejectsAUnionTypeWithNonObjectMemberTypes()
        {
            SDLShouldFail(@"
              type Query {
                test: BadUnion
              }

              type TypeA {
                field: String
              }

              type TypeB {
                field: String
              }

              union BadUnion =
                | TypeA
                | String
                | TypeB

              extend union BadUnion = Int
            ",
                Error("Union type BadUnion can only include Object types, it cannot include String.", (15, 5)),
                Error("Union type BadUnion can only include Object types, it cannot include Int.", (18, 25))
            );
        }
    }
}