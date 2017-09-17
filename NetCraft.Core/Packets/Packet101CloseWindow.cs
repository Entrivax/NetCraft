using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
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
