using System;

namespace Oxide.Ext.Discord.Logging
{
    /// <summary>
    /// Represents a discord extension logger
    /// </summary>
    public class DiscordLogger : ILogger
    {
        private DiscordLogLevel _logLevel;
        private readonly IDiscordLoggingConfig _config;
        private readonly DiscordLogHandler _handler;

        /// <summary>
        /// Creates a new logger with the given log level
        /// </summary>
        /// <param name="logLevel">Log level of the logger</param>
        /// <param name="config">Configuration for the logger</param>
        /// <param name="handler">Handler for the logger</param>
        internal DiscordLogger(DiscordLogLevel logLevel, IDiscordLoggingConfig config, DiscordLogHandler handler)
        {
            _logLevel = logLevel;
            _config = config;
            _handler = handler;
        }

        /// <inheritdoc/>
        public void Log(DiscordLogLevel level, string message, Exception exception = null)
        {
            if (IsConsoleLogging(level))
            {
                _handler.LogConsole(level, message, exception);
            }

            if (IsFileLogging(level))
            {
                _handler.LogFile(level, message, exception);
            }
        }

        /// <inheritdoc/>
        public void UpdateLogLevel(DiscordLogLevel level)
        {
            _logLevel = level;
        }

        /// <inheritdoc/>
        public bool IsLogging(DiscordLogLevel level)
        {
            return level >= _logLevel && (level >= _config.ConsoleLogLevel || level >= _config.FileLogLevel);
        }
        
        /// <inheritdoc/>
        public bool IsConsoleLogging(DiscordLogLevel level)
        {
            return level >= _logLevel && level >= _config.ConsoleLogLevel;
        }
        
        /// <inheritdoc/>
        public bool IsFileLogging(DiscordLogLevel level)
        {
            return level >= _logLevel && level >= _config.FileLogLevel;
        }
        
        /// <inheritdoc/>
        public void Shutdown()
        {
            _handler.Shutdown();
        }
    }
}