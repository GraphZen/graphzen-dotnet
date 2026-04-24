// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Playground;
using GraphZen.Playground.Internal;

namespace GraphZen.AspNetCore.Playground.Tests;

public class PlaygroundHtmlWriterTests
{
    [Fact]
    public void value_should_contain_playground_html()
    {
        var html = PlaygroundHtmlWriter.GetHtml();
        Assert.False(string.IsNullOrWhiteSpace(html));
        Assert.Contains("<html>", html);
        Assert.Contains("<title>GraphQL Playground</title>", html);
    }

    [Fact]
    public void playground_html_should_contain_options_object()
    {
        var options = new PlaygroundOptions { Endpoint = "foo" };
        var html = PlaygroundHtmlWriter.GetHtml(options);
        var optionsJson = PlaygroundHtmlWriter.GetPlaygroundOptionsJson(options);
        Assert.Contains(optionsJson, html);
    }
}
