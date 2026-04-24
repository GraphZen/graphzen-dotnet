// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using Superpower;

namespace GraphZen.LanguageModel.Internal;

internal static partial class Grammar
{
    private static TokenListParser<TokenKind, UnionTypeDefinitionSyntax> UnionTypeDefinition { get; } =
        (from desc in Parse.Ref(() => Description!).AsNullable().OptionalOrDefault()
         from union in Keyword("union")
         from name in Name
         from directives in Directives.AsNullable().OptionalOrDefault()
         from types in UnionMemberTypes!.AsNullable().OptionalOrDefault()
         select new UnionTypeDefinitionSyntax(name!, desc, directives, types,
             SyntaxLocation.FromMany(desc, union, name!, directives?.GetLocation(), types?.GetLocation())))
        .Named("union type definition");

    /// <summary>
    ///     http://facebook.github.io/graphql/June2018/#UnionMemberTypes
    /// </summary>
    private static TokenListParser<TokenKind, NamedTypeSyntax[]> UnionMemberTypes { get; } =
        (
            from assignment in Parse.Ref(() => Assignment!)
            from pipe in Parse.Ref(() => Pipe!).Try().AsNullable().OptionalOrDefault()
            from types in NamedType.AtLeastOnceDelimitedBy(Pipe)
            select types!)
        .Try()
        .Named("union member types");
}
