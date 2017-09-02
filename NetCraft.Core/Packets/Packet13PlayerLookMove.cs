using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet13PlayerLookMove : IPacket
    {
        public int Size => 41;

        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double ZPosition { get; set; }
        public double Stance { get; set; }
        public float Yaw { get; set; }
        public float Pitch { get; set; }
        public bool OnGround { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            XPosition = stream.ReadDouble();
            YPosition = stream.ReadDouble();
            Stance = stream.ReadDouble();
            ZPosition = stream.ReadDouble();
            Yaw = stream.ReadSingle();
            Pitch = stream.ReadSingle();
            OnGround = stream.ReadByte() != 0;
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteDouble(XPosition);
            stream.WriteDouble(YPosition);
            stream.WriteDouble(Stance);
            stream.WriteDouble(ZPosition);
            stream.WriteSingle(Yaw);
            stream.WriteSingle(Pitch);
            stream.WriteByte(OnGround ? (byte)1 : (byte)0);
        }
    }
}
