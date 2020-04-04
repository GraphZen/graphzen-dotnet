// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.IO;
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
            Target(Test, () => Run("dotnet", $"test -c release --no-build --logger trx --results-directory {TestLogDir} /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura"));
            Target(Pack, DependsOn(Compile), () => Run("dotnet", $"pack -c Release -o ./{PackageDir} --no-build"));
            Target(Default, DependsOn(Compile, Test, Pack));
            RunTargetsAndExit(args);
        }

        private static void CleanDir(string path)
        {
            Directory.CreateDirectory(path).Delete(true);
            Directory.CreateDirectory(path);
        }
    }
}