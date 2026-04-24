// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Text.Json.Nodes;

namespace GraphZen.Infrastructure;

public class JsonNodeSortingExtensionsTests
{
    [Theory]
    [InlineData(@"{""c"":1, ""a"": -1, ""b"": 2}", @"{""a"":-1, ""b"": 2, ""c"": 1 }")]
    public void it_should_sort_json_object_properties_by_key(string unsortedJson, string expectedJson)
    {
        // Arrange
        var unsortedNode = JsonNode.Parse(unsortedJson);
        var expectedNode = JsonNode.Parse(expectedJson);

        // Act
        unsortedNode.Sort();

        // Assert
        unsortedJson = unsortedNode!.ToJsonString();
        expectedJson = expectedNode!.ToJsonString();
        StringAssert.Equal(unsortedJson, expectedJson, true);
    }

    [Theory]
    [InlineData(@"[]", @"[]")]
    [InlineData(@"[3,2,1]", @"[1,2,3]")]
    public void it_should_sort_json_arrays_by_item(string unsortedJson, string expectedJson)
    {
        // Arrange
        var unsortedNode = JsonNode.Parse(unsortedJson);
        var expectedNode = JsonNode.Parse(expectedJson);

        // Act
        unsortedNode.Sort();

        // Assert
        unsortedJson = unsortedNode!.ToJsonString();
        expectedJson = expectedNode!.ToJsonString();
        StringAssert.Equal(unsortedJson, expectedJson, true);
    }
}
