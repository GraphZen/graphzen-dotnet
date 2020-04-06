// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;

namespace GraphZen.LanguageModel.Internal
{
    internal static partial class Grammar
    {
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#InputObjectTypeDefinition
        /// </summary>
        private static TokenListParser<TokenKind, InputObjectTypeDefinitionSyntax> InputObjectTypeDefinition { get; } =
            (from desc in Parse.Ref(() => Description).OptionalOrNull()
                from input in Keyword("input")
                from name in Name
                from directives in Directives.OptionalOrNull()
                from fields in InputFieldsDefinition.OptionalOrNull()
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