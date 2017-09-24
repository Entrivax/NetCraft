using NetCraft.Core.Network;
using System.IO;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet20NamedEntitySpawn : IPacket
    {
        public int Size => 28;

        public int EntityId { get; set; }
        public string Name { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }
        public byte Rotation { get; set; }
        public byte Pitch { get; set; }
        public int CurrentItem { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            Name = stream.ReadString();
            if (Name.Length > 16)
                throw new IOException();
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadInt32();
            ZPosition = stream.ReadInt32();
            Rotation = (byte)stream.ReadByte();
            Pitch = (byte)stream.ReadByte();
            CurrentItem = stream.ReadInt16();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteString(Name);
            stream.WriteInt32(XPosition);
            stream.WriteInt32(YPosition);
            stream.WriteInt32(ZPosition);
            stream.WriteByte(Rotation);
            stream.WriteByte(Pitch);
            stream.WriteInt16((short)CurrentItem);
        }
    }
}
