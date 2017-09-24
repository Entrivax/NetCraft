using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet105UpdateProgressBar : IPacket
    {
        public int windowId;
        public int progressBar;
        public int progressBarValue;

        Packet105UpdateProgressBar()
        {

        }
        public int Size => 5;

        public void ReadPacketData(JavaDataStream stream)
        {
            windowId = stream.ReadByte();
            progressBar = stream.ReadInt16();
            progressBarValue = stream.ReadInt16();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteByte((byte)windowId);
            stream.WriteInt16((short)progressBar);
            stream.WriteInt16((short)progressBarValue);
        }
    }
}
