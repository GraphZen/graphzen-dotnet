
#load "./build-scripts/parameters.cake"
// Modules 
#module nuget:?package=Cake.DotNetTool.Module&version=0.4.0
#tool "nuget:?package=ReportGenerator&version=4.3.6"
#tool "nuget:?package=GitVersion.CommandLine&version=5.1.2"
#tool "nuget:?package=xunit.runner.console&version=2.4.1"
#tool "nuget:?package=JetBrains.ReSharper.CommandLineTools&version=2019.2.3"
#tool "nuget:?package=docfx.console&version=2.47.0"
#addin "nuget:?package=Cake.Coverlet&version=2.3.4"
#addin "Cake.Powershell&version=0.4.8"
#addin "nuget:?package=Cake.Git&version=0.21.0"
#addin "nuget:?package=Cake.Incubator&version=5.1.0"
#tool dotnet:?package=dotnet-format&version=3.1.37601
using System.Diagnostics;


var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

void Netlify(string directory, string siteId) {
 var authToken = Argument<string>("netlify-api-key");
 Information($"Deploying netlify site '{siteId}' from directory '{directory}'");
 var process = ($"netlify deploy --dir {directory} --site {siteId} --prod --auth {authToken}");
 Information(process);
 var exitCode= StartProcess(process);
  if (exitCode != 0) {
    throw new Exception($"DocFx failed with exit code {exitCode}");
  }
}

FilePath GetFile(string pattern) {
  var files =GetFiles(pattern);
  if (files.Count() == 1) {
    return files.Single();
  }
  if (files.Count() > 1) {
    var filePaths = string.Join(",\n", files.Select(_ => _.FullPath));
    throw new Exception($"found more than one file matching pattern \"{pattern}\": \n{filePaths}");
  } else {
    throw new Exception($"Could not find file matching pattern \"{pattern}\"");
  }
}

var paths = new {
  sln = "GraphZen.sln",
  coreProject= "src/GraphZen/GraphZen.csproj",
  testProject= "test/GraphZen.Tests/GraphZen.Tests.csproj",
  inspectCode = GetFile("tools/JetBrains.ReSharper.CommandLineTools*/tools/inspectcode.exe"),
  dupFinder= GetFile("tools/JetBrains.ReSharper.CommandLineTools*/tools/dupfinder.exe"),
  cleanupCode= GetFile("tools/JetBrains.ReSharper.CommandLineTools*/tools/cleanupcode.exe"),
  docFx= GetFile("tools/docfx.console*/tools/docfx.exe"),
};

Setup<BuildParameters>(context =>
{
   var version = GitVersion(new GitVersionSettings {
     NoFetch = true,
    });
    context.Tools.RegisterFile(paths.cleanupCode);
    context.Tools.RegisterFile(paths.inspectCode);
    context.Tools.RegisterFile(paths.dupFinder);

    var buildParams = BuildParameters.GetParameters(context);
    buildParams.Initialize(context, version);
    return buildParams;
});
Task("Format-Check").Does(() => {
    var dotnetFormatExe = Context.Tools.Resolve("dotnet-format.exe");
    var exitCode= StartProcess(dotnetFormatExe, new ProcessSettings {
        Arguments = new ProcessArgumentBuilder()
            .Append("--dry-run")
            .Append("--check")
        });
        if (exitCode != 0) {
            throw new Exception("Solution files not formatted correctly");
        }
});

Task("Format").Does(() => {
    var dotnetFormatExe = Context.Tools.Resolve("dotnet-format.exe");
    StartProcess(dotnetFormatExe);
});


Task("Get-Version").Does<BuildParameters>(data => {
  Information("Package Version: " + data.PackageVersion);
});
Task("DocFx")
.Does(() => {
  var exitCode= StartProcess(paths.docFx, new ProcessSettings {
    Arguments = new ProcessArgumentBuilder().Append("./api-website/docfx.json")
  });
  if (exitCode != 0) {
    throw new Exception($"DocFx failed with exit code {exitCode}");
  }
});

