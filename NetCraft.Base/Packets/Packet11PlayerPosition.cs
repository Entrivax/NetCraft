using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet11PlayerPosition : IPacket
    {
        public int Size => 33;

        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double ZPosition { get; set; }
        public double Stance { get; set; }
        public bool OnGround { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            XPosition = stream.ReadDouble();
            YPosition = stream.ReadDouble();
            Stance = stream.ReadDouble();
            ZPosition = stream.ReadDouble();
            OnGround = stream.ReadByte() != 0;
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteDouble(XPosition);
            stream.WriteDouble(YPosition);
            stream.WriteDouble(Stance);
            stream.WriteDouble(ZPosition);
            stream.WriteByte(OnGround ? (byte)1 : (byte)0);
        }
    }
}
