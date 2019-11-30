// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.LanguageModel
{
    public class BlockStringPrinterTests : ParserTestBase
    {
        [Fact]
        public void ItCorrectlyPrintsSingleLineWithLeadingSpace()
        {
            ParseDocument(@"{ field(arg: """"""    space-led value"""""") }")
                .ToSyntaxString().Should().Be(
                    @"
              {
                field(arg: """"""    space-led value"""""")
              }
              ".Dedent());
        }

        [Fact]
        public void ItCorrectlyPrintsStringWithFirstLineOfIndentation()
        {
            ParseDocument(@"
                {
                  field(arg: """"""
                        first
                      line
                    indentation
                  """""")
                }
            ").ToSyntaxString().Should().Be(
                @"
                {
                  field(arg: """"""
                        first
                      line
                    indentation
                  """""")
                }
                ".Dedent());
        }
    }
}