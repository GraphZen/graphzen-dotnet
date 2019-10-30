// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;

namespace GraphZen
{
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
                        if (collection.InspectCollectionItem)
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
            Check.NotNull(collection, nameof(collection));
            Check.NotNull(parent, nameof(parent));
            var typeName = typeof(CollectionElementConfigurationTests<,,,,,>).Name.Split("`")[0];

            return $@"{typeName}<{collection.MarkerInterfaceType.Name}, 
                                 {collection.MutableMarkerInterfaceType.Name}, 
                                 {parent.MemberDefinitionType.Name}, 
                                 {parent.MemberType.Name}, 
                                 {collection.CollectionItem.MemberDefinitionType.Name},
                                 {collection.CollectionItem.MemberType.Name}>";
        }

        public static string GetLeafTestBaseClassName(LeafElement leaf, Vector parent)
        {
            var typeName = typeof(LeafElementConfigurationTests<,,,,>).Name.Split("`")[0];

            return
                $"{typeName}<{leaf.MarkerInterfaceType.Name}, {leaf.MutableMarkerInterfaceType.Name}, {parent.MemberDefinitionType.Name}, {parent.MemberType.Name}, {leaf.ElementType.Name}>";
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
}