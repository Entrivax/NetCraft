using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet12PlayerLook : IPacket
    {
        public int Size => 9;

        public float Yaw { get; set; }
        public float Pitch { get; set; }
        public bool OnGround { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            Yaw = stream.ReadSingle();
            Pitch = stream.ReadSingle();
            OnGround = stream.ReadByte() != 0;
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteSingle(Yaw);
            stream.WriteSingle(Pitch);
            stream.WriteByte(OnGround ? (byte)1 : (byte)0);
        }
    }
}
