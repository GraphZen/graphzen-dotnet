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
        ///     http://facebook.github.io/graphql/June2018/#InterfaceTypeDefinition
        /// </summary>
        private static TokenListParser<TokenKind, InterfaceTypeDefinitionSyntax> InterfaceTypeDefinition { get; } =
            (from desc in Parse.Ref(() => Description.OptionalOrDefault())
                from @interface in Keyword("interface")
                from name in Name
                from directives in Directives.OptionalOrDefault()
                from fields in FieldsDefinition.OptionalOrDefault()
                select new InterfaceTypeDefinitionSyntax(name, desc, directives, fields,
                    SyntaxLocation.FromMany(desc, @interface, name, directives.GetLocation(), fields.GetLocation())))
            .Named("interface");
    }
}