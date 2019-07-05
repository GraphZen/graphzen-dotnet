// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Superpower;

namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        private static TokenListParser<TokenKind, NamedTypeSyntax> NamedType { get; } =
            (from name in Parse.Ref(() => Name)
             select new NamedTypeSyntax(name, name.Location))
            .Named("named type");

        private static TokenListParser<TokenKind, ListTypeSyntax> ListType { get; } =
            (from leftBracket in Parse.Ref(() => LeftBracket)
             from type in Type
             from rightBracket in RightBracket
             select new ListTypeSyntax(type, new SyntaxLocation(leftBracket, rightBracket)))
            .Try()
            .Named("list type");

        internal static TokenListParser<TokenKind, TypeSyntax> Type { get; } =
            (from type in ListType
                 .Select(n => (NullableTypeSyntax) n)
                 .Or(NamedType.Select(n => (NullableTypeSyntax) n))
             from bang in Bang.OptionalOrDefault()
             select bang == null
                 ? type
                 : new NonNullTypeSyntax(type, SyntaxLocation.FromMany(type, bang)) as TypeSyntax)
            .Try()
            .Named("type");
    }
}