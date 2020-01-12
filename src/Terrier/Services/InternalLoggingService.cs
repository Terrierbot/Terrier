using Discord;
using Discord.WebSocket;
using Finite.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Terrier.Commands;

namespace Terrier.Services
{
    internal class InternalLoggingService : ITerrierService
    {
        private readonly ILoggerFactory _factory;
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly CommandService<DiscordCommandContext> _commandService;

        public InternalLoggingService(
            ILoggerFactory factory,
            DiscordSocketClient discordSocketClient,
            CommandService<DiscordCommandContext> commandService)
        {
            _factory = factory;
            _commandService = commandService;
            _discordSocketClient = discordSocketClient;
        }

        public void Start()
        {
            _discordSocketClient.Log += OnLogAsync;
        }

        public void Stop()
            => throw new NotImplementedException();

        private Task OnLogAsync(LogMessage msg)
        {
            var logger = _factory.CreateLogger("Discord.Net");
            string message = msg.Exception?.ToString() ?? msg.Message;
            switch (msg.Severity)
            {
                case LogSeverity.Debug:
                    logger.LogDebug(message);
                    break;
                case LogSeverity.Warning:
                    logger.LogWarning(message);
                    break;
                case LogSeverity.Error:
                    logger.LogError(message);
                    break;
                case LogSeverity.Critical:
                    logger.LogCritical(message);
                    break;
                default:
                    logger.LogInformation(message);
                    break;
            }
            return Task.CompletedTask;
        }
    }
}
