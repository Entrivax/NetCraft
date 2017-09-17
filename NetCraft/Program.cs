using NetCraft.Core.Network;
using NetCraft.Core.Packets;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;

namespace NetCraft
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(25565);
            listener.Start();
            var c = listener.AcceptTcpClient();
            var s = c.GetStream();
            var js = new JavaDataStream(s);
            PacketManager pm = new PacketManager();
            pm.RegisterPacketHandler<Packet2Handshake>(2, handleHandshake);

            pm.RegisterPacketHandler<Packet0KeepAlive>(0, (p,j,pa) => LogPacket(pa));
            pm.RegisterPacketHandler<Packet11PlayerPosition>(11, (p, j, pa) => LogPacket(pa));
            pm.RegisterPacketHandler<Packet12PlayerLook>(12, (p, j, pa) => LogPacket(pa));
            pm.RegisterPacketHandler<Packet13PlayerLookMove>(13, (p, j, pa) => LogPacket(pa));
            pm.RegisterPacketHandler<Packet14BlockDig>(14, (p, j, pa) => LogPacket(pa));
            pm.RegisterPacketHandler<Packet5PlayerInventory>(5, (p, j, pa) => LogPacket(pa));
            pm.RegisterPacketHandler<Packet101CloseWindow>(101, handleCloseWindow);

            pm.RegisterPacketId<Packet1Login>(1);
            pm.RegisterPacketId<Packet2Handshake>(2);
            pm.RegisterPacketId<Packet4UpdateTime>(4);
            pm.RegisterPacketId<Packet6SpawnPosition>(6);
            pm.RegisterPacketId<Packet50PreChunk>(50);
            pm.RegisterPacketId<Packet101CloseWindow>(101);
            while (true)
                pm.HandlePackets(js);
        }

        public static void LogPacket(IPacket packet)
        {
            Console.WriteLine(JsonConvert.SerializeObject(packet));
        }

        public static void handleCloseWindow(PacketManager packetManager, JavaDataStream stream, Packet101CloseWindow packet)
        {
            Console.WriteLine(JsonConvert.SerializeObject(packet));
            packetManager.SendPacket(stream, new Packet101CloseWindow { WindowId = packet.WindowId });
        }

        public static void handleHandshake(PacketManager packetManager, JavaDataStream stream, Packet2Handshake packet)
        {
            Console.WriteLine(JsonConvert.SerializeObject(packet));
            packetManager.SendPacket(stream, new Packet2Handshake { Username = "-" });
            packetManager.SendPacket(stream, new Packet1Login { Username = "-", Dimension = 0, MapSeed = 43567, ProtocolVersion = 1 });
            packetManager.SendPacket(stream, new Packet6SpawnPosition { XPosition = 0, YPosition = 20, ZPosition = 0 });
            packetManager.SendPacket(stream, new Packet4UpdateTime { Time = 12000 });
            packetManager.SendPacket(stream, new Packet50PreChunk { XPosition = 0, YPosition = 0, Mode = true });
        }
    }
}
