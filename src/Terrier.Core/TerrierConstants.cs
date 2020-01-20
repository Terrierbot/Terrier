using System;
using System.IO;
using System.Reflection;
using Terrier.Commands;
using Terrier.Config;
using Terrier.Logging;

namespace Terrier
{
    public static class TerrierConstants
    {
        public static string Version { get; } =
            typeof(TerrierConstants).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(TerrierConstants).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";

        public static string ConfigFileName = "_config.yml";
        public static string ConfigFilePath = Path.Combine(AppContext.BaseDirectory, ConfigFileName);
        public static string PluginsDirectory = Path.Combine(AppContext.BaseDirectory, "plugins");

        public static Type[] SharedTypes = new[]
        {
            typeof(DiscordModuleBase),
            typeof(AppConfig),
            typeof(CommandConfig),
            typeof(DiscordConfig),
            typeof(LoggingConfig),
            typeof(BotLogger),
            typeof(BotLoggerProvider),
            typeof(DiscordModuleBase),
            typeof(ITerrierService),
            typeof(TerrierConstants),
            typeof(TerrierPlugin)
        };
    }
}
