using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet34EntityTeleport : IPacket
    {
        public int Size => 34;

        public int EntityId { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }
        public byte Yaw { get; set; }
        public byte Pitch { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadInt32();
            ZPosition = stream.ReadInt32();
            Yaw = (byte)stream.ReadByte();
            Pitch = (byte)stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteInt32(XPosition);
            stream.WriteInt32(YPosition);
            stream.WriteInt32(ZPosition);
            stream.WriteByte(Yaw);
            stream.WriteByte(Pitch);
        }
    }
}
