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
        /// <summary>
        ///     http://facebook.github.io/graphql/June2018/#InputObjectTypeExtension
        /// </summary>
        private static TokenListParser<TokenKind, InputObjectTypeExtensionSyntax> InputObjectTypeExtension { get; } =
            (from extend in Keyword("extend")
             from input in Keyword("input")
             from name in Parse.Ref(() => Name)
             from directives in Directives.OptionalOrNull()
             from fields in InputFieldsDefinition
             select new InputObjectTypeExtensionSyntax(name, directives, fields,
                 SyntaxLocation.FromMany(extend, fields.GetLocation())))
            .Try().Or
            ((from extend in Keyword("extend")
              from input in Keyword("input")
              from name in Parse.Ref(() => Name)
              from directives in Directives
              select new InputObjectTypeExtensionSyntax(name, directives, null,
                  SyntaxLocation.FromMany(extend, directives.GetLocation())))
                .Try())
            .Named("input object type extension");
    }
}