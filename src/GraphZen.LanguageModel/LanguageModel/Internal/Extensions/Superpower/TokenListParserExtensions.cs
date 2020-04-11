// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;

namespace GraphZen.LanguageModel.Internal
{
    public static class TokenListParserExtensions
    {
        public static TokenListParser<TokenKind, T?> OptionalOrNull<T>(this TokenListParser<TokenKind, T> parser)
            where T : class =>
            // ReSharper disable once RedundantCast
            parser.Select(_ => (T?)_).OptionalOrDefault();
    }
}