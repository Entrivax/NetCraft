using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet23VehicleSpawn : IPacket
    {
        public int Size => 21 + ThrowerEntityId > 0 ? 6 : 0;

        public int EntityId { get; set; }
        public int Type { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }
        public int ThrowerEntityId { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public int SpeedZ { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            EntityId = stream.ReadInt32();
            Type = stream.ReadByte();
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadInt32();
            ZPosition = stream.ReadInt32();
            ThrowerEntityId = stream.ReadInt32();

            if (ThrowerEntityId > 0)
            {
                SpeedX = stream.ReadInt16();
                SpeedY = stream.ReadInt16();
                SpeedZ = stream.ReadInt16();
            }
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(EntityId);
            stream.WriteByte((byte)Type);
            stream.WriteInt32(XPosition);
            stream.WriteInt32(YPosition);
            stream.WriteInt32(ZPosition);
            stream.WriteInt32(ThrowerEntityId);

            if (ThrowerEntityId > 0)
            {
                stream.WriteInt16((short)SpeedX);
                stream.WriteInt16((short)SpeedY);
                stream.WriteInt16((short)SpeedZ);
            }
        }
    }
}