Task("Api-Website-Clean").Does(() => CleanDirectory("./api-website-artifacts"));
Task("Api-Website")
    .IsDependentOn("Api-Website-Clean")
    .IsDependentOn("Compile")
    .IsDependentOn("DocFx");

Task("Publish-Api-Website").Does(() => {
  Netlify("./api-website-artifacts/dist", "05a8d28d-39e4-46f1-ba94-e6fcf9b11250");
});
    




Setup(context => {
    });

Task("Clean-Packages")
.Does<BuildParameters>(data => {
    CleanDirectories(data.Paths.Directories.ToClean);
});
Task("Clean")
.IsDependentOn("Clean-Packages")
    .Does<BuildParameters>(data =>
{
    CleanDirectories($"./src/**/obj/{data.Configuration}");
    CleanDirectories($"./src/**/bin/{data.Configuration}");
    CleanDirectories($"./test/**/obj/{data.Configuration}");
    CleanDirectories($"./test/**/bin/{data.Configuration}");
    CleanDirectories($"./*artifacts");
    CleanDirectories(data.Paths.Directories.ToClean);
});

Task("Clean-All")
    .Does<BuildParameters>(data =>
{
    CleanDirectories($"./src/**/obj");
    CleanDirectories($"./src/**/bin");
    CleanDirectories($"./test/**/obj");
    CleanDirectories($"./test/**/bin");
    CleanDirectories(data.Paths.Directories.ToClean);
});

void ResharperCleanupCode(string profile) {
  var exitCode= StartProcess(paths.cleanupCode, new ProcessSettings {
    Arguments = new ProcessArgumentBuilder().Append(paths.sln).Append($"--profile=\"{profile}\"")
  });
  if (exitCode != 0) {
    throw new Exception($"Cleanup code failed with exit code {exitCode}");
  }
}

Task("Cleanup").IsDependentOn("Cleanup-Full").IsDependentOn("Format");

Task("Cleanup-Full")
.IsDependentOn("Compile")
.Does(() => ResharperCleanupCode("GraphZen: Full Cleanup"));

Task("Restore")
.IsDependentOn("Clean")
.Does(() => {
  DotNetCoreRestore(paths.sln);
});


Task("Run-Gen")
.Does(() => {
   var settings = new DotNetCoreRunSettings
     {
     };

     DotNetCoreRun("./src/GraphZen.DevCli", "gen", settings);
});


Task("Gen")
.IsDependentOn("Run-Gen")
.IsDependentOn("Format");

Task("Compile")
.IsDependentOn("Restore")
.IsDependentOn("Clean")
.Does<BuildParameters>(data => {

   var settings = new DotNetCoreBuildSettings{
        NoRestore = true,
        // Verbosity = DotNetCoreVerbosity.Normal,
        Configuration = data.Configuration,
        MSBuildSettings = new DotNetCoreMSBuildSettings()
        .WithProperty("Version", data.PackageVersion)
        .SetMaxCpuCount(Environment.ProcessorCount)
    };

    DotNetCoreBuild(data.Paths.Directories.Solution.FullPath, settings);
});

Task("Test")
.IsDependentOn("Compile")
.Does<BuildParameters>(buildParams => {
    var testArtifactsDir = buildParams.Paths.Directories.TestArtifacts.FullPath;
    var coverageJson = testArtifactsDir + "/coverage.json";
    var settings = new DotNetCoreTestSettings
    {
        NoRestore = true,
        NoBuild = true,
        Configuration = buildParams.Configuration,
        DiagnosticOutput = true,
        ArgumentCustomization = args => args
        .Append($"--logger trx --results-directory {testArtifactsDir}")
    };
    var coverletSettings = new CoverletSettings
    {
        CollectCoverage = true,
        CoverletOutputDirectory = testArtifactsDir,
        CoverletOutputName =  "coverage",
        Exclude = new List<string> {
          "[*]JetBrains.Annotations.*"
        },
    };

    var testProjects = GetFiles("./test/**/*Tests.csproj").ToList();
    for (int i = 0; i < testProjects.Count; i++)
    {
        var isFirst = i == 0;
        var isLast = i == testProjects.Count - 1;

        if (isLast) {
            coverletSettings.CoverletOutputFormat  = CoverletOutputFormat.cobertura;
        } else if (!isFirst) {
          coverletSettings.MergeWithFile = coverageJson;
        }

        var testProjectFile = testProjects[i];
        DotNetCoreTest(testProjectFile, settings, coverletSettings);
    }
});

