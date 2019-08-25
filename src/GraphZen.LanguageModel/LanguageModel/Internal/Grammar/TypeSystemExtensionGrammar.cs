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
        private static TokenListParser<TokenKind, TypeSystemExtensionSyntax> TypeSystemExtension { get; } =
            Parse.Ref(() => SchemaExtension).Select(_ => (TypeSystemExtensionSyntax)_)
                .Or(Parse.Ref(() => TypeExtension).Select(_ => (TypeSystemExtensionSyntax)_))
                .Named("type system extension");
    }
}