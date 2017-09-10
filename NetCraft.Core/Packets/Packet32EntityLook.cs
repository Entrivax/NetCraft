using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet32EntityLook : IPacket
    {
        public int Size => 6;

        public int EntityId { get; set; }
        public byte Yaw { get; set; }
        public byte Pitch { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            Yaw = (byte)stream.ReadByte();
            Pitch = (byte)stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteByte(Yaw);
            stream.WriteByte(Pitch);
        }
    }
}
