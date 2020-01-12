using Microsoft.Extensions.Logging;
using Terrier.Config;

namespace Terrier.Logging
{
    public class BotLoggerProvider : ILoggerProvider
    {
        private readonly LoggingConfig _options;

        public BotLoggerProvider(LoggingConfig options)
        {
            _options = options;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new BotLogger(categoryName, _options);
        }

        public void Dispose()
        {
            return;
        }
    }
}
