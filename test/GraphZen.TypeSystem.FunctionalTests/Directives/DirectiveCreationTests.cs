// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.FunctionalTests.Directives;

public class DirectiveCreationTests
{
    [Fact(Skip = "obsolete")]
    public void it_can_create_directive_by_name()
    {
        Schema.Create(_ => { _.Object("Foo").DirectiveAnnotation("example"); });
    }

    [Fact]
    public void it_can_create_directive_from_sdl()
    {
        var schema = Schema.Create(@"directive @foo on FIELD");
        var foo = schema.FindDirective("foo");
        Assert.IsType<Directive>(foo);
        Assert.Equal("foo", foo.Name);
    }

    [Fact(Skip = "TODO")]
    public void it_cannot_create_directive_from_sdl_without_directive_locations()
    {
        Action act = () => Schema.Create(@"directive @foo on FIELD");
        Assert.Throws<InvalidOperationException>(act);
    }
}