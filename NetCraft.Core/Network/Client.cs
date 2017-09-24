using NetCraft.Core.Packets;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace NetCraft.Core.Network
{
    public class Client : IDisposable
    {
        public TcpClient TcpClient { get; private set; }
        public JavaDataStream JavaDataStream { get; private set; }
        public List<IPacket> PendingPackets { get; private set; }

        public Client(TcpClient tcpClient)
        {
            TcpClient = tcpClient;
            JavaDataStream = new JavaDataStream(TcpClient.GetStream());
            PendingPackets = new List<IPacket>();
        }

        public void Dispose()
        {
            JavaDataStream = null;
            PendingPackets?.Clear();
            PendingPackets = null;
            TcpClient?.Close();
            TcpClient = null;
        }
    }
}
