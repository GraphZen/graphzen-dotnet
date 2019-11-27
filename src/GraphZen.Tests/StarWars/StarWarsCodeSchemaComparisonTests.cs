// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.StarWars
{
    [NoReorder]
    public class StarWarsCodeSchemaComparisonTests : StarWarsSchemaAndData
    {
        [Fact]
        public void SDLMatches()
        {
            string Print(Schema schema) =>
                schema.ToDocumentSyntax().WithoutBuiltInDefinitions().WithSortedChildren()
                    .ToSyntaxString();

            var codeFirstSdl = Print(CodeFirstSchema);
            var schemaBuilderSdl = Print(SchemaBuilderSchema);
            codeFirstSdl.Should().Be(schemaBuilderSdl, opt =>
            {
                opt.ShowActual = false;
                opt.ShowExpected = false;
            });
        }
    }
}