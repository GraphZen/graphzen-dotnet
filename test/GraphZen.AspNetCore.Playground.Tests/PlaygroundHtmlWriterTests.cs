// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.Playground;
using GraphZen.Playground.Internal;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.AspNetCore.Playground.Tests
{
    public class PlaygroundHtmlWriterTests
    {
        [Fact]
        public void value_should_contain_playground_html()
        {
            var html = PlaygroundHtmlWriter.GetHtml();
            html.Should().NotBeNullOrWhiteSpace();
            html.Should().Contain("<html>");
            html.Should().Contain("<title>GraphQL Playground</title>");
        }

        [Fact]
        public void playground_html_should_contain_options_object()
        {
            var options = new PlaygroundOptions { Endpoint = "foo" };
            var html = PlaygroundHtmlWriter.GetHtml(options);
            var optionsJson = PlaygroundHtmlWriter.GetPlaygroundOptionsJson(options);
            html.Should().Contain(optionsJson);
        }
    }
}