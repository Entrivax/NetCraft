using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet61DoorChange : IPacket
    {
        public int Size => 20;

        public int SfxId { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }
        public int AuxData { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            SfxId = stream.ReadInt32();
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadByte();
            ZPosition = stream.ReadInt32();
            AuxData = stream.ReadInt32();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(SfxId);
            stream.WriteInt32(XPosition);
            stream.WriteByte((byte)YPosition);
            stream.WriteInt32(ZPosition);
            stream.WriteInt32(AuxData);
        }
    }
}
