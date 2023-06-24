#tool "dotnet:?package=GitVersion.Tool&version=5.10.3"
#addin nuget:?package=Cake.FileHelpers&version=5.0.0

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var buildArtifacts = Directory("..\\build-artifacts");
var solutionFile = File("..\\Searcher.sln");
var webHostFile = File("..\\src\\WebHost\\Searcher.WebHost.csproj");

Task("Clean")
    .Does(() => CleanDirectory(buildArtifacts));

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() => {
        DotNetRestore(solutionFile, new DotNetCoreRestoreSettings {
            NoCache = true
        });
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
        DotNetPublish(webHostFile, new DotNetPublishSettings{
            Configuration = configuration,
            NoRestore = true,
            OutputDirectory = buildArtifacts                    
        });
    });

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);
