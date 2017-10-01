using NetCraft.Plugin;
using System;

namespace NetCraft.Logging
{
    public class Logger
    {
        private string _pluginName;
        private LoggerManager _loggerManager;

        internal Logger(LoggerManager loggerManager, string pluginName)
        {
            _loggerManager = loggerManager;
            _pluginName = pluginName;
        }

        internal Logger(LoggerManager loggerManager, IPlugin plugin) : this(loggerManager, plugin.Name) { }

        public void Log(Severity severity, string message)
        {
            _loggerManager.Log(severity, $"{{{DateTime.Now}}} [{severity}] [{_pluginName}] {message}");
        }

        public void Trace(string message) => Log(Severity.TRACE, message);
        public void Debug(string message) => Log(Severity.DEBUG, message);
        public void Info(string message) => Log(Severity.INFO, message);
        public void Warning(string message) => Log(Severity.WARNING, message);
        public void Error(string message) => Log(Severity.ERROR, message);
    }
}
