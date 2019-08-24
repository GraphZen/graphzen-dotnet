// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem
{
    public static class SchemaPrinter
    {
        public static string Print(this Schema schema) =>
            Check.NotNull(schema, nameof(schema))
                .ToDocumentSyntax()
                .WithoutBuiltInDefinitions()
                .ToSyntaxString();
    }
}