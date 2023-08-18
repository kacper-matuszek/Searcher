#tool "dotnet:?package=GitVersion.Tool&version=5.10.3"
#addin nuget:?package=Cake.FileHelpers&version=5.0.0

#load "mainArguments.cake"

var target = Argument("target", "Default");

Task("Clean")
    .Does(() => CleanDirectory(buildArtifactsDirectory));

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
            OutputDirectory = buildArtifactsDirectory                    
        });
    });

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);
