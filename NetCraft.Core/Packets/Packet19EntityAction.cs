using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet19EntityAction : IPacket
    {
        public int Size => 5;

        public int EntityId { get; set; }
        public int State { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            State = stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteByte((byte)State);
        }
    }
}
