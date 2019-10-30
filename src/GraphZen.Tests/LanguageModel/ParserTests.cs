// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.LanguageModel
{
    public class ParserTests : ParserTestBase
    {
        [Fact]
        public void ItCanParseKitchenSink()
        {
            var block = @""""""" \"""""" """"""";
            var tokens = SuperPowerTokenizer.Instance.Tokenize(block);
            Assert.Equal(TokenKind.BlockString, tokens.First().Kind);

            var kitchenSink = File.ReadAllText("./LanguageModel/kitchen-sink.graphql");
            ParseDocument(kitchenSink);
        }


        [Fact]
        public void ItCanParseSchemaKitchenSink()
        {
            var kitchenSink = File.ReadAllText("./LanguageModel/schema-kitchen-sink.graphql");
            var result = ParseDocument(kitchenSink);
            var printResult = ParseDocument(result.ToSyntaxString());
            TestHelpers.AssertEquals(result.ToSyntaxString(), printResult.ToSyntaxString());
            Assert.Equal(result, printResult);
        }

        [Fact]
        public void ItCanPrintKitchenSink()
        {
            var kitchenSink = File.ReadAllText("./LanguageModel/schema-kitchen-sink.graphql");
            var result = ParseDocument(kitchenSink);
            var printResult = result.ToSyntaxString();
            var expected = @"
              schema {
                query: QueryType
                mutation: MutationType
              }

              """"""
              This is a description
              of the `Foo` type.
              """"""
              type Foo implements Bar & Baz {
                one: Type
                """"""
                This is a description of the `two` field.
                """"""
                two(
                  """"""
                  This is a description of the `argument` argument.
                  """"""
                  argument: InputType!
                ): Type
                three(argument: InputType, other: String): Int
                four(argument: String = ""string""): String
                five(argument: [String] = [""string"", ""string""]): String
                six(argument: InputType = {key: ""value""}): Type
                seven(argument: Int = null): Type
              }

              type AnnotatedObject @onObject(arg: ""value"") {
                annotatedField(arg: Type = ""default"" @onArg): Type @onField
              }

              type UndefinedType

              extend type Foo {
                seven(argument: [String]): Type
              }

              extend type Foo @onType

              interface Bar {
                one: Type
                four(argument: String = ""string""): String
              }

              interface AnnotatedInterface @onInterface {
                annotatedField(arg: Type @onArg): Type @onField
              }

              interface UndefinedInterface

              extend interface Bar {
                two(argument: InputType!): Type
              }

              extend interface Bar @onInterface

              union Feed = Story | Article | Advert

              union AnnotatedUnion @onUnion = A | B

              union AnnotatedUnionTwo @onUnion = A | B

              union UndefinedUnion

              extend union Feed = Photo | Video

              extend union Feed @onUnion

              scalar CustomScalar

              scalar AnnotatedScalar @onScalar

              extend scalar CustomScalar @onScalar

              enum Site {
                DESKTOP
                MOBILE
              }

              enum AnnotatedEnum @onEnum {
                ANNOTATED_VALUE @onEnumValue
                OTHER_VALUE
              }

              enum UndefinedEnum

              extend enum Site {
                VR
              }

              extend enum Site @onEnum

              input InputType {
                key: String!
                answer: Int = 42
              }

              input AnnotatedInput @onInputObject {
                annotatedField: Type @onField
              }

              input UndefinedInput

              extend input InputType {
                other: Float = 1.23e4
              }

              extend input InputType @onInputObject

              directive @skip(if: Boolean!) on FIELD | FRAGMENT_SPREAD | INLINE_FRAGMENT

              directive @include(if: Boolean!) on FIELD | FRAGMENT_SPREAD | INLINE_FRAGMENT

              directive @include2(if: Boolean!) on FIELD | FRAGMENT_SPREAD | INLINE_FRAGMENT

              extend schema @onSchema

              extend schema @onSchema {
                subscription: SubscriptionType
              }
            ".Dedent();
            TestHelpers.AssertEquals(expected, printResult, new ResultComparisonOptions
            {
                ShowActual = true,
                ShowExpected = false,
                ShowDiffs = true
            });
        }

        [Fact]
        public void ItProvidesUsefulErrors()
        {
            AssertSyntaxError("{", "Syntax error: unexpected end of input, expected selection.", (1, 2));
        }

        [Fact]
        public void SourceHasBody()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Source(null));
            Assert.Contains("body", exception.Message);
        }

        [Fact]
        public void SourceIsProvided()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => { ParseDocument(null); });
            Assert.Contains("document", exception.Message);
        }
    }
}