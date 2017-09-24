using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet52MultiBlockChange : IPacket
    {
        public int Size => 10 + Coordinates.Length * 4;

        public int XPosition { get; set; }
        public int ZPosition { get; set; }
        public short[] Coordinates { get; set; }
        public byte[] Types { get; set; }
        public byte[] Metadatas { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            XPosition = stream.ReadInt32();
            ZPosition = stream.ReadInt32();
            var size = stream.ReadInt16();
            Coordinates = stream.ReadInt16Array(size);
            Types = stream.ReadUInt8Array(size);
            Metadatas = stream.ReadUInt8Array(size);
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(XPosition);
            stream.WriteInt32(ZPosition);
            stream.WriteInt16((short)Coordinates.Length);
            stream.WriteInt16Array(Coordinates);
            stream.WriteUInt8Array(Types);
            stream.WriteUInt8Array(Metadatas);
        }
    }
}
