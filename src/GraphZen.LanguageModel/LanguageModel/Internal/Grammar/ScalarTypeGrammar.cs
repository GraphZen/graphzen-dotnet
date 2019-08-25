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
        private static TokenListParser<TokenKind, ScalarTypeDefinitionSyntax> ScalarTypeDefinitionSyntax { get; } =
            (from desc in Parse.Ref(() => Description).OptionalOrDefault()
             from scalar in Keyword("scalar")
             from name in Name
             from directives in Directives.OptionalOrDefault()
             select new ScalarTypeDefinitionSyntax(name, desc, directives,
                 SyntaxLocation.FromMany(desc, scalar, name, directives.GetLocation())))
            .Try()
            .Named("scalar type");
    }
}