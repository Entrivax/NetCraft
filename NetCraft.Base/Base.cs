using NetCraft.Plugin;
using System;
using NetCraft.Network;
using NetCraft.Base.Packets;
using System.Collections.Generic;
using NetCraft.Core.Network;
using NetCraft.Base.Entities;
using NetCraft.Base.Handlers;

namespace NetCraft.Base
{
    public class Base : IPlugin
    {
        private Dictionary<Client, Player> _players;

        public void Load(Server server)
        {
            Console.WriteLine("Loading NetCraft server base");

            _players = new Dictionary<Client, Player>();

            server.PacketManager.RegisterPacketHandler<Packet2Handshake>(2, (client, packet) => {
                LoginHandler.ConnectClient(_players, client, packet);
            });
            server.PacketManager.RegisterPacketId<Packet2Handshake>(2);

            server.OnDisconnect += OnClientDisconnect;
        }

        private void OnClientDisconnect(object sender, Client client)
        {
            if (_players.ContainsKey(client))
            {
                Console.WriteLine($"Player {_players[client].Username} disconnected");
                _players.Remove(client);
            }
        }

        public void Unload(Server server)
        {
            Console.WriteLine("Unloading NetCraft server base");
        }
    }
}
