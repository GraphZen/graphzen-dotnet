// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;



namespace GraphZen.LanguageModel.Internal
{
    internal static partial class Grammar
    {
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#Directives
        /// </summary>
        internal static TokenListParser<TokenKind, DirectiveSyntax[]> Directives { get; } =
            Parse.Ref(() => Directive).AtLeastOnce().Named("directives");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#Directive
        /// </summary>
        internal static TokenListParser<TokenKind, DirectiveSyntax> Directive { get; } =
            (from at in Parse.Ref(() => AtSymbol.Named("directive symbol"))
                from name in Name.Named("directive name")
                from args in Arguments.OptionalOrDefault().Named("directive arguments")
                select new DirectiveSyntax(name, args,
                    SyntaxLocation.FromMany(at, name, args.GetLocation()))).Try()
            .Named("directive");
    }
}