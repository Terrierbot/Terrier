namespace Terrier.Config
{
    public class AppConfig
    {
        public DiscordConfig Discord { get; } = new DiscordConfig();
        public LoggingConfig Logging { get; } = new LoggingConfig();
    }
}
