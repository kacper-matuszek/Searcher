using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Searcher.Common.Host.Extensions;

internal static class ConfigurationBuilderExtensions
{
    private const string ConfigurationFolderName = "Configurations";

    internal static IConfigurationBuilder AddAppSettingsConfiguration(
        this IConfigurationBuilder configurationBuilder,
        IHostEnvironment host)
    {
        configurationBuilder
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"{ConfigurationFolderName}/appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{ConfigurationFolderName}/appsettings.{host.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

        return configurationBuilder;
    }

    internal static IConfigurationBuilder AddLogsConfiguration(
        this IConfigurationBuilder configurationBuilder,
        IHostEnvironment host)
    {
        configurationBuilder
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"{ConfigurationFolderName}/log-config.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{ConfigurationFolderName}/log-config.{host.EnvironmentName}.json", optional: true);

        return configurationBuilder;
    }
}