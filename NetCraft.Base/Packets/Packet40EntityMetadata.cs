using System;
using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet40EntityMetadata : IPacket // TODO implement packet after implementing DataWatcher
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
