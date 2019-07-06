// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using Xunit;

namespace GraphZen.LanguageModel
{
    public class BlockStringPrinterTests : ParserTestBase
    {
        [Fact]
        public void ItCorrectlyPrintsSingleLineWithLeadingSpace()
        {
            var doc = ParseDocument(@"{ field(arg: """"""    space-led value"""""") }");
            TestHelpers.AssertEquals(@"
              {
                field(arg: """"""    space-led value"""""")
              }
              ".Dedent(), doc.ToSyntaxString());
        }

        [Fact]
        public void ItCorrectlyPrintsStringWithFirstLineOfIndentation()
        {
            var doc = ParseDocument(@"
                {
                  field(arg: """"""
                        first
                      line
                    indentation
                  """""")
                }
            ");
            TestHelpers.AssertEquals(@"
                {
                  field(arg: """"""
                        first
                      line
                    indentation
                  """""")
                }
                ".Dedent(), doc.ToSyntaxString());
        }
    }
}