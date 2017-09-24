using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet9Respawn : IPacket
    {
        public int Size => 1;

        public byte Dimension { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            Dimension = (byte)stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteByte(Dimension);
        }
    }
}