Task("Test-Old")
.IsDependentOn("Compile")
.Does<BuildParameters>(buildParams =>
{
    var testArtifactsDir = buildParams.Paths.Directories.TestArtifacts.FullPath;
    var settings = new DotNetCoreTestSettings
    {
        NoRestore = true,
        NoBuild = true,
        Configuration = buildParams.Configuration,
        DiagnosticOutput = true,
        ArgumentCustomization = args => args
        .Append($"--logger trx --results-directory {testArtifactsDir}")
    };
    var coverletSettings = new CoverletSettings
    {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.cobertura,
        CoverletOutputDirectory = testArtifactsDir,
        CoverletOutputName = $"coverage",
        
    };

    DotNetCoreTest(buildParams.Paths.Directories.Solution.FullPath, settings, coverletSettings);
});

 Task("Test-Coverage-Report")
 .IsDependentOn("Test")
.Does<BuildParameters>(buildParams =>
{
    var testArtifactsDir = buildParams.Paths.Directories.TestArtifacts.FullPath;
    var reportSettings = new ReportGeneratorSettings {
      AssemblyFilters = new [] { "+GraphZen*",  },
      ReportTypes = new [] {
        ReportGeneratorReportType.Badges,
        ReportGeneratorReportType.HtmlChart,
        ReportGeneratorReportType.HtmlInline_AzurePipelines,
        ReportGeneratorReportType.Cobertura,
      }
    };
    ReportGenerator($"{testArtifactsDir}/*cobertura.xml", $"{testArtifactsDir}/coverage-reports", reportSettings); 
 });


Task("Pack")
.IsDependentOn("Clean-Packages")
.IsDependentOn("Compile")
.Does<BuildParameters>(_ => {
   var settings = new DotNetCorePackSettings{
        NoRestore = true,
        NoBuild = true,
        IncludeSource = false,
        IncludeSymbols = true,
        OutputDirectory = _.Paths.Directories.BuildArtifacts,
        Configuration = _.Configuration,
        MSBuildSettings = new DotNetCoreMSBuildSettings()
          .WithProperty("PackageVersion", _.PackageVersion)
    };
    DotNetCorePack(_.Paths.Directories.Solution.FullPath, settings);
});

void Push(BuildParameters buildParams, string source) {
  var settings = new DotNetCoreNuGetPushSettings {
    ApiKey = "apikey",
    Source = source,
    ArgumentCustomization = args => args.Append("--interactive")
  };
  var packages = GetFiles($"{buildParams.Paths.Directories.BuildArtifacts}/*");
  foreach (var package in packages) {
    Information($"Publishing package {package}");
    try {
      DotNetCoreNuGetPush(package.ToString(), settings);
    } catch (Exception e) {
      if (e.Message.Contains("Conflict")) {
        Warning(e.Message);
      } else {
        throw;
      }
    }
  }
}

Task("Push-Azure")
.IsDependentOn("Pack")
.Does<BuildParameters>((buildParams) =>  {
  Push(buildParams, "GraphZen-Public");
});


Task("Default")
  .IsDependentOn("Build");
Task("Build")
.IsDependentOn("Clean")
.IsDependentOn("Restore")
.IsDependentOn("Compile")
.IsDependentOn("Test")
.IsDependentOn("Pack");

RunTarget(target);