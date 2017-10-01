namespace NetCraft.Logging
{
    public interface ILogHandler
    {
        void Log(Severity severity, string message);
    }
}
