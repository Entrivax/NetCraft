using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    public class Packet70Bed : IPacket
    {
        public int Size => 1;

        /// <summary>
        /// Either 1 or 2. 1 indicates to begin raining, 2 indicates to stop raining
        /// </summary>
        public byte BedState { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            BedState = (byte)stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteByte(BedState);
        }
    }
}
