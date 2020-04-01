// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Xunit;
// ReSharper disable UnusedParameter.Local

namespace GraphZen.CodeGen
{
    public class SyntaxFactoryGeneratorTests
    {
        public const string TestValue = "hello";
        public class TestSyntax
        {
            [GenFactory(nameof(SyntaxFactory))]
            public TestSyntax(List<string> hello, SyntaxLocation location, string? nullalbe = TestValue, bool test = false)
            {
            }
        }

        [Fact(Skip = "wip")]
        public void get_factory_method_match_expected()
        {
            //var method = FactoryGenerator.GetFactoryMethods(typeof(TestSyntax)).First();
            // method.Method.Should().Be("");
        }
    }
}