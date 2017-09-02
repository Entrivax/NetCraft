using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet17Sleep : IPacket
    {
        public int Size => 14;

        public int EntityId { get; set; }
        public int BedX { get; set; }
        public int BedY { get; set; }
        public int BedZ { get; set; }

        /// <summary>
        /// Should be 0
        /// </summary>
        public int Unknown { get; set; } = 0;

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            Unknown = stream.ReadByte();
            BedX = stream.ReadInt32();
            BedY = stream.ReadByte();
            BedZ = stream.ReadInt32();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteByte((byte)Unknown);
            stream.WriteInt32(BedX);
            stream.WriteByte((byte)BedY);
            stream.WriteInt32(BedZ);
        }
    }
}
