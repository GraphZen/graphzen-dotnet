// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
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


    public class MetaModelTestCaseGenerator
    {
        public MetaModelTestCaseGenerator(bool writeEnabled)
        {
            WriteEnabled = writeEnabled;
        }

        public bool WriteEnabled { get; }
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
            var collectionTests = new TestClass($"{path}__{collection.Name}", collection);
            var explicitTestCases = Enumerable.Empty<string>();
            collectionTests.Cases.AddRange(explicitTestCases);
            var conventionTestCases = Enumerable.Empty<string>();
            foreach (var convention in collection.Conventions)
            {
                var conventionTests = new TestClass($"{path}__{collection.Name}_{convention}", collection);
                conventionTests.Cases.AddRange(conventionTestCases);
                collectionTests.SubClasses.Add(conventionTests);
            }

            return collectionTests;
        }

        public IEnumerable<TestClass> GenerateCasesForVector(ImmutableArray<Element> parents, Vector vector) =>
            throw new NotImplementedException();

        public static string LeafElementConfigurationTests(LeafElement leaf, Vector parent)
        {
            var typeName = typeof(LeafElementConfigurationTests<,,,,>).Name.Split("`")[0];

            return
                $"{typeName}<{leaf.MarkerInterfaceType.Name}, {leaf.MutableMarkerInterfaceType.Name}, {parent.Name}Definition, {parent.Name}, {leaf.ElementType.Name}>";
        }

        public void WriteClassFile(string name, string basename, bool @abstract, bool generated, IEnumerable<string> testCases)
        {
            if (!WriteEnabled)
            {
                return;
            }



            var regenerateFlag = "regenerate:true";
            var filename = generated ? $"{name}.Generated.cs" : $"{name}.cs";
            var filePath = CodeGenHelpers.GetFilePath(filename);
            var scaffold = !generated;
            if (scaffold && File.Exists(filePath))
            {
                return;
            }

            var content = new StringBuilder();
            content.AppendLine($"// Last generated: {DateTime.Now:F}");
            content.AppendLine("// ReSharper disable PossibleNullReferenceException");
            content.AppendLine("// ReSharper disable AssignNullToNotNullAttribute");
            content.AppendLine("// ReSharper disable InconsistentNaming");
            content.AppendLine("// ReSharper disable RedundantUsingDirective");

            content.AppendLine("using System;");
            content.AppendLine("using GraphZen.TypeSystem;");
            content.AppendLine("using GraphZen.TypeSystem.Taxonomy;");
            content.AppendLine("using Xunit;");

            content.AppendLine("namespace GraphZen.Configuration {");

            content.Append("public ");
            if (@abstract)
            {
                content.Append("abstract ");
            }
            if (scaffold)
            {
                content.Append($"/* {regenerateFlag} */");
            }


            content.AppendLine($" class {name} : {basename} {{");
            foreach (var testCase in testCases)
            {
                content.AppendLine("[Fact]");
                content.AppendLine($"public override void {testCase}() => base.{testCase}(); ");
            }

            content.AppendLine("}");
            content.AppendLine("}");
            File.AppendAllText(filePath, content.ToString());
            if (generated)
            {
                Console.WriteLine($"Generated {filename} with {testCases.Count()} test cases");

            }
            else
            {
                Console.WriteLine($"Scaffolded {filename} with {testCases.Count()} test cases");

            }
        }

        public void GenerateClass(string name, string baseTypeName, IEnumerable<string> cases) =>
            WriteClassFile(name, baseTypeName, true, true, cases);

        public void ScaffoldClass(string name, string baseTypeName, bool @abstract = true) =>
            WriteClassFile(name, baseTypeName, @abstract, false, Enumerable.Empty<string>());


        public TestClass GenerateCasesForLeaf(
            ImmutableArray<Element> parents, LeafElement leaf)
        {
            var parent = parents[parents.Length - 1] as Vector;
            var path = GetTestPath(parents);
            var leafElementConfigurationTestsBase = LeafElementConfigurationTests(leaf, parent);
            var defaultScenario = $"{path}__{leaf.Name}";
            var leafElementExplicitValues = $"{defaultScenario}_Base";

            var leafTests = new TestClass($"{path}__{leaf.Name}", leaf);
            var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf);
            leafTests.Cases.AddRange(explicitTestCases);
            ScaffoldClass(leafElementExplicitValues, leafElementConfigurationTestsBase);
            var casesBase = defaultScenario + "_Cases";
            GenerateClass(casesBase, leafElementExplicitValues, explicitTestCases);
            ScaffoldClass(defaultScenario, casesBase);

            var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf);
            foreach (var parentConvention in parent.Conventions)
            {
                var conventionContextScenario = $"{path}_{parentConvention}__{leaf.Name}";
                var conventionCasesBase = conventionContextScenario + "_Cases";
                GenerateClass(conventionCasesBase, leafElementExplicitValues, conventionTestCases);
                ScaffoldClass(conventionContextScenario, conventionCasesBase);

                var conventionTests = new TestClass($"{path}_{parentConvention}__{leaf.Name}", leaf);
                conventionTests.Cases.AddRange(conventionTestCases);
                leafTests.SubClasses.Add(conventionTests);
            }

            return leafTests;
        }

        private static string GetTestPath(ImmutableArray<Element> parents)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var segments = parents
                .Where(_ => !(_ is Collection))
                .Where(_ => parents.Length < 2 || _.Name != "Schema")
                // .TakeLast(3)
                .Select(_ => _.Name);
            return string.Join("__", segments);
        }
    }


    public class TestClass
    {
        public TestClass(string name, Element element)
        {
            Name = name;
            Element = element;
        }

        public Element Element { get; }

        public string Name { get; }
        public bool Generated { get; set; }
        public bool Abstract { get; set; }
        public List<string> Cases { get; } = new List<string>();
        public List<TestClass> SubClasses { get; } = new List<TestClass>();
    }

    public class LeafElementCodeGeneratorTests
    {
        [Theory(Skip = "wip")]
        [InlineData("")]
        public void ShouldContainTest(string expectedTestClassName)
        {
            var models =
                new MetaModelTestCaseGenerator(false).GetTemplateModels(ImmutableArray<Element>.Empty,
                    GraphQLMetaModel.Schema());
            // ReSharper disable once AssignNullToNotNullAttribute
            var names = models
                // ReSharper disable once AssignNullToNotNullAttribute
                .Concat(models.SelectMany(_ => _.SubClasses))
                .Select(_ => _.Name)
                .ToArray();
            if (!names.Contains(expectedTestClassName))
            {
                throw new Exception(
                    $"expect {expectedTestClassName}, names ({names.Length}) were: \n\n{string.Join('\n', names)}\n\n");
            }
        }
    }

    public class LeafElementTestCaseGeneratorTests
    {
        public static LeafElementConfigurationTests<INamed, IMutableNamed, ScalarTypeDefinition, ScalarType, string>
            TestCases =>
            throw new NotImplementedException();

        [Fact]
        public void optional_define_scenarios()
        {
            //foreach (var element in GraphQLMetaModel.ElementsDeep().Where(_ => _.Optional))
            //{
            //    ConfigurationTestCaseGenerator.GetTestCasesForElement(element).Should()
            //        .Contain(nameof(TestCases.optional_not_defined_by_convention));
            //}
        }

        [Theory]
        [InlineData(nameof(TestCases.configured_explicitly_reconfigured_explicitly))]
        public void all_leafs(string testCase)
        {
            var leafs = new List<LeafElement>()
            {
                new LeafElement<INamed, IMutableNamed, string>("foo")
                {
                    Optional = false
                },
                new LeafElement<INamed, IMutableNamed, string>("foo")
                {
                    Optional = true,
                    ConfiguredByConvention = true,
                    ConfiguredByDataAnnotation = true
                }
            };

            foreach (var leaf in leafs)
            {

                var testCases = TestCaseGenerator.GetTestCasesForLeaf(leaf);
                testCases.Should().Contain(testCase);

            }

        }


        [Theory]
        [InlineData(nameof(TestCases.optional_not_defined_by_convention_when_parent_configured_explicitly))]
        [InlineData(nameof(TestCases.optional_not_defined_by_convention_then_configured_explicitly))]
        public void optional_leafs(string testCase)
        {
            var optionalLeaf = new LeafElement<INamed, IMutableNamed, string>("foo")
            {
                Optional = true
            };

            var optionalLeafTestCases = TestCaseGenerator.GetTestCasesForLeaf(optionalLeaf);
            optionalLeafTestCases.Should().Contain(testCase);
            var requiredLeaf = new LeafElement<INamed, IMutableNamed, string>("foo")
            {
                Optional = false
            };
            var requiredLeafTestCases = TestCaseGenerator.GetTestCasesForLeaf(requiredLeaf);
            requiredLeafTestCases.Should().NotContain(testCase);
        }
    }
}