using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet8UpdateHealth : IPacket
    {
        public int Size => 2;

        public short Health;

        public void ReadPacketData(JavaDataStream stream)
        {
            Health = stream.ReadInt16();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt16(Health);
        }
    }
}
