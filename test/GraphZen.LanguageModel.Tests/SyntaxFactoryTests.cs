using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace GraphZen.LanguageModel.Tests
{
    public class SyntaxFactoryTests
    {
        [Fact]
        public void it_should_have_factory_method_for_every_syntax_node()
        {
            var factoryMethods = typeof(SyntaxFactory).GetMethods().Select(_ => _.Name).ToArray();
            var expectedMethodNames = typeof(SyntaxNode).Assembly.GetTypes()
                .Where(_ => _.IsSubclassOf(typeof(SyntaxNode)) && !_.IsAbstract).Select(_ => _.Name.Replace("Syntax", ""));
            var missingMethods = expectedMethodNames.Where(_ => !factoryMethods.Contains(_));
            missingMethods.Should().BeEmpty();
        }
    }
}
