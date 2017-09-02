using System;
using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet101CloseWindow : IPacket
    {

        public int windowId;

        Packet101CloseWindow()
        {

        }

        public Packet101CloseWindow(int i)
        {
            windowId = i;
        }

        public int Size => 1;

        public void ReadPacketData(JavaDataStream stream)
        {
            windowId = stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteByte((byte)windowId);
        }
    }
}
