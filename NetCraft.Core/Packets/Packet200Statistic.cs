using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet200Statistic : IPacket
    {
        public int Size => 6;

        public int StatisticId { get; set; }
        public int Amount { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            StatisticId = stream.ReadInt32();
            Amount = stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(StatisticId);
            stream.WriteByte((byte)Amount);
        }
    }
}
