using NetCraft.Core.Network;

namespace NetCraft.Core.Packets
{
    interface IPacket
    {
        /// <summary>
        /// This method is used to read data of the packet
        /// </summary>
        /// <param name="stream"></param>
        void ReadPacketData(JavaDataStream stream);

        /// <summary>
        /// This method is used to write data of the packet
        /// </summary>
        /// <param name="stream"></param>
        void WritePacketData(JavaDataStream stream);

        int Size { get; }
    }
}
