// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;
using GraphZen.TypeSystem.Internal;

namespace GraphZen
{

    public class ConfigurationTestCaseGeneratorTests
    {
        [NotNull] private readonly ConfigurationTestCaseGenerator _sut = new ConfigurationTestCaseGenerator();

        /*[Fact]
        public void leaf_element_defined_by_convention_should_have_test_case()
        {
            // ReSharper disable once PossibleNullReferenceException
            _sut.GenerateTestCasesForElement(new LeafElement("foo", new ConfigurationScenarios
            {
                Define = new[] {ConfigurationSource.Convention}
            })).Should().Contain(ConfigurationTestCases.leaf_item_can_be_defined_by_convention);
        }

        [Fact]
        public void leaf_element_not_defined_by_convention_should_not_have_test_case()
        {
            // ReSharper disable once PossibleNullReferenceException
            _sut.GenerateTestCasesForElement(new LeafElement("foo", new ConfigurationScenarios()
            )).Should().NotContain(ConfigurationTestCases.leaf_item_can_be_defined_by_convention);
        }*/
    }
}