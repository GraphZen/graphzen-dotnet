// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;

namespace GraphZen
{
    public class ConfigurationTestCodeGenerator
    {
        public static void Generate()
        {
            var g = new MetaModelTestCaseGenerator();
            var testClasses = g.GetTemplateModels(ImmutableArray<Element>.Empty, GraphQLMetaModel.Schema()).ToList();
            var genDir = CodeGenHelpers.GetTargetDirectory();
            CodeGenHelpers.DeleteGeneratedFiles(genDir);

            foreach (var testClass in testClasses.Where(_ => _.Type == NodeType.Leaf))
            {

            }
        }

        private static void CreateClass(TestClass testClass)
        {

        }


        public static void GenerateCode([NotNull] IEnumerable<Element> elements)
        {
            var genDir = CodeGenHelpers.GetTargetDirectory();
            CodeGenHelpers.DeleteGeneratedFiles(genDir);


            foreach (var element in elements)
            {
                switch (element)
                {
                    case Collection _:
                        break;
                    case LeafElement _:
                        break;
                    case Vector vector:
                        foreach (var member in vector.OfType<LeafElement>())
                        {
                            var baseName = $"{vector.Name}{member.Name}TestsBase";
                            var name = $"{vector.Name}{member.Name}Tests";

                            var testCases = new ConventionTestCaseGenerator().GetTestCasesForElement(member)
                                .Select(testCase =>
                                    $@"[Fact] public override void {testCase}()  => base.{testCase}(); ");


                            var basePath = Path.Combine(genDir, $"{baseName}.Generated.cs");
                            File.AppendAllText(basePath, $@"
// ReSharper disable RedundantUsingDirective
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;

namespace GraphZen.Configuration
{{
    public abstract class {baseName}: LeafElementConfigurationTests<{member.MarkerInterface?.Name}, {member.MutableMarkerInterface?.Name},{vector.Name}Definition,{vector.Name}> {{

{string.Join(Environment.NewLine, testCases)}

}}
}}");
                            Console.WriteLine($"wrote file {basePath}");
                            var testPath = Path.Combine(genDir, $"{name}.cs");

                            var regenerateFlag = "regenerate:true";

                            // ReSharper disable once PossibleNullReferenceException
                            if (!File.Exists(testPath) || File.ReadAllText(testPath).Contains(regenerateFlag))

                            {
                                File.Delete(testPath);
                                File.AppendAllText(testPath,
                                    $@"
// Last generated: {DateTime.Now:F}
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute


namespace GraphZen.Configuration
{{
    public abstract /* {regenerateFlag} */ class {name}: {baseName} {{ 
    }}
}}
");
                            }
                        }

                        break;
                }
            }
        }
    }
}