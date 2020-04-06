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
        internal static TokenListParser<TokenKind, FieldSyntax> Field { get; } =
            (from firstName in Parse.Ref(() => Name.OptionalOrNull())
                from aliasedName in (from colon in Colon
                    from aliasedName in Name
                    select aliasedName).OptionalOrNull()
                from arguments in Arguments.OptionalOrNull().Named("field arguments")
                from directives in Directives.OptionalOrNull().Named("field directives")
                from selectionSet in SelectionSet.OptionalOrNull().Named("field selections")
                let alias = aliasedName != null ? firstName : null
                let name = aliasedName ?? firstName
                where firstName != null
                select new FieldSyntax(name,
                    alias,
                    arguments,
                    directives,
                    selectionSet,
                    SyntaxLocation.FromMany(alias, name, arguments?.GetLocation(),
                        selectionSet,
                        directives?.GetLocation()
                    )))
            .Try()
            .Named("field");
    }
}