using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Terrier.Config;
using Terrier.Logging;

namespace Terrier.Services
{
    internal class StartupService : ITerrierService
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;
        private readonly DiscordSocketClient _discordSocketClient;

        public StartupService(
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider,
            IConfiguration config,
            DiscordSocketClient discordSocketClient)
        {
            _loggerFactory = loggerFactory;
            _serviceProvider = serviceProvider;
            _config = config;
            _discordSocketClient = discordSocketClient;
        }

        public void Start()
            => StartAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        public void Stop()
            => throw new NotImplementedException();

        public async Task StartAsync()
        {
            var fileLoggerOptions = new LoggingConfig();
            _config.Bind("logging", fileLoggerOptions);
            _loggerFactory.AddProvider(new BotLoggerProvider(fileLoggerOptions));

            await _discordSocketClient.LoginAsync(Discord.TokenType.Bot, _config["discord:token"]);
            await _discordSocketClient.StartAsync();
        }
    }
}
