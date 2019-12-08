using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.Objects.DirectiveAnnotations
{
    public class ObjectDirectiveAnnotationFunctionalTests
    {
        [Fact]
        public void it_throw_on_adding_unknown_directive()
        {
            Schema.Create(_ =>
            {
                Action act = () => _.Object("Foo").DirectiveAnnotation("unknown");
                act.Should().Throw<InvalidOperationException>()
                    .WithMessage("Unknown directive: cannot add 'unknown' directive to object 'Foo'. Ensure the directive is defined in the schema before it is used.");
            });
        }
    }
}