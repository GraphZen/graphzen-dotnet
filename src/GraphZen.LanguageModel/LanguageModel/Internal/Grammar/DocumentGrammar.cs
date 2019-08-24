#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Superpower;

namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#sec-Language.Document
        /// </summary>
        internal static TokenListParser<TokenKind, DocumentSyntax> Document { get; } =
            (from leadingComments in Parse.Ref(() => Comment.Many())
             from definitions in Parse.Ref(() => Definition).Many()
             from trailingComments in Comment.Many()
             select new DocumentSyntax(definitions,
                 definitions.GetLocation().Location))
            .Named("document");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#Definition
        /// </summary>
        private static TokenListParser<TokenKind, DefinitionSyntax> Definition { get; } =
            (from leadingComments in Parse.Ref(() => Comment.Many())
             from def in ExecutableDefinition.Select(_ => (DefinitionSyntax)_)
                 .Or(TypeSystemDefinition.Select(_ => (DefinitionSyntax)_))
                 .Or(TypeSystemExtension.Select(_ => (DefinitionSyntax)_))
             from trailingComments in Comment.Many()
             select def)
            .Named("definition");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#ExecutableDefinition
        /// </summary>
        private static TokenListParser<TokenKind, ExecutableDefinitionSyntax> ExecutableDefinition { get; } =
            Parse.Ref(() => OperationDefintion).Select(_ => (ExecutableDefinitionSyntax)_)
                .Or(FragmentDefinition.Select(_ => (ExecutableDefinitionSyntax)_))
                .Named("executable definition");
    }
}