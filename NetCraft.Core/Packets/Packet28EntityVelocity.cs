using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet28EntityVelocity : IPacket
    {
        public int Size => 10;

        public int EntityId { get; set; }
        public int MotionX { get; set; }
        public int MotionY { get; set; }
        public int MotionZ { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            MotionX = stream.ReadInt16();
            MotionY = stream.ReadInt16();
            MotionZ = stream.ReadInt16();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteInt16((short)MotionX);
            stream.WriteInt16((short)MotionY);
            stream.WriteInt16((short)MotionZ);
        }
    }
}
