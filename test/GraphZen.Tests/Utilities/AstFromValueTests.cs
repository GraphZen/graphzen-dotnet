// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;
using static GraphZen.TypeSystem.Internal.AstFromValue;
using ListType = GraphZen.TypeSystem.ListType;

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
            Assert.Equal(BooleanValue(true), Get(Some(true), SpecScalars.Boolean));

            Assert.Equal(BooleanValue(false), Get(Some(false), SpecScalars.Boolean));

            Assert.Null(Get(None(), SpecScalars.Boolean));

            Assert.Equal(NullValue(), Get(Some(null!), SpecScalars.Boolean));

            Assert.Equal(BooleanValue(false), Get(Some(0), SpecScalars.Boolean));

            Assert.Equal(BooleanValue(true), Get(Some(1), SpecScalars.Boolean));

            Assert.Equal(BooleanValue(true), Get(Some(1), NonNullType.Of(SpecScalars.Boolean)));

            Assert.Equal(BooleanValue(false), Get(Some(0), NonNullType.Of(SpecScalars.Boolean)));
        }


        [Fact]
        public void ItConvertsIntValuesToIntValueNodes()
        {
            Assert.Equal(IntValue(-1), Get(Some(-1), SpecScalars.Int));

            Assert.Equal(IntValue(123), Get(Some(123.0), SpecScalars.Int));

            Assert.Equal(IntValue(10000), Get(Some(1e4), SpecScalars.Int));

            Assert.Equal("Int cannot represent non-integer value: 123.5",
                Assert.Throws<Exception>(() => Get(Some(123.5), SpecScalars.Int)).Message);

            Assert.Equal("Int cannot represent non 32-bit signed integer value: 1E+40",
                Assert.Throws<Exception>(() => Get(Some(1e40), SpecScalars.Int)).Message);
        }

        [Fact]
        public void ItConvertsFloatvaluesToIntFloatNodeValues()
        {
            Assert.Equal(IntValue(-1), Get(Some(-1), SpecScalars.Float));

            Assert.Equal(IntValue(123), Get(Some(123.0), SpecScalars.Float));

            Assert.Equal(FloatValue("123.5"), Get(Some(123.5), SpecScalars.Float));

            Assert.Equal(IntValue(10000), Get(Some(1e4), SpecScalars.Float));

            Assert.Equal(FloatValue("1e+40"), Get(Some(1e40), SpecScalars.Float));
        }

        [Fact]
        public void ItConvertsStringValuesToStringValueNodes()
        {
            Assert.Equal(StringValue("hello"), Get(Some("hello"), SpecScalars.String));

            Assert.Equal(StringValue("VALUE"), Get(Some("VALUE"), SpecScalars.String));

            Assert.Equal(StringValue("VA\nLUE"), Get(Some("VA\nLUE"), SpecScalars.String));

            Assert.Equal(StringValue("123"), Get(Some(123), SpecScalars.String));

            Assert.Equal(StringValue("false"), Get(Some(false), SpecScalars.String));

            Assert.Equal(StringValue("true"), Get(Some(true), SpecScalars.String));

            Assert.Equal(NullValue(), Get(Some(null!), SpecScalars.String));

            Assert.Null(Get(None(), SpecScalars.String));
        }

        [Fact]
        public void ItConvertsIdValuesToIntStringValueNodes()
        {
            Assert.Equal(StringValue("hello"), Get(Some("hello"), SpecScalars.ID));

            Assert.Equal(StringValue("VALUE"), Get(Some("VALUE"), SpecScalars.ID));

            Assert.Equal(StringValue("VA\nLUE"), Get(Some("VA\nLUE"), SpecScalars.ID));

            Assert.Equal(IntValue(-1), Get(Some(-1), SpecScalars.ID));

            Assert.Equal(IntValue(123), Get(Some(123), SpecScalars.ID));

            Assert.Equal(IntValue(123), Get(Some("123"), SpecScalars.ID));

            Assert.Equal(StringValue("01"), Get(Some("01"), SpecScalars.ID));

            Assert.Equal("ID cannot represent value: false",
                Assert.Throws<Exception>(() => Get(Some(false), SpecScalars.ID)).Message);

            Assert.Equal(NullValue(), Get(Some(null!), SpecScalars.ID));

            Assert.Null(Get(None(), SpecScalars.ID));
        }

        [Fact]
        public void ItConvertsNonNullValuesToNullValue()
        {
            var nnBoolean = NonNullType.Of(SpecScalars.Boolean);
            Assert.Null(Get(null!, nnBoolean));
        }


        [Fact]
        public void ItConvertsStringValuesToEnumValueNodesIfPossible()
        {
            Assert.Equal(EnumValue(Name("HELLO")), Get(Some("HELLO"), MyEnum));
            Assert.Equal(EnumValue(Name("COMPLEX")), Get(Some(ComplexValue), MyEnum));
            Assert.Null(Get(Some("hello"), MyEnum));
            Assert.Null(Get(Some("VALUE"), MyEnum));
        }


        [Fact]
        public void ItConvertsArrayValuesToListValueNodes()
        {
            Assert.Equal(ListValue(StringValue("FOO"), StringValue("BAR")),
                Get(Some(new object[] { "FOO", "BAR" }), ListType.Of(SpecScalars.String)));

            Assert.Equal(ListValue(EnumValue(Name("HELLO")), EnumValue(Name("GOODBYE"))),
                Get(Some(new[] { "HELLO", "GOODBYE" }), ListType.Of(MyEnum)));
        }

        [Fact]
        public void ItConvertsListSingltons()
        {
            Assert.Equal(StringValue("FOO"), Get(Some("FOO"), ListType.Of(SpecScalars.String)));
        }

        [Fact]
        public void ItConvertsInputObjects()
        {
            Assert.Equal(ObjectValue(ObjectField(Name("foo"), IntValue(3)),
                    ObjectField(Name("bar"), EnumValue(Name("HELLO")))),
                Get(Some(new
                {
                    foo = 3,
                    bar = "HELLO"
                }), MyInputObj));
        }

        [Fact]
        public void ItConvertsInputObjectsWithExplicitNulls()
        {
            Assert.Equal(ObjectValue(ObjectField(Name("foo"), NullValue())),
                Get(Some(new
                {
                    foo = (string?)null
                }), MyInputObj));
        }
    }
}
