// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.Tests.LanguageModel
{
    public class BlockStringPrinterTests : ParserTestBase
    {
        [Fact]
        public void ItCorrectlyPrintsSingleLineWithLeadingSpace()
        {
            Assert.Equal(
                @"
              {
                field(arg: """"""    space-led value"""""")
              }
              ".Dedent(),
                ParseDocument(@"{ field(arg: """"""    space-led value"""""") }")
                    .ToSyntaxString());
        }

        [Fact]
        public void ItCorrectlyPrintsStringWithFirstLineOfIndentation()
        {
            Assert.Equal(
                @"
                {
                  field(arg: """"""
                        first
                      line
                    indentation
                  """""")
                }
                ".Dedent(),
                ParseDocument(@"
                {
                  field(arg: """"""
                        first
                      line
                    indentation
                  """""")
                }
            ").ToSyntaxString());
        }
    }
}
