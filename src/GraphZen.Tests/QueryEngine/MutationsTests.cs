// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using Xunit;

namespace GraphZen.QueryEngine
{
    [NoReorder]
    public class MutationsTests : ExecutorHarness
    {
        public class NumberHolder
        {
            public NumberHolder(int theNumber)
            {
                TheNumber = theNumber;
            }

            public int TheNumber { get; set; }
        }

        public class Root
        {
            public Root(int originalNumber)

            {
                NumberHolder = new NumberHolder(originalNumber);
            }

            public NumberHolder NumberHolder { get; }

            public NumberHolder ImmediatelyChangeTheNumber(int newNumber)
            {
                NumberHolder.TheNumber = newNumber;
                return NumberHolder;
            }

            public async Task<NumberHolder> PromiseToChangeTheNumber(int newNumber)
            {
                await Task.Delay(10);
                NumberHolder.TheNumber = newNumber;
                return NumberHolder;
            }

            public NumberHolder FailToChangeTheNumber() => throw new Exception("Cannot change the number");

            public Task<NumberHolder> PromiseAndFailToChangeTheNumber() => Task.Run(() =>
            {
                throw new Exception("Cannot change the number");
#pragma warning disable 162
                return Task.FromResult(NumberHolder);
#pragma warning restore 162
            });
        }

        public static Schema Schema = Schema.Create(sb =>
        {
            sb.Object<NumberHolder>().Field(_ => _.TheNumber, _ => _.FieldType("Int"));
            sb.Object<Root>()
                .Name("Mutation")
                .Field("immediatelyChangeTheNumber", "NumberHolder", _ => _.Argument("newNumber", "Int").Resolve(
                    (root, args) => root.ImmediatelyChangeTheNumber(args.newNumber)))
                .Field("promiseToChangeTheNumber", "NumberHolder", _ => _.Argument("newNumber", "Int").Resolve(
                    (root, args) => root.PromiseToChangeTheNumber(args.newNumber)))
                .Field("failToChangeTheNumber", "NumberHolder", _ => _.Argument("newNumber", "Int").Resolve(
                    (root, args) => root.FailToChangeTheNumber()))
                .Field("promiseAndFailToChangeTheNumber", "NumberHolder", _ => _.Argument("newNumber", "Int").Resolve(
                    (root, args) => root.PromiseAndFailToChangeTheNumber()))
                ;

            sb.QueryType("NumberHolder");
            sb.MutationType("Mutation");
        });


        [Fact]
        public Task EvaluatesMutationsSerially() => ExecuteAsync(Schema, @"
            mutation M {
              first: immediatelyChangeTheNumber(newNumber: 1) {
                theNumber
              },
              second: promiseToChangeTheNumber(newNumber: 2) {
                theNumber
              },
              third: immediatelyChangeTheNumber(newNumber: 3) {
                theNumber
              }
              fourth: promiseToChangeTheNumber(newNumber: 4) {
                theNumber
              },
              fifth: immediatelyChangeTheNumber(newNumber: 5) {
                theNumber
              }
            }", new Root(6)).ShouldEqual(new
        {
            data = new
            {
                first = new { theNumber = 1 },
                second = new { theNumber = 2 },
                third = new { theNumber = 3 },
                fourth = new { theNumber = 4 },
                fifth = new { theNumber = 5 }
            }
        });


        [Fact]
        public Task evaluates_mutation_correctly_in_presence_of_failed_mutation() => ExecuteAsync(Schema, @"
            mutation M {
              first: immediatelyChangeTheNumber(newNumber: 1) {
                theNumber
              },
              second: promiseToChangeTheNumber(newNumber: 2) {
                theNumber
              },
              third: failToChangeTheNumber(newNumber: 3) {
                theNumber
              }
              fourth: promiseToChangeTheNumber(newNumber: 4) {
                theNumber
              },
              fifth: immediatelyChangeTheNumber(newNumber: 5) {
                theNumber
              },
              sixth: promiseAndFailToChangeTheNumber(newNumber: 6) {
                theNumber
              }
            }", new Root(6)).ShouldEqual(new
        {
            data = new
            {
                first = new { theNumber = 1 },
                second = new { theNumber = 2 },
                third = (object)null,
                fourth = new { theNumber = 4 },
                fifth = new { theNumber = 5 },
                sixth = (object)null
            },
            errors = new object[]
            {
                new
                {
                    message = "Cannot change the number",
                    locations = new object[]
                    {
                        new {line = 9, column = 15}
                    },
                    path = new object[] {"third"}
                },
                new
                {
                    message = "Cannot change the number",
                    locations = new object[]
                    {
                        new {line = 18, column = 15}
                    },
                    path = new object[] {"sixth"}
                }
            }
        });
    }
}