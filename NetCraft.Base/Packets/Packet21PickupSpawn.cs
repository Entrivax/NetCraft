using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet21PickupSpawn : IPacket
    {
        public int Size => 24;

        public int EntityId { get; set; }
        public int ItemId { get; set; }
        public int Count { get; set; }
        public int ItemDamage { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }
        public byte Rotation { get; set; }
        public byte Pitch { get; set; }
        public byte Roll { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            ItemId = stream.ReadInt16();
            Count = stream.ReadByte();
            ItemDamage = stream.ReadInt16();
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadInt32();
            ZPosition = stream.ReadInt32();
            Rotation = (byte)stream.ReadByte();
            Pitch = (byte)stream.ReadByte();
            Roll = (byte)stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteInt16((short)ItemId);
            stream.WriteByte((byte)Count);
            stream.WriteInt16((short)ItemDamage);
            stream.WriteInt32(XPosition);
            stream.WriteInt32(YPosition);
            stream.WriteInt32(ZPosition);
            stream.WriteByte(Rotation);
            stream.WriteByte(Pitch);
            stream.WriteByte(Roll);
        }
    }
}
