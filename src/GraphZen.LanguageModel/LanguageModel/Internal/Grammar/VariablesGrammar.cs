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
        ///     http://facebook.github.io/graphql/June2018/#VariableDefinitions
        /// </summary>
        private static TokenListParser<TokenKind, VariableDefinitionSyntax[]> VariableDefinitions { get; } =
            (from lp in Parse.Ref(() => LeftParen)
                from variableDefinitionNodes in VariableDefinition.Many()
                from rp in RightParen
                select variableDefinitionNodes)
            .Named("variable definitions");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#VariableDefinition
        /// </summary>
        internal static TokenListParser<TokenKind, VariableDefinitionSyntax> VariableDefinition { get; } =
            (from v in Parse.Ref(() => Variable)
                from c in Colon
                from t in Type
                from defaultValue in DefaultValue.OptionalOrDefault()
                select new VariableDefinitionSyntax(v, t, defaultValue, SyntaxLocation.FromMany(v, c, t, defaultValue)))
            .Named("variable definition");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#Variable
        /// </summary>
        internal static TokenListParser<TokenKind, VariableSyntax> Variable { get; } =
            (from d in Parse.Ref(() => DollarSign)
                from n in Name.Named("variable name")
                select new VariableSyntax(n, new SyntaxLocation(d, n)))
            .Named("variable");

        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#DefaultValue
        /// </summary>
        private static TokenListParser<TokenKind, ValueSyntax> DefaultValue { get; } =
            (from assignment in Parse.Ref(() => Assignment).Named("assignment")
                from value in Value.Named("default value")
                select value)
            .Named("default value");
    }
}