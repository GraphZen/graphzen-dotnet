// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;

#nullable disable


namespace GraphZen.LanguageModel.Internal
{
    internal static partial class Grammar
    {
        internal static TokenListParser<TokenKind, StringValueSyntax> Description { get; } =
            (from value in Parse.Ref(() => StringValue) select value)
            .Select(_ =>
            {
                Debug.Assert(_ != null, nameof(_) + " != null");
                return _;
            })
            .Named("description");
    }
}