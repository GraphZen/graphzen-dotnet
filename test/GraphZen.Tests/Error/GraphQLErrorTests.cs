// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using Xunit;

namespace GraphZen.Error
{
    public class GraphQLErrorTests
    {
        [Fact]
        public void ItCanBeCreated()
        {
            var e = new GraphQLError("msg");
            Assert.IsType<GraphQLError>(e);
        }


        [Fact]
        public void ItConvertsNodesToPositionsAndLocations()
        {
            var gql = @"{ field }";
            var ast = new SuperpowerParser().ParseDocument(@"{ field }");
            var fieldNode = ast.Definitions[0].As<OperationDefinitionSyntax>().SelectionSet.Selections[0];
            Assert.IsType<FieldSyntax>(fieldNode);
            var e = new GraphQLError("msg", new[] { fieldNode });
            Assert.Equal(new[] { fieldNode }, e.Nodes);
            Assert.Equal(gql, e.Source.Body);
            Assert.Equal(new[] { 2 }, e.Positions);
            Assert.Equal(new[] { new SourceLocation(1, 3) }, e.Locations);
        }

        [Fact]
        public void ItConvertsNodeWith0StartValueToPositionsAndLocations()
        {
            var gql = "{ field }";
            var ast = new SuperpowerParser().ParseDocument(gql);
            var operationNode = (OperationDefinitionSyntax)ast.Definitions.First();
            var e = new GraphQLError("msg", new[] { operationNode });
            Assert.Equal(new[] { operationNode }, e.Nodes);
            Assert.Equal(gql, e.Source.Body);
            Assert.Equal(new[] { 0 }, e.Positions);
            Assert.Equal(new[] { new SourceLocation(1, 1) }, e.Locations);
        }

        [Fact]
        public void ItSerializesToIncludeMessage()
        {
            var e = new GraphQLError("msg");
            TestHelpers.AssertEqualsDynamic(new
            {
                message = "msg"
            }, e);
        }

        [Fact]
        public void ItSerializesToIncludeMessageAndLocations()
        {
            var gql = @"{ field }";
            var ast = new SuperpowerParser().ParseDocument(gql);
            var node = ast.Definitions[0].As<OperationDefinitionSyntax>().SelectionSet.Selections.First();
            var e = new GraphQLError("msg", new[] { node });
            TestHelpers.AssertEqualsDynamic(new
            {
                message = "msg",
                locations = new object[] { new { line = 1, column = 3 } }
            }, e);
        }

        [Fact]
        public void ItSerializesToIncludePath()
        {
            var e = new GraphQLError("msg", null, null, null, new object[] { "path", 3, "to", "field" });
            Assert.Equal(new object[] { "path", 3, "to", "field" }, e.Path);
            TestHelpers.AssertEqualsDynamic(new
            {
                message = "msg",
                path = new object[] { "path", 3, "to", "field" }
            }, e);
        }
    }
}