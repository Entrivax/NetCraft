using NetCraft.Core.Packets;
using NetCraft.Network;
using NetCraft.Plugin;

namespace NetCraft
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(new PacketManager(), new PluginManager());
            server.LoadPlugins();
            server.Start(System.Net.IPAddress.Any, 25565);
        }
    }
}
