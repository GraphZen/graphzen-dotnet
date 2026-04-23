// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using Xunit.Sdk;

namespace GraphZen.Infrastructure
{
    public class JsonAssertTests
    {
        [Fact]
        public void equal_objects_should_not_fail()
        {
            JsonAssert.EquivalentToJsonFromObject(new { foo = 1 }, new { foo = 1 });
            JsonAssert.EquivalentToJsonFromObject(new { foo = 1 }, new { foo = 1 }, new JsonDiffOptions());
            JsonAssert.EquivalentToJsonFromObject(new { foo = 1 }, new { foo = 1 }, options => { });
        }

        [Fact]
        public void unequal_objects_should_fail_with_diff()
        {
            var actual = new { foo = 1 };
            var expected = new { foo = 2 };
            var expectedDiff = JsonDiffer.GetDiff(actual, expected);
            Assert.Contains("Differences found", expectedDiff);
            var ex1 = Assert.Throws<XunitException>(() =>
                JsonAssert.EquivalentToJsonFromObject(actual, expected));
            Assert.Equal(expectedDiff, ex1.Message);
            var ex2 = Assert.Throws<XunitException>(() =>
                JsonAssert.EquivalentToJsonFromObject(actual, expected, new JsonDiffOptions()));
            Assert.Equal(expectedDiff, ex2.Message);
            var ex3 = Assert.Throws<XunitException>(() =>
                JsonAssert.EquivalentToJsonFromObject(actual, expected, opt => { }));
            Assert.Equal(expectedDiff, ex3.Message);
        }

        [Fact]
        public void equal_json_should_not_fail()
        {
            var actual = new { foo = 1 };
            var expected = @"{""foo"":1}";
            JsonAssert.EquivalentToJson(actual, expected);
            JsonAssert.EquivalentToJson(actual, expected, new JsonDiffOptions());
            JsonAssert.EquivalentToJson(new { foo = 1 }, expected, options => { });
        }

        [Fact]
        public void unequal_json_should_fail()
        {
            var actual = new { foo = 1 };
            var expected = @"{""foo"":2}";
            var expectedParsed = JsonNode.Parse(expected)!;
            var expectedDiff = JsonDiffer.GetDiff(actual, expectedParsed);
            var ex1 = Assert.Throws<XunitException>(() =>
                JsonAssert.EquivalentToJson(actual, expected));
            Assert.Equal(expectedDiff, ex1.Message);
            var ex2 = Assert.Throws<XunitException>(() =>
                JsonAssert.EquivalentToJson(actual, expected, new JsonDiffOptions()));
            Assert.Equal(expectedDiff, ex2.Message);
            var ex3 = Assert.Throws<XunitException>(() =>
                JsonAssert.EquivalentToJson(new { foo = 1 }, expected, options => { }));
            Assert.Equal(expectedDiff, ex3.Message);
        }
    }
}
