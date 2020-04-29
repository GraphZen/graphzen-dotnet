// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using LibGit2Sharp;
using Palmmedia.ReportGenerator.Core;
using static Bullseye.Targets;
using static SimpleExec.Command;

namespace BuildTargets
{
    internal static class Program
    {
        // Paths
        private const string ArtifactsDir = "build-artifacts";


        // Targets
        private const string Compile = nameof(Compile);
        private const string Quick = nameof(Quick);
        private const string Pack = nameof(Pack);
        private const string Push = nameof(Push);
        private const string Default = nameof(Default);
        private const string Test = nameof(Test);
        private const string TestQuick = nameof(TestQuick);
        private const string Specs = nameof(Specs);
        private const string CoverageReport = nameof(CoverageReport);
        private const string CoverageReportHtml = nameof(CoverageReportHtml);
        private const string Gen = nameof(Gen);
        private const string GenQuick = nameof(GenQuick);
        private const string Restore = nameof(Restore);
        private static readonly string TestArtifactsDir = Path.Combine(ArtifactsDir, "test");
        private static readonly string TestLogDir = Path.Combine(TestArtifactsDir, "logs");
        private static readonly string TestReportsDir = Path.Combine(TestArtifactsDir, "coverage-reports");
        private static readonly string PackageDir = Path.Combine(ArtifactsDir, "packages");

        private static string OutputDir { get; } =
            Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)!;

        private static string GetReSharperTool(string name) => Path.Combine(OutputDir, "ReSharperTools", name);

        private static void Main(string[] args)
        {
            Target(Restore, () =>
            {
                Run("dotnet", "tool restore");
                Run("dotnet", "restore");
            });

            Target(Compile, () => Run("dotnet", "build -c Release --no-restore"));

            Target(nameof(BuildCli), BuildCli);

            Target(Gen, DependsOn(nameof(BuildCli)), () => RunCodeGen());

            Target(GenQuick, DependsOn(nameof(BuildCli)), () => RunCodeGen(false));

            Target(Test, () =>
            {
                CleanDir(TestLogDir);
                Run("dotnet",
                    $"test  -c release --no-build --logger trx --results-directory {TestLogDir} --collect:\"XPlat Code Coverage\" --settings:./BuildTargets/coverlet.runsettings.xml");
            });

            Target(Specs, DependsOn(nameof(BuildCli)), () => RunCli("specs"));

            Target(TestQuick, () =>
            {
                CleanDir(TestLogDir);
                Run("dotnet", "test  -c release --no-build");
            });

            Target(CoverageReport, () => GenerateCodeCoverageReport());

            Target(CoverageReportHtml, () => GenerateCodeCoverageReport(true));

            Target(Pack, () =>
            {
                CleanDir(PackageDir);
                Run("dotnet", $"pack -c Release -o ./{PackageDir} --no-build");
            });

            Target(Push, () =>
            {
                var packages = Directory.GetFiles(PackageDir, "*.nupkg");
                foreach (var package in packages)
                {
                    Run("dotnet", $"nuget push  {package} -s GraphZen-Public --api-key Local");
                }
            });

            Target(nameof(CleanupCode), () => CleanupCode());

            Target(nameof(DotNetFormat), DotNetFormat);

            Target(nameof(DotNetFormatCheck), DotNetFormatCheck);

            Target(Default, DependsOn(Restore, nameof(DotNetFormatCheck), Compile, Test, Pack, CoverageReportHtml));

            Target(Quick, DependsOn(Restore, Compile, TestQuick, Pack));

            RunTargetsAndExit(args);
        }

        private static void CleanupCode(params string[] includes) => CleanupCode(includes.AsEnumerable());


        private static void CleanupCode(IEnumerable<string> includes)
        {
            var incls = includes.ToImmutableList();
            var include = incls.Any() ? string.Join(";", incls) : null;
            Run(GetReSharperTool("cleanupcode"),
                $@"--config=./BuildTargets/cleanupcode.config  {(include != null ? $"--include=\"{include}\"" : "")}");
            DotNetFormat();
        }

        private static void DotNetFormat() => Run("dotnet", "dotnet-format");
        private static void DotNetFormatCheck() => Run("dotnet", "dotnet-format --check");

        private static void RunCodeGen(bool format = true)
        {
            RunCli("gen");
            if (format)
            {
                using var repo = new Repository("./");
                var modified = repo.RetrieveStatus()
                    .Where(_ => _.State == FileStatus.ModifiedInWorkdir || _.State == FileStatus.NewInWorkdir)
                    .Where(_ => _.FilePath.EndsWith("Generated.cs")).Select(_ => "./" + _.FilePath).ToImmutableList();
                if (modified.Any())
                {
                    var includes = string.Join(";", modified);

                    CleanupCode(includes);
                }
            }
        }
        private static void BuildCli()
        {
            Run("dotnet", $"build -c Release --no-restore ./src/GraphZen.DevCli/GraphZen.DevCli.csproj ");
        }

        private static void RunCli(string task) => Run("dotnet",
            $"run -c Release  --no-build --no-restore --project ./src/GraphZen.DevCli/GraphZen.DevCli.csproj -- {task}");

        private static void CleanDir(string path)
        {
            Directory.CreateDirectory(path).Delete(true);
            Directory.CreateDirectory(path);
        }

        private static void GenerateCodeCoverageReport(bool html = false)
        {
            CleanDir(TestReportsDir);
            var reportTypes = new List<string>
            {
                "Cobertura"
            };
            if (html)
            {
                reportTypes.Add("HtmlInline");
            }

            new Generator().GenerateReport(new ReportConfiguration(
                new List<string> { $"./{TestLogDir}/**/*coverage.cobertura.xml" },
                TestReportsDir, new List<string>(), null,
                reportTypes,
                new List<string>(), new List<string>(), new List<string>(), new List<string>(), null,
                null));
        }
    }
}