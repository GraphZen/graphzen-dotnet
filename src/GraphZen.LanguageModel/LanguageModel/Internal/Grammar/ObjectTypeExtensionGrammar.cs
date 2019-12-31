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
        private static TokenListParser<TokenKind, ObjectTypeExtensionSyntax> ObjectTypeExtension { get; } =
            (from extend in Keyword("extend")
             from type in Keyword("type")
             from name in Name
             from ifaces in ImplementsIntefaces.OptionalOrNull()
             from directives in Directives.OptionalOrNull()
             from fields in FieldsDefinition
             select new ObjectTypeExtensionSyntax(name, ifaces, directives, fields,
                 SyntaxLocation.FromMany(extend, name, ifaces?.GetLocation(), directives?.GetLocation(),
                     fields?.GetLocation()))).Try()
            .Or(
                (from extend in Keyword("extend")
                 from type in Keyword("type")
                 from name in Name
                 from ifaces in ImplementsIntefaces.OptionalOrNull()
                 from directives in Directives
                 select new ObjectTypeExtensionSyntax(name, ifaces, directives, null,
                     SyntaxLocation.FromMany(extend, name, ifaces?.GetLocation(), directives?.GetLocation())))
                .Try()
            ).Or(
                (from extend in Keyword("extend")
                 from type in Keyword("type")
                 from name in Name
                 from ifaces in ImplementsIntefaces
                 select new ObjectTypeExtensionSyntax(name, ifaces, null, null,
                     SyntaxLocation.FromMany(extend, name, ifaces?.GetLocation()))).Try()
            )
            .Named("object type extension");
    }
}