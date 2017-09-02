using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet10Flying : IPacket
    {
        public int Size => 1;

        public bool OnGround { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            OnGround = stream.ReadByte() != 0;
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteByte(OnGround ? (byte)1 : (byte)0);
        }
    }
}
