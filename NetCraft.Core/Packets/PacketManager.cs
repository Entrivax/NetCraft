using NetCraft.Core.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetCraft.Core.Packets
{
    public class PacketManager
    {
        private Dictionary<int, Type> _handledPackets;
        private Dictionary<Type, int> _packetIds;
        private Dictionary<int, Action<Client, IPacket>> _packetCallbacks;

        public PacketManager()
        {
            _handledPackets = new Dictionary<int, Type>();
            _packetIds = new Dictionary<Type, int>();
            _packetCallbacks = new Dictionary<int, Action<Client, IPacket>>();
        }

        public bool IsPacketIdRegistered(int id)
        {
            return _packetIds.ContainsValue(id);
        }

        public bool IsPacketHandled(int id)
        {
            return _handledPackets.ContainsKey(id);
        }

        public int GetHandledPacketsCount()
        {
            return _handledPackets.Count;
        }

        public int GetRegisteredPacketsCount()
        {
            return _packetIds.Count;
        }

        public void RegisterPacketHandler<T>(int id, Action<Client, T> callback) where T : IPacket
        {
            _handledPackets.Add(id, typeof(T));
            var action = new Action<Client, IPacket>((client, p) => callback(client, (T)p));
            _packetCallbacks.Add(id, action);
        }

        public void RegisterPacketId<T>(int id) where T : IPacket
        {
            _packetIds.Add(typeof(T), id);
        }

        public void UnregisterPacketHandler(int id)
        {
            _handledPackets.Remove(id);
            _packetCallbacks.Remove(id);
        }

        public void UnregisterPacketId(int id)
        {
            _packetIds.Remove(_packetIds.FirstOrDefault(k => k.Value == id).Key);
        }

        public void UnregisterAllPacketIds()
        {
            _packetIds.Clear();
        }

        public void UnregisterAllPacketHandlers()
        {
            _handledPackets.Clear();
            _packetCallbacks.Clear();
        }

        public void SendPacket<T>(JavaDataStream stream, T packet) where T : IPacket
        {
            stream.WriteByte((byte)_packetIds[packet.GetType()]);
            packet.WritePacketData(stream);
        }

        public void HandlePackets(Client client)
        {
            var packetId = client.JavaDataStream.ReadUInt8();
            if (_handledPackets.ContainsKey(packetId))
            {
                var packet = ((IPacket)Activator.CreateInstance(_handledPackets[packetId]));
                packet.ReadPacketData(client.JavaDataStream);
                _packetCallbacks[packetId](client, packet);
            }
            else
            {
                throw new IOException($"Unhandled packet {packetId}");
            }
        }
    }
}
