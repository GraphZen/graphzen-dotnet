// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Palmmedia.ReportGenerator.Core;
using static Bullseye.Targets;
using static SimpleExec.Command;

namespace BuildTargets
{
    internal static class Program
    {
        // Paths
        private const string ArtifactsDir = "build-artifacts";
        private static readonly string PackageDir = Path.Combine(ArtifactsDir, "packages");
        private static readonly string TestLogDir = Path.Combine(ArtifactsDir, "test-logs");
        private static string GetReSharperTool(string name) => Path.Combine(OutputDir, "ReSharperTools", name);

        private static string OutputDir { get; } =
            Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)!;


        // Targets
        private const string Compile = nameof(Compile);
        private const string Default = nameof(Default);
        private const string Pack = nameof(Pack);
        private const string Test = nameof(Test);
        private const string HtmlReport = nameof(HtmlReport);
        private const string Gen = nameof(Gen);
        private const string GenQuick = nameof(GenQuick);

        private static void Main(string[] args)
        {
            CleanDir($"./{ArtifactsDir}");

            Target(Compile, () => Run("dotnet", "build -c Release"));

            Target(Gen, () => RunCodeGen());

            Target(GenQuick, () => RunCodeGen(false));

            Target(Test, () =>
            {
                Run("dotnet",
                    $"test  -c release --no-build --logger trx --results-directory {TestLogDir} --collect:\"XPlat Code Coverage\" --settings:./BuildTargets/coverlet.runsettings.xml");
                GenerateCodeCoverageReport();
            });

            Target(HtmlReport, DependsOn(Test), () => GenerateCodeCoverageReport(true));

            Target(Pack, DependsOn(Compile), () => Run("dotnet", $"pack -c Release -o ./{PackageDir} --no-build"));

            Target(nameof(CleanupCode), () => CleanupCode());

            Target(nameof(DotNetFormat), DotNetFormat);

            Target(Default, DependsOn(Compile, Test, Pack));

            RunTargetsAndExit(args);
        }

        private static void CleanupCode(string? includes = null)
        {
            Run(GetReSharperTool("cleanupcode"),
                $@"--config=./BuildTargets/cleanupcode.config  {(includes != null ? $"--include=\"{includes}\"" : "")}");
            DotNetFormat();
        }

        private static void DotNetFormat() => Run("dotnet dotnet-format");

        private static void RunCodeGen(bool format = true)
        {
            DeleteFiles("**/*.Generated.cs");
            Run("dotnet", "run -c Release --project ./src/GraphZen.DevCli/GraphZen.DevCli.csproj -- gen");
            if (format)
            {
                CleanupCode("./**/*.Generated.cs");
            }
        }

        private static void CleanDir(string path)
        {
            Directory.CreateDirectory(path).Delete(true);
            Directory.CreateDirectory(path);
        }

        private static void DeleteFiles(string pattern)
        {
            foreach (var file in Directory.GetFiles("./", pattern, SearchOption.AllDirectories))
            {
                Console.WriteLine($"Deleting {file}");
                File.Delete(file);
            }
        }

        private static void GenerateCodeCoverageReport(bool html = false)
        {
            var reportTypes = new List<string>
            {
                "Cobertura"
            };
            if (html) reportTypes.Add("HtmlInline");
            new Generator().GenerateReport(new ReportConfiguration(
                new List<string> { $"./{TestLogDir}/**/*coverage.cobertura.xml" },
                $"./{ArtifactsDir}/coverage-reports/", new List<string>(), null,
                reportTypes,
                new List<string>(), new List<string>(), new List<string>(), new List<string>(), null,
                null));
        }
    }
}