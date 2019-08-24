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
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#UnionTypeExtension
        /// </summary>
        private static TokenListParser<TokenKind, UnionTypeExtensionSyntax> UnionTypeExtension { get; } =
            (from extend in Keyword("extend")
             from union in Keyword("union")
             from name in Parse.Ref(() => Name)
             from directives in Directives
             select new UnionTypeExtensionSyntax(name, directives, null,
                 SyntaxLocation.FromMany(extend, directives.GetLocation())))
            .Select(_ => { return _; })
            .Try()
            .Or(from extend in Keyword("extend")
                from union in Keyword("union")
                from name in Parse.Ref(() => Name)
                from directives in Directives.OptionalOrDefault()
                from types in UnionMemberTypes
                select new UnionTypeExtensionSyntax(name, directives, types,
                    SyntaxLocation.FromMany(extend, types.GetLocation()))).Try()
            .Named("union type extension");
    }
}