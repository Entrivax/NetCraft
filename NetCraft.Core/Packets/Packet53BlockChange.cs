using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet53BlockChange : IPacket
    {
        public int Size => 11;

        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }
        public byte Type { get; set; }
        public byte Metadata { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadByte();
            ZPosition = stream.ReadInt32();
            Type = (byte)stream.ReadByte();
            Metadata = (byte)stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(XPosition);
            stream.WriteByte((byte)YPosition);
            stream.WriteInt32(ZPosition);
            stream.WriteByte(Type);
            stream.WriteByte(Metadata);
        }
    }
}
