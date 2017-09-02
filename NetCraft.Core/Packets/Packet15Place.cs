using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet15Place : IPacket // TODO implement packet after implementing ItemStack
    {
        public int Size => throw new NotImplementedException();

        public void ReadPacketData(JavaDataStream stream)
        {
            throw new NotImplementedException();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            throw new NotImplementedException();
        }
    }
}
