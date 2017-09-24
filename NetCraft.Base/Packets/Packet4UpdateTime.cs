using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet4UpdateTime : IPacket
    {
        public int Size => 8;

        public long Time { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            Time = stream.ReadInt64();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt64(Time);
        }
    }
}
