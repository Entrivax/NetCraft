using NetCraft.Config;
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

            var ip = IPAddress.Parse(server.Configuration.GetValue("ip", IPAddress.Any.ToString()));
            var port = int.Parse(server.Configuration.GetValue("port", "25565"));
            server.SaveConfiguration();
            server.Start(ip, port);
        }
    }
}
