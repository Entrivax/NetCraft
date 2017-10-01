using System;

namespace NetCraft.Logging
{
    public class ConsoleLogHandler : ILogHandler
    {
        public Severity MinSeverity { get; set; } = Severity.INFO;

        public void Log(Severity severity, string message)
        {
            if (severity >= MinSeverity)
                Console.WriteLine(message);
        }
    }
}
