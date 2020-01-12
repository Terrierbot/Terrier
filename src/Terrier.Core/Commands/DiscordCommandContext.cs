using Discord;
using Discord.WebSocket;
using Finite.Commands;

namespace Terrier.Commands
{
    public class DiscordCommandContext : ICommandContext
    {
        public DiscordSocketClient Client { get; }
        public SocketMessage Message { get; }
        public ISocketMessageChannel Channel { get; }
        public SocketUser Author { get; }
        public SocketGuild Guild { get; }

        public bool IsPrivate => Channel is IPrivateChannel;

        public DiscordCommandContext(DiscordSocketClient client,
            SocketMessage message)
        {
            Client = client;
            Message = message;
            Channel = message.Channel;
            Author = message.Author;
            Guild = (Channel as SocketGuildChannel)?.Guild;
        }

        string ICommandContext.Message => Message.ToString();
        string ICommandContext.Author => Author.ToString();
    }
}