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
        ///     http://facebook.github.io/graphql/June2018/#InputObjectTypeDefinition
        /// </summary>
        private static TokenListParser<TokenKind, InputObjectTypeDefinitionSyntax> InputObjectTypeDefinition { get; } =
            (from desc in Parse.Ref(() => Description).OptionalOrDefault()
             from input in Keyword("input")
             from name in Name
             from directives in Directives.OptionalOrDefault()
             from fields in InputFieldsDefinition.OptionalOrDefault()
             select new InputObjectTypeDefinitionSyntax(name, desc, directives, fields,
                 SyntaxLocation.FromMany(desc, input, name, directives.GetLocation(), fields.GetLocation())))
            .Named("input object type definition");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#InputFieldsDefinition
        /// </summary>
        private static TokenListParser<TokenKind, InputValueDefinitionSyntax[]> InputFieldsDefinition { get; } =
            (from lb in Parse.Ref(() => LeftBrace)
             from values in InputValueDefinition.Many()
             from rb in RightBrace
             select values)
            .Try()
            .Named("input fields definition");
    }
}