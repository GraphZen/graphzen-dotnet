// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using Superpower;

namespace GraphZen.LanguageModel.Internal;

internal static partial class Grammar
{
    /// <summary>
    ///     http://facebook.github.io/graphql/June2018/#InterfaceTypeDefinition
    /// </summary>
    private static TokenListParser<TokenKind, InterfaceTypeDefinitionSyntax> InterfaceTypeDefinition { get; } =
        (from desc in Parse.Ref(() => Description!.AsNullable().OptionalOrDefault())
            from @interface in Keyword("interface")
            from name in Name!
            from directives in Directives.AsNullable().OptionalOrDefault()
            from fields in FieldsDefinition!.AsNullable().OptionalOrDefault()
            select new InterfaceTypeDefinitionSyntax(name!, desc, directives, fields,
                SyntaxLocation.FromMany(desc, @interface, name!, directives.GetLocation(), fields.GetLocation())))
        .Named("interface");
}
