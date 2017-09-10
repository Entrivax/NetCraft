using NetCraft.Core.Network;
using System;
using System.Collections.Generic;

namespace NetCraft.Core.Packets
{
    public class PacketManager
    {
        private Dictionary<int, Type> _handledPackets;
        private Dictionary<Type, int> _packetIds;
        private Dictionary<int, Action<JavaDataStream, IPacket>> _packetCallbacks;

        public PacketManager()
        {
            _handledPackets = new Dictionary<int, Type>();
            _packetIds = new Dictionary<Type, int>();
            _packetCallbacks = new Dictionary<int, Action<JavaDataStream, IPacket>>();
        }

        public void RegisterPacketHandler<T>(int id, Action<PacketManager, JavaDataStream, T> callback) where T : IPacket
        {
            _handledPackets.Add(id, typeof(T));
            var action = new Action<JavaDataStream, IPacket>((stream, p) => callback(this, stream, (T)p));
            _packetCallbacks.Add(id, action);
        }

        public void RegisterPacketId<T>(int id) where T : IPacket
        {
            _packetIds.Add(typeof(T), id);
        }

        public void SendPacket<T>(JavaDataStream stream, T packet) where T : IPacket
        {
            stream.WriteByte((byte)_packetIds[typeof(T)]);
            packet.WritePacketData(stream);
        }

        public void HandlePackets(JavaDataStream stream)
        {
            var packetId = stream.ReadUInt8();
            if (_handledPackets.ContainsKey(packetId))
            {
                var packet = ((IPacket)Activator.CreateInstance(_handledPackets[packetId]));
                packet.ReadPacketData(stream);
                _packetCallbacks[packetId](stream, packet);
            }
            else
            {
                Console.WriteLine($"Unhandled packet {packetId}");
            }
        }
    }
}
