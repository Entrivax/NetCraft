using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCraft.Core.Network;
using System.IO;

namespace NetCraft.Core.Packets
{
    class Packet130UpdateSign : IPacket
    {
        public int xPosition;
        public int yPosition;
        public int zPosition;
        public string[] signLines;
        public bool isChunkDataPacket;

        public Packet130UpdateSign()
        {
            isChunkDataPacket = true;
        }

        public Packet130UpdateSign(int i, int j, int k, String[] lines)
        {
            isChunkDataPacket = true;
            xPosition = i;
            yPosition = j;
            zPosition = k;
            signLines = lines;
        }

        public int Size => throw new NotImplementedException();

        public void ReadPacketData(JavaDataStream stream)
        {
            xPosition = stream.ReadInt32();
            yPosition = stream.ReadInt16();
            zPosition = stream.ReadInt32();
            signLines = new String[4];
            for (int i = 0; i < 4; i++)
            {
                if (stream.ReadString().Length > 15)
                    throw new IOException();
                signLines[i] = stream.ReadString();
            }
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(xPosition);
            stream.WriteInt16((short)yPosition);
            stream.WriteInt32(zPosition);
            for (int i = 0; i < 4; i++)
            {
                stream.WriteString(signLines[i]);
            }
        }
    }
}
