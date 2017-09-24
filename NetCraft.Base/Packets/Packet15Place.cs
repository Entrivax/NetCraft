using System;
using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
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
