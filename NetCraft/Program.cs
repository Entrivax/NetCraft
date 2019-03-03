using NetCraft.Core.Packets;
using NetCraft.Logging;
using NetCraft.Network;
using NetCraft.Plugin;
using System.Net;

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

            var ip = IPAddress.Parse(server.Configuration.Ip ?? IPAddress.Any.ToString());
            var port = server.Configuration.Port ?? 25565;
            server.SaveConfiguration();
            server.Start(ip, port);
        }
    }
}
