// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;

#nullable disable


namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        private static TokenListParser<TokenKind, NamedTypeSyntax> TypeCondition { get; } =
            (from @on in Keyword("on")
             from type in NamedType
             select type)
            .Try()
            .Named("type condition");

        private static TokenListParser<TokenKind, NameSyntax> FragmentName { get; } =
            (from name in Parse.Ref(() => Name)
             where !name.Value.Equals("on", StringComparison.OrdinalIgnoreCase)
             select name)
            .Try()
            .Named("fragment name");

        internal static TokenListParser<TokenKind, FragmentSpreadSyntax> FragmentSpread { get; } =
            (from spread in Parse.Ref(() => Spread)
             from name in FragmentName.OptionalOrDefault()
             from directives in Directives.OptionalOrDefault()
             where name != null
             select new FragmentSpreadSyntax(name, directives,
                 SyntaxLocation.FromMany(spread, name, directives.GetLocation())))
            .Try()
            .Named("fragment spread");

        internal static TokenListParser<TokenKind, InlineFragmentSyntax> InlineFragment =>
            (from spread in Spread
             from typeCondition in TypeCondition.OptionalOrDefault()
             from directives in Directives.OptionalOrDefault()
             from selectionSet in SelectionSet
             select new InlineFragmentSyntax(selectionSet, typeCondition, directives,
                 new SyntaxLocation(spread, selectionSet))).Named("inline fragment");


        internal static TokenListParser<TokenKind, FragmentDefinitionSyntax> FragmentDefinition =>
            (from fragment in Keyword("fragment")
             from fragmentName in FragmentName
             from type in TypeCondition.OptionalOrDefault()
             from directives in Directives.OptionalOrDefault()
             from selectionSet in SelectionSet
             select new FragmentDefinitionSyntax(fragmentName, type, selectionSet, directives,
                 new SyntaxLocation(fragment, selectionSet))).Named("fragment definition");
    }
}