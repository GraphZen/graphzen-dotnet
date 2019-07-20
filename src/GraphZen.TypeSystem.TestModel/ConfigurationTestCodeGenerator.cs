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
            var g = new MetaModelTestCaseGenerator(true);
            var genDir = CodeGenHelpers.GetTargetDirectory();
            CodeGenHelpers.DeleteGeneratedFiles(genDir);
            var testClasses = g.GetTemplateModels(ImmutableArray<Element>.Empty, GraphQLMetaModel.Schema()).ToList();
            /*

                        foreach (var testClass in testClasses.Where(_ => _.Element is LeafElement).Where(_ => _.SubClasses.Any()).Take(1))
                        {
                            var leaf = (LeafElement)testClass.Element;

                            File.Create(Path.Combine(genDir, testClass.Name + "_ExplicitConfigurationBase" + ".Generated.cs"));
                            var explicitConfigurationBase = testClass.Name + "_ExplicitConfigurationBase";
                            var lct = typeof(LeafElementConfigurationTests<,,,>).GetType().Name.Split('`')[0];
                            var tMarker = leaf.MarkerInterface.Name;
                            //var tMutableMarker = leaf.MutableMarkerInterface.Name;
                            //var tParentMemberDef = leaf.Name + "Definition";
                            //var tParentMember= leaf.Name ;

                            //CreateClassFile(explicitConfigurationBase,  testClass.Element);

                            File.Create(Path.Combine(genDir, testClass.Name + ".Generated.cs"));
                            File.Create(Path.Combine(genDir, testClass.Name + ".cs"));
                            foreach (var subClass in testClass.SubClasses)
                            {
                                File.Create(Path.Combine(genDir, subClass.Name + ".Generated.cs"));

                            }


                        }
                        */
        }

        public static void CreateClassFile(string name, string baseTypeName, bool generated, Element element)
        {

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