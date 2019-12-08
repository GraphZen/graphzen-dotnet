using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using GraphZen.LanguageModel.Internal;
using Xunit;

namespace GraphZen.LanguageModel.Tests.Internal.Grammar
{
    public class DirectiveLocationExtensionTests
    {
        [Fact]
        public void get_display_value_should_not_be_null_for_all_values()
        {
            foreach (var value in Enum.GetValues(typeof(DirectiveLocation)).Cast<DirectiveLocation>())
            {
                value.GetDisplayValue().Should().NotBeNull();
            }
        }
    }
}
