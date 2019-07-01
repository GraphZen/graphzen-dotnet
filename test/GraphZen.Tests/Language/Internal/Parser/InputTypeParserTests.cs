// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.Language.SyntaxFactory;

namespace GraphZen.Language.Internal
{
    [NoReorder]
    public class InputTypeParserTests : ParserTestBase
    {
        [Fact]
        public void ParseListType() =>
            ParseType("[Foo]").Should().Be(ListType(NamedType(Name("Foo"))));

        [Fact]
        public void ParseNamedType() =>
            ParseType("Foo").Should().Be(NamedType(Name("Foo")));

        [Fact]
        public void ParseNonNullableListType() =>
            ParseType("[Foo]!").Should()
                .Be(NonNull(ListType(NamedType(Name("Foo")))));

        [Fact]
        public void ParseNonNullableNamedType() =>
            ParseType("Foo!").Should().Be(NonNull(NamedType(Name("Foo"))));
    }
}