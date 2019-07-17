// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
    public abstract class LeafConfigurationTests
    {
        public abstract object ExplicitValue { get; }
    }

    public class InterfaceField_Name_ExplicitConfigurationValues : LeafConfigurationTests
    {
        public override object ExplicitValue { get; }
    }

    public class
        InterfaceFieldViaExplicit_Name_ExplicitConfigurationTests : InterfaceField_Name_ExplicitConfigurationValues
    {
        // Code genned, opt in to explicit configuration tests
    }

    public abstract class
        InterfaceFieldViaClrProperty_Name_ConventionalConfigurationTestsBase :
            InterfaceField_Name_ExplicitConfigurationValues
    {
        // Code genned, opt in to conventional and perhaps data annotation tests
    }

    /*   Vector CollectionItem CollectionConventionVariant  LeafItem  */
    public class
        InterfaceTypeFieldViaClrProperty_Name_ConventionalConfigurationTests :
            InterfaceFieldViaClrProperty_Name_ConventionalConfigurationTestsBase
    {
    }

    /********** Collection *********************/

    public class InterfaceTypeFieldViaClrProperty_ConfigurationTests
    {
        // Convention:
        // can_be_added_by_convention
        // ignored_by_convention?

        // Data Annotation
        // can_be_ignored_by_data_annotation

        // Explicit
        // added_by_convention_can_be_ignored_by_explicit_configuration
        // ignored_by_data_annotation_can_be_ignored_by_explicit_configuration
        // ignored_by_data_annotation_can_be_unignored_by_explicit_configuration
    }


    public class LeafElementTestTemplateModel
    {
        public string ElementPath { get; set; }
        public Element Element { get; set; }
        public List<string> ConventionalConfigurationTestCases { get; set; }
        public List<string> ExplicitConfigurationTestCases { get; set; }
    }

    public class MetaModelTestCaseGenerator
    {
        public List<TestClass> TestClasses { get; } = new List<TestClass>();

        public IEnumerable<TestClass> GetTemplateModels(ImmutableArray<Element> parents, Vector element)
        {
            var newParents = parents.Add(element);
            var tests = new List<TestClass>();

            foreach (var member in element)
            {
                switch (member)
                {
                    case Collection collection:
                        tests.Add(GenerateCasesCollection(newParents, collection));
                        if (collection.CollectionItem != null)
                        {
                            tests.AddRange(GetTemplateModels(newParents.Add(collection), collection.CollectionItem));
                        }
                        break;
                    case LeafElement leaf:
                        tests.Add(GenerateCasesForLeaf(newParents, leaf));
                        break;
                    case Vector vector:
                        tests.AddRange(GenerateCasesForVector(newParents, vector));
                        break;
                }
            }

            return tests;
        }

        public TestClass GenerateCasesCollection(ImmutableArray<Element> parents, Collection collection)
        {
            var path = GetTestPath(parents);
            var collectionTests = new TestClass($"{path}__{collection.Name}");
            var explicitTestCases = new ExplicitTestCaseGenerator().GetTestCasesForElement(collection);
            collectionTests.Cases.AddRange(explicitTestCases);
            var conventionTestCases = new ConventionTestCaseGenerator().GetTestCasesForElement(collection);
            foreach (var convention in collection.Conventions)
            {
                var conventionTests = new TestClass($"{path}__{collection.Name}_{convention}");
                conventionTests.Cases.AddRange(conventionTestCases);
                collectionTests.SubClasses.Add(conventionTests);
            }

            return collectionTests;
        }

        public IEnumerable<TestClass> GenerateCasesForVector(ImmutableArray<Element> parents, Vector vector) =>
            throw new NotImplementedException();

        public TestClass GenerateCasesForLeaf(
            ImmutableArray<Element> parents, LeafElement leaf)
        {
            var parent = parents[parents.Length - 1] as Vector;
            var path = GetTestPath(parents);
            var leafTests = new TestClass($"{path}__{leaf.Name}");
            var explicitTestCases = new ExplicitTestCaseGenerator().GetTestCasesForElement(leaf);
            leafTests.Cases.AddRange(explicitTestCases);
            var conventionTestCases = new ConventionTestCaseGenerator().GetTestCasesForElement(leaf);
            foreach (var parentConvention in parent.Conventions)
            {
                var conventionTests = new TestClass($"{path}_{parentConvention}__{leaf.Name}");
                conventionTests.Cases.AddRange(conventionTestCases);
                leafTests.SubClasses.Add(conventionTests);
            }

            return leafTests;
        }

        private static string GetTestPath(ImmutableArray<Element> parents)
        {
            var segements = parents
                .Where(_ => !(_ is Collection))
                .Where(_ => parents.Length < 2 || _.Name != "Schema")
                // .TakeLast(3)
                .Select(_ => _.Name);
            return string.Join("__", segements);
        }

        public class TestClass
        {
            public TestClass(string name)
            {
                Name = name;
            }

            public string Name { get; }
            public bool Generated { get; set; }
            public bool Abstract { get; set; }
            public List<string> Cases { get; } = new List<string>();
            public List<TestClass> SubClasses { get; } = new List<TestClass>();
        }
    }

    public class LeafElementCodeGeneratorTests
    {
        [Theory]
        [InlineData("")]
        public void ShouldContainTest(string expectedTestClassName)
        {
            var models =
                new MetaModelTestCaseGenerator().GetTemplateModels(ImmutableArray<Element>.Empty,
                    GraphQLMetaModel.Schema());
            var names = models
                .Concat(models.SelectMany(_ => _.SubClasses))
                .Select(_ => _.Name)
                .ToArray();
            var match = names.Contains(expectedTestClassName);
            if (!names.Contains(expectedTestClassName))
            {
                throw new Exception(
                    $"expect {expectedTestClassName}, names ({names.Length}) were: \n\n{string.Join('\n', names)}\n\n");
            }
        }
    }

    public class LeafElementTestCaseGeneratorTests
    {
        public static LeafElementConfigurationTests<INamed, IMutableNamed, ScalarTypeDefinition, ScalarType>
            TestCases =>
            throw new NotImplementedException();


        [Fact]
        public void optional_not_defined_by_convention()
        {
            var optionalLeaf = new LeafElement<INamed, IMutableNamed>("foo")
            {
                Optional = true
            };

            var optionalLeafTestCases = new ConventionTestCaseGenerator().GetTestCasesForElement(optionalLeaf);
            optionalLeafTestCases.Should().Contain(nameof(TestCases.optional_not_defined_by_convention));
            var requiredLeaf = new LeafElement<INamed, IMutableNamed>("foo")
            {
                Optional = false
            };
            var requiredLeafTestCases = new ConventionTestCaseGenerator().GetTestCasesForElement(requiredLeaf);
            requiredLeafTestCases.Should().NotContain(nameof(TestCases.optional_not_defined_by_convention));
        }

        [Fact]
        public void optional_define_scenarios()
        {
            //foreach (var element in GraphQLMetaModel.ElementsDeep().Where(_ => _.Optional))
            //{
            //    ConfigurationTestCaseGenerator.GetTestCasesForElement(element).Should()
            //        .Contain(nameof(TestCases.optional_not_defined_by_convention));
            //}
        }
    }
}