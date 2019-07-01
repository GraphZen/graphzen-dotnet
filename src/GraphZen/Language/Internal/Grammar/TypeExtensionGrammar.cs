// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;

namespace GraphZen.Language.Internal
{
    internal static partial class Grammar
    {
        private static TokenListParser<TokenKind, TypeExtensionSyntax> TypeExtension { get; } =
            Parse.Ref(() => ScalarTypeExtension).Select(_ => (TypeExtensionSyntax) _)
                .Or(Parse.Ref(() => ObjectTypeExtension).Select(_ => (TypeExtensionSyntax) _))
                .Or(Parse.Ref(() => InterfaceTypeExtension).Select(_ => (TypeExtensionSyntax) _))
                .Or(Parse.Ref(() => UnionTypeExtension).Select(_ => (TypeExtensionSyntax) _))
                .Or(Parse.Ref(() => EnumTypeExtension).Select(_ => (TypeExtensionSyntax) _))
                .Or(Parse.Ref(() => InputObjectTypeExtension).Select(_ => (TypeExtensionSyntax) _))
                .Named("type extension");
    }
}