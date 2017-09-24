using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet6SpawnPosition : IPacket
    {
        public int Size => 12;

        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadInt32();
            ZPosition = stream.ReadInt32();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(XPosition);
            stream.WriteInt32(YPosition);
            stream.WriteInt32(ZPosition);
        }
    }
}
