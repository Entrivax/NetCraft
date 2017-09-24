using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet39AttachEntity : IPacket
    {
        public int Size => 8;

        public int EntityId { get; set; }
        public int VehicleEntityId { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            VehicleEntityId = stream.ReadInt32();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteInt32(VehicleEntityId);
        }
    }
}
