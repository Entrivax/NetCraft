using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCraft.Core.Network;
using System.IO;

namespace NetCraft.Core.Packets
{
    public class Packet3Chat : IPacket
    {
        public int Size => throw new NotImplementedException();

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value.Length > 119 ? value.Substring(0, 119) : value; }
        }

        public void ReadPacketData(JavaDataStream stream)
        {
            var message = stream.ReadString();
            if (message.Length > 119)
                throw new IOException();
            Message = message;
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteString(Message);
        }
    }
}
