// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

using Superpower;

namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#ObjectTypeDefinition
        /// </summary>
        private static TokenListParser<TokenKind, ObjectTypeDefinitionSyntax> ObjectTypeDefinition { get; } =
            (from desc in Parse.Ref(() => Description).OptionalOrDefault()
             from type in Keyword("type")
             from typeName in Name
             from interfaces in ImplementsIntefaces.OptionalOrDefault()
             from directives in Directives.OptionalOrDefault()
             from fields in FieldsDefinition.OptionalOrDefault()
             select new ObjectTypeDefinitionSyntax(typeName,
                 desc,
                 interfaces, directives, fields,
                 SyntaxLocation.FromMany(desc, type, typeName, interfaces?.GetLocation(),
                     directives?.GetLocation(),
                     fields?.GetLocation()))).Named("object type definition");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#ImplementsInterfaces
        /// </summary>
        private static TokenListParser<TokenKind, NamedTypeSyntax[]> ImplementsIntefaces { get; } =
            (from impl in Keyword("implements")
             from amp in Ampersand.Optional()
             from ifaces in NamedType.Select(nt => nt).ManyDelimitedBy(Ampersand.OptionalOrDefault())
             select ifaces)
            .Named("implements interfaces");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#FieldsDefinition
        /// </summary>
        private static TokenListParser<TokenKind, FieldDefinitionSyntax[]> FieldsDefinition { get; } =
            (from lb in Parse.Ref(() => LeftBrace)
             from defs in FieldDefinition.Many()
             from rb in RightBrace
             select defs)
            .Try()
            .Named("fields definition");


        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#FieldDefinition
        /// </summary>
        private static TokenListParser<TokenKind, FieldDefinitionSyntax> FieldDefinition { get; } =
            (from desc in Parse.Ref(() => Description).OptionalOrDefault()
             from name in Name
             from args in ArgumentsDefinition.OptionalOrDefault()
             from c in Colon
             from type in Type
             from directives in Directives.OptionalOrDefault()
             select new FieldDefinitionSyntax(name, type, desc, args, directives,
                 SyntaxLocation.FromMany(desc, name, args?.GetLocation(), c, type)))
            .Try()
            .Named("field definition");


        private static TokenListParser<TokenKind, InputValueDefinitionSyntax[]> ArgumentsDefinition { get; } =
            (from lp in Parse.Ref(() => LeftParen)
             from defs in InputValueDefinition.Many()
             from rp in RightParen
             select defs)
            .Try()
            .Named("arguments definition");

        private static TokenListParser<TokenKind, InputValueDefinitionSyntax> InputValueDefinition { get; } =
            (from desc in Parse.Ref(() => Description).OptionalOrDefault()
             from name in Name
             from c in Colon
             from type in Type
             from defaultValue in DefaultValue.OptionalOrDefault()
             from directives in Directives.OptionalOrDefault()
             select new InputValueDefinitionSyntax(name, type, desc, defaultValue, directives,
                 SyntaxLocation.FromMany(
                     desc, name, type, defaultValue, directives?.GetLocation())))
            .Try()
            .Named("input value definition");
    }
}