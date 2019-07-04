// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

using Superpower;

namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#ScalarTypeExtension
        /// </summary>
        private static TokenListParser<TokenKind, ScalarTypeExtensionSyntax> ScalarTypeExtension { get; } =
            (from extend in Keyword("extend")
             from scalar in Keyword("scalar")
             from name in Parse.Ref(() => Name)
             from directives in Directives
             select new ScalarTypeExtensionSyntax(name, directives,
                 SyntaxLocation.FromMany(extend, directives.GetLocation())))
            .Try()
            .Named("scalar type extension");
    }
}