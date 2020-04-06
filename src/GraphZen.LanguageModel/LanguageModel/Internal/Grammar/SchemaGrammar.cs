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
        private static TokenListParser<TokenKind, SchemaDefinitionSyntax> SchemaDefinition { get; } =
            (from schema in Keyword("schema")
                from directives in Directives.OptionalOrNull()
                from leftBrace in Parse.Ref(() => LeftBrace)
                from operationTypeDefinitionNodes in OperationTypeDefinition.Many()
                from rightBrace in RightBrace
                select new SchemaDefinitionSyntax(operationTypeDefinitionNodes, directives,
                    SyntaxLocation.FromMany(schema, rightBrace))).Try().Named("schema definition");

        private static TokenListParser<TokenKind, OperationTypeDefinitionSyntax> OperationTypeDefinition { get; } =
            (from opType in Parse.Ref(() => OperationType)
                from colon in Colon
                from type in NamedType
                select new OperationTypeDefinitionSyntax(opType.type,
                    type,
                    SyntaxLocation.FromMany(opType.location, type.Location)))
            .Try()
            .Named("operation type definition");
    }
}