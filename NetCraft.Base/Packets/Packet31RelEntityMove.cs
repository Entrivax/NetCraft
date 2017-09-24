using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet31RelEntityMove : IPacket
    {
        public int Size => 7;

        public int EntityId { get; set; }
        public byte XPosition { get; set; }
        public byte YPosition { get; set; }
        public byte ZPosition { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            XPosition = (byte)stream.ReadByte();
            YPosition = (byte)stream.ReadByte();
            ZPosition = (byte)stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteByte(XPosition);
            stream.WriteByte(YPosition);
            stream.WriteByte(ZPosition);
        }
    }
}
