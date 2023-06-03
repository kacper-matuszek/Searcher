namespace Searcher.Persistence.Mongo;

public class MongoOptions
{
    public const string SectionName = "mongo";

    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string DatabaseName { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
}