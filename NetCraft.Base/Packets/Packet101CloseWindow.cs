using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet101CloseWindow : IPacket
    {
        public int WindowId { get; set; }

        public int Size => 1;

        public void ReadPacketData(JavaDataStream stream)
        {
            WindowId = stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteByte((byte)WindowId);
        }
    }
}
