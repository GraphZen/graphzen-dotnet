﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Palmmedia.ReportGenerator.Core;
using static Bullseye.Targets;
using static SimpleExec.Command;

namespace BuildTargets
{
    internal class Program
    {
        // Paths
        private const string ArtifactsDir = "build-artifacts";

        private static string OutputDir { get; } =
            Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)!;

        private static readonly string PackageDir = Path.Combine(ArtifactsDir, "packages");
        private static readonly string TestLogDir = Path.Combine(ArtifactsDir, "test-logs");
        private static string GetReSharperTool(string name) => Path.Combine(OutputDir, "ReSharperTools", name);

        // Targets
        private const string Compile = nameof(Compile);
        private const string Default = nameof(Default);
        private const string CleanupCode = nameof(CleanupCode);
        private const string Format = nameof(Format);
        private const string Pack = nameof(Pack);
        private const string Test = nameof(Test);
        private const string HtmlReport = nameof(HtmlReport);

        private static void Main(string[] args)
        {
            CleanDir($"./{ArtifactsDir}");
            Target(Compile, () => Run("dotnet", "build -c Release"));
            Target(Test, () =>
            {
                Run("dotnet",
                    $"test  -c release --no-build --logger trx --results-directory {TestLogDir} --collect:\"XPlat Code Coverage\" --settings:./BuildTargets/coverlet.runsettings.xml");
                GenerateCodeCoverageReport();
            });
            Target(HtmlReport, DependsOn(Test), () => GenerateCodeCoverageReport(true));
            Target(Pack, DependsOn(Compile), () => Run("dotnet", $"pack -c Release -o ./{PackageDir} --no-build"));
            Target(CleanupCode,
                () => { Run(GetReSharperTool("cleanupcode"), @"--config=./BuildTargets/cleanupcode.config"); });
            Target(Format, () => { Run("dotent-format"); });
            Target(Default, DependsOn(Compile, Test, Pack));
            RunTargetsAndExit(args);
        }


        private static void CleanDir(string path)
        {
            Directory.CreateDirectory(path).Delete(true);
            Directory.CreateDirectory(path);
        }

        private static void GenerateCodeCoverageReport(bool html = false)
        {
            var reportTypes = new List<string>
            {
                "Cobertura"
            };
            if (html) reportTypes.Add("HtmlInline");
            new Generator().GenerateReport(new ReportConfiguration(
                new List<string> {$"./{TestLogDir}/**/*coverage.cobertura.xml"},
                $"./{ArtifactsDir}/coverage-reports/", new List<string>(), null,
                reportTypes,
                new List<string>(), new List<string>(), new List<string>(), new List<string>(), null,
                null));
        }
    }
}