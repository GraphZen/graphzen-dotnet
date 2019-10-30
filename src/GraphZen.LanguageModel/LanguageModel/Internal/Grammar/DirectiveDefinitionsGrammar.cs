// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;

#nullable disable


namespace GraphZen.LanguageModel.Internal
{
    internal static partial class Grammar
    {
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#DirectiveDefinition
        /// </summary>
        private static TokenListParser<TokenKind, DirectiveDefinitionSyntax> DirectiveDefinition { get; } =
            (from desc in Parse.Ref(() => Description.OptionalOrDefault())
             from directive in Keyword("directive")
             from at in AtSymbol
             from name in Name
             from args in ArgumentsDefinition.OptionalOrDefault()
             from @on in Keyword("on")
             from locations in DirectiveLocations
             select new DirectiveDefinitionSyntax(name, locations, desc, args,
                 SyntaxLocation.FromMany(desc, directive, at, name,
                     args?.GetLocation()
                     , @on,
                     locations.GetLocation())))
            .Try()
            .Named("directive definition");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#DirectiveLocations
        /// </summary>
        private static TokenListParser<TokenKind, NameSyntax[]> DirectiveLocations { get; } =
            (from pipe in Parse.Ref(() => Pipe).OptionalOrDefault()
             from locations in DirectiveLocation.ManyDelimitedBy(Pipe)
             select locations)
            .Try()
            .Named("directive locations");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#DirectiveLocation
        /// </summary>
        private static TokenListParser<TokenKind, NameSyntax> DirectiveLocation { get; } =
            Parse.Ref(() => ExecutableDirectiveLocation.Or(TypeSystemDirectiveLocation))
                .Named("directive location");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#ExecutableDirectiveLocation
        /// </summary>
        private static TokenListParser<TokenKind, NameSyntax> ExecutableDirectiveLocation { get; } =
            KeywordIn(DirectiveLocationHelper.ExecutableDirectiveLocations.Values)
                .Named("executable directive location");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#TypeSystemDirectiveLocation
        /// </summary>
        private static TokenListParser<TokenKind, NameSyntax> TypeSystemDirectiveLocation { get; } =
            KeywordIn(DirectiveLocationHelper.TypeSystemDirectiveLocations.Values)
                .Named("type system directive location");
    }
}