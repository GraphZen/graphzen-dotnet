// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Sdk;

namespace GraphZen.Infrastructure
{
    public class ReferenceTypeJsonEquivalencyAssertionsTests
    {
        [Fact]
        public void equal_objects_should_not_fail()
        {
            new {foo = 1}.Should().BeEquivalentToJsonFromObject(new {foo = 1});
            new {foo = 1}.Should().BeEquivalentToJsonFromObject(new {foo = 1}, new JsonDiffOptions());
            new {foo = 1}.Should().BeEquivalentToJsonFromObject(new {foo = 1}, options => { });
        }

        [Fact]
        public void unequal_objects_should_fail_with_diff()
        {
            var actual = new {foo = 1};
            var expected = new {foo = 2};
            var expectedDiff = JsonDiffer.GetDiff(actual, expected);
            expectedDiff.Should().Contain("Differences found");
            Action act = () => actual.Should().BeEquivalentToJsonFromObject(expected);
            Action actOptions = () => actual.Should().BeEquivalentToJsonFromObject(expected, new JsonDiffOptions());
            Action actOptionsAction = () => actual.Should().BeEquivalentToJsonFromObject(expected, opt => { });
            act.Should().Throw<XunitException>().WithMessage(expectedDiff);
            actOptions.Should().Throw<XunitException>().WithMessage(expectedDiff);
            actOptionsAction.Should().Throw<XunitException>().WithMessage(expectedDiff);
        }

        [Fact]
        public void equal_json_should_not_fail()
        {
            var actual = new {foo = 1};
            var expected = @"{""foo"":1}";
            actual.Should().BeEquivalentToJson(expected);
            actual.Should().BeEquivalentToJson(expected, new JsonDiffOptions());
            new {foo = 1}.Should().BeEquivalentToJson(expected, options => { });
        }

        [Fact]
        public void unequal_json_should_fail()
        {
            var actual = new {foo = 1};
            var expected = @"{""foo"":2}";
            var expectedDiff = JsonDiffer.GetDiff(actual, JObject.Parse(expected));
            Action act = () => actual.Should().BeEquivalentToJson(expected);
            Action actOptions = () => actual.Should().BeEquivalentToJson(expected, new JsonDiffOptions());
            Action actOptionsAction = () => new {foo = 1}.Should().BeEquivalentToJson(expected, options => { });
            act.Should().Throw<XunitException>().WithMessage(expectedDiff);
            actOptions.Should().Throw<XunitException>().WithMessage(expectedDiff);
            actOptionsAction.Should().Throw<XunitException>().WithMessage(expectedDiff);
        }
    }
}