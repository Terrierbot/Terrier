using Discord;

namespace Terrier.Config
{
    public class DiscordConfig
    {
        public string Token { get; } = "";
        public LogSeverity LogLevel { get; } = LogSeverity.Info;
        public int MessageCacheSize { get; } = 1000;
        public bool AlwaysDownloadUsers { get; } = true;
        public bool AllowGuildSubscriptions { get; } = false;
    }
}
