using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet100OpenWindow : IPacket
    {
        public int windowId;
        public int inventoryType;
        public String windowTitle;
        public int slotsCount;

        Packet100OpenWindow()
        {

        }
        public int Size => 3 + windowTitle.Length;

        public void ReadPacketData(JavaDataStream stream)
        {
            windowId = stream.ReadByte();
            inventoryType = stream.ReadByte();
            windowTitle = stream.ReadString8();
            slotsCount = stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteByte((byte)windowId);
            stream.WriteByte((byte)inventoryType);
            stream.WriteString8(windowTitle);
            stream.WriteByte((byte)slotsCount);
        }
    }
}
