public class BuildPaths
{
    public BuildDirectories Directories { get; private set; }

    public static BuildPaths GetPaths(
        ICakeContext context
        )
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }


        var rootDir = context.MakeAbsolute(context.Directory("./"));
        var solutionDir = rootDir;
        var artifactsDir = rootDir.Combine("build-artifacts");
        var testArtifacts = context.EnvironmentVariable("TEST_RESULTS_DIR") ?? rootDir.Combine("test-artifacts");

        // Directories
        var buildDirectories = new BuildDirectories(
            artifactsDir,
            rootDir,
            solutionDir, testArtifacts);

        return new BuildPaths
        {
            Directories = buildDirectories
        };
    }
}

public class BuildFiles
{
    public FilePath VersionProperties { get; private set; }

    public BuildFiles(
        FilePath versionProperties
        )
    {
        VersionProperties = versionProperties;
    }
}

public class BuildDirectories
{
    public DirectoryPath BuildArtifacts { get; }
    public DirectoryPath TestArtifacts { get; }
    public DirectoryPath Root { get; }
    public DirectoryPath Solution { get; }
    public ICollection<DirectoryPath> ToClean { get; }

    public BuildDirectories(
        DirectoryPath artifactsDir,
        DirectoryPath rootDir,
        DirectoryPath solutionDir,
        DirectoryPath testArtifactsDir
        )
    {
        TestArtifacts= testArtifactsDir;
        BuildArtifacts = artifactsDir;
        Root = rootDir;
        Solution = solutionDir;
        ToClean = new[] {
            BuildArtifacts,
            TestArtifacts
        };
    }
}