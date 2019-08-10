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


    public class MetaModelTestCaseCodeGenerator
    {
        public MetaModelTestCaseCodeGenerator(bool writeEnabled)
        {
            WriteEnabled = writeEnabled;
        }

        public bool WriteEnabled { get; }
        public List<TestClass> TestClasses { get; } = new List<TestClass>();

        public IEnumerable<TestClass> GenerateCode(ImmutableArray<Element> parents, Vector element)
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
                            tests.AddRange(GenerateCode(newParents.Add(collection), collection.CollectionItem));
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

        

        public IEnumerable<TestClass> GenerateCasesForVector(ImmutableArray<Element> parents, Vector vector) =>
            throw new NotImplementedException();

        public static string GetCollectionTestBaseClassName(Collection collection, Vector parent)
        {
            //var typeName = typeof(LeafElementConfigurationTests<,,,,>).Name.Split("`")[0];

            throw new NotImplementedException();
            //return $"{typeName}<{leaf.MarkerInterfaceType.Name}, {leaf.MutableMarkerInterfaceType.Name}, {parent.Name}Definition, {parent.Name}, {leaf.ElementType.Name}>";
        }

        public static string GetLeafTestBaseClassName(LeafElement leaf, Vector parent)
        {
            var typeName = typeof(LeafElementConfigurationTests<,,,,>).Name.Split("`")[0];

            return
                $"{typeName}<{leaf.MarkerInterfaceType.Name}, {leaf.MutableMarkerInterfaceType.Name}, {parent.Name}Definition, {parent.Name}, {leaf.ElementType.Name}>";
        }

        public void WriteClassFile(string name, string basename, bool @abstract, bool generated,
            IEnumerable<string> testCases, Element element, bool conventionContext)
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
            if (element is LeafElement leaf && testCases != null && testCases.Any())
            {
                content.AppendLine($@"
public override bool DefinedByConvention {{ get; }} = {(conventionContext && leaf.ConfiguredByConvention).ToString().ToLower()};
public override bool DefinedByDataAnnotation {{ get; }} = {(conventionContext && leaf.ConfiguredByDataAnnotation).ToString().ToLower()};
");



            }
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

        public void GenerateClass(string name, string baseTypeName, IEnumerable<string> cases, Element element, bool conventionContext) =>
            WriteClassFile(name, baseTypeName, true, true, cases, element, conventionContext);

        public void ScaffoldClass(string name, string baseTypeName, bool @abstract = true) =>
            WriteClassFile(name, baseTypeName, @abstract, false, Enumerable.Empty<string>(), null, false);


        public TestClass GenerateCasesForLeaf(
            ImmutableArray<Element> parents, LeafElement leaf)
        {
            var parent = parents[parents.Length - 1] as Vector;
            var path = GetTestPath(parents);
            var leafElementConfigurationTestsBase = GetLeafTestBaseClassName(leaf, parent);
            var defaultScenario = $"{path}__{leaf.Name}";
            var leafElementExplicitValues = $"{defaultScenario}_Base";

            var leafTests = new TestClass($"{path}__{leaf.Name}", leaf);
            var explicitTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, false);
            leafTests.Cases.AddRange(explicitTestCases);
            ScaffoldClass(leafElementExplicitValues, leafElementConfigurationTestsBase);
            var casesBase = defaultScenario + "_Cases";
            GenerateClass(casesBase, leafElementExplicitValues, explicitTestCases, leaf, false);
            ScaffoldClass(defaultScenario, casesBase);

            var conventionTestCases = TestCaseGenerator.GetTestCasesForLeaf(leaf, true);
            foreach (var parentConvention in parent.Conventions)
            {
                var conventionContextScenario = $"{path}_{parentConvention}__{leaf.Name}";
                var conventionCasesBase = conventionContextScenario + "_Cases";
                GenerateClass(conventionCasesBase, leafElementExplicitValues, conventionTestCases, leaf, true);
                ScaffoldClass(conventionContextScenario, conventionCasesBase);

                var conventionTests = new TestClass($"{path}_{parentConvention}__{leaf.Name}", leaf);
                conventionTests.Cases.AddRange(conventionTestCases);
                leafTests.SubClasses.Add(conventionTests);
            }

            return leafTests;
        } 
        
        public TestClass GenerateCasesCollection(ImmutableArray<Element> parents, Collection collection)
        {
            var parent = parents[parents.Length - 1] as Vector;
            var path = GetTestPath(parents);

            var collectionElementConfigurationTestsBase = GetCollectionTestBaseClassName(collection, parent);

            var defaultScenario = $"{path}__{collection.Name}";
            var collectionElementBaseClass = $"{defaultScenario}_Base";
            var collectionTests = new TestClass($"{path}__{collection.Name}", collection);
            var explicitTestCases = TestCaseGenerator.GetTestCasesForCollection(collection, false);
            collectionTests.Cases.AddRange(explicitTestCases);
            ScaffoldClass(collectionElementBaseClass, collectionElementConfigurationTestsBase);
            var casesBase = defaultScenario + "_Cases";
            GenerateClass(casesBase, collectionElementBaseClass, explicitTestCases, collection, false);
            ScaffoldClass(defaultScenario, casesBase);

            collectionTests.Cases.AddRange(explicitTestCases);
            var conventionTestCases = TestCaseGenerator.GetTestCasesForCollection(collection, true);
            foreach (var convention in collection.Conventions)
            {
                var conventionTests = new TestClass($"{path}__{collection.Name}_{convention}", collection);
                conventionTests.Cases.AddRange(conventionTestCases);
                collectionTests.SubClasses.Add(conventionTests);
            }

            return collectionTests;
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
                new MetaModelTestCaseCodeGenerator(false).GenerateCode(ImmutableArray<Element>.Empty,
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