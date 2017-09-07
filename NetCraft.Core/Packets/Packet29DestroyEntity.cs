using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet29DestroyEntity : IPacket
    {
        public int Size => 4;

        public int EntityId { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
        }
    }
}
