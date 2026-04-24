// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;

namespace GraphZen.Tests.StarWars;

[NoReorder]
public class StarWarsCodeSchemaComparisonTests : StarWarsSchemaAndData
{
    [Fact]
    public void SdlMatches()
    {
        string Print(Schema schema) =>
            schema.ToDocumentSyntax().WithoutBuiltInDefinitions().WithSortedChildren()
                .ToSyntaxString();

        var codeFirstSdl = Print(CodeFirstSchema);
        var schemaBuilderSdl = Print(SchemaBuilderSchema);
        StringAssert.Equal(codeFirstSdl, schemaBuilderSdl, opt =>
        {
            opt.ShowActual = false;
            opt.ShowExpected = false;
        });
    }
}
