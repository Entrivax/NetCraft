using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet50PreChunk : IPacket
    {
        public int Size => 9;

        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public bool Mode { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadInt32();
            Mode = stream.ReadBoolean();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(XPosition);
            stream.WriteInt32(YPosition);
            stream.WriteBoolean(Mode);
        }
    }
}
