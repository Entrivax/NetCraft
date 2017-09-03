using System;
using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet18Animation : IPacket
    {
        public int Size => 5;

        public int EntityId { get; set; }
        public int Animate { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            Animate = stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteByte((byte)Animate);
        }
    }
}
