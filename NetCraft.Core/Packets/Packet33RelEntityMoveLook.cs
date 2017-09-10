using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet33RelEntityMoveLook : IPacket
    {
        public int Size => 9;

        public int EntityId { get; set; }
        public byte XPosition { get; set; }
        public byte YPosition { get; set; }
        public byte ZPosition { get; set; }
        public byte Yaw { get; set; }
        public byte Pitch { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            XPosition = (byte)stream.ReadByte();
            YPosition = (byte)stream.ReadByte();
            ZPosition = (byte)stream.ReadByte();
            Yaw = (byte)stream.ReadByte();
            Pitch = (byte)stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteByte(XPosition);
            stream.WriteByte(YPosition);
            stream.WriteByte(ZPosition);
            stream.WriteByte(Yaw);
            stream.WriteByte(Pitch);
        }
    }
}
