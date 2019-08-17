// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;

// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable PossibleNullReferenceException

namespace GraphZen
{
    /********** Collection *********************/


    [NoReorder]
    public class LeafElementTestCaseGeneratorTests
    {
        [Theory]
        [InlineData(nameof(all_A))]
        [InlineData(nameof(TestCases.configured_explicitly_reconfigured_explicitly))]
        public void all_A(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                explicitTestCases.Should().Contain(testCase, leaf.ToString());
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                conventionTestCases.Should().Contain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(all_optional_B))]
        [InlineData(nameof(TestCases.optional_not_defined_by_convention_when_parent_configured_explicitly))]
        [InlineData(nameof(TestCases.optional_not_defined_by_convention_then_configured_explicitly))]
        [InlineData(nameof(TestCases.optional_not_defined_by_convention_then_configured_explicitly_then_removed))]
        public void all_optional_B(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (leaf.Optional)
                {
                    explicitTestCases.Should().Contain(testCase, leaf.ToString());
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    explicitTestCases.Should().NotContain(testCase, leaf.ToString());
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }
            }
        }

        [Theory]
        [InlineData(nameof(all_required_C))]
        public void all_required_C(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (!leaf.Optional)
                {
                    explicitTestCases.Should().Contain(testCase, leaf.ToString());
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    explicitTestCases.Should().NotContain(testCase, leaf.ToString());
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }
            }
        }

        [Theory]
        [InlineData(nameof(explicit_only_D))]
        public void explicit_only_D(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (leaf.ExplicitOnly)
                {
                    explicitTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    explicitTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                conventionTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(convention_and_data_annotation_E))]
        public void convention_and_data_annotation_E(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (leaf.ConfiguredByConvention && leaf.ConfiguredByDataAnnotation)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(optional_convention_and_data_annotation_F))]
        public void optional_convention_and_data_annotation_F(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (leaf.Optional && leaf.ConfiguredByConvention && leaf.ConfiguredByDataAnnotation)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(required_convention_and_data_annotation_G))]
        public void required_convention_and_data_annotation_G(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (!leaf.Optional && leaf.ConfiguredByConvention && leaf.ConfiguredByDataAnnotation)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(convention_only_H))]
        public void convention_only_H(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (leaf.ConfiguredByConvention && !leaf.ConfiguredByDataAnnotation)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(optional_convention_only_I))]
        public void optional_convention_only_I(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (leaf.Optional && leaf.ConfiguredByConvention && !leaf.ConfiguredByDataAnnotation)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(required_convention_only_J))]
        public void required_convention_only_J(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (!leaf.Optional && leaf.ConfiguredByConvention && !leaf.ConfiguredByDataAnnotation)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }


        [Theory]
        [InlineData(nameof(data_annotation_only_K))]
        public void data_annotation_only_K(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (!leaf.ConfiguredByConvention && leaf.ConfiguredByDataAnnotation)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(optional_data_annotation_only_l))]
        public void optional_data_annotation_only_l(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (leaf.Optional && !leaf.ConfiguredByConvention && leaf.ConfiguredByDataAnnotation)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(required_data_annotation_only_m))]
        public void required_data_annotation_only_m(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (!leaf.Optional && !leaf.ConfiguredByConvention && leaf.ConfiguredByDataAnnotation)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(all_data_annotation))]
        [InlineData(nameof(TestCases.configured_by_data_annotation))]
        [InlineData(nameof(TestCases.configured_by_data_annotation_then_reconfigured_explicitly))]
        public void all_data_annotation(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (leaf.ConfiguredByDataAnnotation)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(optional_data_annotation))]
        [InlineData(nameof(TestCases.optional_configured_by_data_annotation_then_removed_explicitly))]
        public void optional_data_annotation(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (leaf.Optional && leaf.ConfiguredByDataAnnotation)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(required_data_annotation))]
        public void required_data_annotation(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (!leaf.Optional && leaf.ConfiguredByDataAnnotation)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(all_convention))]
        public void all_convention(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (leaf.ConfiguredByConvention)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(optional_convention))]
        public void optional_convention(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (leaf.Optional && leaf.ConfiguredByConvention)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        [Theory]
        [InlineData(nameof(required_convention))]
        public void required_convention(string testCase)
        {
            foreach (var leaf in GetLeafScenarios())
            {
                var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false, false);
                var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true, false);
                if (!leaf.Optional && leaf.ConfiguredByConvention)
                {
                    conventionTestCases.Should().Contain(testCase, leaf.ToString());
                }
                else
                {
                    conventionTestCases.Should().NotContain(testCase, leaf.ToString());
                }

                explicitTestCases.Should().NotContain(testCase, leaf.ToString());
            }
        }

        public static LeafElementConfigurationTests<INamed, IMutableNamed, ScalarTypeDefinition, ScalarType, string>
                    TestCases =>
                    throw new NotImplementedException();


        private IEnumerable<LeafElement> GetLeafScenarios()
        {
            var trueFalse = new[] { true, false };
            foreach (var configuredByConventionValue in trueFalse)
            {
                foreach (var optionalValue in trueFalse)
                {
                    foreach (var configuredByDataAnnotationValue in trueFalse)
                    {
                        yield return new LeafElement<INamed, IMutableNamed, string>("foo")
                        {
                            Optional = optionalValue,
                            ConfiguredByConvention = configuredByConventionValue,
                            ConfiguredByDataAnnotation = configuredByDataAnnotationValue
                        };
                    }
                }
            }
        }
    }
}