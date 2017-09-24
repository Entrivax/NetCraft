using NetCraft.Core.Network;
using System.IO;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet25EntityPainting : IPacket
    {
        public int Size => 24;

        public int EntityId { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }
        public int Direction { get; set; }
        public string Title { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            Title = stream.ReadString();
            if (Title.Length > 13)
                throw new IOException();
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadInt32();
            ZPosition = stream.ReadInt32();
            Direction = stream.ReadInt32();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteString(Title);
            stream.WriteInt32(XPosition);
            stream.WriteInt32(YPosition);
            stream.WriteInt32(ZPosition);
            stream.WriteInt32(Direction);
        }
    }
}
