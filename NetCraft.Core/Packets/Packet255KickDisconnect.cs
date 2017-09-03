using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCraft.Core.Network;
using System.IO;

namespace NetCraft.Core.Packets
{
    class Packet255KickDisconnect : IPacket
    {
        public String reason;

        public Packet255KickDisconnect()
        {
        }

        public Packet255KickDisconnect(String s)
        {
            reason = s;
        }

        public int Size => reason.Length;

        public void ReadPacketData(JavaDataStream stream)
        {
            if (stream.ReadString().Length > 100)
                throw new IOException();
            reason = stream.ReadString();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteString(reason);
        }
    }
}
