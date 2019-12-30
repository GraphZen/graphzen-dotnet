// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.CodeGen
{
    public class SyntaxFactoryGeneratorTests
    {
        public class TestSyntax
        {
            [GenFactory(nameof(SyntaxFactory))]
            public TestSyntax(SyntaxLocation location)
            {
            }
        }

        [Fact(Skip = "wip")]
        public void get_factory_method_match_expected()
        {
            var method = GenerateSyntaxFactory.GetFactoryMethod(typeof(TestSyntax)).First().method;
            method.Should().Be("hello");
        }
    }
}