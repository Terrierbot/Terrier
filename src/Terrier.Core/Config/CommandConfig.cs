using Discord;
using Discord.Commands;

namespace Terrier.Config
{
    public class CommandConfig
    {
        public string Prefix { get; } = "";
        public LogSeverity LogLevel { get; } = LogSeverity.Info;
        public RunMode DefaultRunMode { get; } = RunMode.Async;
        public bool AllowMentionPrefix { get; } = true;
        public bool CaseSensitiveCommands { get; } = false;
        public bool IgnoreExtraArgs { get; } = false;
    }
}
