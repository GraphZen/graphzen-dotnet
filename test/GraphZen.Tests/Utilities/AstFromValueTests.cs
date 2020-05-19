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
using NonNullType = GraphZen.TypeSystem.NonNullType;

namespace GraphZen.Tests.Utilities
{
    [NoReorder]
    public class AstFromValueTests
    {
        private static Maybe<object> Some(object some) => Maybe.Some(some);

        private static Maybe<object> None() => Maybe.None<object>();


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

        private static readonly EnumType MyEnum = TestSchema.GetEnum("MyEnum");
        private static readonly InputObjectType MyInputObj = TestSchema.GetInputObject("MyInputObj");


        [Fact]
        public void ConvertsBooleanValuesToBooleanValueNodes()
        {
            var schema = Schema.Create();
            var boolean = schema.GetScalar<bool>();
            Get(Some(true), boolean).Should().Be(BooleanValue(true));

            Get(Some(false), boolean).Should().Be(BooleanValue(false));

            Get(None(), boolean).Should().Be(null);

            Get(Some(null), boolean).Should().Be(NullValue());

            Get(Some(0), boolean).Should().Be(BooleanValue(false));

            Get(Some(1), boolean).Should().Be(BooleanValue(true));

            Get(Some(1), NonNullType.Of(boolean)).Should().Be(BooleanValue(true));

            Get(Some(0), NonNullType.Of(boolean)).Should().Be(BooleanValue(false));
        }


        [Fact]
        public void ItConvertsIntValuesToIntValueNodes()
        {
            var intType = Schema.Create().GetScalar<int>();
            Get(Some(-1), intType).Should().Be(IntValue(-1));

            Get(Some(123.0), intType).Should().Be(IntValue(123));

            Get(Some(1e4), intType).Should().Be(IntValue(10000));

            Assert.Throws<Exception>(() => Get(Some(123.5), intType))
                .Message.Should().Be("Int cannot represent non-integer value: 123.5");

            Assert.Throws<Exception>(() => Get(Some(1e40), intType))
                .Message.Should().Be("Int cannot represent non 32-bit signed integer value: 1E+40");
        }

        [Fact]
        public void ItConvertsFloatValuesToIntFloatNodeValues()
        {
            var floatType = Schema.Create().GetScalar<float>();
            Get(Some(-1), floatType).Should().Be(IntValue(-1));

            Get(Some(123.0), floatType).Should().Be(IntValue(123));

            Get(Some(123.5), floatType).Should().Be(FloatValue("123.5"));

            Get(Some(1e4), floatType).Should().Be(IntValue(10000));

            Get(Some(1e40), floatType).Should().Be(FloatValue("1e+40"));
        }

        [Fact]
        public void ItConvertsStringValuesToStringValueNodes()
        {
            var stringScalar = Schema.Create().GetScalar<string>();
            Get(Some("hello"), stringScalar).Should().Be(StringValue("hello"));

            Get(Some("VALUE"), stringScalar).Should().Be(StringValue("VALUE"));

            Get(Some("VA\nLUE"), stringScalar).Should().Be(StringValue("VA\nLUE"));

            Get(Some(123), stringScalar).Should().Be(StringValue("123"));

            Get(Some(false), stringScalar).Should().Be(StringValue("false"));

            Get(Some(true), stringScalar).Should().Be(StringValue("true"));

            Get(Some(null), stringScalar).Should().Be(NullValue());

            Get(None(), stringScalar).Should().Be(null);
        }

        [Fact]
        public void ItConvertsIdValuesToIntStringValueNodes()
        {
            var idScalar = Schema.Create().GetScalar("ID");
            Get(Some("hello"), idScalar).Should().Be(StringValue("hello"));

            Get(Some("VALUE"), idScalar).Should().Be(StringValue("VALUE"));

            Get(Some("VA\nLUE"), idScalar).Should().Be(StringValue("VA\nLUE"));

            Get(Some(-1), idScalar).Should().Be(IntValue(-1));

            Get(Some(123), idScalar).Should().Be(IntValue(123));

            Get(Some("123"), idScalar).Should().Be(IntValue(123));

            Get(Some("01"), idScalar).Should().Be(StringValue("01"));

            Assert.Throws<Exception>(() => Get(Some(false), idScalar)).Message
                .Should().Be("ID cannot represent value: false");

            Get(Some(null), idScalar).Should().Be(NullValue());

            Get(None(), idScalar).Should().Be(null);
        }

        [Fact]
        public void ItConvertsNonNullValuesToNullValue()
        {
            var booleanScalar = Schema.Create().GetScalar<bool>();
            var nnBoolean = NonNullType.Of(booleanScalar);
            Get(null!, nnBoolean).Should().Be(null);
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
            var stringScalar = Schema.Create().GetScalar<string>();

            Get(Some(new object[] { "FOO", "BAR" }), ListType.Of(stringScalar)).Should()
                .Be(ListValue(StringValue("FOO"), StringValue("BAR")));

            Get(Some(new[] { "HELLO", "GOODBYE" }), ListType.Of(MyEnum))
                .Should()
                .Be(ListValue(EnumValue(Name("HELLO")), EnumValue(Name("GOODBYE"))));
        }

        [Fact]
        public void ItConvertsListSingltons()
        {
            var stringScalar = Schema.Create().GetScalar<string>();
            Get(Some("FOO"), ListType.Of(stringScalar)).Should().Be(StringValue("FOO"));
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