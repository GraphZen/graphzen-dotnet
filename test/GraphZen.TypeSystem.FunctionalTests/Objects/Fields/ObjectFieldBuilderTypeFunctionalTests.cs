using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Objects.Fields
{
    public class ObjectFieldBuilderTypeFunctionalTests
    {
        [Fact]
        public void invalid_field_type_should_throw_helpful_exception()
        {
            Schema.Create(_ =>
            {
                Action act = () => _.Object("Foo").Field("Bar", "List<>");
                act.Should().Throw<InvalidOperationException>()
                    .WithMessage("Invalid type reference: 'List<>' is not a valid type reference for object field 'Foo.Bar'.");
            });

        }
    }
}
