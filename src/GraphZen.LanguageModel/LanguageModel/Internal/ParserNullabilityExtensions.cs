// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using Superpower;

namespace GraphZen.LanguageModel.Internal
{
    // Workaround: Superpower's OptionalOrDefault<T>() expects TokenListParser<TKind, T?> but our
    // grammar parsers produce TokenListParser<TKind, T> (non-nullable). This bridges the gap until
    // Superpower provides nullable-annotated APIs. See https://github.com/GraphZen/graphzen-dotnet/issues/44
    internal static class ParserNullabilityExtensions
    {
        internal static TokenListParser<TKind, T?> AsNullable<TKind, T>(this TokenListParser<TKind, T> parser)
            where T : class =>
            parser!;
    }
}
