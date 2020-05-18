// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.Tests.QueryEngine;
using GraphZen.TypeSystem;
using GraphZen.Utilities;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.Tests.TypeSystem.IntrospectionTests
{
    [NoReorder]
    public class IntrospectionTests : ExecutorHarness
    {
        [Fact]
        public void IntrospectionSchemaContainsExpectedTypes()
        {
            var schema = Schema.Create();
            schema.HasObject("__Directive").Should().BeTrue();
            schema.HasEnum("__DirectiveLocation").Should().BeTrue();
            schema.HasObject("__EnumValue").Should().BeTrue();
            schema.HasObject("__Field").Should().BeTrue();
            schema.HasObject("__InputValue").Should().BeTrue();
            schema.HasObject("__Schema").Should().BeTrue();
            schema.HasObject("__Type").Should().BeTrue();
            schema.HasEnum("__TypeKind").Should().BeTrue();
            //var expectedIntrospectionTypes = new[]
            //{
            //    "__Directive", "__DirectiveLocation", "__EnumValue", "__Field", "__InputValue", "__Schema", "__Type",
            //    "__TypeKind"
            //};
            //var introspectionTypeNames = IntrospectionSpec.IntrospectionTypes.Select(_ => _.Name).ToArray();

            //introspectionTypeNames.Should().BeEquivalentTo(expectedIntrospectionTypes);

            //IntrospectionSpec.Schema.GetTypes().Count().Should() .Be(expectedIntrospectionTypes.Length);
        }


        [Fact]
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