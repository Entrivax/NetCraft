using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet5PlayerInventory : IPacket
    {
        public int Size => 8;

        public int EntityId { get; set; }
        public int Slot { get; set; }
        public int ItemId { get; set; }
        public int ItemDamage { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            Slot = stream.ReadInt16();
            ItemId = stream.ReadInt16();
            ItemDamage = stream.ReadInt16();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteInt16((short)Slot);
            stream.WriteInt16((short)ItemId);
            stream.WriteInt16((short)ItemDamage);
        }
    }
}
