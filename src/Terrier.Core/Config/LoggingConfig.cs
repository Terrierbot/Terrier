﻿namespace Terrier.Config
{
    public class LoggingConfig
    {
        public bool UseColorOutput { get; } = true;
        public bool UseRelativeOutput { get; } = true;
        public int MaxFileSizeKb { get; } = 5000;
        public string OutputDirectory { get; } = "logs";
        public string DateTimeFormat { get; } = "yyyy-MM-dd";
    }
}
