// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;
using GraphZen.TypeSystem.Internal;

namespace GraphZen
{
    public static class CodeGenHelpers
    {
        [NotNull]
        public static DirectoryInfo GetSolutionDirectory(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            return directory ?? throw new Exception("Could not find solution directory");
        }
    }

    public class ConfigurationTestCodeGenerator
    {
        public static void Generate()
        {
            GenerateCode(GraphQLMetaModel.Elements);
        }


        public static void GenerateCode([NotNull] IEnumerable<Element> elements)
        {
            var solutionDir = CodeGenHelpers.GetSolutionDirectory();
            var genDir = Path.Combine(solutionDir.ToString(), "src", "GraphZen.TypeSystem.Tests",
                "Configuration");
            var generatedFiles =
                Directory.GetFiles(genDir, "*.Generated.cs", SearchOption.AllDirectories);
            foreach (var generatedFile in generatedFiles)
            {
                File.Delete(generatedFile);
            }

            foreach (var element in elements)
            {
                switch (element)
                {
                    case Collection collection:
                        break;
                    case LeafElement leafElement:
                        break;
                    case Vector vector:

                        foreach (var member in vector)
                        {
                            var baseName = $"{vector.Name}{member.Name}TestsBase";
                            var name = $"{vector.Name}{member.Name}Tests";


                            var basePath = Path.Combine(genDir, $"{baseName}.Generated.cs");
                            File.AppendAllText(basePath, $@"using GraphZen.Infrastructure;

namespace GraphZen.Configuration
{{
    public abstract class {baseName}: LeafElementConfigurationTests {{}}
}}");
                            Console.WriteLine($"wrote file {basePath}");
                            var testPath = Path.Combine(genDir, $"{name}.cs");
                            var regenerateFlag = "regenerate:true";
                            if (!File.Exists(testPath) || File.ReadAllText(testPath).Contains(regenerateFlag))

                            {
                                File.Delete(testPath);
                                File.AppendAllText(testPath,
                                    $@"
// {regenerateFlag}

/*
namespace GraphZen.Configuration
{{
    public class {name}: {baseName} {{}}
}}
*/
");
                            }
                        }

                        break;
                }
            }
        }
    }

    public class ConfigurationTestCaseGenerator
    {
        [NotNull]
        [ItemNotNull]
        public IEnumerable<string> GenerateTestCasesForElement([NotNull] Element element)
        {
            switch (element)
            {
                case Collection collection:
                    return GenerateTestCasesForCollection(collection);
                case LeafElement leafElement:
                    return GenerateTestCasesForLeaf(leafElement);
                case Vector vector:
                    return GenerateTestCasesForVector(vector);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [NotNull]
        [ItemNotNull]
        private IEnumerable<string> GenerateTestCasesForLeaf([NotNull] LeafElement element)
        {
            if (element.ConfigurationScenarios.Define.Contains(ConfigurationSource.Convention))
            {
                yield return "";
            }
        }

        [NotNull]
        [ItemNotNull]
        private IEnumerable<string> GenerateTestCasesForVector([NotNull] Vector element) => Enumerable.Empty<string>();

        [NotNull]
        [ItemNotNull]
        private IEnumerable<string> GenerateTestCasesForCollection([NotNull] Collection element) =>
            Enumerable.Empty<string>();
    }
}