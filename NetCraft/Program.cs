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

            pm.RegisterPacketId<Packet1Login>(1);
            while (true)
                pm.HandlePackets(js);
        }

        public static void LogPacket(IPacket packet)
        {
            Console.WriteLine(JsonConvert.SerializeObject(packet));
        }

        public static void handleHandshake(PacketManager packetManager, JavaDataStream stream, Packet2Handshake packet)
        {
            Console.WriteLine(JsonConvert.SerializeObject(packet));
            packetManager.SendPacket(stream, new Packet1Login { Username = "-", Dimension = 0, MapSeed = 43567, ProtocolVersion = 1 });
        }
    }
}
