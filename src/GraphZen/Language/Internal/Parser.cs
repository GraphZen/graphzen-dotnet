// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Language.Internal
{
    public static class Parser
    {
        [NotNull] private static readonly IParser Instance = new SuperpowerParser();

        [NotNull]
        public static DocumentSyntax ParseDocument(string text) =>
            Instance.ParseDocument(text);

        [NotNull]
        public static ValueSyntax ParseValue(string text) =>
            Instance.ParseValue(text);

        [NotNull]
        public static TypeSyntax ParseType(string text) =>
            Instance.ParseType(text);
    }
}