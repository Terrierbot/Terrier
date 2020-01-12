using Discord.WebSocket;
using Finite.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Terrier.Commands;

namespace Terrier.Services
{
    internal class InternalCommandHandlingService : ITerrierService
    {
        private readonly ILogger<InternalCommandHandlingService> _logger;
        private readonly CommandService<DiscordCommandContext> _commandService;
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly IServiceProvider _serviceProvider;

        public InternalCommandHandlingService(
            ILogger<InternalCommandHandlingService> logger,
            CommandService<DiscordCommandContext> commandService,
            DiscordSocketClient discordSocketClient,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _commandService = commandService;
            _discordSocketClient = discordSocketClient;
            _serviceProvider = serviceProvider;
        }

        public void Start()
        {
            _discordSocketClient.MessageReceived += OnMessageReceivedAsync;
            _logger.LogInformation("Started");
        }

        public void Stop()
        {
            _discordSocketClient.MessageReceived -= OnMessageReceivedAsync;
            _logger.LogInformation("Stopped");
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            if (!(s is SocketUserMessage msg)) return;
            if (!(s.Channel is SocketGuildChannel)) return;

            var context = new DiscordCommandContext(_discordSocketClient, msg);

            var result = await _commandService.ExecuteAsync(context, _serviceProvider);
        }
    }
}
