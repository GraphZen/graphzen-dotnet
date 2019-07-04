// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.Utilities;

using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;
using static GraphZen.Utilities.Helpers;
using ListType = GraphZen.TypeSystem.ListType;

namespace GraphZen
{
    [NoReorder]
    public class AstFromValueTests
    {
        private static Maybe<object> Some(object some) => Maybe.Some(some);
        private static Maybe<object> None() => Maybe.None<object>();


        private static readonly object ComplexValue = new {someArbitrary = "complexValue"};

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
            AstFromValue(Some(true), SpecScalars.Boolean).Should().Be(BooleanValue(true));

            AstFromValue(Some(false), SpecScalars.Boolean).Should().Be(BooleanValue(false));

            AstFromValue(None(), SpecScalars.Boolean).Should().Be(null);

            AstFromValue(Some(null), SpecScalars.Boolean).Should().Be(NullValue());

            AstFromValue(Some(0), SpecScalars.Boolean).Should().Be(BooleanValue(false));

            AstFromValue(Some(1), SpecScalars.Boolean).Should().Be(BooleanValue(true));

            AstFromValue(Some(1), NonNullType.Of(SpecScalars.Boolean)).Should().Be(BooleanValue(true));

            AstFromValue(Some(0), NonNullType.Of(SpecScalars.Boolean)).Should().Be(BooleanValue(false));
        }


        [Fact]
        public void ItConvertsIntValuesToIntValueNodes()
        {
            AstFromValue(Some(-1), SpecScalars.Int).Should().Be(IntValue(-1));

            AstFromValue(Some(123.0), SpecScalars.Int).Should().Be(IntValue(123));

            AstFromValue(Some(1e4), SpecScalars.Int).Should().Be(IntValue(10000));

            Assert.Throws<Exception>(() => AstFromValue(Some(123.5), SpecScalars.Int))
                .Message.Should().Be("Int cannot represent non-integer value: 123.5");

            Assert.Throws<Exception>(() => AstFromValue(Some(1e40), SpecScalars.Int))
                .Message.Should().Be("Int cannot represent non 32-bit signed integer value: 1E+40");
        }

        [Fact]
        public void ItConvertsFloatvaluesToIntFloatNodeValues()
        {
            AstFromValue(Some(-1), SpecScalars.Float).Should().Be(IntValue(-1));

            AstFromValue(Some(123.0), SpecScalars.Float).Should().Be(IntValue(123));

            AstFromValue(Some(123.5), SpecScalars.Float).Should().Be(FloatValue("123.5"));

            AstFromValue(Some(1e4), SpecScalars.Float).Should().Be(IntValue(10000));

            AstFromValue(Some(1e40), SpecScalars.Float).Should().Be(FloatValue("1e+40"));
        }

        [Fact]
        public void ItConvertsStringValuesToStringValueNodes()
        {
            AstFromValue(Some("hello"), SpecScalars.String).Should().Be(StringValue("hello"));

            AstFromValue(Some("VALUE"), SpecScalars.String).Should().Be(StringValue("VALUE"));

            AstFromValue(Some("VA\nLUE"), SpecScalars.String).Should().Be(StringValue("VA\nLUE"));

            AstFromValue(Some(123), SpecScalars.String).Should().Be(StringValue("123"));

            AstFromValue(Some(false), SpecScalars.String).Should().Be(StringValue("false"));

            AstFromValue(Some(true), SpecScalars.String).Should().Be(StringValue("true"));

            AstFromValue(Some(null), SpecScalars.String).Should().Be(NullValue());

            AstFromValue(None(), SpecScalars.String).Should().Be(null);
        }

        [Fact]
        public void ItConvertsIdValuesToIntStringValueNodes()
        {
            AstFromValue(Some("hello"), SpecScalars.ID).Should().Be(StringValue("hello"));

            AstFromValue(Some("VALUE"), SpecScalars.ID).Should().Be(StringValue("VALUE"));

            AstFromValue(Some("VA\nLUE"), SpecScalars.ID).Should().Be(StringValue("VA\nLUE"));

            AstFromValue(Some(-1), SpecScalars.ID).Should().Be(IntValue(-1));

            AstFromValue(Some(123), SpecScalars.ID).Should().Be(IntValue(123));

            AstFromValue(Some("123"), SpecScalars.ID).Should().Be(IntValue(123));

            AstFromValue(Some("01"), SpecScalars.ID).Should().Be(StringValue("01"));

            Assert.Throws<Exception>(() => AstFromValue(Some(false), SpecScalars.ID)).Message
                .Should().Be("ID cannot represent value: false");

            AstFromValue(Some(null), SpecScalars.ID).Should().Be(NullValue());

            AstFromValue(None(), SpecScalars.ID).Should().Be(null);
        }

        [Fact]
        public void ItConvertsNonNullValuesToNullValue()
        {
            var nnBoolean = NonNullType.Of(SpecScalars.Boolean);
            AstFromValue(null, nnBoolean).Should().Be(null);
        }


        [Fact]
        public void ItConvertsStringValuesToEnumValueNodesIfPossible()
        {
            AstFromValue(Some("HELLO"), MyEnum).Should().Be(EnumValue(Name("HELLO")));
            AstFromValue(Some(ComplexValue), MyEnum).Should().Be(EnumValue(Name("COMPLEX")));
            AstFromValue(Some("hello"), MyEnum).Should().Be(null);
            AstFromValue(Some("VALUE"), MyEnum).Should().Be(null);
        }


        [Fact]
        public void ItConvertsArrayValuesToListValueNodes()
        {
            AstFromValue(Some(new object[] {"FOO", "BAR"}), ListType.Of(SpecScalars.String)).Should()
                .Be(ListValue(StringValue("FOO"), StringValue("BAR")));

            AstFromValue(Some(new[] {"HELLO", "GOODBYE"}), ListType.Of(MyEnum))
                .Should()
                .Be(ListValue(EnumValue(Name("HELLO")), EnumValue(Name("GOODBYE"))));
        }

        [Fact]
        public void ItConvertsListSingltons()
        {
            AstFromValue(Some("FOO"), ListType.Of(SpecScalars.String)).Should().Be(StringValue("FOO"));
        }

        [Fact]
        public void ItConvertsInputObjects()
        {
            AstFromValue(Some(new
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
            AstFromValue(Some(new
            {
                foo = (string) null
            }), MyInputObj).Should().Be(ObjectValue(ObjectField(Name("foo"), NullValue())));
        }
    }
}