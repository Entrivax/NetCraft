using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet0KeepAlive : IPacket
    {
        public Packet0KeepAlive()
        {

        }

        public int Size => 0;

        public void ReadPacketData(JavaDataStream stream)
        {
        }

        public void WritePacketData(JavaDataStream stream)
        {
        }
    }
}
