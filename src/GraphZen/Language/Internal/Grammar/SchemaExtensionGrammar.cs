// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Superpower;

namespace GraphZen.Language.Internal
{
    internal static partial class Grammar
    {
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#SchemaExtension
        /// </summary>
        private static TokenListParser<TokenKind, SchemaExtensionSyntax> SchemaExtension { get; } =
            (from extend in Keyword("extend")
             from schema in Keyword("schema")
             from directives in Directives.OptionalOrDefault()
             from lb in Parse.Ref(() => LeftBrace)
             from defs in OperationTypeDefinition.Many()
             from rb in RightBrace
             select new SchemaExtensionSyntax(directives, defs, SyntaxLocation.FromMany(extend, rb))).Try().Or(
                from extend in Keyword("extend")
                from schema in Keyword("schema")
                from directives in Directives
                select new SchemaExtensionSyntax(directives, null,
                    SyntaxLocation.FromMany(extend, directives.GetLocation()))
            ).Try().Named("schema extension");
    }
}