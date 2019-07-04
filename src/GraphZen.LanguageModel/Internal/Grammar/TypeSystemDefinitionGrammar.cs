// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;

namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        private static TokenListParser<TokenKind, TypeSystemDefinitionSyntax> TypeSystemDefinition { get; } =
            Parse.Ref(() => SchemaDefinition).Select(_ => (TypeSystemDefinitionSyntax) _)
                .Or(Parse.Ref(() => TypeDefintion).Select(_ => (TypeSystemDefinitionSyntax) _).Try())
                .Or(Parse.Ref(() => DirectiveDefinition).Select(_ => (TypeSystemDefinitionSyntax) _).Try())
                .Named("type system definition");

        private static TokenListParser<TokenKind, TypeDefinitionSyntax> TypeDefintion { get; } =
            Parse.Ref(() => ScalarTypeDefinitionSyntax).Select(_ => (TypeDefinitionSyntax) _)
                .Or(Parse.Ref(() => ObjectTypeDefinition).Select(_ => (TypeDefinitionSyntax) _).Try())
                .Or(Parse.Ref(() => InterfaceTypeDefinition).Select(_ => (TypeDefinitionSyntax) _).Try())
                .Or(Parse.Ref(() => UnionTypeDefinition).Select(_ => (TypeDefinitionSyntax) _).Try())
                .Or(Parse.Ref(() => EnumTypeDefinition).Select(_ => (TypeDefinitionSyntax) _).Try())
                .Or(Parse.Ref(() => InputObjectTypeDefinition).Select(_ => (TypeDefinitionSyntax) _).Try())
                .Named("type definition");
    }
}