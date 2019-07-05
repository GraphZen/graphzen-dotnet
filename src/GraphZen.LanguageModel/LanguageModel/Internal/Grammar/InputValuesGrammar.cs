// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using GraphZen.Infrastructure;
using Superpower;
using Superpower.Parsers;

namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        private static TokenListParser<TokenKind, NullValueSyntax> NullValue { get; } =
            (from @null in Keyword("null")
             select SyntaxFactory.NullValue())
            .Try()
            .Named("null value");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#EnumValue
        /// </summary>
        private static TokenListParser<TokenKind, EnumValueSyntax> EnumValue { get; } =
            (from name in Parse.Ref(() => Name)
             where EnumValueSyntax.IsValidValue(name.Value)
             select new EnumValueSyntax(name))
            .Try()
            .Named("enum value");

        private static TokenListParser<TokenKind, ListValueSyntax> ListValue { get; } =
            (from leftBracket in Parse.Ref(() => LeftBracket)
             from values in Value.Many()
             from rightBracket in RightBracket
             select new ListValueSyntax(values, new SyntaxLocation(leftBracket, rightBracket)))
            .Try()
            .Named("list value");


        private static TokenListParser<TokenKind, ObjectFieldSyntax> ObjectField { get; } =
            (from n in Parse.Ref(() => Name)
             from c in Colon
             from v in Value
             select SyntaxFactory.ObjectField(n, v))
            .Try()
            .Named("object field");

        private static TokenListParser<TokenKind, ObjectValueSyntax> ObjectValue { get; } =
            (from lb in Parse.Ref(() => LeftBrace)
             from fields in ObjectField.Many()
             from rb in RightBrace
             select new ObjectValueSyntax(fields, new SyntaxLocation(lb, rb)))
            .Try().Named("object value");

        private static TokenListParser<TokenKind, IntValueSyntax> IntValue { get; } =
            Token.EqualTo(TokenKind.IntValue).Select(t => int.TryParse(t.ToStringValue(), out var intValue)
                ? new IntValueSyntax(intValue, t.Span.ToLocation())
                : throw new InvalidOperationException("Unable to parse int token")).Named("int value");

        private static TokenListParser<TokenKind, FloatValueSyntax> FloatValue { get; } =
            Token.EqualTo(TokenKind.FloatValue).Select(t =>
                new FloatValueSyntax(t.ToStringValue(), t.Span.ToLocation()));

        internal static TokenListParser<TokenKind, StringValueSyntax> StringValue { get; } =
            Token.EqualTo(TokenKind.String)
                .Select(t =>
                {
                    var stringValue = t.Span.Source?.Substring(t.Span.Position.Absolute + 1, t.Span.Length - 2);
                    return new StringValueSyntax(stringValue, false, t.Span.ToLocation());
                }).Or(Token.EqualTo(TokenKind.BlockString).Select(t =>
                {
                    var stringValue = t.Span.Source?.Substring(t.Span.Position.Absolute + 3, t.Span.Length - 6);
                    return new StringValueSyntax(stringValue, true, t.Span.ToLocation());
                })).Named("string value");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#BooleanValue
        /// </summary>
        private static TokenListParser<TokenKind, BooleanValueSyntax> BooleanValue { get; } =
            Keyword("true").Or(Keyword("false"))
                .Select(name =>
                {
                    Debug.Assert(name != null, nameof(name) + " != null");
                    return new BooleanValueSyntax(bool.Parse(name.Value), name.Location);
                })
                .Try()
                .Named("boolean value");

        internal static TokenListParser<TokenKind, ValueSyntax> Value { get; } =
            Parse.Ref(() => Variable).Select(_ => (ValueSyntax) _)
                .Or(IntValue.Select(_ => (ValueSyntax) _))
                .Or(FloatValue.Select(_ => (ValueSyntax) _))
                .Or(StringValue.Select(_ => (ValueSyntax) _))
                .Or(BooleanValue.Select(_ => (ValueSyntax) _))
                .Or(NullValue.Select(_ => (ValueSyntax) _))
                .Or(EnumValue.Select(_ => (ValueSyntax) _))
                .Or(ListValue.Select(_ => (ValueSyntax) _))
                .Or(ObjectValue.Select(_ => (ValueSyntax) _))
                .Named("value");
    }
}