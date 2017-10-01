using NetCraft.Core.Packets;
using NetCraft.Logging;
using NetCraft.Network;
using NetCraft.Plugin;

namespace NetCraft
{
    class Program
    {
        static void Main(string[] args)
        {
            var logHandler = new ConsoleLogHandler();
            var loggerManager = new LoggerManager();
            loggerManager.RegisterLogHandler(logHandler);
            var server = new Server(new PacketManager(), new PluginManager(), loggerManager);
            server.LoadPlugins();
            server.Start(System.Net.IPAddress.Any, 25565);
        }
    }
}
