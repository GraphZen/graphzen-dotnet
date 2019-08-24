// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using Superpower;

namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#InterfaceTypeExtension
        /// </summary>
        private static TokenListParser<TokenKind, InterfaceTypeExtensionSyntax> InterfaceTypeExtension { get; } =
            (from extend in Keyword("extend")
             from iface in Keyword("interface")
             from name in Name
             from directives in Directives.OptionalOrDefault()
             from fields in FieldsDefinition
             select new InterfaceTypeExtensionSyntax(name, directives, fields,
                 SyntaxLocation.FromMany(extend, fields.GetLocation()))).Try().Or(
                from extend in Keyword("extend")
                from iface in Keyword("interface")
                from name in Name
                from directives in Directives
                select new InterfaceTypeExtensionSyntax(name, directives, null,
                    SyntaxLocation.FromMany(extend, directives.GetLocation()))
            ).Try().Named("interface type extension");
    }
}