// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;

#nullable disable


namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#sec-Selection-Sets
        /// </summary>
        internal static TokenListParser<TokenKind, SelectionSetSyntax> SelectionSet { get; } =
            (from lb in Parse.Ref(() => LeftBrace)
             from selections in Selection
                 .AtLeastOnce()
             from rb in RightBrace
             select new SelectionSetSyntax(selections, new SyntaxLocation(lb, rb)))
            .Named("selection set");


        internal static TokenListParser<TokenKind, SelectionSyntax> Selection { get; } =
            Parse.Ref(() => Field).Select(_ => (SelectionSyntax)_)
                .Or(Parse.Ref(() => FragmentSpread.Select(_ => (SelectionSyntax)_)))
                .Or(InlineFragment.Select(_ => (SelectionSyntax)_)).Named("selection");
    }
}