namespace Terrier.Config
{
    public class AppConfig
    {
        public DiscordConfig Discord { get; } = new DiscordConfig();
        public CommandConfig Commands { get; } = new CommandConfig();
        public LoggingConfig Logging { get; } = new LoggingConfig();
    }
}
