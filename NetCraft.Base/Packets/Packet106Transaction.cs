using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet106Transaction : IPacket
    {
        public int windowId;
        public short field_20028_b;
        public bool field_20030_c;

        public Packet106Transaction()
        {
        }

        public Packet106Transaction(int i, short word0, bool flag)
        {
            windowId = i;
            field_20028_b = word0;
            field_20030_c = flag;
        }

        public int Size => 4;

        public void ReadPacketData(JavaDataStream stream)
        {
            windowId = stream.ReadByte();
            field_20028_b = stream.ReadInt16();
            field_20030_c = stream.ReadByte() != 0;
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteByte((byte)windowId);
            stream.WriteInt16(field_20028_b);
            stream.WriteByte((byte)(field_20030_c ? 1 : 0));
        }
    }
}
