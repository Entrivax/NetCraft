using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet0KeepAlive : IPacket
    {
        public Packet0KeepAlive()
        {

        }

        public int Size => 0;

        public void ReadPacketData(JavaDataStream stream)
        {
        }

        public void WritePacketData(JavaDataStream stream)
        {
        }
    }
}
