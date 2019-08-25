// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable disable
using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;
using static GraphZen.TypeSystem.Internal.AstFromValue;
using ListType = GraphZen.TypeSystem.ListType;

namespace GraphZen
{
    [NoReorder]
    public class AstFromValueTests
    {
        private static Maybe<object> Some(object some)
        {
            return Maybe.Some(some);
        }

        private static Maybe<object> None()
        {
            return Maybe.None<object>();
        }


        private static readonly object ComplexValue = new { someArbitrary = "complexValue" };

        private static readonly Schema TestSchema = Schema.Create(_ =>
        {
            _.Enum("MyEnum")
                .Value("HELLO")
                .Value("GOODBYE")
                .Value("COMPLEX", ev => ev.CustomValue(ComplexValue));

            _.InputObject("MyInputObj")
                .Field("foo", "Float")
                .Field("bar", "MyEnum");
        });

        private static readonly EnumType MyEnum = TestSchema.GetType<EnumType>("MyEnum");
        private static readonly InputObjectType MyInputObj = TestSchema.GetType<InputObjectType>("MyInputObj");


        [Fact]
        public void ConvertsBooleanValuesToBooleanValueNodes()
        {
            Get(Some(true), SpecScalars.Boolean).Should().Be(BooleanValue(true));

            Get(Some(false), SpecScalars.Boolean).Should().Be(BooleanValue(false));

            Get(None(), SpecScalars.Boolean).Should().Be(null);

            Get(Some(null), SpecScalars.Boolean).Should().Be(NullValue());

            Get(Some(0), SpecScalars.Boolean).Should().Be(BooleanValue(false));

            Get(Some(1), SpecScalars.Boolean).Should().Be(BooleanValue(true));

            Get(Some(1), NonNullType.Of(SpecScalars.Boolean)).Should().Be(BooleanValue(true));

            Get(Some(0), NonNullType.Of(SpecScalars.Boolean)).Should().Be(BooleanValue(false));
        }


        [Fact]
        public void ItConvertsIntValuesToIntValueNodes()
        {
            Get(Some(-1), SpecScalars.Int).Should().Be(IntValue(-1));

            Get(Some(123.0), SpecScalars.Int).Should().Be(IntValue(123));

            Get(Some(1e4), SpecScalars.Int).Should().Be(IntValue(10000));

            Assert.Throws<Exception>(() => Get(Some(123.5), SpecScalars.Int))
                .Message.Should().Be("Int cannot represent non-integer value: 123.5");

            Assert.Throws<Exception>(() => Get(Some(1e40), SpecScalars.Int))
                .Message.Should().Be("Int cannot represent non 32-bit signed integer value: 1E+40");
        }

        [Fact]
        public void ItConvertsFloatvaluesToIntFloatNodeValues()
        {
            Get(Some(-1), SpecScalars.Float).Should().Be(IntValue(-1));

            Get(Some(123.0), SpecScalars.Float).Should().Be(IntValue(123));

            Get(Some(123.5), SpecScalars.Float).Should().Be(FloatValue("123.5"));

            Get(Some(1e4), SpecScalars.Float).Should().Be(IntValue(10000));

            Get(Some(1e40), SpecScalars.Float).Should().Be(FloatValue("1e+40"));
        }

        [Fact]
        public void ItConvertsStringValuesToStringValueNodes()
        {
            Get(Some("hello"), SpecScalars.String).Should().Be(StringValue("hello"));

            Get(Some("VALUE"), SpecScalars.String).Should().Be(StringValue("VALUE"));

            Get(Some("VA\nLUE"), SpecScalars.String).Should().Be(StringValue("VA\nLUE"));

            Get(Some(123), SpecScalars.String).Should().Be(StringValue("123"));

            Get(Some(false), SpecScalars.String).Should().Be(StringValue("false"));

            Get(Some(true), SpecScalars.String).Should().Be(StringValue("true"));

            Get(Some(null), SpecScalars.String).Should().Be(NullValue());

            Get(None(), SpecScalars.String).Should().Be(null);
        }

        [Fact]
        public void ItConvertsIdValuesToIntStringValueNodes()
        {
            Get(Some("hello"), SpecScalars.ID).Should().Be(StringValue("hello"));

            Get(Some("VALUE"), SpecScalars.ID).Should().Be(StringValue("VALUE"));

            Get(Some("VA\nLUE"), SpecScalars.ID).Should().Be(StringValue("VA\nLUE"));

            Get(Some(-1), SpecScalars.ID).Should().Be(IntValue(-1));

            Get(Some(123), SpecScalars.ID).Should().Be(IntValue(123));

            Get(Some("123"), SpecScalars.ID).Should().Be(IntValue(123));

            Get(Some("01"), SpecScalars.ID).Should().Be(StringValue("01"));

            Assert.Throws<Exception>(() => Get(Some(false), SpecScalars.ID)).Message
                .Should().Be("ID cannot represent value: false");

            Get(Some(null), SpecScalars.ID).Should().Be(NullValue());

            Get(None(), SpecScalars.ID).Should().Be(null);
        }

        [Fact]
        public void ItConvertsNonNullValuesToNullValue()
        {
            var nnBoolean = NonNullType.Of(SpecScalars.Boolean);
            Get(null, nnBoolean).Should().Be(null);
        }


        [Fact]
        public void ItConvertsStringValuesToEnumValueNodesIfPossible()
        {
            Get(Some("HELLO"), MyEnum).Should().Be(EnumValue(Name("HELLO")));
            Get(Some(ComplexValue), MyEnum).Should().Be(EnumValue(Name("COMPLEX")));
            Get(Some("hello"), MyEnum).Should().Be(null);
            Get(Some("VALUE"), MyEnum).Should().Be(null);
        }


        [Fact]
        public void ItConvertsArrayValuesToListValueNodes()
        {
            Get(Some(new object[] { "FOO", "BAR" }), ListType.Of(SpecScalars.String)).Should()
                .Be(ListValue(StringValue("FOO"), StringValue("BAR")));

            Get(Some(new[] { "HELLO", "GOODBYE" }), ListType.Of(MyEnum))
                .Should()
                .Be(ListValue(EnumValue(Name("HELLO")), EnumValue(Name("GOODBYE"))));
        }

        [Fact]
        public void ItConvertsListSingltons()
        {
            Get(Some("FOO"), ListType.Of(SpecScalars.String)).Should().Be(StringValue("FOO"));
        }

        [Fact]
        public void ItConvertsInputObjects()
        {
            Get(Some(new
            {
                foo = 3,
                bar = "HELLO"
            }), MyInputObj).Should()
                .Be(ObjectValue(ObjectField(Name("foo"), IntValue(3)),
                    ObjectField(Name("bar"), EnumValue(Name("HELLO")))));
        }

        [Fact]
        public void ItConvertsInputObjectsWithExplicitNulls()
        {
            Get(Some(new
            {
                foo = (string)null
            }), MyInputObj).Should().Be(ObjectValue(ObjectField(Name("foo"), NullValue())));
        }
    }
}