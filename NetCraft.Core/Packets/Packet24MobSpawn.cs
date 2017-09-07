using System;
using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet24MobSpawn : IPacket // TODO implement packet after implementing DataWatcher
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
