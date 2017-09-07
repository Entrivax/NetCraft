using NetCraft.Core.Network;
using System.IO;

namespace NetCraft.Core.Packets
{
    public class Packet2Handshake : IPacket
    {
        public int Size => 8 + Username.Length;

        public string Username { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            Username = stream.ReadString();
            if (Username.Length > 32)
                throw new IOException();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteString(Username);
        }
    }
}
