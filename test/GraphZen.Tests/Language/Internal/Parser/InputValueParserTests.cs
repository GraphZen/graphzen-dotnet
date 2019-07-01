// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    public class InputValueParserTests : ParserTestBase
    {
        [Theory]
        [InlineData("-4", -4)]
        [InlineData("4", 4)]
        [InlineData("9", 9)]
        [InlineData("0", 0)]
        public void ParseIntValue(string source, int intValue) =>
            ParseValue(source).Should().Be(IntValue(intValue));

        [Theory]
        [InlineData("4.123")]
        [InlineData("-4.123")]
        [InlineData("0.123")]
        [InlineData("123e4")]
        [InlineData("123E4")]
        [InlineData("123e-4")]
        [InlineData("123e+4")]
        [InlineData("-1.123e4")]
        [InlineData("-1.123E4")]
        [InlineData("-1.123e-4")]
        [InlineData("-1.123e+4")]
        public void ParseFloatValue(string source) =>
            ParseValue(source).Should().Be(FloatValue(source));

        [Theory]
        [InlineData(@"""foo""", "foo")]
        public void ParseStringValue(string source, string stringValue) =>
            ParseValue(source).Should().Be(StringValue(stringValue));

        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        public void ParseBooleanValue(string source, bool booleanValue) =>
            ParseValue(source).Should().Be(BooleanValue(booleanValue));

        [Theory]
        [InlineData("null")]
        public void ParseNullValue(string source) =>
            ParseValue(source).Should().Be(NullValue());

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData("BAZ_bar")]
        public void ParseEnumValue(string source)
        {
            ParseValue(source).Should().Be(EnumValue(Name(source)));
        }

        [Fact]
        public void ParseInputObjectValue() => ParseValue(@"
          {
            foo: 1
            bar: $test
          }
        ").Should().Be(ObjectValue(
            ObjectField(Name("foo"), IntValue(1)),
            ObjectField(Name("bar"), Variable(Name("test")))));

        [Fact]
        public void ParseListValue() =>
            ParseValue(@"[foo, ""bar""]").Should()
                .Be(ListValue(EnumValue(Name("foo")),
                    StringValue("bar")));
    }
}