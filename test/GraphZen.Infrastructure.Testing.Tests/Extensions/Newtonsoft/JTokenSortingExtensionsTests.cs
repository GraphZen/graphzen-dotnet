// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace GraphZen.Infrastructure
{
    public class JTokenSortingExtensionsTests
    {
        [Theory]
        [InlineData(@"{""c"":1, ""a"": -1, ""b"": 2}", @"{""a"":-1, ""b"": 2, ""c"": 1 }")]
        public void it_should_sort_json_object_properties_by_key(string unsortedJson, string expectedJson)
        {
            // Arrange
            var unsortedJToken = JToken.Parse(unsortedJson);
            var expectedJToken = JToken.Parse(expectedJson);

            // Act
            unsortedJToken.Sort();

            // Assert
            unsortedJson = JsonConvert.SerializeObject(unsortedJToken);
            expectedJson = JsonConvert.SerializeObject(expectedJToken);
            unsortedJson.Should().Be(expectedJson, true);
        }

        [Theory]
        [InlineData(@"[]", @"[]")]
        [InlineData(@"[3,2,1]", @"[1,2,3]")]
        public void it_should_sort_json_arrays_by_item(string unsortedJson, string expectedJson)
        {
            // Arrange
            var unsortedJToken = JToken.Parse(unsortedJson);
            var expectedJToken = JToken.Parse(expectedJson);

            // Act
            unsortedJToken.Sort();

            // Assert
            unsortedJson = JsonConvert.SerializeObject(unsortedJToken);
            expectedJson = JsonConvert.SerializeObject(expectedJToken);
            unsortedJson.Should().Be(expectedJson, true);
        }
    }
}