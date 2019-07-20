// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.IO;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen
{
    public static class CodeGenHelpers
    {

        public static string GetFilePath(string fileName)
        {
            return Path.Combine(GetTargetDirectory(), fileName);
        }

        [NotNull]
        public static DirectoryInfo GetSolutionDirectory(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory() ?? throw new Exception("unable to get directory"));
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            return directory ?? throw new Exception("Could not find solution directory");
        }

        public static string GetTargetDirectory()
        {
            var solutionDir = CodeGenHelpers.GetSolutionDirectory();
            var genDir = Path.Combine(solutionDir.ToString(), "src", "GraphZen.TypeSystem.Tests",
                "Configuration");
            return genDir;
        }

        public const string RegenerateFlag = "regenerate:true";

        public static void DeleteGeneratedFiles(string genDir)
        {

            var generatedFiles =
                Directory.GetFiles(genDir, "*.Generated.cs", SearchOption.AllDirectories) ??
                throw new Exception("Unable to get generated files");
            foreach (var generatedFile in generatedFiles)
            {
                File.Delete(generatedFile);
            }

            var possiblyScaffoldedFiles = Directory.GetFiles(genDir, "*.cs", SearchOption.AllDirectories);
            foreach (var maybeScaffoldedFile in possiblyScaffoldedFiles)
            {
                if (File.ReadAllText(maybeScaffoldedFile).Contains(RegenerateFlag))
                {
                    Console.WriteLine($"Deleting scaffolded file: {maybeScaffoldedFile}");
                    File.Delete(maybeScaffoldedFile);
                }
            }
        }
    }
}