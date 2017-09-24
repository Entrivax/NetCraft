using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet22Collect : IPacket
    {
        public int Size => 8;

        public int CollectedEntityId { get; set; }
        public int CollectorEntityId { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            CollectedEntityId = stream.ReadInt32();
            CollectorEntityId = stream.ReadInt32();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(CollectedEntityId);
            stream.WriteInt32(CollectorEntityId);
        }
    }
}
