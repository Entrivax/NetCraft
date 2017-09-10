using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet54PlayNoteBlock : IPacket
    {
        public int Size => 12;

        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }
        public byte InstrumentType { get; set; }
        public byte Pitch { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadInt16();
            ZPosition = stream.ReadInt32();
            InstrumentType = (byte)stream.ReadByte();
            Pitch = (byte)stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(XPosition);
            stream.WriteInt16((short)YPosition);
            stream.WriteInt32(ZPosition);
            stream.WriteByte(InstrumentType);
            stream.WriteByte(Pitch);
        }
    }
}
