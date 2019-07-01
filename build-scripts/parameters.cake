#load "./paths.cake"

public class BuildParameters
{
    public string Configuration { get; private set; }
    public bool IsLocalBuild { get; private set; }
    public bool IsRunningOnUnix { get; private set; }
    public bool IsRunningOnWindows { get; private set; }
    public bool IsRunningOnVSTS { get; private set; }
    public bool IsPullRequest => PackageVersion.Contains("pullrequest");
    public bool IsMainGraphZenRepo { get; private set; }
    public bool IsMasterBranch  => GitVersion.BranchName == "master";
    public string NugetApiKey {get; private set;}
    public string NugetSource  {get; private set;}
    public BuildPaths Paths { get; private set; }
    public GitVersion GitVersion { get; private set; }
    public string PackageVersion => GitVersion.NuGetVersionV2;

    public void Initialize(ICakeContext context, GitVersion version)
    {
        GitVersion = version;
        NugetApiKey = context.Argument("nuget-api-key", context.EnvironmentVariable("GRAPHZEN_NUGET_API_KEY"));
        NugetSource = context.Argument("nuget-source", context.EnvironmentVariable("GRAPHZEN_NUGET_SOURCE"));
    }

    public static BuildParameters GetParameters(ICakeContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        var target = context.Argument("target", "Default");
        var buildSystem = context.BuildSystem();

        return new BuildParameters {
            Configuration = context.Argument("configuration", "Release"),
            IsLocalBuild = buildSystem.IsLocalBuild,
            IsRunningOnUnix = context.IsRunningOnUnix(),
            IsRunningOnWindows = context.IsRunningOnWindows(),
            IsRunningOnVSTS = buildSystem.TFBuild.IsRunningOnVSTS,
            IsMainGraphZenRepo = StringComparer.OrdinalIgnoreCase.Equals("GraphZen", buildSystem.TFBuild.Environment.Repository.RepoName),
            Paths = BuildPaths.GetPaths(context)
        };
    }
}