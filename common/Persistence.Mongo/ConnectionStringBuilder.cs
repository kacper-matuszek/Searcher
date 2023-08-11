using System.Collections.Generic;
using System.Linq;

namespace Searcher.Common.Persistence.Mongo;

internal static class ConnectionStringBuilder
{
    private const string MongoDb = "mongodb";
    private const string DefaultAuthDb = "admin";

    private static readonly IReadOnlyDictionary<string, string> InvalidCharactersEncoding = new Dictionary<string, string>()
    {
        {":", "%3A" },
        {"/", "%2F" },
        {"?", "%3F" },
        {"#", "%23" },
        {"[", "%5B" },
        {"]", "%5D" },
        {"@", "%40" },
    };

    public static string Build(MongoOptions options)
        => $"{MongoDb}://{ReplaceInvalidCharacters(options.User)}:{ReplaceInvalidCharacters(options.Password)}" +
           $"@{options.Host}:{options.Port}/{options.DatabaseName}?authSource={DefaultAuthDb}";

    private static string ReplaceInvalidCharacters(string content)
        => InvalidCharactersEncoding.Aggregate(
            seed: content,
            func: (current, keyValuePair) => current.Replace(keyValuePair.Key, keyValuePair.Value));
}
