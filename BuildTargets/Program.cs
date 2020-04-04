// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Palmmedia.ReportGenerator.Core;
using static Bullseye.Targets;
using static SimpleExec.Command;

namespace BuildTargets
{
    internal class Program
    {
        private const string ArtifactsDir = "build-artifacts";
        private static readonly string PackageDir = Path.Combine(ArtifactsDir, "packages");
        private static readonly string TestLogDir = Path.Combine(ArtifactsDir, "test-logs");

        private const string Compile = nameof(Compile);
        private const string Default = nameof(Default);
        private const string Pack = nameof(Pack);
        private const string Test = nameof(Test);


        private static void Main(string[] args)
        {
            CleanDir($"./{ArtifactsDir}");
            Target(Compile, () => Run("dotnet", "build -c release"));
            Target(Test, () =>
            {
                var testProjects = new DirectoryInfo("./test").GetDirectories().SelectMany(d => d.GetFiles("*.csproj"))
                        .Take(1)
                    // .Where(f => true || f.Name.Contains("Client.Tests"))
                    ;
                foreach (var testProject in testProjects)
                {
                    Console.WriteLine(testProject);

                    //Run("dotnet", $"test {testProject} -c release --no-build --logger trx --results-directory {TestLogDir} --collect:\"XPlat Code Coverage\" --settings:./BuildTargets/coverlet.runsettings.xml");
                }

                Run("dotnet",
                    $"test  -c release --no-build --logger trx --results-directory {TestLogDir} --collect:\"XPlat Code Coverage\" --settings:./BuildTargets/coverlet.runsettings.xml");
                GenerateCodeCoverageReport();

                //Run("dotnet", $"test -c release --no-build --logger trx --results-directory {TestLogDir} /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:ExcludeByAttribute=\"Obsolete, GeneratedCodeAttribute, CompilerGeneratedAttribute\"");
            });
            Target(Pack, DependsOn(Compile), () => Run("dotnet", $"pack -c Release -o ./{PackageDir} --no-build"));
            Target(Default, DependsOn(Compile, Test, Pack));
            RunTargetsAndExit(args);
        }

        private static void CleanDir(string path)
        {
            Directory.CreateDirectory(path).Delete(true);
            Directory.CreateDirectory(path);
        }

        private static void GenerateCodeCoverageReport()
        {
            new Generator().GenerateReport(new ReportConfiguration(
                new List<string> {$"./{TestLogDir}/**/*coverage.cobertura.xml"},
                $"./{ArtifactsDir}/coverage-report/", new List<string>(), null, new List<string>
                {
                    "Cobertura"
                },
                new List<string>(), new List<string>(), new List<string>(), new List<string>(), null,
                null));
        }
    }
}