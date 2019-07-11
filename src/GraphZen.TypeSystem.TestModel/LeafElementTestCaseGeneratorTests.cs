// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;
// ReSharper disable PossibleMultipleEnumeration

// ReSharper disable PossibleNullReferenceException

namespace GraphZen
{
    public class LeafElementTestCaseGeneratorTests
    {
        public static LeafElementConfigurationTests<INamed, IMutableNamed, ScalarTypeDefinition, ScalarType>
            TestCases =>
            throw new NotImplementedException();


        [Theory]
        [InlineData(new ConfigurationSource[] { }, new string[] { })]
        [InlineData(new[] { ConfigurationSource.Convention }, new[] { nameof(TestCases.defined_by_convention) })]
        [InlineData(new[] { ConfigurationSource.DataAnnotation }, new[] { nameof(TestCases.define_by_data_annotation) })]
        [InlineData(new[] { ConfigurationSource.DataAnnotation, ConfigurationSource.Explicit }, new[]
        {
            nameof(TestCases.define_by_data_annotation),
            nameof(TestCases.define_by_data_annotation_overridden_by_explicit_configuration)
        })]
        public void define_scenarios(ConfigurationSource[] defineScenarios, string[] expectedTestCases)
        {
            var element = new LeafElement("foo", "foo", new ConfigurationScenarios
            {
                Define = defineScenarios
            });
            var testCases = ConfigurationTestCaseGenerator.GetTestCasesForElement(element);
            if (expectedTestCases.Length == 0)
            {
                testCases.Should().BeEmpty();
            }
            else
            {
                foreach (var actualTestCase in testCases)
                {
                    expectedTestCases.Should().Contain(actualTestCase, $"the test case '{actualTestCase}' is not required for combination of configuration sources: {string.Join(",", defineScenarios)}");
                }

                foreach (var expectedTestCase in expectedTestCases)
                {
                    testCases.Should().Contain(expectedTestCase, $"the test case '{expectedTestCase}' is required with supported combination of configuration sources: {string.Join(",", defineScenarios)}");
                }

            }
        }

        [Fact]
        public void optional_define_scenarios()
        {
            foreach (var element in GraphQLMetaModel.ElementsDeep().Where(_ => _.Optional))
            {
                ConfigurationTestCaseGenerator.GetTestCasesForElement(element).Should()
                    .Contain(nameof(TestCases.optional_not_defined_by_convention));
            }
        }
    }
}