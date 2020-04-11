// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.Tests.QueryEngine;
using GraphZen.TypeSystem;
using GraphZen.Utilities;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Tests.TypeSystem.IntrospectionTests
{
    [NoReorder]
    public class IntrospectionTests : ExecutorHarness
    {
        [Fact]
        public void IntrospectionSchemaContainsExpectedTypes()
        {
            var expectedIntrospectionTypes = new[]
            {
                "__Directive", "__DirectiveLocation", "__EnumValue", "__Field", "__InputValue", "__Schema", "__Type",
                "__TypeKind"
            };
            var introspectionTypeNames = Introspection.IntrospectionTypes.Select(_ => _.Name).ToArray();

            introspectionTypeNames.Should().BeEquivalentTo(expectedIntrospectionTypes);

            Introspection.Schema.GetTypes().Count().Should()
                .Be(expectedIntrospectionTypes.Length + SpecScalars.All.Count);
        }


        [Fact()]
        public async Task ItExecutesIntrospectionQuery()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("QueryRoot").Field("onlyField", "String");
                _.QueryType("QueryRoot");
            });

            var query = Helpers.IntrospectionQuery(false);
            await ExecuteAsync(schema, query, throwOnError: true)
                .ShouldEqualJsonFile("./TypeSystem/IntrospectionTests/introspection-expected-result.json",
                    new JsonDiffOptions
                    {
                        SortBeforeCompare = true,
                        StringDiffOptions =
                        {
                            ShowExpected = false,
                            ShowActual = false,
                            ShowDiffs = true
                        }
                    });
        }
    }
}