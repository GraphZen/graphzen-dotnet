// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;



namespace GraphZen.Tests.LanguageModel.Internal.Parser
{
    public class InputValueParserTests : ParserTestBase
    {
        [Theory]
        [InlineData("-4", -4)]
        [InlineData("4", 4)]
        [InlineData("9", 9)]
        [InlineData("0", 0)]
        public void ParseIntValue(string source, int intValue)
        {
            Assert.Equal(SyntaxFactory.IntValue(intValue), ParseValue(source));
        }

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
        public void ParseFloatValue(string source)
        {
            Assert.Equal(SyntaxFactory.FloatValue(source), ParseValue(source));
        }

        [Theory]
        [InlineData(@"""foo""", "foo")]
        public void ParseStringValue(string source, string stringValue)
        {
            Assert.Equal(SyntaxFactory.StringValue(stringValue), ParseValue(source));
        }

        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        public void ParseBooleanValue(string source, bool booleanValue)
        {
            Assert.Equal(SyntaxFactory.BooleanValue(booleanValue), ParseValue(source));
        }

        [Theory]
        [InlineData("null")]
        public void ParseNullValue(string source)
        {
            Assert.Equal(SyntaxFactory.NullValue(), ParseValue(source));
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData("BAZ_bar")]
        public void ParseEnumValue(string source)
        {
            Assert.Equal(SyntaxFactory.EnumValue(SyntaxFactory.Name(source)), ParseValue(source));
        }

        [Fact]
        public void ParseInputObjectValue()
        {
            Assert.Equal(SyntaxFactory.ObjectValue(
                SyntaxFactory.ObjectField(SyntaxFactory.Name("foo"), SyntaxFactory.IntValue(1)),
                SyntaxFactory.ObjectField(SyntaxFactory.Name("bar"),
                    SyntaxFactory.Variable(SyntaxFactory.Name("test")))), ParseValue(@"
          {
            foo: 1
            bar: $test
          }
        "));
        }

        [Fact]
        public void ParseListValue()
        {
            Assert.Equal(SyntaxFactory.ListValue(SyntaxFactory.EnumValue(SyntaxFactory.Name("foo")),
                    SyntaxFactory.StringValue("bar")), ParseValue(@"[foo, ""bar""]"));
        }
    }
}
