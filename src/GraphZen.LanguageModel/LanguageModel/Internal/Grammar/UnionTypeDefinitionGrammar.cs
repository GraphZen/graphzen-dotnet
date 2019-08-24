// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using Superpower;

namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        private static TokenListParser<TokenKind, UnionTypeDefinitionSyntax> UnionTypeDefinition { get; } =
            (from desc in Parse.Ref(() => Description).OptionalOrDefault()
             from union in Keyword("union")
             from name in Name
             from directives in Directives.OptionalOrDefault()
             from types in UnionMemberTypes.OptionalOrDefault()
             select new UnionTypeDefinitionSyntax(name, desc, directives, types,
                 SyntaxLocation.FromMany(desc, union, name, directives?.GetLocation(), types?.GetLocation())))
            .Named("union type definition");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#UnionMemberTypes
        /// </summary>
        private static TokenListParser<TokenKind, NamedTypeSyntax[]> UnionMemberTypes { get; } =
            (
                from assignment in Parse.Ref(() => Assignment)
                from pipe in Parse.Ref(() => Pipe).Try().OptionalOrDefault()
                from types in NamedType.AtLeastOnceDelimitedBy(Pipe)
                select types)
            .Try()
            .Named("union member types");
    }
}