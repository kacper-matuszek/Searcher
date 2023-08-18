
#load "mainArguments.cake"

var testDirectory = Directory($"{mainDirectory}\\{mainTestDirectoryName}");
var testArtifactsDirectory = Directory($"{mainDirectory}\\test-artifacts");
var testFiles = GetFiles($"{testDirectory}\\**\\*.csproj");

void CleanTestDirectory()
{
    CleanDirectory(testArtifactsDirectory);
}

Task("CleanTest")
    .Does(CleanTestDirectory);

Task("BuildTest")
    .IsDependentOn("CleanTest")
    .Does(() => 
    {
        var settings = new DotNetPublishSettings
        {
            Configuration = configuration,
            NoRestore = true,
            OutputDirectory = testArtifactsDirectory                    
        };

        foreach(var file in testFiles)        
        {
            DotNetPublish(file.FullPath, settings);
        }
    });

Task("Test")
    .IsDependentOn("BuildTest")
    .Does(() => 
    {
        var settings = new DotNetTestSettings 
        {
            Configuration = configuration,
            NoRestore = true,
            NoBuild = true,
            OutputDirectory = testArtifactsDirectory
        };

        foreach(var file in testFiles)
        {
            DotNetTest(file.FullPath, settings);
        }
    });

Task("RunTest")
    .IsDependentOn("Test")
    .Does(CleanTestDirectory);