using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet4UpdateTime : IPacket
    {
        public int Size => 8;

        public long Time { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            Time = stream.ReadInt64();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt64(Time);
        }
    }
}
