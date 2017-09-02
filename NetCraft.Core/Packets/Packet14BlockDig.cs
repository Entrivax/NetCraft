using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet14BlockDig : IPacket
    {
        public int Size => 11;

        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }
        public int Face { get; set; }
        public int Status { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            Status = stream.ReadByte();
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadByte();
            ZPosition = stream.ReadInt32();
            Face = stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteByte((byte)Status);
            stream.WriteInt32(XPosition);
            stream.WriteByte((byte)YPosition);
            stream.WriteInt32(ZPosition);
            stream.WriteByte((byte)Face);
        }
    }
}
