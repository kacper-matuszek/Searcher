const string SearcherName = "Searcher";
const string mainSourceDirectoryName = "src";
const string mainTestDirectoryName = "test";

var mainDirectory = Directory("..\\");
var solutionFile = File($"{mainDirectory}\\{SearcherName}.sln");
var buildArtifactsDirectory = Directory($"{mainDirectory}\\build-artifacts");
var webHostFile = File($"{mainDirectory}\\{mainSourceDirectoryName}\\WebHost\\{SearcherName}.WebHost.csproj");
