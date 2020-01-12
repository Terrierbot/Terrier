using Discord.Addons.Finite.Commands;
using Discord.WebSocket;
using Finite.Commands;
using Finite.Commands.Extensions;
using McMaster.NETCore.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Terrier.Commands;
using Terrier.Config;
using Terrier.Services;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Terrier
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public PluginManager PluginManager { get; }

        public Startup(string[] args)
        {
            TryGenerateConfiguration();
            var builder = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddYamlFile(TerrierConstants.ConfigFileName);
            Configuration = builder.Build();
            PluginManager = new PluginManager();
        }

        public static bool TryGenerateConfiguration()
        {
            if (File.Exists(TerrierConstants.ConfigFilePath)) return false;

            var serializer = new SerializerBuilder()
                .WithNamingConvention(new UnderscoredNamingConvention())
                .Build();

            var yaml = serializer.Serialize(new AppConfig());
            File.WriteAllText(TerrierConstants.ConfigFilePath, yaml);
            return true;
        }

        public async Task RunAsync()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<StartupService>().Start();
            provider.GetRequiredService<InternalLoggingService>().Start();
            provider.GetRequiredService<InternalCommandHandlingService>().Start();

            await Task.Delay(-1);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var config = new AppConfig();
            Configuration.Bind(config);

            services
            .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                MessageCacheSize = config.Discord.MessageCacheSize,
                LogLevel = config.Discord.LogLevel,
                AlwaysDownloadUsers = config.Discord.AlwaysDownloadUsers,
                GuildSubscriptions = config.Discord.AllowGuildSubscriptions
            }))
            .AddSingleton(Configuration)
            .AddSingleton<StartupService>()
            .AddSingleton<InternalLoggingService>()
            .AddSingleton<InternalCommandHandlingService>()
            .AddLogging();

            var builder = new CommandServiceBuilder<DiscordCommandContext>()
                .AddCommandParser<DefaultCommandParser<DiscordCommandContext>>()
                .AddTypeReaderFactory<NullTypeReaderFactory>();
            PluginManager.LoadPlugins(services, builder);

            services.AddSingleton(PluginManager);
            services.AddSingleton(builder.BuildCommandService());
        }
    }
}
