// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.Tests.LanguageModel.Internal.Parser;

[NoReorder]
public class InputTypeParserTests : ParserTestBase
{
    [Fact]
    public void ParseListType()
    {
        Assert.Equal(SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("Foo"))), ParseType("[Foo]"));
    }

    [Fact]
    public void ParseNamedType()
    {
        Assert.Equal(SyntaxFactory.NamedType(SyntaxFactory.Name("Foo")), ParseType("Foo"));
    }

    [Fact]
    public void ParseNonNullableListType()
    {
        Assert.Equal(SyntaxFactory.NonNull(SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("Foo")))),
            ParseType("[Foo]!"));
    }

    [Fact]
    public void ParseNonNullableNamedType()
    {
        Assert.Equal(SyntaxFactory.NonNull(SyntaxFactory.NamedType(SyntaxFactory.Name("Foo"))), ParseType("Foo!"));
    }
}