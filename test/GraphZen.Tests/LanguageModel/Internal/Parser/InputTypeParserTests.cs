// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Tests.LanguageModel.Internal.Parser
{
    [NoReorder]
    public class InputTypeParserTests : ParserTestBase
    {
        [Fact]
        public void ParseListType()
        {
            ParseType("[Foo]").Should().Be(SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("Foo"))));
        }

        [Fact]
        public void ParseNamedType()
        {
            ParseType("Foo").Should().Be(SyntaxFactory.NamedType(SyntaxFactory.Name("Foo")));
        }

        [Fact]
        public void ParseNonNullableListType()
        {
            ParseType("[Foo]!").Should()
                .Be(SyntaxFactory.NonNullType(SyntaxFactory.ListType(SyntaxFactory.NamedType(SyntaxFactory.Name("Foo")))));
        }

        [Fact]
        public void ParseNonNullableNamedType()
        {
            ParseType("Foo!").Should().Be(SyntaxFactory.NonNullType(SyntaxFactory.NamedType(SyntaxFactory.Name("Foo"))));
        }
    }
}