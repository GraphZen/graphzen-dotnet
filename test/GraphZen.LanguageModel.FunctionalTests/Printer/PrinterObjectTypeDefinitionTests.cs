using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using static GraphZen.LanguageModel.SyntaxFactory;

namespace GraphZen.LanguageModel.FunctionalTests.Printer
{
    public class PrinterObjectTypeDefinitionTests
    {
        private IPrinter sut = new Internal.Printer();

        [Fact]
        public void it_should_print_keyword_and_typename()
        {
            var syntax = ObjectTypeDefinition(Name("Foo"));
            sut.Print(syntax).Should().Be("type Foo");
        }
    }
}
