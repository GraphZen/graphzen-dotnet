#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using Superpower;
using Superpower.Parsers;

namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static partial class Grammar
    {
        private static TokenListParser<TokenKind, OperationDefinitionSyntax> OperationDefintion { get; } =
            Parse.Ref(() => QueryShorthandOpeartion).Or(
                    from type in Parse.Ref(() => OperationType).Named("operation type")
                    from name in Name.OptionalOrDefault().Named("operation name")
                    from varDefs in VariableDefinitions.OptionalOrDefault().Named("operation variable definitions")
                    from directives in Directives.OptionalOrDefault().Named("operation directives")
                    from selectionSet in SelectionSet
                    select new OperationDefinitionSyntax(type.type, selectionSet, name,
                        varDefs, directives,
                        new SyntaxLocation(type.location, selectionSet.Location)))
                .Named("operation definition");

        private static TokenListParser<TokenKind, (OperationType type, SyntaxLocation location)> OperationType { get; }
            =
            (from name in Token.EqualTo(TokenKind.Name).Named("operation type")
             let nameValue = name.ToStringValue() ?? ""
             let isQuery = nameValue?.Equals("query", StringComparison.OrdinalIgnoreCase) ?? false
             let isMutation = nameValue?.Equals("mutation", StringComparison.OrdinalIgnoreCase) ?? false
             let isSubscription = nameValue?.Equals("subscription", StringComparison.OrdinalIgnoreCase) ?? false
             where isQuery || isMutation || isSubscription
             let type = isQuery
                 ? LanguageModel.OperationType.Query
                 : isMutation
                     ? LanguageModel.OperationType.Mutation
                     : LanguageModel.OperationType.Subscription
             select (type, name.Span.ToLocation()))
            .Named("operation type");

        private static TokenListParser<TokenKind, OperationDefinitionSyntax> QueryShorthandOpeartion { get; } =
            (from selectionSet in Parse.Ref(() => SelectionSet)
             select new OperationDefinitionSyntax(LanguageModel.OperationType.Query, selectionSet,
                 location: selectionSet.Location))
            .Named("query shortand operation");
    }
}