using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet16BlockItemSwitch : IPacket
    {
        public int Size => 2;

        public int Id { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            Id = stream.ReadInt16();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt16((short)Id);
        }
    }
}
