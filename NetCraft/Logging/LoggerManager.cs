using NetCraft.Plugin;
using System.Collections.Generic;

namespace NetCraft.Logging
{
    public class LoggerManager
    {
        private List<ILogHandler> _loggers;

        public LoggerManager()
        {
            _loggers = new List<ILogHandler>();
        }

        internal void Log(Severity severity, string message)
        {
            _loggers.ForEach(logger => logger.Log(severity, message));
        }

        public void RegisterLogHandler(ILogHandler logHandler)
        {
            _loggers.Add(logHandler);
        }

        public void UnregisterLogHandler(ILogHandler logHandler)
        {
            _loggers.Remove(logHandler);
        }

        public void ClearLogHandlers()
        {
            _loggers.Clear();
        }

        public Logger GetLogger(IPlugin plugin) => new Logger(this, plugin);
    }
}
