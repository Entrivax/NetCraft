using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet38EntityStatus : IPacket
    {
        public int Size => 5;

        public int EntityId { get; set; }
        public byte EntityStatus { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            EntityStatus = (byte)stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteByte(EntityStatus);
        }
    }
}
