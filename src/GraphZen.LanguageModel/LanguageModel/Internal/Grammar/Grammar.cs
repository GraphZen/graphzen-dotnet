#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Superpower;

namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#Argument
        /// </summary>
        internal static TokenListParser<TokenKind, ArgumentSyntax> Argument { get; } =
            (from desc in Parse.Ref(() => Description).OptionalOrDefault()
             from name in Parse.Ref(() => Name.Named("argument name"))
             from colon in Colon
             from value in Value.Named("argument value")
             select new ArgumentSyntax(name, desc, value, SyntaxLocation.FromMany(name, value))).Try()
            .Named("argument");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#Arguments
        /// </summary>
        internal static TokenListParser<TokenKind, ArgumentSyntax[]> Arguments { get; } =
            (from lp in Parse.Ref(() => LeftParen)
             from args in Argument.Many()
             from rp in RightParen
             select args).Try()
            .Named("arguments");
    }
}