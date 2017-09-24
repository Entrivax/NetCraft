using System;
using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet103SetSlot : IPacket
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
