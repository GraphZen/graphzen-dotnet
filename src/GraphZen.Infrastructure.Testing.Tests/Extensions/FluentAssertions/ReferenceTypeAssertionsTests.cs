// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using Xunit.Sdk;

namespace GraphZen.Infrastructure
{
    public class ReferenceTypeAssertionsTests
    {
        [Fact]
        public void equal_objects_should_not_fail()
        {
            new {foo = 1}.Should().BeEquivalentToJson(new {foo = 1});
            new {foo = 1}.Should().BeEquivalentToJson(new {foo = 1}, new JsonDiffOptions());
            new {foo = 1}.Should().BeEquivalentToJson(new {foo = 1}, options => { });
        }

        [Fact]
        public void unequal_objects_should_fail_with_diff()
        {
            var actual = new {foo = 1};
            var expected = new {foo = 2};
            var expectedDiff = JsonDiffer.GetDiff(actual, expected);
            expectedDiff.Should().Contain("Differences found");
            Action act = () => actual.Should().BeEquivalentToJson(expected);
            Action actOptions = () => actual.Should().BeEquivalentToJson(expected, new JsonDiffOptions());
            Action actOptionsAction = () => actual.Should().BeEquivalentToJson(expected, opt => { });
            act.Should().Throw<XunitException>().WithMessage(expectedDiff);
            actOptions.Should().Throw<XunitException>().WithMessage(expectedDiff);
            actOptionsAction.Should().Throw<XunitException>().WithMessage(expectedDiff);
        }
    }
}