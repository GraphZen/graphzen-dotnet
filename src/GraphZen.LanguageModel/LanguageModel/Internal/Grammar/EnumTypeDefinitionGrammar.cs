// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

using Superpower;

namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#EnumTypeDefinition
        /// </summary>
        private static TokenListParser<TokenKind, EnumTypeDefinitionSyntax> EnumTypeDefinition { get; } =
            (from desc in Parse.Ref(() => Description).OptionalOrDefault()
             from @enum in Keyword("enum")
             from name in Name
             from directives in Directives.OptionalOrDefault()
             from values in EnumValuesDefinition.OptionalOrDefault()
             select new EnumTypeDefinitionSyntax(name, desc, directives, values,
                 SyntaxLocation.FromMany(desc, @enum, name, directives.GetLocation(), values.GetLocation())))
            .Named("enum type definition");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#EnumValuesDefinition
        /// </summary>
        private static TokenListParser<TokenKind, EnumValueDefinitionSyntax[]> EnumValuesDefinition { get; } =
            (from lb in Parse.Ref(() => LeftBrace)
             from values in EnumValueDefinition.Many()
             from rb in RightBrace
             select values)
            .Try()
            .Named("enum values");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#EnumValueDefinition
        /// </summary>
        private static TokenListParser<TokenKind, EnumValueDefinitionSyntax> EnumValueDefinition { get; } =
            (from desc in Parse.Ref(() => Description.OptionalOrDefault())
             from value in EnumValue
             from directives in Directives.OptionalOrDefault()
             select new EnumValueDefinitionSyntax(value, desc, directives,
                 SyntaxLocation.FromMany(desc, value, directives.GetLocation())))
            .Try()
            .Named("enum value");
    }
}