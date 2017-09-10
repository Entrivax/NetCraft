using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet71Weather : IPacket
    {
        public int Size => 17;

        public int EntityId { get; set; }
        public bool IsLightningBolt { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            IsLightningBolt = stream.ReadBoolean();
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadInt32();
            ZPosition = stream.ReadInt32();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteBoolean(IsLightningBolt);
            stream.WriteInt32(XPosition);
            stream.WriteInt32(YPosition);
            stream.WriteInt32(ZPosition);
        }
    }
}
