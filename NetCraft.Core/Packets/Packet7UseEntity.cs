using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet7UseEntity : IPacket
    {
        public int Size => 9;

        public int PlayerEntityId { get; set; }
        public int TargetEntityId { get; set; }
        public bool IsLeftClick { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            PlayerEntityId = stream.ReadInt32();
            TargetEntityId = stream.ReadInt32();
            IsLeftClick = stream.ReadBoolean();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(PlayerEntityId);
            stream.WriteInt32(TargetEntityId);
            stream.WriteBoolean(IsLeftClick);
        }
    }
}
