using NetCraft.Core.Network;
using NetCraft.Core.Packets;
using System.IO;

namespace NetCraft.Base.Packets
{
    public class Packet1Login : IPacket
    {
        public int Size => Username?.Length ?? 0 + 13;

        public int ProtocolVersion { get; set; }
        public string Username { get; set; }
        public long MapSeed { get; set; }
        public byte Dimension { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            ProtocolVersion = stream.ReadInt32();
            Username = stream.ReadString();
            if (Username.Length > 16)
                throw new IOException();
            MapSeed = stream.ReadInt64();
            Dimension = (byte)stream.ReadByte();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(ProtocolVersion);
            stream.WriteString(Username);
            stream.WriteInt64(MapSeed);
            stream.WriteByte(Dimension);
        }
    }
}
